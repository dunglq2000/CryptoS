namespace Algebra;

public class F4AlgorithmList : GroebnerAlgorithm
{
    private List<Tuple<int, int>> _pairs = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> _selectedPairs = new List<Tuple<int, int>>();
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        List<Polynomial> G = polynomials;
        int k = G.Count;
        for (int i = 0; i < k; ++i)
        {
            for (int j = i + 1; j < k; ++j)
            {
                _pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (_pairs.Count > 0)
        {
            Select(G);
            _pairs = _pairs.Except(_selectedPairs).ToList();
            List<Polynomial> G_ = Reduction(_selectedPairs, G);
            foreach (var h in G_)
            {
                G.Add(h);
                k += 1;
                for (int i = 0; i < k - 1; ++i)
                {
                    _pairs.Add(new Tuple<int, int>(i, k - 1));
                }
            }
        }
        return G;
    }
    public override void Select(List<Polynomial> polynomials)
    {
        _selectedPairs.Clear();
        int sz = _pairs.Count;
        List<int> lcmDegrees = new List<int>(sz);
        int minDegree = int.MaxValue;
        for (int i = 0; i < sz; ++i)
        {
            var (u, v) = _pairs[i];
            int degree = (polynomials[u].GetLeadingTerm() * polynomials[v].GetLeadingTerm()).Degree;
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
    private List<Polynomial> Reduction(List<Tuple<int, int>> selectedPairs, List<Polynomial> polynomials)
    {
        List<Polynomial> L = SymbolicPreprocessing(selectedPairs, polynomials);
        List<Monomial> leadingMonomials = polynomials.Select(polynomial => polynomial.GetLeadingTerm()).ToList();
        MacaulayMatrix macaulayMatrix = new MacaulayMatrix(L);
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
    private List<Polynomial> SymbolicPreprocessing(List<Tuple<int, int>> selectedPairs, List<Polynomial> polynomials)
    {
        MonomialOrdering monomialOrdering = polynomials[0].monomialOrdering;
        HashSet<Polynomial> L = new HashSet<Polynomial>();
        foreach (var (i, j) in selectedPairs)
        {
            Monomial lcm = polynomials[i].GetLeadingTerm() * polynomials[j].GetLeadingTerm();
            Polynomial left  = polynomials[i] * (lcm / polynomials[i].GetLeadingTerm());
            Polynomial right = polynomials[j] * (lcm / polynomials[j].GetLeadingTerm());
            L.Add(left);
            L.Add(right);
        }
        SortedSet<Monomial> done = new SortedSet<Monomial>(
            L.Select(polynomial => polynomial.GetLeadingTerm()), 
            monomialOrdering
        );
        SortedSet<Monomial> MonL = new SortedSet<Monomial>(monomialOrdering);
        foreach (var polynomial in L)
        {
            MonL.UnionWith(polynomial.monomials);
        }
        while (true)
        {
            List<Monomial> diff = MonL.Except(done).OrderBy(monomial => monomial, monomialOrdering).ToList();
            if (diff.Count == 0)
            {
                break;
            }
            Monomial m = diff[diff.Count - 1];
            done.Add(m);
            foreach (var g in polynomials)
            {
                Monomial leadingTermG = g.GetLeadingTerm();
                if (m.IsDivisibleBy(leadingTermG))
                {
                    var f = g;
                    f.Multiply(m / leadingTermG);
                    L.Add(f);
                    MonL.UnionWith(f.monomials);
                    break;
                }
            }
        }
        // Console.WriteLine($"\t\tNumber of monomials: {MonL.Count}");
        return L.ToList();
    }
}
