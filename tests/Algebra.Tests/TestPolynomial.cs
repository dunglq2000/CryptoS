namespace Algebra.Tests;

[TestClass]
public class TestPolynomial
{
    [TestMethod]
    public void TestPolynomial1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b, c]);
        Polynomial q = new Polynomial(new LexOrdering(), [b, a, a]);
        Assert.AreEqual(p, q);
    }
    [TestMethod]
    public void TestPolynomialAddition1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b, c]);
        Polynomial q = p + a;
        Assert.AreEqual(q, new Polynomial(new LexOrdering(), [b]));
    }
    [TestMethod]
    public void TestPolynomialAddition2()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        Polynomial q = p + c;
        Assert.AreEqual(q, new Polynomial(new LexOrdering(), [c, b, a]));
    }
    [TestMethod]
    public void TestPolynomialAddition3()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b, c]);
        p.Add(a);
        Assert.AreEqual(p, new Polynomial(new LexOrdering(), [b]));
    }
    [TestMethod]
    public void TestPolynomialAddition4()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        p.Add(c);
        Assert.AreEqual(new Polynomial(new LexOrdering(), [a, b, c]), p);
    }
}
