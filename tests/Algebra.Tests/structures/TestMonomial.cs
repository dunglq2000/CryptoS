namespace CryptoS.Algebra.Boolean.Tests;

[TestClass]
public class TestMonomial
{
    [TestMethod]
    public void TestMonomial1()
    {
        Monomial f = new Monomial(32, [0b_1110_1011_1000_1111U]);
        var degree = f.Degree;
        Assert.AreEqual(11, degree);
    }
    [TestMethod]
    public void TestMonomial2()
    {
        Monomial f = new Monomial(5, [0b11010U << 27]);
        var degree = f.Degree;
        Assert.AreEqual(3, degree);
    }
    [TestMethod]
    public void TestMonomial3()
    {
        Monomial f = new Monomial(5, "x0*x1*x4");
        Monomial g = new Monomial(5, [0b11001U << 27]);
        var result = f.Equals(g);
        Assert.IsTrue(result);
    }
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
        var result = a.IsDivisibleBy(b);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestMonomialDivisibility2()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        var result = a.IsDivisibleBy(b);
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
