namespace Algebra;

public class F4AlgorithmHashSet : GroebnerAlgorithm
{
    private HashSet<Tuple<int, int>> _pairs = new HashSet<Tuple<int, int>>();
    private HashSet<Tuple<int, int>> _selectedPairs = new HashSet<Tuple<int, int>>();
    private List<Polynomial> _polynomials = new List<Polynomial>();
    private MonomialOrdering? _monomialOrdering = null;
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        _monomialOrdering = polynomials[0].Order;
        _polynomials.AddRange(polynomials);
        int k = _polynomials.Count;
        for (int i = 0; i < k; ++i)
        {
            for (int j = i + 1; j < k; ++j)
            {
                _pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (_pairs.Count > 0)
        {
            Select();
            _pairs.ExceptWith(_selectedPairs);
            List<Polynomial> G_ = Reduction();
            foreach (var h in G_)
            {
                _polynomials.Add(h);
                k += 1;
                for (int i = 0; i < k - 1; ++i)
                {
                    _pairs.Add(new Tuple<int, int>(i, k - 1));
                }
            }
        }
        return _polynomials;
    }
    public override void Select()
    {
        _selectedPairs.Clear();
        Dictionary<Tuple<int, int>, int> lcmDegrees = new Dictionary<Tuple<int, int>, int>();
        int minDegree = int.MaxValue;
        foreach (var pair in _pairs)
        {
            var (u, v) = pair;
            int degree = (_polynomials[u].GetLeadingTerm() * _polynomials[v].GetLeadingTerm()).Degree;
            minDegree = (minDegree < degree) ? minDegree : degree;
            lcmDegrees.Add(pair, degree);
        }
        foreach (var degree in lcmDegrees)
        {
            if (degree.Value == minDegree)
            {
                _selectedPairs.Add(degree.Key);
            }
        }
    }
    private List<Polynomial> Reduction()
    {
        HashSet<Polynomial> L = SymbolicPreprocessing();
        HashSet<Monomial> leadingMonomials = _polynomials.Select(polynomial => polynomial.GetLeadingTerm()).ToHashSet();
        MacaulayMatrix macaulayMatrix = new MacaulayMatrix(L.ToList());
        macaulayMatrix.Reduce();
        List<Polynomial> L_ = macaulayMatrix.GetPolynomials();
        List<Polynomial> G_ = new List<Polynomial>();
        foreach (var f in L_)
        {
            if (f.IsZero()) break;
            bool flag = true;
            foreach (var g in leadingMonomials)
            {
                if (f.GetLeadingTerm().Equals(g))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                G_.Add(f);
            }
        }
        return G_;
    }
    private HashSet<Polynomial> SymbolicPreprocessing()
    {
        HashSet<Polynomial> L = new HashSet<Polynomial>();
        foreach (var (i, j) in _selectedPairs)
        {
            Monomial lcm = _polynomials[i].GetLeadingTerm() * _polynomials[j].GetLeadingTerm();
            Polynomial left  = _polynomials[i] * (lcm / _polynomials[i].GetLeadingTerm());
            Polynomial right = _polynomials[j] * (lcm / _polynomials[j].GetLeadingTerm());
            L.Add(left);
            L.Add(right);
        }
        SortedSet<Monomial> done = new SortedSet<Monomial>(
            L.Select(polynomial => polynomial.GetLeadingTerm()), 
            _monomialOrdering
        );
        SortedSet<Monomial> MonL = new SortedSet<Monomial>(_monomialOrdering);
        foreach (var polynomial in L)
        {
            MonL.UnionWith(polynomial.Monomials);
        }
        while (true)
        {
            List<Monomial> diff = MonL.Except(done).OrderBy(monomial => monomial, _monomialOrdering).ToList();
            if (diff.Count == 0)
            {
                break;
            }
            Monomial m = diff[diff.Count - 1];
            done.Add(m);
            foreach (var g in _polynomials)
            {
                Monomial leadingTermG = g.GetLeadingTerm();
                if (m.IsDivisibleBy(leadingTermG))
                {
                    var f = g;
                    f.Multiply(m / leadingTermG);
                    L.Add(f);
                    MonL.UnionWith(f.Monomials);
                    break;
                }
            }
        }
        return L;
    }
}
