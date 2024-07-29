using io.github.embeddedrpc.erpc.auxiliary;
using io.github.embeddedrpc.erpc.client;
using io.github.embeddedrpc.erpc.codec;
using mat.erpc_matrix_multiply.client;
using mat.erpc_matrix_multiply.common;
using io.github.embeddedrpc.erpc.server;
using io.github.embeddedrpc.erpc.transport;
using System.IO.Ports;

namespace io.github.Main;

class Program
{
    public static readonly String HOST = "127.0.0.1";
    public static readonly int PORT = 40;
    public static readonly int BAUDRATE = 115200;
    static void Main(string[] args)
    {
        // Transport serial = new SerialTransport("COM3", 9600);
        Transport serial = new TCPClientTransport(HOST, PORT);

        client(serial);

    }

    public static void fillMatrix(int[][] matrix)
    {
        Random rand = new Random();
        for (int y = 0; y < matrix.Length; y++)
        {
            var row = new int[5];
            for (int x = 0; x < 5; x++)
            {
                row[x] = rand.Next(0, 10);
            }
            matrix[y] = row;
        }
    }

    public static void printMatrix(int[][] matrix)
    {
        foreach (int[] y in matrix)
        {
            foreach (int x in y)
            {
                Console.Write(x);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public static void server(Transport transport)
    {
        Server server = new SimpleServer(transport, new BasicCodecFactory());

        server.addService(new MatrixMultiplyService());

        server.run();
    }

    public static void client(Transport transport)
    {
        ClientManager clientManager = new ClientManager(transport, new BasicCodecFactory());
        MatrixMultiplyServiceClient client = new MatrixMultiplyServiceClient(clientManager);

        try
        {
            while (true)
            {
                int[][] matrix1 = new int[Constants.matrix_size][];
                int[][] matrix2 = new int[5][];
                Reference<int[][]> matrixResult = new();

                fillMatrix(matrix1);
                fillMatrix(matrix2);

                Console.WriteLine("Matrix #1\r\n=========");
                printMatrix(matrix1);
                Console.WriteLine("\r\nMatrix #1\r\n=========");
                printMatrix(matrix2);

                Console.WriteLine("\r\neRPC request is sent to the server");
                client.erpcMatrixMultiply(matrix1, matrix2, matrixResult);

                Console.WriteLine("\r\nMatrix result\r\n=========");
                printMatrix(matrixResult.get());

                Console.WriteLine("\r\nPress Enter to initiate the next matrix multiplication or 'q' to quit");

            }
        }
        finally
        {
            transport.close();
        }

        Console.WriteLine("eRPC Matrix Multiply TCP example finished.");
    }
}