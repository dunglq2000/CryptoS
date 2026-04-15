using System.Numerics;
using System.Text;
namespace Algebra;

public class MacaulayMatrix : AbstractMatrix
{
    private readonly List<Monomial> m_monomials;
    private readonly MonomialOrdering m_monomialOrdering;
    public MacaulayMatrix(List<Polynomial> polynomials)
    {
        m_monomialOrdering = polynomials[0].monomialOrdering;
        m_monomials = new List<Monomial>();
        foreach (var polynomial in polynomials)
        {
            m_monomials.AddRange(polynomial.monomials);
        }
        m_monomials = m_monomials.Distinct().OrderBy(monomial => monomial, m_monomialOrdering).ToList();
        m_nrows = polynomials.Count;
        m_ncols = m_monomials.Count;
        int ndwords = (m_ncols + 31) >> 5;
        m_matrix = new uint[m_nrows * ndwords];
        for (int i = 0; i < m_nrows; ++i)
        {
            for (int j = 0; j < m_ncols; ++j)
            {
                int current_dword = j >> 5;
                int col_in_dword = j & 0x1f;
                int index = polynomials[i].monomials.BinarySearch(m_monomials[m_ncols - 1 - j], m_monomialOrdering);
                if (index >= 0)
                {
                    m_matrix[i * ndwords + current_dword] |= 1U << (31 - col_in_dword);
                }
            }
        }
    }
    public List<Polynomial> GetPolynomials()
    {
        List<Polynomial> result = new List<Polynomial>();
        int ndwords = (m_ncols + 31) >> 5;
        for (int i = 0; i < m_nrows; ++i)
        {
            Polynomial p = new Polynomial(m_monomialOrdering);
            for (int j = 0; j < m_ncols; ++j)
            {
                int current_dword = j >> 5;
                int col_in_dword = j & 0x1f;
                if (((m_matrix[i * ndwords + current_dword] >> (31 - col_in_dword)) & 1) == 1)
                {
                    p.Add(m_monomials[m_ncols - 1 - j]);
                }
            }
            result.Add(p);
        }
        return result;
    }
}
