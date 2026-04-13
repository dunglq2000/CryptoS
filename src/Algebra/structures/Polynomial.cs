using System.Text;

namespace Algebra;

public class Polynomial
{
    public List<Monomial> monomials { get; set; }
    public readonly MonomialOrdering monomialOrdering;
    public Polynomial(MonomialOrdering monomialOrdering)
    {
        this.monomialOrdering = monomialOrdering;
        monomials = new List<Monomial>();
    }
    public Polynomial(MonomialOrdering monomialOrdering, List<Monomial> monomials)
    {
        this.monomialOrdering = monomialOrdering;
        this.monomials = new List<Monomial>();
        foreach(var monomial in monomials)
        {
            Add(monomial);
        }
    }
    public bool IsZero()
    {
        return monomials.Count == 0;
    }
    public static Polynomial operator+(Polynomial polynomial, Monomial monomial)
    {
        MonomialOrdering monomialOrdering = polynomial.monomialOrdering;
        List<Monomial> monomials = polynomial.monomials;
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
    public static Polynomial operator*(Polynomial polynomial, Monomial monomial)
    {
        MonomialOrdering monomialOrdering = polynomial.monomialOrdering;
        List<Monomial> monomials = polynomial.monomials.Select(mono => mono * monomial).ToList();
        return new Polynomial(monomialOrdering, monomials);
    }
    public void Add(Monomial monomial)
    {
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
    }
    public void Multiply(Monomial monomial)
    {
        List<Monomial> newMonmials = monomials.Select(mono => mono * monomial).ToList();
        monomials.Clear();
        foreach (var mono in newMonmials)
        {
            Add(mono);
        }
    }
    public bool Equals(Polynomial other)
    {
        if (monomials.Count != other.monomials.Count)
        {
            return false;
        }
        for (var i = 0; i < monomials.Count; ++i)
        {
            if (monomials[i].Equals(other.monomials[i]) == false)
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
        if (monomials.Count != other.monomials.Count)
        {
            return false;
        }
        for (var i = 0; i < monomials.Count; ++i)
        {
            if (monomials[i].Equals(other.monomials[i]) == false)
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
        for (var i = 0; i < monomials.Count; ++i)
        {
            result = (result * 2 + monomials[i].GetHashCode()) % modulus;
        }
        return result;
    }
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        List<string> strings = new List<string>();
        for (var i = monomials.Count - 1; i >= 0 ; --i)
        {
            strings.Add($"({monomials[i]})");
        }
        result.AppendJoin(" + ", strings);
        return result.ToString();
    }
}
