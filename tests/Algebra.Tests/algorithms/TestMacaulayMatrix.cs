namespace CryptoS.Algebra.Boolean.Tests;

[TestClass]
public class TestMacaulayMatrix
{
    [TestMethod]
    public void TestMacaulayMatrix1()
    {
        Polynomial f = new Polynomial(
            new LexOrdering(),
            [
                new Monomial(32, [0b1100U << 28]),
                new Monomial(32, [0b1111U << 28]),
                new Monomial(32, [0b0001U << 28])
            ]
        );
        Polynomial g = new Polynomial(
            new LexOrdering(),
            [
                new Monomial(32, [0b1111U << 28]),
                new Monomial(32, [0b0000U << 28]),
                new Monomial(32, [0b1101U << 28])
            ]
        );
        MacaulayMatrix macaulayMatrix = new MacaulayMatrix([f, g]);
        bool result = macaulayMatrix.Matrix.SequenceEqual([0b10110U << 27, 0b11001U << 27]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestMacaulayMatrix2()
    {
        Polynomial f = new Polynomial(
            new LexOrdering(),
            [
                new Monomial(32, [0b1100U << 28]),
                new Monomial(32, [0b1111U << 28]),
                new Monomial(32, [0b0001U << 28])
            ]
        );
        Polynomial g = new Polynomial(
            new LexOrdering(),
            [
                new Monomial(32, [0b1111U << 28]),
                new Monomial(32, [0b0000U << 28]),
                new Monomial(32, [0b1101U << 28])
            ]
        );
        MacaulayMatrix macaulayMatrix = new MacaulayMatrix([f, g]);
        macaulayMatrix.Reduce();
        List<Polynomial> polynomials = macaulayMatrix.GetPolynomials();
        List<Polynomial> expectedResult = [
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b1111U << 28]),
                    new Monomial(32, [0b1100U << 28]),
                    new Monomial(32, [0b0001U << 28])
                ]
            ),
            new Polynomial(
                new LexOrdering(),
                [
                    new Monomial(32, [0b1101U << 28]),
                    new Monomial(32, [0b1100U << 28]),
                    new Monomial(32, [0b0001U << 28]),
                    new Monomial(32, [0b0000U << 28])
                ]
            )
        ];
        var result = expectedResult.Except(polynomials);
        Assert.HasCount(0, result);
    }
}
