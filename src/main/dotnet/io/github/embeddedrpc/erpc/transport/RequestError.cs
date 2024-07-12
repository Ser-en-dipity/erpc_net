/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

namespace io.github.embeddedrpc.erpc.transport;

/**
 * Request error.
 */
public class TransRequestError : Exception
{
    /**
     * Request error.
     *
     * @param message error message
     */
    public TransRequestError(string message) : base(message)
    {
    }
}