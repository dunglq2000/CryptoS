using System.Drawing;
using System.Numerics;
using System.Text;

namespace Sudoku;

/// <summary>
/// Class for representation standard Sudoku's matrix and solve it.
/// </summary>
/// <example>
/// <code>
/// using Sudoku;
/// SudokuMatrix sudokuMatrix = new SudokuMatrix([
///     2, 9, 5, 7, 0, 0, 8, 6, 0,
///     0, 3, 1, 8, 6, 5, 0, 2, 0,
///     8, 0, 6, 0, 0, 0, 0, 0, 0,
///     0, 0, 7, 0, 5, 0, 0, 0, 6,
///     0, 0, 0, 3, 8, 7, 0, 0, 0,
///     5, 0, 0, 0, 1, 6, 7, 0, 0,
///     0, 0, 0, 5, 0, 0, 1, 0, 9,
///     0, 2, 0, 6, 0, 0, 3, 5, 0,
///     0, 5, 4, 0, 0, 8, 6, 7, 2
/// ]);
/// Console.WriteLine("Input sudoku:");
/// Console.WriteLine(sudokuMatrix);
/// sudokuMatrix.Solve();
/// Console.WriteLine("Output sudoku:");
/// Console.WriteLine(sudokuMatrix);
/// </code>
/// </example>
public class SudokuMatrix
{
    /// <summary>
    /// The size of small squares inside the grid. For standard sudoku, <c>N = 3</c>.
    /// </summary>
    public readonly int N;
    /// <summary>
    /// The number of rows/columns, which equals <c>N * N</c>.
    /// </summary>
    public readonly int Size;
    /// <summary>
    /// Represent sudoku's grid as matrix.
    /// </summary>
    public int[] Matrix;
    private int[] Result;
    /// <summary>
    /// Constructor with matrix in row-major order and size of small square.
    /// </summary>
    /// <param name="matrix">Matrix in row-major order.</param>
    /// <param name="n">Size of small square. Each dimension of the matrix should equal <c>n</c>x<c>n</c>.</param>
    /// <exception cref="ArgumentException">Throw exception when the size of the matrix is not <c>n</c>^4.</exception>
    public SudokuMatrix(int[] matrix, int n = 3)
    {
        Size = n * n;
        if (matrix.Length != Size * Size)
        {
            throw new ArgumentException($"Input matrix must have {Size * Size} elements.");
        }
        N = n;
        Matrix = new int[Size * Size];
        Result = new int[Size * Size];
        matrix.CopyTo(Matrix);
    }
    /// <summary>
    /// Constructor with matrix in as 2D-array and size of small square.
    /// </summary>
    /// <param name="matrix">Matrix as 2D-array.</param>
    /// <param name="n">Size of small square. Each dimension of the matrix should equal <c>n</c>x<c>n</c>.</param>
    /// <exception cref="ArgumentException">Throw exception when the size of the matrix is not <c>n</c>^4.</exception>

    public SudokuMatrix(int[,] matrix, int n = 3)
    {
        Size = n * n;
        if (matrix.GetLength(0) != Size && matrix.GetLength(1) != Size)
        {
            throw new ArgumentException($"Input matrix must be {Size}x{Size} array.");
        }
        N = n;
        Matrix = new int[Size * Size];
        Result = new int[Size * Size];
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                Matrix[i * Size + j] = matrix[i, j];
            }
        }
    }
    /// <summary>
    /// Print Sudoku's grid as matrix from <see cref="Matrix"/> .
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        // if (Size < 10)
        // {
            StringBuilder matrixStringBuilder = new StringBuilder();
            List<string> rowsList = new List<string>();
            for (int i = 0; i < Size; ++i)
            {
                StringBuilder rowStringBuilder = new StringBuilder();
                List<string> rowList = new List<string>();
                for (int j = 0; j < Size; ++j)
                {
                    rowList.Add($"{Matrix[i * Size + j]}");
                }
                rowStringBuilder.AppendJoin(" ", rowList);
                rowsList.Add(rowStringBuilder.ToString());
            }
            matrixStringBuilder.AppendJoin("\n", rowsList);
        // }
        return matrixStringBuilder.ToString();
    }
    /// <summary>
    /// Solve the Sudoku.
    /// </summary>
    public void Solve()
    {
        BruteForce(0, 0);
        Result.CopyTo(Matrix);
    }
    private void BruteForce(int x, int y)
    {
        if (x == Size && y == 0)
        {
            Matrix.CopyTo(Result);
            return;
        }
        if (Matrix[x * Size + y] != 0)
        {
            if (y == Size - 1)
            {
                BruteForce(x + 1, 0);
            }
            else
            {
                BruteForce(x, y + 1);
            }
        }
        else
        {
            for (int v = 1; v <= Size; ++v)
            {
                Matrix[x * Size + y] = v;
                var checkCell = CheckRow(x, y) & CheckCol(x, y) & CheckSquare3x3(x, y);
                if (checkCell == false)
                {
                    // backtrack if failed
                    Matrix[x * Size + y] = 0;
                    continue;
                }
                if (y == Size - 1)
                {
                    BruteForce(x + 1, 0);
                }
                else
                {
                    BruteForce(x, y + 1);
                }
                // backtrack
                Matrix[x * Size + y] = 0;
            }   
        }
    }
    /// <summary>
    /// Verify that element at row <c>x</c> and column <c>y</c> is unique on the column <c>y</c>.
    /// </summary>
    /// <param name="x">Index of the row.</param>
    /// <param name="y">Index of the column.</param>
    /// <returns>true if element at row <c>x</c> and column <c>y</c> is unique on the column <c>y</c>, false otherwise.</returns>
    private bool CheckCol(int x, int y)
    {
        if (Matrix[x * Size + y] == 0)
        {
            throw new ArgumentException($"Matrix element at row {x} and column {y} has not been set.");
        }
        for (int i = 0; i < Size; ++i)
        {
            if (i == x) continue;
            if (Matrix[i * Size + y] == Matrix[x * Size + y])
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Verify that element at row <c>x</c> and column <c>y</c> is unique on the column <c>y</c>.
    /// </summary>
    /// <param name="x">Index of the row.</param>
    /// <param name="y">Index of the column.</param>
    /// <returns>true if element at row <c>x</c> and column <c>y</c> is unique on the column <c>y</c>, false otherwise.</returns>
    private bool CheckRow(int x, int y)
    {
        if (Matrix[x * Size + y] == 0)
        {
            throw new ArgumentException($"Matrix element at row {x} and column {y} has not been set.");
        }
        for (int j = 0; j < Size; ++j)
        {
            if (j == y) continue;
            if (Matrix[x * Size + j] == Matrix[x * Size + y])
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Verify that element at row <c>x</c> and column <c>y</c> is unique on the 3x3 square. This function is applied only for standard Sudoku on 9x9 grid.
    /// </summary>
    /// <param name="x">Index of the row.</param>
    /// <param name="y">Index of the column.</param>
    /// <returns>true if element at row <c>x</c> and column <c>y</c> is unique on the 3x3 square, false otherwise.</returns>
    private bool CheckSquare3x3(int x, int y)
    {
        if (Matrix[x * N * N + y] == 0)
        {
            throw new ArgumentException($"Matrix element at row {x} and column {y} has not been set.");
        }
        int size = N * N;
        int cellRowIndex = x / N, cellColIndex = y / N;
        for (int i = 0; i < N; ++i)
        {
            if ((cellRowIndex * N + i) == x) continue;
            for (int j = 0; j < N; ++j)
            {
                if ((cellColIndex * N + j) == y) continue;
                if (Matrix[(cellRowIndex * N + i) * size + cellColIndex * N + j] == Matrix[x * size + y])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
