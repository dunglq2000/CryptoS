using System.Text;

namespace Algebra;

/// <summary>
/// Class for finite field GF(2^128) with modulus x^128 + x^7 + x^2 + x + 1
/// </summary>
public class GF128
{
    public byte[] data = new byte[16];
    public GF128()
    {
        for (int i = 0; i < 16; i++)
            data[i] = 0;
    }
    public GF128(byte[] data)
    {
        for (int i = 0; i < 16; i++)
            this.data[i] = data[i];
    }
    public GF128(GF128 other)
    {
        for (int i = 0; i < 16; i++)
            data[i] = other.data[i];
    }
    public static GF128 operator +(GF128 a, GF128 b)
    {
        GF128 result = new GF128();
        for (int i = 0; i < 16; i++)
            result.data[i] = (byte)(a.data[i] ^ b.data[i]);
        return result;
    }
    public static GF128 operator *(GF128 a, GF128 b)
    {
        GF128 result = new GF128();
        GF128 c = new GF128(a);
        while (IsZero(b) == false)
        {
            if ((b.data[15] & 1) != 0)
                result = result + c;
            c = xtime(c);
            // Console.WriteLine(c.ToString());
            b >>= 1;
        }
        return result;
    }
    public static GF128 operator >>(GF128 a, int b)
    {
        byte[] result = new byte[16];
        a.data.CopyTo(result, 0);
        while (b != 0)
        {
            for (int i = 15; i > 0; i--)
                result[i] = (byte)((result[i] >> 1) | ((result[i - 1] & 1) << 7));
            result[0] >>= 1;
            b >>= 1;
        }
        return new GF128(result);
    }
    public static bool IsZero(GF128 a)
    {
        bool flag = true;
        for (int i = 0; i < 16; i++)
            if (a.data[i] != 0) flag = false;
        return flag;
    }
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 16; i++)
            stringBuilder.Append(data[i].ToString("x2"));
        return stringBuilder.ToString();
    }
    public static GF128 xtime(GF128 a)
    {
        byte[] result = new byte[16];
        a.data.CopyTo(result, 0);
        bool flag = (a.data[0] & 0x80) != 0;
        for (int i = 0; i < 15; i++)
        {
            result[i] = (byte)((result[i] << 1) | (result[i + 1] >> 7));
        }
        result[15] <<= 1;
        if (flag)
        {
            result[15] ^= 0x87;
        }
        return new GF128(result);
    }

    // Convert hex string (big-endian) to GF128 element
    public static GF128 FromHexString(string hex)
    {
        if (hex.Length != 32)
            throw new ArgumentException("Hex string must be 32 characters long");

        byte[] bytes = Enumerable.Range(0, 32)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();

        return new GF128(bytes);
    }
}
