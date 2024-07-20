/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System;

namespace io.github.embeddedrpc.erpc.auxiliary;

/**
 * Request error thrown when unexpected situation occurred during sending and receiving message.
 */
public class RequestError : Exception
{
    /**
     * Request error.
     *
     * @param message error message
     */
    public RequestError(string message) : base(message)
    {
    }

    /**
     * Request error.
     * @param message error message
     * @param cause error chain
     */
    public RequestError(string message, Exception cause) : base(message, cause)
    {
    }
}
