namespace CryptoS.Algebra.Boolean;

/// <summary>
/// Buchberger's algorithm for calculating Groeber's basis.
/// </summary>
public class BuchbergerAlgorithm: GroebnerAlgorithm
{
    private List<Tuple<int, int>> _pairs = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> _selectedPairs = new List<Tuple<int, int>>();
    private List<Polynomial> _polynomials = new List<Polynomial>();
    /// <summary>
    /// Calculate Groebner's basis.
    /// </summary>
    /// <param name="polynomials">List of polynomials for calculating Groebner's basis.</param>
    /// <returns>New list of polynomials, which is Groebner's basis of input polynomials.</returns>
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        _polynomials.AddRange(polynomials);
        var k = _polynomials.Count;
        for (var i = 0; i < k; ++i)
        {
            for (var j = i + 1; j < k; ++j)
            {
                _pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (_pairs.Count > 0)
        {
            Select();
            _pairs = _pairs.Except(_selectedPairs).ToList();
            foreach (var pair in _selectedPairs)
            {
                var S = _polynomials[pair.Item1].SPoly(_polynomials[pair.Item2]);
                var (r, q) = S.Divide(_polynomials);
                if (r.IsZero() == false)
                {
                    
                    _polynomials.Add(r);
                    k += 1;
                    for (var i = 0; i < k - 1; ++i)
                    {
                        _pairs.Add(new Tuple<int, int>(i, k - 1));
                    }
                }
            }
        }
        return _polynomials;
    }
    /// <summary>
    /// Select critical pairs of polynomials, used in Groebner's basis calculation.
    /// </summary>
    /// <remarks>
    /// In this naive implementation only choose first pair.
    /// </remarks>
    public override void Select()
    {
        _selectedPairs.Clear();
        _selectedPairs.Add(_pairs[0]);
    }
}
