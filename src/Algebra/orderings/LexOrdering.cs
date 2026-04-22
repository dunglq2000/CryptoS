namespace Algebra.Boolean;

/// <summary>
/// Lexicographic ord
/// </summary>
public class LexOrdering : MonomialOrdering
{
    /// <summary>
    /// Compare two monomials.
    /// </summary>
    /// <param name="left">First monomial.</param>
    /// <param name="right">Second monomial.</param>
    /// <returns>Using lexicographic order, return -1 if <paramref name="left"/> is less than <paramref name="right"/>, 0 if <paramref name="left"/> is equal to <paramref name="right"/>, 1 if <paramref name="left"/> is greater than <paramref name="right"/>.</returns>
    /// <exception cref="ArgumentException">Two monomials must be in the same ring, i.e have the same number of variables.</exception>
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
