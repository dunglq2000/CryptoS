namespace Algebra;

public class F4AlgorithmList : GroebnerAlgorithm
{
    private List<Tuple<int, int>> _pairs = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> _selectedPairs = new List<Tuple<int, int>>();
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
            _pairs = _pairs.Except(_selectedPairs).ToList();
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
        int sz = _pairs.Count;
        List<int> lcmDegrees = new List<int>(sz);
        int minDegree = int.MaxValue;
        for (int i = 0; i < sz; ++i)
        {
            var (u, v) = _pairs[i];
            int degree = (_polynomials[u].GetLeadingTerm() * _polynomials[v].GetLeadingTerm()).Degree;
            minDegree = (minDegree < degree) ? minDegree : degree;
            lcmDegrees.Add(degree);
        }
        for (int i = 0; i < sz; ++i)
        {
            if (lcmDegrees[i] == minDegree)
            {
                _selectedPairs.Add(_pairs[i]);
            }
        }
    }
    private List<Polynomial> Reduction()
    {
        HashSet<Polynomial> L = SymbolicPreprocessing();
        List<Monomial> leadingMonomials = _polynomials.Select(polynomial => polynomial.GetLeadingTerm()).ToList();
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
            L.Add(_polynomials[i] * (lcm / _polynomials[i].GetLeadingTerm()));
            L.Add(_polynomials[j] * (lcm / _polynomials[j].GetLeadingTerm()));
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
