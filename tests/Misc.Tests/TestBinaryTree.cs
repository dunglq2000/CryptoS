namespace CryptoS.Misc.Tests;

[TestClass]
public class TestBinaryTreen
{
    [TestMethod]
    public void TestBinaryTreeSearch1()
    {
        BinaryTree<int> binaryTree = new BinaryTree<int>();
        for (int i = 0; i < 5; ++i)
        {
            binaryTree.Add((i + 2) % 5);
        }
        int j = binaryTree.Find(3);
        Assert.AreEqual(2, j);
    }
    [TestMethod]
    public void TestBinaryTreeSearch2()
    {
        BinaryTree<int> binaryTree = new BinaryTree<int>();
        for (int i = 0; i < 5; ++i)
        {
            binaryTree.Add((i + 2) % 5);
        }
        List<int> nodes = binaryTree.Traversal(BinaryTree<int>.TraversalType.InOrder);
        var result = nodes.SequenceEqual([2, 0, 1, 3, 4]);
        Assert.IsTrue(result);
    }
}
