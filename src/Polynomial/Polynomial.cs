namespace Polynomials;

public class Polynomial
{
    public List<Monomial> monomials;
    public readonly MonomialOrdering monomialOrdering;
    public Polynomial(MonomialOrdering monomialOrdering)
    {
        this.monomialOrdering = monomialOrdering;
        monomials = new List<Monomial>();
    }
    public Polynomial(MonomialOrdering monomialOrdering, List<Monomial> monomials)
    {
        this.monomialOrdering = monomialOrdering;
        this.monomials = monomials.Distinct().OrderByDescending(monomial => monomial, monomialOrdering).ToList();
    }
}