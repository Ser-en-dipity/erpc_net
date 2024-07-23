using io.github.embeddedrpc.erpc.auxiliary;
using LED.server;
using LED.common.enums;

public class MyTestService : AbstractIOService
{
    public override void set_led(LEDName whichLed, bool onOrOff)
    {
        Console.WriteLine("Setting LED " + whichLed + " to " + onOrOff);
    }
}