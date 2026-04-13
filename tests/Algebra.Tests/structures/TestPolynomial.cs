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
    [TestMethod]
    public void TestSPoly1()
    {
        Monomial a = new Monomial(32, [3758096384U]);   // 1, 1, 1, 0
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Monomial d = new Monomial(32, [ 536870912U]);   // 0, 0, 1, 0
        Monomial e = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, c, b]);
        Polynomial q = new Polynomial(new LexOrdering(), [c, d]);
        Polynomial result = new Polynomial(new LexOrdering(), [e, c, b]);
        Assert.AreEqual(Polynomial.SPoly(p, q), result);
    }
    [TestMethod]
    public void TestSPoly2()
    {
        Monomial a = new Monomial(32, [3758096384U]);   // 1, 1, 1, 0
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial c = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Monomial d = new Monomial(32, [ 536870912U]);   // 0, 0, 1, 0
        Monomial e = new Monomial(32, [2684354560U]);   // 1, 0, 1, 0
        Polynomial p = new Polynomial(new LexOrdering(), [a, c, b]);
        Polynomial q = new Polynomial(new LexOrdering(), [c, d]);
        Polynomial result = new Polynomial(new LexOrdering(), [e, c, b]);
        Assert.AreEqual(p.SPoly(q), result);
    }
    [TestMethod]
    public void TestPolynomialDivision()
    {
        Monomial a = new Monomial(32, [3758096384U]);   // 1, 1, 1, 0
        Monomial b = new Monomial(32, [1073741824U]);   // 0, 1, 0, 0
        Monomial c = new Monomial(32, [         0U]);   // 0, 0, 0, 0
        // p = xyz + y + 1
        Polynomial p = new Polynomial(new LexOrdering(), [a, b, c]);

        Monomial d = new Monomial(32, [3221225472U]);   // 1, 1, 0, 0
        Monomial e = new Monomial(32, [1342177280U]);   // 0, 1, 0, 1
        Monomial f = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        Monomial g = new Monomial(32, [ 536870912U]);   // 0, 0, 1, 0
        Monomial h = new Monomial(32, [ 805306368U]);   // 0, 0, 1, 1
        List<Polynomial> polynomials = [
            new Polynomial(new LexOrdering(), [d, e, f]),
            new Polynomial(new LexOrdering(), [e, g])
        ];
        var (r, q) = p.Divide(polynomials);
        Assert.AreEqual(new Polynomial(new LexOrdering(), [b, h, g, c]), r);
    }
}
