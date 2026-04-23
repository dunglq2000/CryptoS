# GOST cryptography

Russian block ciphers:

- implement Russian block ciphers Kuznyechik and Magma (GOST 34.12-2015) [1];
- unit tests for functions and encryption/decryption, which follow examples (test vectors) [1].

# Usage

The following code takes an 32-byte key as input for Kuznyechik instance and encrypt one 16-byte block of data.

```cs
using System.Text;
using CryptoS.Crypto.GostCrypto;

byte[] key = Convert.FromHexString("8899aabbccddeeff0011223344556677fedcba98765432100123456789abcdef");
Kuznyechik kuznyechik = new Kuznyechik(key);
byte[] plaintext = Convert.FromHexString("1122334455667700ffeeddccbbaa9988");
byte[] ciphertext = kuznyechik.EncryptBlock(plaintext);
```

We use the same way for Magma algorithm but the block size now is 8-byte, not 16-byte as Kuznyechik.

```cs
using System.Text;
using CryptoS.Crypto.GostCrypto;

byte[] key = Convert.FromHexString("8899aabbccddeeff0011223344556677fedcba98765432100123456789abcdef");
Magma magma = new Magma(key);
byte[] plaintext = Convert.FromHexString("1122334455667700");
byte[] ciphertext = magma.EncryptBlock(plaintext);
```

# Bibliography

[1] ГОСТ Р 34.12-2015. Информационная технология. Криптографическая защита информации. Блочные шифры.
