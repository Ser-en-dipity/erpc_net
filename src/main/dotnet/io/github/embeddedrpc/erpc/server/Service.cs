/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System;
using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.codec;
using System.Collections.Generic;

namespace io.github.embeddedrpc.erpc.server;

/**
 * Abstract class representing server's service. Service handle its methods invocation.
 */
public abstract class Service
{
    private readonly int serviceId;
    private IDictionary<int, Action<int, Codec>> methodHandlers = new Dictionary<int, Action<int, Codec>>();

    /**
     * Create new service with ID.
     *
     * @param serviceId service ID
     */
    public Service(int serviceId)
    {
        this.serviceId = serviceId;
    }

    /**
     * Return service ID.
     *
     * @return ID
     */
    public int getServiceId()
    {
        return serviceId;
    }

    /**
     * Add method to the service.
     *
     * @param methodId method id
     * @param handler  function that handle method
     */
    protected void addMethodHandler(int methodId, Action<int, Codec> handler)
    {
        methodHandlers.Add(methodId, handler);
    }

    /**
     * Handle service invocation. Find corresponding handler for method and execute it.
     *
     * @param methodId Method ID
     * @param sequence Message sequence number
     * @param codec    Codec with method's data
     */
    public void handleInvocation(int methodId, int sequence, Codec codec)
    {
        Action<int, Codec> method = methodHandlers[methodId];

        if (method == null)
        {
            throw new RequestError("Invalid method ID (" + methodId + ").");
        }

        try
        {
            method.accept(sequence, codec);
        }
        catch (Exception e) when (e is CodecError || e is RequestError)
        {
            throw new RequestError(
                    "Invalid method ID (" + methodId + ") or method implementation.", e);
        }
    }
}
