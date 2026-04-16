using System.Text;

namespace Algebra;

public class AbstractMatrix
{
    public uint[] Matrix { get; set; }
    protected int _nrows;
    protected int _ncols;
    public AbstractMatrix()
    {
        _nrows = 1;
        _ncols = 1;
        int ndwords = (_ncols + 31) >> 5;
        Matrix = new uint[_nrows * ndwords];
    }
    public AbstractMatrix(uint[] matrix, int nrows, int ncols)
    {
        _nrows = nrows;
        _ncols = ncols;
        int ndwords = (_ncols + 31) >> 5;
        Matrix = new uint[nrows * ndwords];
        matrix.CopyTo(Matrix);
    }
    public void Reduce()
    {
        int pivotRow = 0;
        int nwords = (_ncols + 31) >> 5;
        for (int col = 0; col < _ncols && pivotRow < _nrows; ++col)
        {
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
                if (i != pivotRow && (Matrix[i * nwords + current_dword] & (1U << (31 - index))) != 0)
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
                int index = j % 32;
                row_bits.Add($"{(Matrix[i * nwords + current_dword] >> (31 - index)) & 1}");
            }
            row_string_builder.AppendJoin(", ", row_bits);
            row_strings.Add(row_string_builder.ToString());
        }
        result_string_builder.AppendJoin("\n", row_strings);
        return result_string_builder.ToString();
    }
}