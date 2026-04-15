namespace Algebra;

public class F4Algorithm : GroebnerAlgorithm
{
    public override List<Polynomial> Compute(List<Polynomial> polynomials)
    {
        List<Polynomial> G = polynomials;
        int k = G.Count;
        List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
        for (int i = 0; i < k; ++i)
        {
            for (int j = i + 1; j < k; ++j)
            {
                pairs.Add(new Tuple<int, int>(i, j));
            }
        }
        while (pairs.Count > 0)
        {
            List<Tuple<int, int>> selectedPairs = Select(pairs, G);
            pairs = pairs.Except(selectedPairs).ToList();
            List<Polynomial> G_ = Reduction(selectedPairs, G);
            foreach (var h in G_)
            {
                G.Add(h);
                k += 1;
                for (int i = 0; i < k - 1; ++i)
                {
                    pairs.Add(new Tuple<int, int>(i, k - 1));
                }
            }
        }
        return G;
    }
    public override List<Tuple<int, int>> Select(List<Tuple<int, int>> pairs, List<Polynomial> polynomials)
    {
        int sz = pairs.Count;
        List<int> lcmDegrees = new List<int>(sz);
        int minDegree = int.MaxValue;
        for (int i = 0; i < sz; ++i)
        {
            var (u, v) = pairs[i];
            int degree = (polynomials[u].GetLeadingTerm() * polynomials[v].GetLeadingTerm()).Degree;
            minDegree = (minDegree < degree) ? minDegree : degree;
            lcmDegrees.Add(degree);
        }
        List<Tuple<int, int>> selectedPairs = new List<Tuple<int, int>>();
        for (int i = 0; i < sz; ++i)
        {
            if (lcmDegrees[i] == minDegree)
            {
                selectedPairs.Add(pairs[i]);
            }
        }
        return selectedPairs;
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
        HashSet<Monomial> done = L.Select(polynomial => polynomial.GetLeadingTerm()).ToHashSet();

        while (true)
        {
            HashSet<Monomial> MonL = new HashSet<Monomial>();
            foreach (var polynomial in L)
            {
                foreach (var monomial in polynomial.monomials)
                    MonL.Add(monomial);
            }

            List<Monomial> diff = MonL.Except(done).OrderBy(monomial => monomial, monomialOrdering).ToList();
            if (diff.Count == 0)
            {
                break;
            }
            Monomial m = diff[diff.Count - 1];
            done.Add(m);
            foreach (var g in polynomials)
            {
                if (m.IsDivisibleBy(g.GetLeadingTerm()))
                {
                    var f = g; f.Multiply(m / f.GetLeadingTerm());
                    L.Add(f);
                    break;
                }
            }
        }
        return L.ToList();
    }
}
