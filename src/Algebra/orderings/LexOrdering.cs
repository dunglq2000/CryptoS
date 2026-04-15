namespace Algebra;

public class LexOrdering : MonomialOrdering
{
    public int Compare(Monomial? left, Monomial? right)
    {
        if (left is null || right is null)
        {
            throw new ArgumentException("Monomial cannot be null");
        }
        if (left.BitCount != right.BitCount)
        {
            throw new ArgumentException("Dimension of two monomials must be the same");
        }
        for (var i = 0; i < left.Bits.Length; ++i)
        {
            if (left.Bits[i] != right.Bits[i])
            {
                if (left.Bits[i] > right.Bits[i])
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        return 0;
    }
}
