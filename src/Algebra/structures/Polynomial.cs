using System.Text;

namespace CryptoS.Algebra.Boolean;

/// <summary>
/// Class for polynomial over Boolean polynomial ring.
/// </summary>
public class Polynomial
{
    /// <summary>
    /// List of monomials in polynomial.
    /// </summary>
    public List<Monomial> Monomials { get; set; }
    /// <summary>
    /// Monomial ordering in polynomial.
    /// </summary>
    public readonly MonomialOrdering Order;
    /// <summary>
    /// Constructor of zero polynomial with monomial ordering.
    /// </summary>
    /// <param name="monomialOrdering">Monomial ordering.</param>
    public Polynomial(MonomialOrdering monomialOrdering)
    {
        Order = monomialOrdering;
        Monomials = new List<Monomial>();
    }
    /// <summary>
    /// Constructor for polynomial with monomial ordering.
    /// </summary>
    /// <param name="monomialOrdering">Monomial ordering.</param>
    /// <param name="monomials">List of monomials.</param>
    public Polynomial(MonomialOrdering monomialOrdering, List<Monomial> monomials)
    {
        Order = monomialOrdering;
        Monomials = new List<Monomial>();
        foreach(var monomial in monomials)
        {
            Add(monomial);
        }
    }
    /// <summary>
    /// Constructor for parsing polynomial from string.
    /// </summary>
    /// <param name="bitCount">Number of variables.</param>
    /// <param name="polynomialString">String to be parse.</param>
    /// <param name="monomialOrdering">Monomial ordering.</param>
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
    /// <returns>New polynomial which is the result of <paramref name="polynomial"/>+<paramref name="monomial"/>.</returns>
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
    /// <summary>
    /// Operator for adding two polynomials.
    /// </summary>
    /// <param name="left">Polynomial before +.</param>
    /// <param name="right">Polynomial after +.</param>
    /// <returns>New polynomial which is the result of <paramref name="left"/>+<paramref name="right"/>.</returns>
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
    /// <summary>
    /// Add a polynomial to this polynomial.
    /// </summary>
    /// <param name="polynomial">Other polynomial.</param>
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
    /// <summary>
    /// Calculate $S$-poly of two polynomials.
    /// </summary>
    /// <param name="other">Other polynomial.</param>
    /// <returns>$S$-poly of this and <paramref name="other"/> polynomials.</returns>
    public Polynomial SPoly(Polynomial other)
    {
        Monomial leadThis = GetLeadingTerm();
        Monomial leadOther = other.GetLeadingTerm();
        Monomial lcm = leadThis * leadOther;
        Polynomial result = this * (lcm / leadThis) + other * (lcm / leadOther);
        return result;
    }
    /// <summary>
    /// Calculate $S$-poly of two polynomials.
    /// </summary>
    /// <param name="left">First polynomial.</param>
    /// <param name="right">Second polynomial.</param>
    /// <returns>$S$-poly of polynomials <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Polynomial SPoly(Polynomial left, Polynomial right)
    {
        Monomial leadLeft = left.GetLeadingTerm();
        Monomial leadRight = right.GetLeadingTerm();
        Monomial lcm = leadLeft * leadRight;
        Polynomial result = left * (lcm / leadLeft) + right * (lcm / leadRight);
        return result;
    }
    /// <summary>
    /// Divide this polynomial with a list of polynomials.
    /// </summary>
    /// <param name="polynomials">List of polynomials.</param>
    /// <returns>Remainder and quotients.</returns>
    /// <exception cref="ArgumentException">List of polynomials must be not empty.</exception>
    /// <remarks>
    /// If this polynomial is $f$ and <paramref name="polynomials"/> is a list of polynomials $g_1$, $g_2$, ..., $g_t$,
    /// this function returns a polynomial $r$ and a list of polynomials $q_1$, $q_2$, ..., $q_t$ such that
    /// $f = r + g_1 q_1 + g_2 q_2 + \cdots + g_t q_t$, where $r$ is the remainder.
    /// </remarks>
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
    /// <summary>
    /// Get leading term of polynomial according to monomial ordering <see cref="Order"/>.
    /// </summary>
    /// <returns>Leading term of polynomial.</returns>
    /// <exception cref="ArgumentException">Polynomial must not be zero.</exception>
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
    /// <summary>
    /// Verify equality of two polynomials.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Verify equality of two polynomials.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Get hash value for polynomial.
    /// </summary>
    /// <returns>Hash value for polynomial.</returns>
    /// <remarks>
    /// If this polynomial is $f = m_0 + m_1 + \cdots + m_t$, where $m_i$ are monomials 
    /// sorted by monomial ordering <see cref="Order"/> ascending, let the hash value 
    /// of monomial $m_i$ is $H(m_i)$, then the hash value of polynomial is
    /// 
    /// $$H(f) = H(m_0) + 2 \cdot H(m_1) + 2^2 H(m_2) + \cdots + 2^t H(m_t) \bmod{1069482893},$$
    /// 
    /// with $1069482893$ is 30-bit prime.
    /// </remarks>
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
    /// <summary>
    /// Output polynomial as vectors of its monomials.
    /// </summary>
    /// <returns></returns>
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
