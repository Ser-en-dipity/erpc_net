using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace io.github.embeddedrpc.erpc.transport;

public class TCPDaemon
{
    public static bool isSocketConnected(Socket socket)
    {
        bool part1 = socket.Poll(1000, SelectMode.SelectRead);
        bool part2 = (socket.Available == 0);
        if (part1 && part2)
            return false;
        else
            return true;
    }
}