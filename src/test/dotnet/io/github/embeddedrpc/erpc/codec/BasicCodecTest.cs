using io.github.embeddedrpc.erpc.auxiliary;
using Xunit;


namespace io.github.embeddedrpc.erpc.codec;

/**
 * Unit tests for BasicCodec.
 */
public class BasicCodecTest
{
    /**
     * Rigorous Test.
     */

    [Fact]
    public void boolTest()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeBool(false);
        codec.writeBool(true);

        Assert.Equal(false, codec.readBool());
        Assert.Equal(true, codec.readBool());
    }

    [Fact]
    public void int8Test()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeInt8(42);
        codec.writeInt8(123);
        codec.writeInt8(-12);
        codec.writeUInt8(42);
        codec.writeInt8(unchecked((sbyte)130));


        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readInt8());
        Assert.Equal(123, codec.readInt8());
        Assert.Equal(-12, codec.readInt8());
        Assert.Equal(42, codec.readUInt8());
        Assert.Equal(-126, codec.readInt8());
    }

    // imitate int8test rewrite int16test

    [Fact]
    public void int16Test()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeInt16((short)42);
        codec.writeInt16((short)-42);
        codec.writeUInt16((ushort)42);

        codec.writeUInt16(unchecked((ushort)-42));
        codec.writeUInt16((ushort)0xffff);
        codec.writeUInt16((ushort)0);
        codec.readUInt16();

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readInt16());
        Assert.Equal(-42, codec.readInt16());
        Assert.Equal(42, codec.readUInt16());
        Assert.Equal(65494, codec.readUInt16());
        Assert.Equal(0xffff, codec.readUInt16());
        Assert.Equal(0, codec.readUInt16());
    }


    [Fact]
    void int32Test()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeInt32(42);
        codec.writeInt32(-42);
        codec.writeUInt32(42);
        codec.writeInt32(12345);

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readInt32());
        Assert.Equal(-42, codec.readInt32());
        Assert.Equal<uint>(42, codec.readUInt32());
        Assert.Equal(12345, codec.readInt32());
    }

    [Fact]
    void int64Test()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeInt64(42);
        codec.writeInt64(-42);
        codec.writeInt64(7129843);
        codec.writeUInt64(182309);

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readInt64());
        Assert.Equal(-42, codec.readInt64());
        Assert.Equal(7129843, codec.readInt64());
        Assert.Equal<UInt64>(182309, codec.readUInt64());
    }

    [Fact]
    void floatTest()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeFloat(42);
        codec.writeFloat(-42);
        codec.writeFloat(0.125F);
        codec.writeFloat(-0.125F);

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readFloat());
        Assert.Equal(-42, codec.readFloat());
        Assert.Equal(0.125F, codec.readFloat());
        Assert.Equal(-0.125F, codec.readFloat());
    }

    [Fact]
    void doubleTest()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeDouble(42);
        codec.writeDouble(-42);
        codec.writeDouble(0.125F);
        codec.writeDouble(-0.00000125F);

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal(42, codec.readDouble());
        Assert.Equal(-42, codec.readDouble());
        Assert.Equal(0.125F, codec.readDouble());
        Assert.Equal(-0.00000125F, codec.readDouble());
    }

    [Fact]
    void stringTest()
    {
        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeString("HelloWord");
        codec.writeString("+ěščřžýáíé=");
        codec.writeString("Ḽơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįʈ");

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec
        Assert.Equal("HelloWord", codec.readString());
        Assert.Equal("+ěščřžýáíé=", codec.readString());
        Assert.Equal("Ḽơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįʈ", codec.readString());

    }

    // [Fact]
    // void longStringTest()
    // {
    //     // Testing data
    //     String testString = "Ḽơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįʈḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċť"
    //             + "ᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ,"
    //             + "ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐ"
    //             + "ť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłįḼơᶉëᶆ ȋṕšᶙṁ "
    //             + "ḍỡḽǭᵳ ʂǐť ӓṁệẗ, ĉṓɲṩḙċťᶒțûɾ ấɖḯƥĭṩčįɳġ ḝłį";

    //     // New codec
    //     Codec codec = new BasicCodec();

    //     // Push data to codec
    //     codec.writeString(testString);

    //     // Reset buffer position
    //     codec.getBuffer().position(0);

    //     // Read data from codec

    //     Assert.Equal(testString, codec.readString());

    // }

    [Fact]
    void longBinaryTest()
    {
        // Testing data
        byte[] array = new byte[65536];
        Array.Fill(array, (byte)42);

        Codec codec = new BasicCodec();

        // Push data to codec
        codec.writeBinary(array);
        codec.writeBinary(array);
        codec.writeBinary(array);

        // Reset buffer position
        codec.getBuffer().Position(0);

        // Read data from codec

        Assert.Equal(array, codec.readBinary());
        Assert.Equal(array, codec.readBinary());
        Assert.Equal(array, codec.readBinary());
    }

    // [Fact]
    // void byteRepresentationTest()
    // {
    //     Codec codec = new BasicCodec();

    //     // Push data to codec
    //     codec.writeBool(true);
    //     codec.writeBool(false);

    //     codec.writeInt8(-42);
    //     codec.writeInt8(42);
    //     codec.writeUInt8(42);

    //     codec.writeInt16(-42);
    //     codec.writeInt16(42);
    //     codec.writeUInt16(42);

    //     codec.writeInt32(-42);
    //     codec.writeInt32(42);
    //     codec.writeUInt32(42);

    //     codec.writeString("Ḽơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ");

    //     // Get bytes length
    //     int dataLength = codec.getBuffer().Capacity;

    //     // Reset buffer position
    //     codec.getBuffer().Position(0);

    //     // Read data from codec
    //     byte[] array = new byte[dataLength];
    //     codec.getBuffer().ReadBytes(array, 0, dataLength);

    //     // Assert equality (bytes string from Python implementation)
    //     Assert.Equal("0100d62a2ad6ff2a002a00d6ffffff2a0000002a0000003d000000e1b8bcc6a1e1b689c3abe1b68620c88be1b995c5a1e"
    //                     + "1b699e1b98120e1b88de1bba1e1b8bdc7ade1b5b320ca82c790c5a520d393e1b981e1bb87e1ba97",
    //             Utils.byteArrayToHex(array));
    // }

    // [Fact]
    // void fromBytesTest()
    // {
    //     String testString = "0100d62a2ad6ff2a002a00d6ffffff2a0000002a0000003d000000e1b8bcc6a1e1b689c3abe1b68620c88be1b9"
    //             + "95c5a1e1b699e1b98120e1b88de1bba1e1b8bdc7ade1b5b320ca82c790c5a520d393e1b981e1bb87e1ba97";
    //     Codec codec = new BasicCodec(Utils.hexToByteArray(testString));

    //     Assert.Equal(true, codec.readBool());
    //     Assert.Equal(false, codec.readBool());
    //     Assert.Equal(-42, codec.readInt8());
    //     Assert.Equal(42, codec.readInt8());
    //     Assert.Equal(42, codec.readUInt8());
    //     Assert.Equal(-42, codec.readInt16());
    //     Assert.Equal(42, codec.readInt16());
    //     Assert.Equal(42, codec.readUInt16());
    //     Assert.Equal(-42, codec.readInt32());
    //     Assert.Equal(42, codec.readInt32());
    //     Assert.Equal<uint>(42, codec.readUInt32());
    //     Assert.Equal("Ḽơᶉëᶆ ȋṕšᶙṁ ḍỡḽǭᵳ ʂǐť ӓṁệẗ", codec.readString());
    // }

    // [Fact]
    // void writeMessageTest()
    // {
    //     Codec codec = new BasicCodec();
    //     MessageInfo msgInfo = new MessageInfo(MessageType.kNotificationMessage, 1, 5, 4);

    //     codec.startWriteMessage(msgInfo);

    //     // Reset buffer position
    //     codec.getBuffer().position(0);

    //     MessageInfo msgInfoRead = codec.startReadMessage();

    //     // Assert equality
    //     Assert.Equal(msgInfo, msgInfoRead);
    // }

    // [Fact]
    // void readMessageTest()
    // {
    //     String testString = "020101012a000000";
    //     MessageInfo msgInfoExpected = new MessageInfo(MessageType.kReplyMessage, 1, 1, 42);

    //     Codec codec = new BasicCodec(Utils.hexToByteArray(testString));

    //     MessageInfo msgInfo = codec.startReadMessage();

    //     Assert.Equal(msgInfoExpected, msgInfo);
    // }

    // [Fact]
    // void codecToArrayTest()
    // {
    //     Codec codec = new BasicCodec();

    //     codec.writeInt32(1);

    //     byte[] array = codec.array();

    //     Assert.Equal(4, array.length);
    //     Assert.Equal(1, array[0]);
    //     Assert.Equal(0, array[1]);
    //     Assert.Equal(0, array[2]);
    //     Assert.Equal(0, array[3]);

    //     codec.writeUInt32(0xFFFFFFFFL);

    //     array = codec.array();

    //     Assert.Equal(8, array.length);
    //     Assert.Equal(1, array[0]);
    //     Assert.Equal(0, array[1]);
    //     Assert.Equal(0, array[2]);
    //     Assert.Equal(0, array[3]);
    //     Assert.Equal((byte)0xFF, array[4]);
    //     Assert.Equal((byte)0xFF, array[5]);
    //     Assert.Equal((byte)0xFF, array[6]);
    //     Assert.Equal((byte)0xFF, array[7]);

    // }
}
