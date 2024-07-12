/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using io.github.embeddedrpc.erpc.auxiliary;

using System.IO;
using System.Numerics;

namespace io.github.embeddedrpc.erpc.codec;
/**
 * Basic implementation of the Codec.
 */
public sealed class BasicCodec : Codec
{
    private const int BASIC_CODEC_VERSION = 1;
    private const int DEFAULT_BUFFER_SIZE = 256;

    private MemoryStream buffer;

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
     * @param byteArray byte array
     */
    public BasicCodec(byte[] byteArray)
    {
        this.buffer = ByteBuffer.wrap(byteArray).order(ByteOrder.LITTLE_ENDIAN);
    }

    private void prepareForWrite(int bytesToWrite)
    {
        if (this.buffer.remaining() < bytesToWrite)
        {
            ByteBuffer newBuffer = ByteBuffer.allocate(
                    (this.buffer.capacity() + bytesToWrite) * 2
            );
            newBuffer.order(ByteOrder.LITTLE_ENDIAN);
            this.buffer.flip();

            newBuffer.put(this.buffer);
            this.buffer = newBuffer;
        }
    }

    public override void reset()
    {
        this.buffer = ByteBuffer.allocate(DEFAULT_BUFFER_SIZE)
                .order(ByteOrder.LITTLE_ENDIAN);
    }

    public override byte[] array()
    {
        int position = buffer.position();
        byte[] data = new byte[position];
        buffer.position(0);
        buffer.get(data);
        buffer.position(position);
        return data;
    }

    public override void setArray(byte[] array)
    {
        this.buffer = ByteBuffer.wrap(array).order(ByteOrder.LITTLE_ENDIAN);
    }

    public override void startWriteMessage(MessageInfo msgInfo)
    {
        long header = (BASIC_CODEC_VERSION << 24)
                | ((msgInfo.service() & 0xff) << 16)
                | ((msgInfo.request() & 0xff) << 8)
                | (msgInfo.type().getValue() & 0xff);

        this.writeUInt32(header);
        this.writeUInt32(msgInfo.sequence());
    }

    public override void writeBool(bool value)
    {
        prepareForWrite(1);
        this.buffer.put(value ? (byte)1 : (byte)0);
    }

    public override void writeInt8(byte value)
    {
        prepareForWrite(1);
        this.buffer.put(value);
    }

    public override void writeInt16(short value)
    {
        prepareForWrite(2);
        this.buffer.putShort(value);
    }

    public override void writeInt32(int value)
    {
        prepareForWrite(4);
        this.buffer.putInt(value);
    }

    public override void writeInt64(long value)
    {
        prepareForWrite(8);
        this.buffer.putLong(value);
    }

    public override void writeUInt8(short value)
    {
        Utils.checkUInt8(value);
        prepareForWrite(1);
        this.buffer.put(Utils.uInt8toByte(value));
    }

    public override void writeUInt16(int value)
    {
        Utils.checkUInt16(value);
        prepareForWrite(2);
        this.buffer.putShort(Utils.uInt16toShort(value));
    }

    public override void writeUInt32(long value)
    {
        Utils.checkUInt32(value);
        prepareForWrite(4);
        this.buffer.putInt(Utils.uInt32toInt(value));
    }

    public override void writeUInt64(long value)
    {
        throw new UnsupportedOperationException(
                "Java implementation of the eRPC does not support 'uint64'"
        );
    }

    public override void writeFloat(float value)
    {
        prepareForWrite(4);
        this.buffer.putFloat(value);
    }

    public override void writeDouble(double value)
    {
        prepareForWrite(8);
        this.buffer.putDouble(value);
    }

    public override void writeString(String value)
    {
        this.writeBinary(value.getBytes());
    }

    public override void writeBinary(byte[] value)
    {
        this.writeInt32(value.length);
        prepareForWrite(value.length);
        this.buffer.put(value);
    }

    public override void startWriteList(int length)
    {
        this.writeUInt32(length);
    }

    public override void startWriteUnion(int discriminator)
    {
        this.writeUInt32(discriminator);
    }

    public override void writeNullFlag(int value)
    {
        this.writeUInt32(value != 0 ? 1 : 0);
    }

    public override MessageInfo startReadMessage()
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
        MessageType msgType = MessageType.getMessageType(header & 0xff);

        return new MessageInfo(msgType, service, request, sequence);
    }

    public override bool readBool()
    {
        return this.buffer.get() != 0;
    }

    public override byte readInt8()
    {
        return this.buffer.get();
    }

    public override short readInt16()
    {
        return this.buffer.getShort();
    }

    public override int readInt32()
    {
        return this.buffer.getInt();
    }

    public override long readInt64()
    {
        return this.buffer.getLong();
    }

    public override short readUInt8()
    {
        return Utils.byteToUInt8(this.buffer.get());
    }

    public override int readUInt16()
    {
        return Utils.shortToUInt16(this.buffer.getShort());
    }

    public override long readUInt32()
    {
        return Utils.intToUInt32(this.buffer.getInt());
    }

    public override long readUInt64()
    {
        throw new UnsupportedOperationException(
                "Java implementation of the eRPC does not support 'uint64'"
        );
    }

    public override float readFloat()
    {
        return this.buffer.getFloat();
    }

    public override double readDouble()
    {
        return this.buffer.getDouble();
    }

    public override String readString()
    {
        return new String(this.readBinary());
    }

    public override byte[] readBinary()
    {
        long length = readUInt32();
        byte[] data = new byte[(int)length];
        this.buffer.get(data, 0, (int)length);

        return data;
    }

    public override long startReadList()
    {
        return this.readUInt32();
    }

    public override int startReadUnion()
    {
        return this.readInt8();
    }

    public override bool readNullFlag()
    {
        return this.readUInt8() != 0;
    }

    public override ByteBuffer getBuffer()
    {
        return buffer;
    }
}
