/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;
using io.github.embeddedrpc.erpc.auxiliary;

namespace io.github.embeddedrpc.erpc.client;
/**
 * Client manager.
 */
public class ClientManager
{
    private readonly Transport transport;
    private readonly CodecFactory codecFactory;
    private int sequence;

    /**
     * ClientManager constructor.
     *
     * @param transport    Transport used for communication
     * @param codecFactory Codec factory used for creating codec instances
     */
    public ClientManager(Transport transport,
                         CodecFactory codecFactory)
    {
        this.transport = transport;
        this.codecFactory = codecFactory;
        this.sequence = 0;
    }

    private int getSequence()
    {
        sequence += 1;
        return sequence;
    }

    /**
     * Function creates new request context.
     *
     * @param isOneWay Specify request direction
     * @return New request context
     */
    public RequestContext createRequest(bool isOneWay)
    {
        Codec codec = codecFactory.create();

        return new RequestContext(getSequence(), null, codec, isOneWay);
    }

    /**
     * Function takes @{RequestContext} and perform data exchange with server.
     * If request is not one way, read incoming data and save them to request's
     * codec.
     *
     * @param request Request to be performed
     */
    public void performRequest(RequestContext request)
    {
        this.transport.send(request.codec.array());

        if (!request.isOneWay)
        {
            request.codec.setArray(this.transport.receive());

            MessageInfo info = request.codec.startReadMessage();

            if (info.type != MessageType.kReplyMessage)
            {
                throw new RequestError("Invalid reply message type.");
            }

            if (info.sequence != request.sequence)
            {
                throw new RequestError("Unexpected sequence number in reply (was " + info.sequence + ", expected "
                        + request.sequence + ").");
            }
        }
    }

}