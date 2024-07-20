/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

namespace io.github.embeddedrpc.erpc.codec;

/**
 * Implementation of CodeFactory for BasicCodec.
 */
public sealed class BasicCodecFactory : CodecFactory
{
    protected override Codec createCodec()
    {
        return new BasicCodec();
    }

    protected override Codec createCodec(byte[] array)
    {
        return new BasicCodec(array);
    }

}
