namespace Algebra.Tests;

[TestClass]
public class TestFraction
{
    [TestMethod]
    public void TestFraction1()
    {
        Fraction a = new Fraction(2, 2);
        Assert.AreEqual(a, new Fraction(1, 1));
    }
    [TestMethod]
    public void TestFraction2()
    {
        Fraction a = new Fraction(-3, 6);
        Assert.AreEqual(a, new Fraction(-1, 2));
    }
    [TestMethod]
    public void TestFraction3()
    {
        Fraction a = new Fraction(3, -6);
        Assert.AreEqual(a, new Fraction(-1, 2));
    }
    [TestMethod]
    public void TestFraction4()
    {
        Fraction a = new Fraction(1, 4);
        Fraction b = new Fraction(1, 6);
        Fraction c = a + b;
        Assert.AreEqual(c, new Fraction(5, 12));
    }
}
