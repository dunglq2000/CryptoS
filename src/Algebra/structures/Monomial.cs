using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Algebra;
public class Monomial
{
    public readonly int BitCount;
    public uint[] Bits;
    /// <summary>
    /// <para>Use fast way to count the number of bits in <c>uint</c></para>
    /// <para>Source: <see cref="https://stackoverflow.com/a/12175897"/></para>
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
    public Monomial(int bitCount)
    {
        BitCount = bitCount;
        Bits = new uint[(bitCount + 31) >> 5];
    }
    public Monomial(int bitCount, uint[] bits)
    {
        BitCount = bitCount;
        Bits = new uint[(bitCount + 31) >> 5];
        Array.Copy(bits, Bits, (bitCount + 31) >> 5);
    }
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
    public Monomial(Monomial monomial)
    {
        BitCount = monomial.BitCount;
        Bits = new uint[(BitCount + 31) >> 5];
        Array.Copy(Bits, monomial.Bits, (BitCount + 31) >> 5);
    }
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
    public bool Equals(Monomial other)
    {
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
