/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

namespace io.github.embeddedrpc.erpc.transport;

using System.Diagnostics;
using System;
using System.IO.Ports;
using System.Threading;


/**
 * Serial transport implementation.
 */
public sealed class SerialTransport : FramedTransport
{
    private const int READ_TIMEOUT = 500;
    private const int WRITE_TIMEOUT = 500;
    private readonly SerialPort _serialPort;

    /**
     * Serial transport constructor.
     *
     * @param url      Serial port URL
     * @param baudrate Communication baud rate
     */
    public SerialTransport(string url, int baudrate) : base()
    {

        // Create a new SerialPort object with default settings.
        _serialPort = new SerialPort();

        // Allow the user to set the appropriate properties.
        _serialPort.PortName = url;
        _serialPort.BaudRate = baudrate;
        _serialPort.Parity = Parity.None;
        _serialPort.DataBits = 8;
        _serialPort.StopBits = StopBits.One;
        _serialPort.Handshake = Handshake.None;

        // Set the read/write timeouts
        _serialPort.ReadTimeout = READ_TIMEOUT;
        _serialPort.WriteTimeout = WRITE_TIMEOUT;

        _serialPort.Open();
    }

    public override void baseSend(byte[] message)
    {
        Debug.Assert(_serialPort.IsOpen);

        _serialPort.Write(message, 0, message.Length);

    }

    public override byte[] baseReceive(int count)
    {
        Debug.Assert(_serialPort.IsOpen);

        byte[] received = new byte[count];
        int bytesRead = 0;
        while (bytesRead < count)
        {
            try
            {
                bytesRead += _serialPort.Read(received, bytesRead, count - bytesRead);
            }

            catch (TimeoutException)
            {
                Console.WriteLine("TimeoutException");
            }
        }

        if (bytesRead != count)
        {
            throw new TransportError("Should read: " + count + ", but read: " + bytesRead);
        }

        return received;
    }

    public override void close()
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }

}