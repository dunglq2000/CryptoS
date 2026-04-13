namespace Algebra;

public class BuchbergerAlgorithm: GroebnerAlgorithm
{
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        List<Polynomial> results = polynomials;
        var k = results.Count;
        List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
        for (var i = 0; i < k; ++i)
        {
            for (var j = i + 1; j < k; ++j)
            {
                pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (pairs.Count > 0)
        {
            var selectedPairs = Select(pairs);
            pairs = pairs.Except(selectedPairs).ToList();
            foreach (var pair in selectedPairs)
            {
                var S = results[pair.Item1].SPoly(results[pair.Item2]);
                var (r, q) = S.Divide(results);
                if (r.IsZero() == false)
                {
                    
                    results.Add(r);
                    k += 1;
                    for (var i = 0; i < k - 1; ++i)
                    {
                        pairs.Add(new Tuple<int, int>(i, k - 1));
                    }
                }
            }
        }
        return results;
    }
    public List<Tuple<int, int>> Select(List<Tuple<int, int>> pairs)
    {
        return [pairs[0]];
    }
}
