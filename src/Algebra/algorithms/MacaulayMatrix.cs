namespace CryptoS.Algebra.Boolean;

/// <summary>
/// MacaulayMatrix of a set of polynomials.
/// </summary>
public class MacaulayMatrix : AbstractMatrix
{
    private readonly uint[] _nullRow;
    private readonly List<Monomial> _monomials;
    private readonly MonomialOrdering _monomialOrdering;
    /// <summary>
    /// Constructor with list of polynomials.
    /// </summary>
    /// <param name="polynomials">List of polynomials</param>
    public MacaulayMatrix(List<Polynomial> polynomials)
    {
        _monomialOrdering = polynomials[0].Order;
        _monomials = new List<Monomial>();
        foreach (var polynomial in polynomials)
        {
            _monomials.AddRange(polynomial.Monomials);
        }
        _monomials = _monomials.Distinct().OrderBy(monomial => monomial, _monomialOrdering).ToList();
        _nrows = polynomials.Count;
        _ncols = _monomials.Count;
        int ndwords = (_ncols + 31) >> 5;
        _nullRow = new uint[ndwords];
        Matrix = new uint[_nrows * ndwords];
        for (int i = 0; i < _nrows; ++i)
        {
            for (int j = 0; j < _ncols; ++j)
            {
                int current_dword = j >> 5;
                int col_in_dword = j & 0x1f;
                int index = polynomials[i].Monomials.BinarySearch(_monomials[_ncols - 1 - j], _monomialOrdering);
                if (index >= 0)
                {
                    Matrix[i * ndwords + current_dword] |= 1U << (31 - col_in_dword);
                }
            }
        }
    }
    /// <summary>
    /// Get polynomials from current Macaulay matrix.
    /// </summary>
    /// <returns>List of polynomials from current Macaulay matrix.</returns>
    public List<Polynomial> GetPolynomials()
    {
        List<Polynomial> result = new List<Polynomial>();
        int ndwords = (_ncols + 31) >> 5;
        
        for (int i = 0; i < _nrows; ++i)
        {
            ReadOnlySpan<uint> first = new ReadOnlySpan<uint>(Matrix, i * ndwords, ndwords);
            if (first.SequenceEqual(_nullRow))
            {
                break;
            }
            Polynomial p = new Polynomial(_monomialOrdering);
            for (int j = 0; j < _ncols; ++j)
            {
                int current_dword = j >> 5;
                int col_in_dword = j & 0x1f;
                if (((Matrix[i * ndwords + current_dword] >> (31 - col_in_dword)) & 1) == 1)
                {
                    p.Add(_monomials[_ncols - 1 - j]);
                }
            }
            result.Add(p);
        }
        return result;
    }
}
