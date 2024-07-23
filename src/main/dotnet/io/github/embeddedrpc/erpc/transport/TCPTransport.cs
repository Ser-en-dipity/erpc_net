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
public abstract class TCPTransport : FramedTransport
{
    protected readonly Socket _socket;
    protected readonly IPEndPoint _endpoint;
    protected readonly IPAddress _ip;
    protected Socket _handler;

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
        _endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
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

    public abstract Socket getSocket();

    public abstract void Open();
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


}
