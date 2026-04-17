namespace Algebra;

public class BuchbergerAlgorithm: GroebnerAlgorithm
{
    private List<Tuple<int, int>> _pairs;
    private List<Tuple<int, int>> _selectedPairs;
    public BuchbergerAlgorithm()
    {
        _pairs = new List<Tuple<int, int>>();
        _selectedPairs = new List<Tuple<int, int>>();
    }
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        List<Polynomial> results = polynomials;
        var k = results.Count;
        for (var i = 0; i < k; ++i)
        {
            for (var j = i + 1; j < k; ++j)
            {
                _pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (_pairs.Count > 0)
        {
            Select(results);
            _pairs = _pairs.Except(_selectedPairs).ToList();
            foreach (var pair in _selectedPairs)
            {
                var S = results[pair.Item1].SPoly(results[pair.Item2]);
                var (r, q) = S.Divide(results);
                if (r.IsZero() == false)
                {
                    
                    results.Add(r);
                    k += 1;
                    for (var i = 0; i < k - 1; ++i)
                    {
                        _pairs.Add(new Tuple<int, int>(i, k - 1));
                    }
                }
            }
        }
        return results;
    }
    public override void Select(List<Polynomial> polynomials)
    {
        _selectedPairs.Clear();
        _selectedPairs.Add(_pairs[0]);
    }
}
