/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

namespace io.github.embeddedrpc.erpc.codec;

/**
 * Codec error.
 */
public class CodecError : Exception
{
    /**
     * Request error.
     *
     * @param message error message
     */
    public CodecError(string message) : base(message)
    {
    }
}
