using System.Runtime.InteropServices;
using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.client;
using io.github.embeddedrpc.erpc.codec;
using io.github.embeddedrpc.erpc.transport;
using iCNC.Servo.Connection.interfaces;
using iCNC.Servo.Connection.common.enums;
using iCNC.Servo.Connection.client;

public class Program
{
    public static void Main(String[] args)
    {
        // Transport transport = new TCPClientTransport("127.0.0.1", 40);
        Transport transport = new SerialTransport("COM3", 9600);

        ClientManager clientManager = new ClientManager(transport, new BasicCodecFactory());
        ConnectionClient client = new ConnectionClient(clientManager);
        Reference<Heartbeat> rep = new();
        Console.WriteLine("Calling IsAlive: " + rep.get());
        client.IsAlive(Heartbeat.SYN, rep);
        Console.WriteLine("IsAlive returned " + rep.get());
        transport.close();

    }
}