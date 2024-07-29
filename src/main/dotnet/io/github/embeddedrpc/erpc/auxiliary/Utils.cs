/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */
using System;
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
     * Convert uint16 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return byte array
     */
    public static byte[] uInt16ToBytes(UInt16 value)
    {
        return new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF) };
    }

    /**
     * Convert uint32 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return byte array
     */
    public static byte[] uInt32ToBytes(UInt32 value)
    {
        return new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF), (byte)((value >> 16) & 0xFF), (byte)((value >> 24) & 0xFF) };
    }

    /**
     * Convert int8 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return byte array
     */
    public static byte Int8ToBytes(sbyte value)
    {
        return (byte)value;
    }

    /**
     * Convert int16 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return byte array
     */
    public static byte[] Int16ToBytes(short value)
    {
        return BitConverter.GetBytes(value);
    }

    /**
     * Convert int32 to bytearray (little endian).
     *
     * @param value Value to by converted
     * @return byte array
     */
    public static byte[] Int32ToBytes(int value)
    {
        return BitConverter.GetBytes(value);
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
