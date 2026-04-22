using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Algebra.Boolean;

/// <summary>
/// Monomial over Boolean polynomial ring, i.e. $\mathbb{F}_2[x_1, x_2, \ldots, x_n] / \langle x_1^2 + x_1, x_2^2 + x_2, \ldots, x_n^2 + x_n \rangle$.
/// </summary>
public class Monomial
{
    /// <summary>
    /// Number of variables in monomial.
    /// </summary>
    public readonly int BitCount;
    /// <summary>
    /// Power of variables in monomial, which is $0$ or $1$.
    /// </summary>
    public uint[] Bits;
    /// <summary>
    /// <para>Use fast way to count the number of bits in <c>uint</c></para>
    /// <para>Source: <see href="https://stackoverflow.com/a/12175897"/></para>
    /// </summary>
    public int Degree {
        get
        {
            int result = 0;
            foreach (var chunk in Bits)
            {
                result += BitOperations.PopCount(chunk);
            }
            return result;
        }
    }
    /// <summary>
    /// Constructor for monomial $x_1^0 x_2^0 \cdots x_n^0$ on <paramref name="bitCount"/> variables.
    /// </summary>
    /// <param name="bitCount">The number of variables.</param>
    public Monomial(int bitCount)
    {
        BitCount = bitCount;
        Bits = new uint[(bitCount + 31) >> 5];
    }
    /// <summary>
    /// Constructor for monomial $x_1^{a_1} x_2^{a_2} \cdots x_n^{a_2}$ on <paramref name="bitCount"/> variables with variables' power in <paramref name="bits"/>.
    /// </summary>
    /// <param name="bitCount">The number of variables.</param>
    /// <param name="bits">Variables' power.</param>
    public Monomial(int bitCount, uint[] bits)
    {
        BitCount = bitCount;
        Bits = new uint[(bitCount + 31) >> 5];
        Array.Copy(bits, Bits, (bitCount + 31) >> 5);
    }
    /// <summary>
    /// Constructor for monomial from string.
    /// </summary>
    /// <param name="bitCount"></param>
    /// <param name="str"></param>
    public Monomial(int bitCount, string str)
    {
        BitCount = bitCount;
        Bits = new uint[(bitCount + 31) >> 5];
        string[] variableStrings = str.Split("*");
        foreach (var variableString in variableStrings)
        {
            Match match = Regex.Match(variableString, @"\d+");
            var index = int.Parse(match.Value);
            var currentDWORD = index >> 5;
            var indexInDWORD = index & 0x1f;
            Bits[currentDWORD] |= 1U << (31 - indexInDWORD);
        }
    }
    /// <summary>
    /// Copy constructor.
    /// </summary>
    /// <param name="monomial"></param>
    public Monomial(Monomial monomial)
    {
        BitCount = monomial.BitCount;
        Bits = new uint[(BitCount + 31) >> 5];
        Array.Copy(Bits, monomial.Bits, (BitCount + 31) >> 5);
    }
    /// <summary>
    /// Operator for multiplying two monomials.
    /// </summary>
    /// <param name="left">Monomial before operator *.</param>
    /// <param name="right">Monomial after operator *.</param>
    /// <returns>New monomial, which is multiplication of two monomials.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static Monomial operator*(Monomial left, Monomial right)
    {
        if (left.BitCount != right.BitCount)
        {
            throw new ArgumentException("The number of bits of monomials must the the same!");
        }
        uint[] result = new uint[(left.BitCount + 31) >> 5];
        for (int i = 0; i < result.Length; ++i)
            result[i] = left.Bits[i] | right.Bits[i];
        return new Monomial(left.BitCount, result);
    }
    /// <summary>
    /// Operator for dividing two monomials.
    /// </summary>
    /// <param name="left">Monomial before operator /.</param>
    /// <param name="right">Monomial after operator /.</param>
    /// <returns>New monomial, which is division <paramref name="left"/>/<paramref name="right"/>.</returns>
    public static Monomial operator/(Monomial left, Monomial right)
    {
        if (left.IsDivisibleBy(right) == false)
        {
            throw new ArgumentException("First monomial is not divisible by the second!");
        }
        if (left.BitCount != right.BitCount)
        {
            throw new Exception("The number of bits of monomials must be the same!");
        }
        uint[] result = new uint[(left.BitCount + 31) >> 5];
        for (var i = 0; i < result.Length; ++i)
        {
            result[i] = left.Bits[i] ^ right.Bits[i];
        }
        return new Monomial(left.BitCount, result);
    }
    /// <summary>
    /// Multiply with other monomial into this, i.e. this = this * other.
    /// </summary>
    /// <param name="other">Other monomial.</param>
    /// <exception cref="ArgumentException"></exception>
    public void Multiply(Monomial other)
    {
        if (BitCount != other.BitCount)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        for (var i = 0; i < Bits.Length; ++i)
        {
            Bits[i] |= other.Bits[i];
        }
    }
    /// <summary>
    /// Divide with other monomial into this, i.e. this = this / other.
    /// </summary>
    /// <param name="other">Other monomial.</param>
    /// <exception cref="ArgumentException">This monomial must be divisible by <paramref name="other"/>, which is verified by <see cref="IsDivisibleBy"/>.</exception>
    public void Divide(Monomial other)
    {
        if (BitCount != other.BitCount)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        if (IsDivisibleBy(other) == false)
        {
            throw new ArgumentException("First monomial is not divisible by the second!");
        }
        for (var i = 0; i < Bits.Length; ++i)
        {
            Bits[i] ^= other.Bits[i];
        }
    }
    /// <summary>
    /// Verify if this monomial is divisible by other monomial.
    /// </summary>
    /// <param name="other">Other monomial.</param>
    /// <returns>True if this monomial is divisible by other monomial, false otherwise.</returns>
    /// <exception cref="ArgumentException"></exception>
    public bool IsDivisibleBy(Monomial other)
    {
        if (BitCount != other.BitCount)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        for (var i = 0; i < Bits.Length; ++i)
        {
            uint tmp = Bits[i] | (~other.Bits[i]);
            if (tmp != 0xffffffff)
            {
                return false;
            }
        }
        return true;
    }
    // public bool Equals(Monomial other)
    // {
    //     if (BitCount != other.BitCount)
    //     {
    //         return false;
    //     }
    //     for (var i = 0; i < Bits.Length; ++i)
    //     {
    //         if (Bits[i] != other.Bits[i])
    //         {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
    /// <summary>
    /// Verify equality with other monomial.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        Monomial other = (Monomial)obj;
        if (BitCount != other.BitCount)
        {
            return false;
        }
        for (var i = 0; i < Bits.Length; ++i)
        {
            if (Bits[i] != other.Bits[i])
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Hash value of monomial.
    /// </summary>
    /// <remarks>
    /// For monomial $x_1^{a_1} x_2^{a_2} \cdots x_n^{a_n}$, the hash value will be $a_1 + 2 a_2 + \cdots + 2^{n-1} a_n \bmod{1073741789}$ with $1073741789$ is a 30-bit prime number.
    /// </remarks>
    /// <returns>Hash value of monomial.</returns>
    public override int GetHashCode()
    {
        uint result = 0;
        for (var i = 0; i < Bits.Length; ++i)
        {
            for (var j = 0; j < 32; j++)
            {
                uint bit = (Bits[i] >> (31 - j)) & 1;
                result = (result * 2 + bit) % 1073741789;
            }
        }
        return (int)result;
    }
    /// <summary>
    /// Output monomial by variables' power as vector.
    /// </summary>
    /// <returns>Variables' power as vector</returns>
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        List<string> bits_str = new List<string>();
        for (var i = 0; i < Bits.Length; ++i)
        {
            for (var j = 0; j < 32; ++j)
            {
                if (32 * i + j >= BitCount) break;
                var bit = (Bits[i] >> (31 - j)) & 1;
                bits_str.Add($"{bit}");
            }
        }
        result.AppendJoin(", ", bits_str);
        return result.ToString();
    }
}
