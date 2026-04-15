using System.Text;

namespace Algebra;

public class AbstractMatrix
{
    public uint[] m_matrix { get; set; }
    protected int m_nrows;
    protected int m_ncols;
    public AbstractMatrix()
    {
        m_nrows = 1;
        m_ncols = 1;
        int ndwords = (m_ncols + 31) >> 5;
        m_matrix = new uint[m_nrows * ndwords];
    }
    public AbstractMatrix(uint[] matrix, int nrows, int ncols)
    {
        m_nrows = nrows;
        m_ncols = ncols;
        int ndwords = (m_ncols + 31) >> 5;
        m_matrix = new uint[nrows * ndwords];
        matrix.CopyTo(m_matrix);
    }
    public void Reduce()
    {
        int pivotRow = 0;
        int nwords = (m_ncols + 31) >> 5;
        for (int col = 0; col < m_ncols && pivotRow < m_nrows; ++col)
        {
            int sel = pivotRow;
            int current_dword = col >> 5;
            int index = col & 0x1f;
            // 1. Find pivot row
            while (sel < m_nrows && ((m_matrix[sel * nwords + current_dword] >> (31 - index)) & 1) == 0)
            {
                sel += 1;
            }
            
            // 2. Swap rows
            if (sel == m_nrows) continue;
            if (sel != pivotRow)
            {
                for (int k = 0; k < nwords; ++k)
                {
                    // Method 1: using temporary variable
                    // uint tmp = matrix[sel * nwords + k];
                    // matrix[sel * nwords + k] = matrix[pivotRow * nwords + k];
                    // matrix[pivotRow * nwords + k] = tmp;

                    // Method 2: using a' = a + b, b' = a' - b = a, a'' = a' - b' - b
                    m_matrix[sel * nwords + k] ^= m_matrix[pivotRow * nwords + k];
                    m_matrix[pivotRow * nwords + k] = m_matrix[pivotRow * nwords + k] ^ m_matrix[sel * nwords + k];
                    m_matrix[sel * nwords + k] ^= m_matrix[pivotRow * nwords + k];
                }
            }
            
            // 3. Eliminate other 1's in this column
            for (int i = 0; i < m_nrows; ++i)
            {
                if (i != pivotRow && ((m_matrix[i * nwords + current_dword] >> (31 - index)) & 1) == 1)
                {
                    for (int k = current_dword; k < nwords; ++k)
                    {
                        m_matrix[i * nwords + k] ^= m_matrix[pivotRow * nwords + k];
                    }
                }
            }
            pivotRow += 1;
        }
    }
    public override string ToString()
    {
        StringBuilder result_string_builder = new StringBuilder();
        List<string> row_strings = new List<string>();
        int nwords = (m_ncols + 31) >> 5;
        for (int i = 0; i < m_nrows; ++i)
        {
            StringBuilder row_string_builder = new StringBuilder();
            List<string> row_bits = new List<string>();
            for (int j = 0; j < m_ncols; ++j)
            {
                int current_dword = j >> 5;
                int index = j % 32;
                row_bits.Add($"{(m_matrix[i * nwords + current_dword] >> (31 - index)) & 1}");
            }
            row_string_builder.AppendJoin(", ", row_bits);
            row_strings.Add(row_string_builder.ToString());
        }
        result_string_builder.AppendJoin("\n", row_strings);
        return result_string_builder.ToString();
    }
}