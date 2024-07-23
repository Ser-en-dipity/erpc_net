/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace io.github.embeddedrpc.erpc.transport;

/**
 * Implementation of the TCP transport.
 */
public sealed class TCPClientTransport : TCPTransport
{

    /**
     * TCPTransport constructor.
     *
     * @param ip   IP of the eRPC server
     * @param port Port of the eRPC server
     * @throws UnknownHostException
     * @throws IOException
     */
    public TCPClientTransport(String ip, int port) : base(ip, port)
    {
        Open();
    }

    /**
     * Create TCP transport.
     *
     * @param port TCP port
     * @throws IOException TCP error
     */
    public TCPClientTransport(int port) : base(port)
    {
        Open();
    }

    public override Socket getSocket()
    {
        return _socket;
    }

    public override void Open()
    {
        try
        {
            _socket.Connect(_endpoint);
        }
        catch (IOException e)
        {
            throw new TransportError("Error opening TCP connection", e);
        }
    }
    /**
     * Function closes all opened connections and streams.
     */
    public override void close()
    {
        try
        {
            _socket.Shutdown(SocketShutdown.Both);
        }
        catch (IOException e)
        {
            throw new TransportError("Error closing TCP connection", e);
        }
        finally
        {
            _socket.Close();
        }

    }

    public override byte[] baseReceive(int count)
    {
        byte[] buffer = new byte[count];
        try
        {
            var received = _socket.Receive(buffer, SocketFlags.None);
        }
        catch (IOException e)
        {
            throw new TransportError("Error receiving TCP.", e);
        }
        return buffer;
    }

    public override void baseSend(byte[] message)
    {
        try
        {
            _ = _socket.Send(message, SocketFlags.None);
        }
        catch (IOException e)
        {
            throw new TransportError("Error sending TCP data", e);
        }

    }
}
