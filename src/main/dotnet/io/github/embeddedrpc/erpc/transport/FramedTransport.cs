/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System.Diagnostics;
using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.codec;

using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace io.github.embeddedrpc.erpc.transport;

/**
 * Abstract class for framed transports implementations.
 */
public abstract class FramedTransport : Transport
{
    private const int HEADER_LEN = 6;

    private readonly Crc16 crc16 = new Crc16();
    private readonly object receiveLock = new();
    private readonly object sendLock = new();

    public byte[] receive()
    {
        lock (receiveLock)
        {
            byte[] headerData = baseReceive(HEADER_LEN);
            Codec codec = new BasicCodec(headerData);

            int crcHeader = codec.readUInt16();
            int messageLength = codec.readUInt16();
            int crcBody = codec.readUInt16();

            if (crcHeader == 0 && messageLength == 0 && crcBody == 0)
            {
                return [];
            }

            int computedCrc = crc16.computeCRC16(Utils.uInt16ToBytes(messageLength))
                    + crc16.computeCRC16(Utils.uInt16ToBytes(crcBody));
            computedCrc &= 0xFFFF; // 2 bytes

            if (computedCrc != crcHeader)
            {
                throw new RequestError("Invalid message (header) CRC");
            }

            byte[] data = baseReceive(messageLength);

            int computedBodyCrc16 = crc16.computeCRC16(data);

            if (computedBodyCrc16 != crcBody)
            {
                throw new RequestError("Invalid message (body) CRC");
            }

            return data;
        }

    }

    public void send(byte[] message)
    {
        lock (sendLock)
        {

            Codec codec = new BasicCodec();

            int messageLength = message.Length;
            int crcBody = crc16.computeCRC16(message);
            int crcHeader = crc16.computeCRC16(Utils.uInt16ToBytes(messageLength))
                    + crc16.computeCRC16(Utils.uInt16ToBytes(crcBody));
            crcHeader &= 0xFFFF; // 2 bytes

            codec.writeUInt16(crcHeader);
            codec.writeUInt16(messageLength);
            codec.writeUInt16(crcBody);

            byte[] header = codec.array();

            Debug.Assert(header.Length == HEADER_LEN, "Invalid header length");

            byte[] data = new byte[message.Length + header.Length];

            Array.Copy(header, 0, data, 0, header.Length);
            Array.Copy(message, 0, data, header.Length, message.Length);

            baseSend(data);
        }

    }

    public virtual void close()
    {

    }

    public abstract void baseSend(byte[] data);

    public abstract byte[] baseReceive(int count);

    public void HeartbeatAckSend()
    {
        send(Encoding.UTF8.GetBytes("HEARTBEAT ACK"));
    }
}
