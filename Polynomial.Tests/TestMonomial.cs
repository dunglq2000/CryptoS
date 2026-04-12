using PolynomialService;
namespace Polynomial.Tests;

[TestClass]
public class TestMonomial
{
    [TestMethod]
    public void TestMonomial1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);    // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Assert.AreEqual(a * b, c);
    }
}
