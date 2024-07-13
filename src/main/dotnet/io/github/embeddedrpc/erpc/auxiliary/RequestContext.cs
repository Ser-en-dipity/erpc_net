/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using io.github.embeddedrpc.erpc.codec;
using System.IO;

namespace io.github.embeddedrpc.erpc.auxiliary;

/**
 * Record storing request context.
 *
 * @param sequence Sequence number
 * @param message  Message to be sent
 * @param codec    Codec used to store send and received message
 * @param isOneWay Request direction
 */
public record RequestContext(int sequence, ByteBuffer message, Codec codec, bool isOneWay)
{
}
