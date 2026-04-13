namespace Algebra.Tests;

[TestClass]
public class TestMonomialOrdering
{
    [TestMethod]
    public void TestLexOrdering1()
    {
        // Đối với lex, đơn thức (1, 0, 1, 1) > (0, 0, 0, 1)
        Monomial a = new Monomial(32, [2952790016U]);   // 1, 0, 1, 1
        Monomial b = new Monomial(32, [ 268435456U]);   // 0, 0, 0, 1
        List<Monomial> monomials = [a, b];
        monomials = monomials.OrderByDescending(monomial => monomial, new LexOrdering()).ToList();
        var result = monomials.SequenceEqual([a, b]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestLexOrdering2()
    {
        // Đối với lex, đơn thức (1, 0, 0, 0) > (0, 1, 1, 1)
        Monomial a = new Monomial(32, [2147483648U]);   // 1, 0, 0, 0
        Monomial b = new Monomial(32, [1879048192U]);   // 0, 1, 1, 1
        List<Monomial> monomials = [a, b];
        monomials = monomials.OrderByDescending(monomial => monomial, new LexOrdering()).ToList();
        var result = monomials.SequenceEqual([a, b]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestDeglexOrdering1()
    {
        // Đối với deglex, đa thức (0, 1, 1, 1) > (1, 0, 0, 0)
        Monomial a = new Monomial(32, [2147483648U]);   // 1, 0, 0, 0
        Monomial b = new Monomial(32, [1879048192U]);   // 0, 1, 1, 1
        List<Monomial> monomials = [a, b];
        monomials = monomials.OrderByDescending(monomial => monomial, new DeglexOrdering()).ToList();
        var result = monomials.SequenceEqual([b, a]);
        Assert.IsTrue(result);
    }
}
