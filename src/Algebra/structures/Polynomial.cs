using System.Linq.Expressions;
using System.Text;

namespace Algebra;

/// <summary>
/// Class for polynomial over Boolean polynomial ring.
/// </summary>
public class Polynomial
{
    public List<Monomial> Monomials { get; set; }
    public readonly MonomialOrdering Order;
    public Polynomial(MonomialOrdering monomialOrdering)
    {
        Order = monomialOrdering;
        Monomials = new List<Monomial>();
    }
    public Polynomial(MonomialOrdering monomialOrdering, List<Monomial> monomials)
    {
        Order = monomialOrdering;
        Monomials = new List<Monomial>();
        foreach(var monomial in monomials)
        {
            Add(monomial);
        }
    }
    public Polynomial(int bitCount, string polynomialString, MonomialOrdering monomialOrdering)
    {
        Order = monomialOrdering;
        string[] monomialStrings = polynomialString.Replace(" ", "").Split("+");
        Monomials = new List<Monomial>();
        foreach (var monomialString in monomialStrings)
        {
            Add(new Monomial(bitCount, monomialString));
        }
    }
    /// <summary>
    /// If a polynomial does not have any monomial, it is zero polynomial.
    /// </summary>
    /// <returns>true if polynomial is zero, false otherwise.</returns>
    public bool IsZero()
    {
        return Monomials.Count == 0;
    }
    /// <summary>
    /// Operator for adding polynomial and monomial
    /// </summary>
    /// <param name="polynomial">Polynomial on the left-hand side of + symbol</param>
    /// <param name="monomial">Monomial on the right-hand side of + symbol</param>
    /// <returns>New polynomial which is the result of <c>polynomial + monomial</c></returns>
    public static Polynomial operator+(Polynomial polynomial, Monomial monomial)
    {
        MonomialOrdering monomialOrdering = polynomial.Order;
        List<Monomial> monomials = polynomial.Monomials;
        var index = monomials.BinarySearch(monomial, monomialOrdering);
        if (index < 0) 
        {
            index = ~index;
            monomials.Insert(index, monomial);
        }
        else
        {
            monomials.Remove(monomial);
        }
        return new Polynomial(monomialOrdering, monomials);
    }
    public static Polynomial operator+(Polynomial left, Polynomial right)
    {
        List<Monomial> monomials = left.Monomials.Concat(right.Monomials).ToList();
        return new Polynomial(left.Order, monomials);
    }
    /// <summary>
    /// Operator for multiplying polynomial and monomial
    /// </summary>
    /// <param name="polynomial">Polynomial on the left-hand side of * symbol</param>
    /// <param name="monomial">Monomial on the right-hand side of * symbol</param>
    /// <returns>New polynomial which is the result of <c>polynomial * monomial</c></returns>
    public static Polynomial operator*(Polynomial polynomial, Monomial monomial)
    {
        MonomialOrdering monomialOrdering = polynomial.Order;
        List<Monomial> monomials = polynomial.Monomials.Select(mono => mono * monomial).ToList();
        return new Polynomial(monomialOrdering, monomials);
    }
    /// <summary>
    /// Function for adding monomial to this polynomial
    /// </summary>
    /// <param name="monomial">Monomial to be added</param>
    public void Add(Monomial monomial)
    {
        var index = Monomials.BinarySearch(monomial, Order);
        if (index < 0) 
        {
            index = ~index;
            Monomials.Insert(index, monomial);
        }
        else
        {
            Monomials.Remove(monomial);
        }
    }
    public void Add(Polynomial polynomial)
    {
        foreach (var monomial in polynomial.Monomials)
        {
            Add(monomial);
        }
    }
    /// <summary>
    /// Function for multiplying monomial to this polynomial
    /// </summary>
    /// <param name="monomial">Monomial to be multiplied</param>
    public void Multiply(Monomial monomial)
    {
        List<Monomial> newMonmials = Monomials.Select(mono => mono * monomial).ToList();
        Monomials.Clear();
        foreach (var mono in newMonmials)
        {
            Add(mono);
        }
    }
    public Polynomial SPoly(Polynomial other)
    {
        Monomial leadThis = GetLeadingTerm();
        Monomial leadOther = other.GetLeadingTerm();
        Monomial lcm = leadThis * leadOther;
        Polynomial result = this * (lcm / leadThis) + other * (lcm / leadOther);
        return result;
    }
    public static Polynomial SPoly(Polynomial left, Polynomial right)
    {
        Monomial leadLeft = left.GetLeadingTerm();
        Monomial leadRight = right.GetLeadingTerm();
        Monomial lcm = leadLeft * leadRight;
        Polynomial result = left * (lcm / leadLeft) + right * (lcm / leadRight);
        return result;
    }
    public Tuple<Polynomial, List<Polynomial>> Divide(List<Polynomial> polynomials)
    {
        if (polynomials.Count < 1)
        {
            throw new ArgumentException("Can not divide one polynomial for zero polynomials!");
        }
        MonomialOrdering monomialOrdering = polynomials[0].Order;
        Polynomial r = new Polynomial(monomialOrdering);
        List<Polynomial> q = new List<Polynomial>();
        for (var j = 0; j < polynomials.Count; ++j)
        {
            q.Add(new Polynomial(monomialOrdering));
        }
        Polynomial p = this;
        int i = 0;
        while (p.IsZero() == false)
        {
            if (p.GetLeadingTerm().IsDivisibleBy(polynomials[i].GetLeadingTerm()))
            {
                Monomial termToAdd = p.GetLeadingTerm() / polynomials[i].GetLeadingTerm();
                q[i].Add(termToAdd);
                p.Add(polynomials[i] * termToAdd);
                i = 0;
            }
            i += 1;
            if (i == polynomials.Count)
            {
                r.Add(p.GetLeadingTerm());
                p.Add(p.GetLeadingTerm());
                i = 0;
            }
        }
        return new Tuple<Polynomial, List<Polynomial>>(r, q);
    }
    public Monomial GetLeadingTerm()
    {
        if (Monomials.Count == 0)
        {
            throw new ArgumentException("Zero polynomial does not have leading term.");
        }
        else
        {
            return Monomials[Monomials.Count - 1]; // get last elements
        }
    }
    public bool Equals(Polynomial other)
    {
        if (Monomials.Count != other.Monomials.Count)
        {
            return false;
        }
        for (var i = 0; i < Monomials.Count; ++i)
        {
            if (Monomials[i].Equals(other.Monomials[i]) == false)
            {
                return false;
            }
        }
        return true;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        Polynomial other = (Polynomial)obj;
        if (Monomials.Count != other.Monomials.Count)
        {
            return false;
        }
        for (var i = 0; i < Monomials.Count; ++i)
        {
            if (Monomials[i].Equals(other.Monomials[i]) == false)
            {
                return false;
            }
        }
        return true;
    }
    public override int GetHashCode()
    {
        int modulus = 1069482893;
        int result = 0;
        for (var i = 0; i < Monomials.Count; ++i)
        {
            result = (result * 2 + Monomials[i].GetHashCode()) % modulus;
        }
        return result;
    }
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        List<string> strings = new List<string>();
        for (var i = Monomials.Count - 1; i >= 0 ; --i)
        {
            strings.Add($"({Monomials[i]})");
        }
        result.AppendJoin(" + ", strings);
        return result.ToString();
    }
}
