/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */


using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;

using System.Collections.Generic;
using System.Linq;

namespace io.github.embeddedrpc.erpc.server;
/**
 * Abstract server class.
 */
public abstract class Server
{
    private readonly CodecFactory codecFactory;
    private readonly Transport transport;
    private IDictionary<int, Service> services = new Dictionary<int, Service>();

    /**
     * Server constructor.
     *
     * @param transport    transport used in server
     * @param codecFactory codec factory used for creating codec
     */
    public Server(Transport transport, CodecFactory codecFactory)
    {
        this.transport = transport;
        this.codecFactory = codecFactory;
    }

    protected CodecFactory getCodecFactory()
    {
        return codecFactory;
    }

    protected Transport getTransport()
    {
        return transport;
    }

    /**
     * Get unmodifiable map of all services.
     *
     * @return map of services.
     */
    public IDictionary<int, Service> getServices()
    {
        return services.ToDictionary();
    }

    /**
     * Add new service to the server.
     *
     * @param service Service to be added
     */
    public void addService(Service service)
    {
        services.Add(service.getServiceId(), service);
    }

    /**
     * Takes Codec with received data and process them.
     *
     * @param codec Codec with data
     */
    protected void processRequest(Codec codec)
    {
        MessageInfo info = codec.startReadMessage();

        if (info.type != MessageType.kInvocationMessage && info.type != MessageType.kOnewayMessage)
        {
            throw new RequestError("Invalid type of incoming request");
        }

        Service service = services[info.service];

        if (service == null)
        {
            throw new RequestError("Invalid service ID (" + info.service + ")");
        }

        service.handleInvocation(info.request, info.sequence, codec);

        if (info.type == MessageType.kOnewayMessage)
        {
            codec.reset();
        }
    }

    /**
     * Run the server.
     */
    public abstract void run();

    /**
     * Stop the server.
     */
    public abstract void stop();
}
