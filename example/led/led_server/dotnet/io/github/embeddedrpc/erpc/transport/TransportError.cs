/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

using System;

namespace io.github.embeddedrpc.erpc.transport;

/**
 * Transport runtime exception.
 */
public class TransportError : SystemException
{
    /**
     * Transport error.
     */
    public TransportError() : base()
    {
    }

    /**
     * Transport error.
     *
     * @param message error message
     */
    public TransportError(string message) : base(message)
    {
    }

    /**
     * Transport error.
     *
     * @param message error message
     * @param cause   error chain
     */
    public TransportError(string message, Exception cause) : base(message, cause)
    {
    }
}