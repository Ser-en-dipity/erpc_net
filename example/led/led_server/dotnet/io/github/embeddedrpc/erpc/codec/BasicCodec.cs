/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using io.github.embeddedrpc.erpc.auxiliary;

using System.IO;
using System.Numerics;
using System.Text;

namespace io.github.embeddedrpc.erpc.codec;
/**
 * Basic implementation of the Codec.
 */
public sealed class BasicCodec : Codec
{
    private const int BASIC_CODEC_VERSION = 1;
    private const int DEFAULT_BUFFER_SIZE = 256;

    private ByteBuffer buffer;

    /**
     * Basic codec constructor. Create empty buffer.
     */
    public BasicCodec()
    {
        this.reset();
    }

    /**
     * Basic codec constructor. Create buffer from byte array.
     *
     * all the byte store in big endian order.
     *
     * @param byteArray byte array
     */
    public BasicCodec(byte[] byteArray)
    {
        this.buffer = new ByteBuffer(byteArray);
    }

    private void prepareForWrite(int bytesToWrite)
    {
        if (this.buffer.Remaining() < bytesToWrite)
        {
            ByteBuffer newBuffer = ByteBuffer.Allocate(
                    (this.buffer.Capacity + bytesToWrite) * 2
            );

            newBuffer.Write(this.buffer);
            this.buffer = newBuffer;
        }
    }

    public void reset()
    {
        this.buffer = ByteBuffer.Allocate(DEFAULT_BUFFER_SIZE);
    }

    public byte[] array()
    {
        int position = this.buffer.Capacity;
        byte[] data = new byte[position];
        data = this.buffer.ToArray();
        return data;
    }

    public void setArray(byte[] array)
    {
        this.buffer = new ByteBuffer(array);
    }

    public void startWriteMessage(MessageInfo msgInfo)
    {
        int header = (BASIC_CODEC_VERSION << 24)
                | ((msgInfo.service & 0xff) << 16)
                | ((msgInfo.request & 0xff) << 8)
                | ((int)msgInfo.type & 0xff);

        this.writeInt32(header);
        this.writeInt32(msgInfo.sequence);
    }

    public void writeBool(bool value)
    {
        prepareForWrite(1);
        this.buffer.WriteBoolean(value);
    }

    public void writeInt8(byte value)
    {
        prepareForWrite(1);
        this.buffer.WriteByte(value);
    }

    public void writeInt16(short value)
    {
        prepareForWrite(2);
        this.buffer.WriteShort(value);
    }

    public void writeInt32(int value)
    {
        prepareForWrite(4);
        this.buffer.WriteInt(value);
    }

    public void writeInt64(long value)
    {
        prepareForWrite(8);
        this.buffer.WriteLong(value);
    }

    public void writeUInt8(byte value)
    {
        prepareForWrite(1);
        this.buffer.WriteByte(value);
    }

    public void writeUInt16(UInt16 value)
    {
        prepareForWrite(2);
        this.buffer.WriteUshort(value);
    }

    public void writeUInt32(UInt32 value)
    {
        prepareForWrite(4);
        this.buffer.WriteUint(value);
    }

    public void writeUInt64(UInt64 value)
    {
        throw new NotImplementedException(
                "dotnet implementation of the eRPC does not support 'uint64'"
        );
    }

    public void writeFloat(float value)
    {
        prepareForWrite(4);
        this.buffer.WriteFloat(value);
    }

    public void writeDouble(double value)
    {
        prepareForWrite(8);
        this.buffer.WriteDouble(value);
    }

    public void writeString(String value)
    {
        this.writeBinary(Encoding.ASCII.GetBytes(value));
    }

    public void writeBinary(byte[] value)
    {
        this.writeInt32(value.Length);
        prepareForWrite(value.Length);
        this.buffer.WriteBytes(value);
    }

    public void startWriteList(UInt32 length)
    {
        this.writeUInt32(length);
    }

    public void startWriteUnion(UInt32 discriminator)
    {
        this.writeUInt32(discriminator);
    }

    public void writeNullFlag(int value)
    {
        this.writeInt32(value != 0 ? 1 : 0);
    }

    public MessageInfo startReadMessage()
    {
        int header = (int)this.readUInt32();
        int sequence = (int)this.readUInt32();
        int version = header >> 24;

        if (version != BASIC_CODEC_VERSION)
        {
            throw new CodecError("Unsupported codec version" + version);
        }

        int service = (header >> 16) & 0xff;
        int request = (header >> 8) & 0xff;
        int type = header & 0xff;
        MessageType msgType = (MessageType)type;

        return new MessageInfo(msgType, service, request, sequence);
    }

    public bool readBool()
    {
        return this.buffer.ReadByte() != 0;
    }

    public byte readInt8()
    {
        return this.buffer.ReadByte();
    }

    public short readInt16()
    {
        return this.buffer.ReadShort();
    }

    public int readInt32()
    {
        return this.buffer.ReadInt();
    }

    public long readInt64()
    {
        return this.buffer.ReadLong();
    }

    public short readUInt8()
    {
        return Utils.byteToUInt8(this.buffer.ReadByte());
    }

    public int readUInt16()
    {
        return Utils.shortToUInt16(this.buffer.ReadShort());
    }

    public long readUInt32()
    {
        return Utils.intToUInt32(this.buffer.ReadInt());
    }

    public long readUInt64()
    {
        throw new NotImplementedException(
                "dotnet implementation of the eRPC does not support 'uint64'"
        );
    }

    public float readFloat()
    {
        return this.buffer.ReadFloat();
    }

    public double readDouble()
    {
        return this.buffer.ReadDouble();
    }

    public String readString()
    {
        return System.Text.Encoding.Default.GetString(this.readBinary());
    }

    public byte[] readBinary()
    {
        long length = readUInt32();
        byte[] data = new byte[(int)length];
        this.buffer.ReadBytes(data, 0, (int)length);

        return data;
    }

    public long startReadList()
    {
        return this.readUInt32();
    }

    public int startReadUnion()
    {
        return this.readInt8();
    }

    public bool readNullFlag()
    {
        return this.readUInt8() != 0;
    }

    public ByteBuffer getBuffer()
    {
        return buffer;
    }
}
