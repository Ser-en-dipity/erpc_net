/*
 * Copyright 2023 NXP
 *
 * SPDX-License-Identifier: BSD-3-Clause
 */

namespace io.github.embeddedrpc.erpc.auxiliary;

/**
 * Message type.
 */
public enum MessageType
{
    /**
     * Invocation message.
     */
    kInvocationMessage = 0,

    /**
     * One way message.
     */
    kOnewayMessage = 1,

    /**
     * Reply message.
     */
    kReplyMessage = 2,

    /**
     * Notification message.
     */
    kNotificationMessage = 3

}

/**
 * Function returns @{MessageType} base on value.
 *
 * @param value Value to be converted to MessageType
 * @return Corresponding MessageType
 */
