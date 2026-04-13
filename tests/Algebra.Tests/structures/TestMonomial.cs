namespace Algebra.Tests;

[TestClass]
public class TestMonomial
{
    [TestMethod]
    public void TestMonomialMultiplication1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Assert.AreEqual(a * b, c);
    }
    [TestMethod]
    public void TestMonomialMultiplication2()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        a.Multiply(new Monomial(32, [268435456U]));     // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Assert.AreEqual(a, c);
    }
    [TestMethod]
    public void TestMonomialDivisibility1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        var result = a.IsDivisible(b);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestMonomialDivisibility2()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        var result = a.IsDivisible(b);
        Assert.IsFalse(result);
    }
    [TestMethod]
    public void TestMonomialDivision1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Assert.AreEqual(a / b, c);
    }
    [TestMethod]
    public void TestMonomialDivision2()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Exception? exception = null;
        try
        {
            Monomial c = a / b;
        }
        catch (ArgumentException ex)
        {
            exception = ex;
        }
        Assert.IsNotNull(exception);
    }
        [TestMethod]
    public void TestMonomialDivision3()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        a.Divide(new Monomial(32, [268435456U]));       // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Assert.AreEqual(a, c);
    }
}
