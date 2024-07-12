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

        int bytesSent = _serialPort.writeBytes(message, message.length);

        if (bytesSent != message.length)
        {
            throw new TransportError("Should send: " + message.length + ", but sent: " + bytesSent);
        }
    }

    public override byte[] baseReceive(int count)
    {
        Debug.Assert(serial.isOpen());

        byte[] received = new byte[count];
        int bytesRead = serial.readBytes(received, count);

        if (bytesRead != count)
        {
            throw new TransportError("Should read: " + count + ", but read: " + bytesRead);
        }

        return received;
    }

    public override void close()
    {
        if (serial.isOpen())
        {
            serial.closePort();
        }
    }

}