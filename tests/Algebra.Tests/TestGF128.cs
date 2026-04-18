namespace Algebra.Tests;

[TestClass]
public sealed class GF128Tests
{
    [TestMethod]
    public void TestFromByteArray()
    {
        GF128 a = new GF128([225, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1]);
        GF128 b = GF128.FromHexString("E1000000000000000000000000000001");
        Assert.AreEqual(a.ToString(), b.ToString());
    }

    [TestMethod]
    public void TestGF128Mult1()
    {
        GF128 a = GF128.FromHexString("280502c00a7f3c3e07a4f95c3cee5812");
        GF128 b = GF128.FromHexString("c5039d9a58a5d955c2a480f96191396d");
        GF128 c = a * b;
        Assert.AreEqual("d58736a2aea231e19678f3015dd03abd", c.ToString());
    }
}
