using System.Text;

namespace Algebra.Boolean;

/// <summary>
/// Abstract matrix over $\mathbb{F}_2$ with implementation for row-reduced echelon form.
/// </summary>
/// <remarks>
/// Matrix uses bit-packed technique for $\mathbb{F}_2$. Matrix with $n$ rows and $m$ columns consumes $\lceil nm/32 \rceil$ integers.
/// </remarks>
public class AbstractMatrix
{
    /// <summary>
    /// Array for representing matrix.
    /// </summary>
    public uint[] Matrix { get; set; }
    /// <summary>
    /// Number of rows of matrix.
    /// </summary>
    protected int _nrows;
    /// <summary>
    /// Number of columns of matrix.
    /// </summary>
    protected int _ncols;
    /// <summary>
    /// Default constructor for matrix with 1 row and 1 column.
    /// </summary>
    public AbstractMatrix()
    {
        _nrows = 1;
        _ncols = 1;
        int ndwords = (_ncols + 31) >> 5;
        Matrix = new uint[_nrows * ndwords];
    }
    /// <summary>
    /// Constructor for matrix in row-major.
    /// </summary>
    /// <param name="matrix">Matrix in row-major.</param>
    /// <param name="nrows">Number of rows.</param>
    /// <param name="ncols">Number of columns.</param>
    public AbstractMatrix(uint[] matrix, int nrows, int ncols)
    {
        _nrows = nrows;
        _ncols = ncols;
        int ndwords = (_ncols + 31) >> 5;
        Matrix = new uint[nrows * ndwords];
        matrix.CopyTo(Matrix);
    }
    /// <summary>
    /// Calculate row-reduced echelon form of current matrix.
    /// </summary>
    public void Reduce()
    {
        int pivotRow = 0;
        int nwords = (_ncols + 31) >> 5;
        for (int col = 0; col < _ncols && pivotRow < _nrows; ++col)
        {
            // First row below row <c>pivotRow</c> that has bit 1 in column <c>col</c>
            int sel = pivotRow;
            int current_dword = col >> 5;
            int index = col & 0x1f;
            // 1. Find pivot row
            while (sel < _nrows && (Matrix[sel * nwords + current_dword] & (1U << (31 - index))) == 0)
            {
                sel += 1;
            }
            if (sel == _nrows) continue;
            // 2. Swap rows if necessary
            if (sel != pivotRow)
            {
                Parallel.For(0, nwords, k =>
                {
                    // Method 1: using temporary variable
                    // uint tmp = matrix[sel * nwords + k];
                    // matrix[sel * nwords + k] = matrix[pivotRow * nwords + k];
                    // matrix[pivotRow * nwords + k] = tmp;

                    // Method 2: using a' = a + b, b' = a' - b = a, a'' = a' - b' - b
                    Matrix[sel * nwords + k] ^= Matrix[pivotRow * nwords + k];
                    Matrix[pivotRow * nwords + k] = Matrix[pivotRow * nwords + k] ^ Matrix[sel * nwords + k];
                    Matrix[sel * nwords + k] ^= Matrix[pivotRow * nwords + k];
                });
            }
            
            // 3. Eliminate other 1's in this column
            for (int i = 0; i < _nrows; ++i)
            {
                if (i == pivotRow) continue;
                if ((Matrix[i * nwords + current_dword] & (1U << (31 - index))) != 0)
                {
                    Parallel.For(current_dword, nwords, k =>
                    {
                        Matrix[i * nwords + k] ^= Matrix[pivotRow * nwords + k];
                    });
                }
            }
            pivotRow += 1;
        }
    }
    /// <summary>
    /// Print matrix on $\mathbb{F}_2$.
    /// </summary>
    /// <returns>String representing matrix on $\mathbb{F}_2$.</returns>
    public override string ToString()
    {
        StringBuilder result_string_builder = new StringBuilder();
        List<string> row_strings = new List<string>();
        int nwords = (_ncols + 31) >> 5;
        for (int i = 0; i < _nrows; ++i)
        {
            StringBuilder row_string_builder = new StringBuilder();
            List<string> row_bits = new List<string>();
            for (int j = 0; j < _ncols; ++j)
            {
                int current_dword = j >> 5;
                int index = j & 0x1f;
                row_bits.Add($"{(Matrix[i * nwords + current_dword] >> (31 - index)) & 1}");
            }
            row_string_builder.AppendJoin(", ", row_bits);
            row_strings.Add(row_string_builder.ToString());
        }
        result_string_builder.AppendJoin("\n", row_strings);
        return result_string_builder.ToString();
    }
}