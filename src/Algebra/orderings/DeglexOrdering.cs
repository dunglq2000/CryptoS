namespace Algebra;

public class DeglexOrdering : MonomialOrdering
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
        uint deg_left = 0, deg_right = 0;
        for (var i = 0; i < left.Bits.Length; ++i)
        {
            for (var j = 0; j < 32; j++)
            {
                uint bit_left = (left.Bits[i] >> (31 - j)) & 1;
                uint bit_right = (right.Bits[i] >> (31 - j)) & 1;
                deg_left += bit_left; deg_right += bit_right;
            }
        }
        if (deg_left != deg_right)
        {
            if (deg_left > deg_right)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
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
}
