using io.github.embeddedrpc.erpc.auxiliary;
using iCNC.Servo.Connection.server;
using iCNC.Servo.Connection.common.enums;

public class MyTestService : AbstractConnectionService
{
    public override void IsAlive(Heartbeat req, Reference<Heartbeat> rep)
    {
        Console.WriteLine("IsAlive called with " + req);
        rep.set(Heartbeat.ACK);
    }
}