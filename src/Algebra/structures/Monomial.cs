using System.Text;

namespace Algebra;
public class Monomial
{
    public readonly uint nbits;
    public uint[] bits;
    public Monomial(uint nbits)
    {
        this.nbits = nbits;
        bits = new uint[nbits / 32];
    }
    public Monomial(uint nbits, uint[] bits)
    {
        this.nbits = nbits;
        this.bits = new uint[nbits / 32];
        Array.Copy(bits, this.bits, nbits / 32);
    }
    public Monomial(Monomial monomial)
    {
        nbits = monomial.nbits;
        bits = new uint[nbits / 32];
        Array.Copy(bits, monomial.bits, nbits / 32);
    }
    public static Monomial operator*(Monomial left, Monomial right)
    {
        if (left.nbits != right.nbits)
        {
            throw new ArgumentException("The number of bits of monomials must the the same!");
        }
        uint[] result = new uint[left.nbits / 32];
        for (int i = 0; i < result.Length; ++i)
            result[i] = left.bits[i] | right.bits[i];
        return new Monomial(left.nbits, result);
    }
    public static Monomial operator/(Monomial left, Monomial right)
    {
        if (left.IsDivisibleBy(right) == false)
        {
            throw new ArgumentException("First monomial is not divisible by the second!");
        }
        if (left.nbits != right.nbits)
        {
            throw new Exception("The number of bits of monomials must be the same!");
        }
        uint[] result = new uint[left.nbits / 32];
        for (var i = 0; i < result.Length; ++i)
        {
            result[i] = left.bits[i] ^ right.bits[i];
        }
        return new Monomial(left.nbits, result);
    }
    public void Multiply(Monomial other)
    {
        if (nbits != other.nbits)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        for (var i = 0; i < bits.Length; ++i)
        {
            bits[i] |= other.bits[i];
        }
    }
    public void Divide(Monomial other)
    {
        if (nbits != other.nbits)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        if (IsDivisibleBy(other) == false)
        {
            throw new ArgumentException("First monomial is not divisible by the second!");
        }
        for (var i = 0; i < bits.Length; ++i)
        {
            bits[i] ^= other.bits[i];
        }
    }
    public bool IsDivisibleBy(Monomial other)
    {
        if (nbits != other.nbits)
        {
            throw new ArgumentException("The number of bits of monomials must be the same!");
        }
        for (var i = 0; i < bits.Length; ++i)
        {
            uint tmp = bits[i] | (~other.bits[i]);
            if (tmp != 0xffffffff)
            {
                return false;
            }
        }
        return true;
    }
    public bool Equals(Monomial other)
    {
        if (nbits != other.nbits)
        {
            return false;
        }
        for (var i = 0; i < bits.Length; ++i)
        {
            if (bits[i] != other.bits[i])
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
        if (nbits != other.nbits)
        {
            return false;
        }
        for (var i = 0; i < bits.Length; ++i)
        {
            if (bits[i] != other.bits[i])
            {
                return false;
            }
        }
        return true;
    }
    public override int GetHashCode()
    {
        uint result = 0;
        for (var i = 0; i < bits.Length; ++i)
        {
            for (var j = 0; j < 32; j++)
            {
                uint bit = (bits[i] >> (31 - j)) & 1;
                result = (result * 2 + bit) % 1073741789;
            }
        }
        return (int)result;
    }
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        List<string> bits_str = new List<string>();
        for (var i = 0; i < bits.Length; ++i)
        {
            for (var j = 0; j < 32; ++j)
            {
                var bit = (bits[i] >> (31 - j)) & 1;
                bits_str.Add($"{bit}");
            }
        }
        result.AppendJoin(", ", bits_str);
        return result.ToString();
    }
}
