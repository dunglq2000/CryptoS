namespace CryptoS.Algebra.Boolean.Tests;

[TestClass]
public class TestBuchberger
{
    private readonly MonomialOrdering lexOrdering = new LexOrdering();
    [TestMethod]
    public void TestBuchberger1()
    {
        List<Polynomial> polynomials = [
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b11U << 30]),
                new Monomial(32, [0b01U << 30])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b10U << 30]),
                new Monomial(32, [0b00U << 30])
            ])
        ];
        GroebnerAlgorithm solver = new BuchbergerAlgorithm();
        List<Polynomial> result = solver.Compute(polynomials);
        Assert.HasCount(2, result); // no monomial is added
    }
    [TestMethod]
    public void TestBuchberger2()
    {
        List<Polynomial> polynomials = [
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b110U << 29]),
                new Monomial(32, [0b011U << 29]),
                new Monomial(32, [0b000U << 29])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b101U << 29]),
                new Monomial(32, [0b010U << 29]),
                new Monomial(32, [0b001U << 29])
            ])
        ];
        GroebnerAlgorithm solver = new BuchbergerAlgorithm();
        List<Polynomial> result = solver.Compute(polynomials);
        List<Polynomial> expectedResult = [
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b110U << 29]),
                new Monomial(32, [0b011U << 29]),
                new Monomial(32, [0b000U << 29])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b101U << 29]),
                new Monomial(32, [0b010U << 29]),
                new Monomial(32, [0b001U << 29])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b010U << 29]),
                new Monomial(32, [0b001U << 29])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b001U << 29]),
                new Monomial(32, [0b000U << 29])
            ]),
            new Polynomial(lexOrdering, [
                new Monomial(32, [0b100U << 29])
            ])
        ];
        var diff = result.Except(expectedResult);
        foreach (var poly in diff)
        {
            Console.WriteLine(poly);
        }
        // Assert.HasCount(5, result); //
        Assert.HasCount(0, expectedResult.Except(result));
    }
}