/** 
 * Generated by erpcgen 1.13.0 on Thu Jul 18 14:34:12 2024.
 * 
 * AUTOGENERATED - DO NOT EDIT
 */ 



using io.github.embeddedrpc.erpc.auxiliary;
using System.Collections.Generic;

namespace mat.erpc_matrix_multiply.interfaces;

public interface IMatrixMultiplyService {
    void erpcMatrixMultiply(int[][] matrix1, int[][] matrix2, Reference<int[][]> result_matrix);
}



