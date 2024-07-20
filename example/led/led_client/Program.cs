using System.Runtime.InteropServices;
using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.client;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;
using LED.client;
using LED.common.enums;

public class Program
{
    public static void Main(String[] args)
    {
        // Transport transport = new TCPClientTransport("127.0.0.1", 40);
        Transport transport = new SerialTransport("COM3", 9600);

        ClientManager clientManager = new ClientManager(transport, new BasicCodecFactory());
        IOClient client = new IOClient(clientManager);
        Reference<string> str;
        client.set_led(LEDName.kRed, true);
        transport.close();

    }
}