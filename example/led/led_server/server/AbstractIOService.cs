/** 
 * Generated by erpcgen 1.13.0 on Fri Jul 19 11:28:23 2024.
 * 
 * AUTOGENERATED - DO NOT EDIT
 */
using LED.interfaces;

using LED.common.enums;
using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.server;
using System.Collections.Generic;

namespace LED.server;


/**
 * Testing abstract service class for simple eRPC interface.
 */
public abstract class AbstractIOService
        : Service, IIO {

    private static readonly int SERVICE_ID = 1;
    private static readonly int SET_LED_ID = 1;
    
    public abstract void set_led(LEDName whichLed, bool onOrOff);
    /**
     * Default constructor.
     */
    public AbstractIOService() : base(SERVICE_ID) {
        addMethodHandler(SET_LED_ID, this.set_ledHandler);
    }

    private void set_ledHandler(int sequence, Codec codec) {
        LEDName whichLed;
        bool onOrOff;

        whichLed = (LEDName)codec.readInt32();
        onOrOff = codec.readBool();

        set_led(whichLed, onOrOff);

        codec.reset();

        codec.startWriteMessage(new MessageInfo(
                MessageType.kReplyMessage,
                getServiceId(),
                SET_LED_ID,
                sequence)
        );

        // Read out parameters
    }

}

