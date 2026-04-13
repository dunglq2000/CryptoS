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
        Assert.AreEqual(q, new Polynomial(new LexOrdering(), [a, b]));
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
        Assert.AreEqual(p, new Polynomial(new LexOrdering(), [a, b]));
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
    [TestMethod]
    public void TestPolynomialMultiplication1()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        Polynomial q = p * c;
        Assert.IsTrue(q.IsZero());
    }
    /// <summary>
    /// Test <c>(xzt + t) yt = xyzt + yt</c> with operator <c>*</c>
    /// </summary>
    [TestMethod]
    public void TestPolynomialMultiplication2()
    {  
        Monomial a = new Monomial(32, [3758096384U]);   // 1, 1, 1, 0
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Monomial d = new Monomial(32, [4026531840U]);   // 1, 1, 1, 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        Polynomial q = p * c;
        Assert.AreEqual(q, new Polynomial(new LexOrdering(), [c, d]));
    }
    [TestMethod]
    public void TestPolynomialMultiplication3()
    {
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        p.Multiply(c);
        Assert.IsTrue(p.IsZero());
    }
    /// <summary>
    /// Test <c>(xzt + t) yt = xyzt + yt</c> with function <c>Polynomial.Multiply</c>
    /// </summary>
    [TestMethod]
    public void TestPolynomialMultiplication4()
    {  
        Monomial a = new Monomial(32, [3758096384U]);   // 1, 1, 1, 0
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Monomial d = new Monomial(32, [4026531840U]);   // 1, 1, 1, 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b]);
        p.Multiply(c);
        Assert.AreEqual(p, new Polynomial(new LexOrdering(), [c, d]));
    }
}
