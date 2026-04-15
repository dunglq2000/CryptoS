using Algebra;

namespace Algebra.Tests;

[TestClass]
public class TestF4
{
    [TestMethod]
    public void TestF4_1()
    {
        List<Polynomial> polynomials = [
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b11U << 30]),
                    new Monomial(32, [0b01U << 30])
                ]
            ),
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b01U << 30]),
                    new Monomial(32, [0b00U << 30])
                ]
            )
        ];
        GroebnerAlgorithm solver = new F4Algorithm();
        var result = solver.Compute(polynomials);
        List<Polynomial> expectedResult = [
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b11U << 30]),
                    new Monomial(32, [0b01U << 30])
                ]
            ),
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b01U << 30]),
                    new Monomial(32, [0b00U << 30])
                ]
            ),
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b10U << 30]),
                    new Monomial(32, [0b00U << 30])
                ]
            )
        ];
        List<Polynomial> diff = expectedResult.Except(result).ToList();
        Assert.HasCount(0, diff);
    }
}