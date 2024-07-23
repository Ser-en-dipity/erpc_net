/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System.Net.Sockets;
using System.Text;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;

namespace io.github.embeddedrpc.erpc.server;

/**
 * Simple server implementation. Single thread.
 */
public sealed class SimpleServer : Server
{
    private bool runFlag;

    /**
     * Crea simple server.
     *
     * @param transport    transport used in server
     * @param codecFactory codec factory used for creating codec on request
     */
    public SimpleServer(Transport transport, CodecFactory codecFactory) : base(transport, codecFactory)
    {
    }

    private void receiveRequest()
    {
        byte[] data = getTransport().receive();
        if (data.Length == 0 && getTransport() is TCPServerTransport)
        {
            var transport = getTransport() as TCPServerTransport;
            transport.close();
            return;
        }

        Codec codec = getCodecFactory().create(data);

        processRequest(codec);

        byte[] responseData = codec.array();

        if (responseData.Length != 0)
        {
            getTransport().send(responseData);
        }
    }

    public override void run()
    {
        runFlag = true;

        while (runFlag)
        {
            try
            {
                if (getTransport() is TCPServerTransport)
                {
                    var transport = getTransport() as TCPServerTransport;
                    if (transport.getSocket().Connected == false)
                    {
                        transport.Open();
                    }
                }
                receiveRequest();
            }
            catch (TransportError e)
            {
                Console.WriteLine("Transport error: " + e);
                stop();
            }
            catch (TransRequestError e)
            {
                Console.WriteLine("Error while processing request: " + e);
                stop();
            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket error: " + e);
                // var transport = getTransport() as TCPServerTransport;
                // transport.close();
                // stop();
            }
        }
    }

    public override void stop()
    {
        runFlag = false;
    }

}
