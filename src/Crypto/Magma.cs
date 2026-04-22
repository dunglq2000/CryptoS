namespace CryptoS.Crypto.GostCrypto
{
    /// <summary>
    /// Magma block cipher.
    /// </summary>
    public class Magma
    {
        private static uint[][] pi =
            [
                [12, 4, 6, 2, 10, 5, 11, 9, 14, 8, 13, 7, 0, 3, 15, 1],
                [6, 8, 2, 3, 9, 10, 5, 12, 1, 14, 4, 7, 11, 13, 0, 15],
                [11, 3, 5, 8, 2, 15, 10, 13, 14, 1, 7, 4, 12, 9, 6, 0],
                [12, 8, 2, 1, 13, 4, 15, 6, 7, 0, 10, 5, 3, 14, 9, 11],
                [7, 15, 5, 10, 8, 1, 6, 13, 0, 9, 3, 14, 11, 4, 2, 12],
                [5, 13, 15, 6, 9, 2, 12, 10, 11, 7, 8, 1, 4, 3, 14, 0],
                [8, 14, 2, 5, 6, 9, 1, 12, 15, 4, 11, 0, 13, 10, 3, 7],
                [1, 7, 14, 13, 0, 5, 8, 3, 4, 15, 10, 6, 9, 12, 11, 2]
            ];
        /// <summary>
        /// 256-bit key.
        /// </summary>
        public readonly byte[] Key;
        /// <summary>
        /// Subkeys generated from master key <see cref="Key"/>.
        /// </summary>
        public readonly uint[] Subkeys;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">Input 256-bit/32-byte key.</param>
        public Magma(byte[] key)
        {
            Key = key.ToArray();
            Subkeys = new uint[8];
            for (uint i = 0; i < 8; i++)
            {
                uint subkey = 0;
                for (int j = 0; j < 4; j++)
                {
                    subkey ^= (uint)(Key[4 * i + j] << (8 * (3 - j)));
                }
                Subkeys[i] = subkey;
            }
        }
        /// <summary>
        /// Operation T for Magma.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static uint T(uint state)
        {
            uint result = 0;
            for (int i = 0; i < 8; i++)
            {
                uint low = (state >> (4 * i)) & 0xf;
                result ^= pi[i][low] << (4 * i);
            }
            return result;
        }
        /// <summary>
        /// Addition modulo $2^{32}$.
        /// </summary>
        /// <param name="a">First unsigned integer.</param>
        /// <param name="b">Second unsigned integer.</param>
        /// <returns>Result $(a + b) \bmod{2^{32}}$.</returns>
        public static uint Add32(uint a, uint b)
        {
            return a + b;
        }
        /// <summary>
        /// Rotate 11 bits leftward.
        /// </summary>
        /// <param name="x">Unsigned integer.</param>
        /// <returns>Rotate input 11 bits leftward.</returns>
        public static uint Rot11(uint x)
        {
            return (x << 11) | (x >> 21);
        }
        /// <summary>
        /// Round function of Magma.
        /// </summary>
        /// <param name="x">Input for round, which has 64 bits.</param>
        /// <param name="key">Subkey for round.</param>
        /// <returns>Output for round.</returns>
        public static ulong Round(ulong x, uint key)
        {
            ulong left = x >> 32;
            ulong right = x & 0xffffffff;
            ulong right_ = right;
            right = left ^ Rot11(T(Add32((uint)right , key)));
            left = right_;
            return (left << 32) | right;
        }
        /// <summary>
        /// Encrypt one 8-byte block of data.
        /// </summary>
        /// <param name="state">8-byte block of plaintext.</param>
        /// <returns>8-byte block of ciphertext.</returns>
        public byte[] EncryptBlock(byte[] state)
        {
            byte[] result = new byte[8];
            ulong block = 0;
            // byte[] to block
            for (int i = 0; i < 8; i++)
            {
                block ^= (ulong)state[i] << (8 * (7 - i));
            }
            // Encrypt
            for (int i = 0; i < 24; i++)
            {
                block = Round(block, Subkeys[i % 8]);
            }
            for (int i = 24; i < 32; i++)
            {
                block = Round(block, Subkeys[7 - (i % 8)]);
            }

            block = (block << 32) | (block >> 32);

            for (int i = 0; i < 8; i++)
            {
                result[7 - i] = (byte)((block >> (8 * i)) & 0xFF);
            }
            return result;
        }
        /// <summary>
        /// Decrypt one 8-byte block of data.
        /// </summary>
        /// <param name="state">8-byte block of ciphertext.</param>
        /// <returns>8-byte block of plaintext.</returns>
        public byte[] DecryptBlock(byte[] state)
        {
            byte[] result = new byte[8];
            ulong block = 0;
            for (int i = 0; i < 8; i++)
            {
                block ^= (ulong)state[i] << 8 * (7 - i);
            }

            for (int i = 31; i >= 24; i--)
            {
                block = Round(block, Subkeys[7 - (i % 8)]);
            }
            for (int i = 23; i >= 0; i--)
            {
                block = Round(block, Subkeys[i % 8]);
            }

            block = (block << 32) | (block >> 32);

            for (int i = 0; i < 8; i++)
            {
                result[7-i] = (byte)((block >> (8 * i))& 0xFF);
            }
            return result;
        }
    }
}
