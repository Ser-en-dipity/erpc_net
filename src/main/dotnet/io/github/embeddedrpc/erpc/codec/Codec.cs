/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using io.github.embeddedrpc.erpc.auxiliary;
using System.IO;

namespace io.github.embeddedrpc.erpc.codec;

/**
 * Interface for Codec.
 */
public interface Codec
{
    /**
     * Reset codec.
     */
    void reset();

    /**
     * Return bytea array from code's buffer.
     *
     * @return buffer data
     */
    byte[] array();

    /**
     * Return code's ByteBuffer.
     *
     * @return buffer
     */
    ByteBuffer getBuffer();

    /**
     * Set buffer data.
     *
     * @param array data to be set
     */
    void setArray(byte[] array);

    /**
     * Write message info to buffer.
     *
     * @param msgInfo message info
     */
    void startWriteMessage(MessageInfo msgInfo);

    /**
     * Write bool to buffer.
     *
     * @param value bool value
     */
    void writeBool(bool value);

    /**
     * Write int8 to buffer.
     *
     * @param value int8 value
     */
    void writeInt8(sbyte value);

    /**
     * Write int16 to buffer.
     *
     * @param value 16 value
     */
    void writeInt16(short value);

    /**
     * Write int32 to buffer.
     *
     * @param value int32 value
     */
    void writeInt32(int value);

    /**
     * Write int64 to buffer.
     *
     * @param value int64 value
     */
    void writeInt64(long value);

    /**
     * Write uint8 to buffer.
     *
     * @param value uint8 value
     */
    void writeUInt8(byte value);

    /**
     * Write uint16 to buffer.
     *
     * @param value uint16 value
     */
    void writeUInt16(UInt16 value);

    /**
     * Write uint32 to buffer.
     *
     * @param value uint32 value
     */
    void writeUInt32(UInt32 value);

    /**
     * Java implementation does not support uint64.
     *
     * @param value value
     */
    void writeUInt64(UInt64 value);

    /**
     * Write float to buffer.
     *
     * @param value float value
     */
    void writeFloat(float value);

    /**
     * Write double to buffer.
     *
     * @param value double value
     */
    void writeDouble(double value);

    /**
     * Write string to buffer.
     *
     * @param value string value
     */
    void writeString(String value);

    /**
     * Write binary (byte array) to buffer.
     *
     * @param value binary value
     */
    void writeBinary(byte[] value);

    /**
     * Write list length to buffer.
     *
     * @param length list length
     */
    void startWriteList(int length);

    /**
     * Write union discriminator to buffer.
     *
     * @param discriminator discriminator
     */
    void startWriteUnion(UInt32 discriminator);

    /**
     * Write null flag to buffer.
     *
     * @param value flag value
     */
    void writeNullFlag(int value);

    /**
     * Read message info.
     *
     * @return Message info
     */
    MessageInfo startReadMessage();

    /**
     * Read bool from buffer.
     *
     * @return bool value
     */
    bool readBool();

    /**
     * Read int8 from buffer.
     *
     * @return int8 value
     */
    sbyte readInt8();

    /**
     * Read int16 from buffer.
     *
     * @return int16 value
     */
    short readInt16();

    /**
     * Read int32 from buffer.
     *
     * @return int32 value
     */
    int readInt32();

    /**
     * Read int64 from buffer.
     *
     * @return int64 value
     */
    long readInt64();

    /**
     * Read uint8 from buffer.
     *
     * @return uint8 value
     */
    byte readUInt8();

    /**
     * Read uint16 from buffer.
     *
     * @return uint16 value
     */
    ushort readUInt16();

    /**
     * Read uint32 from buffer.
     *
     * @return uin32 value
     */
    uint readUInt32();

    /**
     * Java implementation does not support uint64.
     *
     * @return bool value
     */
    ulong readUInt64();

    /**
     * Read float from buffer.
     *
     * @return float value
     */
    float readFloat();

    /**
     * Read double from buffer.
     *
     * @return double value
     */
    double readDouble();

    /**
     * Read string from buffer.
     *
     * @return string value
     */
    string readString();

    /**
     * Read binary from buffer.
     *
     * @return binary value
     */
    byte[] readBinary();

    /**
     * Read bool from buffer.
     *
     * @return bool value
     */
    int startReadList();

    /**
     * Read bool from buffer.
     *
     * @return bool value
     */
    int startReadUnion();

    /**
     * Read bool from buffer.
     *
     * @return bool value
     */
    bool readNullFlag();

}
