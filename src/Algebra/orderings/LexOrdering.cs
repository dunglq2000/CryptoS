namespace Algebra;

public class LexOrdering : MonomialOrdering
{
    public int Compare(Monomial? left, Monomial? right)
    {
        if (left is null || right is null)
        {
            throw new ArgumentException("Monomial cannot be null");
        }
        if (left.nbits != right.nbits)
        {
            throw new ArgumentException("Dimension of two monomials must be the same");
        }
        for (var i = 0; i < left.bits.Length; ++i)
        {
            if (left.bits[i] != right.bits[i])
            {
                if (left.bits[i] > right.bits[i])
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
