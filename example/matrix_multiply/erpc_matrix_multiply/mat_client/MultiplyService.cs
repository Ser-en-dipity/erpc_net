using io.github.embeddedrpc.erpc.auxiliary;
using mat.erpc_matrix_multiply.common;
using mat.erpc_matrix_multiply.server;
using io.github.Main;

public class MatrixMultiplyService : AbstractMatrixMultiplyServiceService
{

    public override void erpcMatrixMultiply(int[][] matrix1, int[][] matrix2, Reference<int[][]> result_matrix)
    {
        Console.WriteLine("Server received these matrices:");
        Console.WriteLine("\r\nMatrix #1");
        Console.WriteLine("=========");
        Program.printMatrix(matrix1);

        Console.WriteLine("\r\nMatrix #2");
        Console.WriteLine("=========");
        Program.printMatrix(matrix2);

        result_matrix.set(new int[Constants.matrix_size][]);

        for (int i = 0; i < Constants.matrix_size; i++)
        {
            for (int j = 0; j < Constants.matrix_size; j++)
            {
                for (int k = 0; k < Constants.matrix_size; k++)
                {
                    result_matrix.get()[i][j] += matrix1[i][k] * matrix2[k][j];
                }
            }
        }

        Console.WriteLine("\r\nResult matrix");
        Console.WriteLine("=========");
        Program.printMatrix(result_matrix.get());
    }
}