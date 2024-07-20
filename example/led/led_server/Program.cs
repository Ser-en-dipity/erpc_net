using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.server;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;
using LED.client;
using LED.common.enums;

public class Program
{
    public static void Main(String[] args)
    {
        // Transport transport = new TCPServerTransport("127.0.0.1", 40);
        Transport transport = new SerialTransport("COM4", 9600);

        Server server = new SimpleServer(transport, new BasicCodecFactory());

        server.addService(new MyTestService());

        server.run();
    }
}