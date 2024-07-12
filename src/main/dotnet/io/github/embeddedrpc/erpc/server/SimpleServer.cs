/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

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
                receiveRequest();
            }
            catch (TransportError e)
            {
                Console.WriteLine("Transport error: " + e);
                stop();
            }
            catch (RequestError e)
            {
                Console.WriteLine("Error while processing request: " + e);
                stop();
            }
        }
    }

    public override void stop()
    {
        runFlag = false;
    }

}
