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
public sealed class TCPTransport : FramedTransport
{
    private readonly Socket _socket;
    private readonly IPEndPoint _endpoint;
    private readonly IPAddress _ip;

    /**
     * TCPTransport constructor.
     *
     * @param ip   IP of the eRPC server
     * @param port Port of the eRPC server
     * @throws UnknownHostException
     * @throws IOException
     */
    public TCPTransport(String ip, int port)
    {
        _ip = IPAddress.Parse(ip);
        IPEndPoint _endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        _socket = new Socket(_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }

    /**
     * Create TCP transport.
     *
     * @param port TCP port
     * @throws IOException TCP error
     */
    public TCPTransport(int port)
    {
        _ip = IPAddress.Loopback;
        _endpoint = new IPEndPoint(_ip, port);
        _socket = new Socket(_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }

    /**
     * Function closes all opened connections and streams.
     */
    public override void close()
    {
        try
        {

            _socket.Close();
        }
        catch (IOException e)
        {
            throw new TransportError("Error closing TCP connection", e);
        }
    }

    public override byte[] baseReceive(int count)
    {
        byte[] buffer = new byte[count];
        try
        {
            if (in.read(buffer, 0, count) != count) {
                throw new TransportError("Error receiving TCP data.");
            }
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
            out.write(message, 0, message.length);
            out.flush();
        }
        catch (IOException e)
        {
            throw new TransportError("Error sending TCP data", e);
        }

    }
}
