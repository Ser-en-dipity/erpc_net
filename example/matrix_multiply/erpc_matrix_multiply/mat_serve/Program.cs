﻿using io.github.embeddedrpc.erpc.auxiliary;
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
    public static readonly String HOST = "localhost";
    public static readonly int PORT = 40;
    public static readonly int BAUDRATE = 115200;
    static void Main(string[] args)
    {
        Transport serial = new SerialTransport("COM4", 9600);

        server(serial);

    }

    public static void fillMatrix(int[][] matrix)
    {
        Random rand = new Random();
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                matrix[y][x] = Math.Abs(rand.Next(50) % 10);
            }
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