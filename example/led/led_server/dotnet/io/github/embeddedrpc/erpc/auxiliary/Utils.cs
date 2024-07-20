/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */
using System.Text;

namespace io.github.embeddedrpc.erpc.auxiliary;

/**
 * Utility class.
 */
public sealed class Utils
{
    private const int HEX_BASE = 16;

    private Utils()
    {
    }

    /**
     * Check if UInt8 value is in valid range.
     *
     * @param value UInt8 value
     */
    public static void checkUInt8(short value)
    {
        if (value < 0 || value > 0xFF)
        {
            throw new ArgumentOutOfRangeException("Value has to be in rage from 0 to 2^8");
        }
    }

    /**
     * Check if UInt16 value is in valid range.
     *
     * @param value UInt16 value
     */
    public static void checkUInt16(int value)
    {
        if (value < 0 || value > 0xFFFF)
        {
            throw new ArgumentOutOfRangeException("Value has to be in rage from 0 to 2^16");
        }
    }

    /**
     * Check if Object is null.
     *
     * @param object       Object to be checked
     * @param errorMessage Message for exception
     */
    public static void checkNotNull(object obj, string errorMessage)
    {
        if (obj == null)
        {
            throw new ArgumentOutOfRangeException(errorMessage);
        }
    }

    /**
     * Check if UInt32 value is in valid range.
     *
     * @param value UInt32 value
     */
    public static void checkUInt32(long value)
    {
        if (value < 0 || value > 0xFFFFFFFFL)
        {
            throw new ArgumentOutOfRangeException("Value has to be in rage from 0 to 2^32");
        }
    }

    /**
     * Convert long representing uint32 to int. Persist 32 lower bites.
     * long 4294967295 (0xFFFFFFFF, UINT32_MAX) -> int -1 (0xFFFFFFFF)
     *
     * @param value Value to be converted
     * @return uint32 as int
     */
    public static int uInt32toInt(long value)
    {
        return (int)(value & 0xFFFFFFFFL);
    }

    /**
     * Convert int representing uint16 to short. Persist 16 lower bites.
     *
     * @param value Value to be converted
     * @return uint16 as short
     */
    public static short uInt16toShort(int value)
    {
        return (short)(value & 0xFFFF);
    }

    /**
     * Convert int representing uint8 to byte. Persist 8 lower bites.
     *
     * @param value Value to be converted
     * @return uint8 as byte
     */
    public static byte uInt8toByte(short value)
    {
        return (byte)(value & 0xFF);
    }

    /**
     * Convert int representing uint32 to long.
     *
     * @param value Value to be converted
     * @return uint32 as long
     */
    public static long intToUInt32(int value)
    {
        return ((long)value) & 0xFFFFFFFFL;
    }

    /**
     * Convert short representing uint16 to int.
     *
     * @param value Value to be converted
     * @return uint16 as int
     */
    public static int shortToUInt16(short value)
    {
        return ((int)value) & 0xFFFF;
    }

    /**
     * Convert byte representing uint8 to short.
     *
     * @param value Value to be converted
     * @return uint8 as short
     */
    public static short byteToUInt8(byte value)
    {
        return (short)(((short)value) & (short)0xFF);
    }

    /**
     * Conver int representing uint16 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return uint16 as bytes
     */
    public static byte[] uInt16ToBytes(int value)
    {
        checkUInt16(value);
        return new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF) };
    }

    /**
     * Function creates string representing byte array as hex.
     *
     * @param a Byte array
     * @return String representing byte array
     */
    public static string byteArrayToHex(byte[] a)
    {
        StringBuilder sb = new StringBuilder(a.Length * 2);
        foreach (byte b in a)
        {
            sb.Append(String.Format("%02x", b));
        }
        return sb.ToString();
    }

    /**
     * Function converts string to byte array.
     *
     * @param string String to be converted
     * @return Byte array
     */
    public static byte[] hexToByteArray(string str)
    {
        byte[] array = new byte[str.Length / 2];

        for (int i = 0; i < array.Length; i++)
        {
            int index = i * 2;
            byte j = Convert.ToByte(str.Substring(index, 2), HEX_BASE);
            array[i] = j;
        }

        return array;
    }
}
