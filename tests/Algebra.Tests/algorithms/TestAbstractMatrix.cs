namespace CryptoS.Algebra.Boolean.Tests;

[TestClass]
public class TestAbstractMatrix
{
    [TestMethod]
    public void TestAbstractMatrix1()
    {
        uint[] matrix = [0b1100U << 28, 0b1010U << 28, 0b0111U << 28];
        int nrows = 3;
        int ncols = 4;
        AbstractMatrix abstractMatrix = new AbstractMatrix(matrix, nrows, ncols);
        abstractMatrix.Reduce();
        bool result = abstractMatrix.Matrix.SequenceEqual([0b1010U << 28, 0b0110U << 28, 0b0001U << 28]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestAbstractMatrix2()
    {
        uint[] matrix = [0b011U << 29, 0b101U << 29, 0b110U << 29];
        int nrows = 3;
        int ncols = 3;
        AbstractMatrix abstractMatrix = new AbstractMatrix(matrix, nrows, ncols);
        abstractMatrix.Reduce();
        bool result = abstractMatrix.Matrix.SequenceEqual([0b101U << 29, 0b011U << 29, 0b000U << 29]);
        Assert.IsTrue(result);
    }
}