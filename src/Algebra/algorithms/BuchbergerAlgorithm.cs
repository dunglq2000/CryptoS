namespace Algebra;

public class BuchbergerAlgorithm: GroebnerAlgorithm
{
    private List<Tuple<int, int>> _pairs = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> _selectedPairs = new List<Tuple<int, int>>();
    private List<Polynomial> _polynomials = new List<Polynomial>();
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
    public override void Select()
    {
        _selectedPairs.Clear();
        _selectedPairs.Add(_pairs[0]);
    }
}
