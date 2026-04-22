namespace GraphTheory.Traversal.Tests;

[TestClass]
public class TestGraphFindPath
{
    [TestMethod]
    public void Test_UndirectedGraph_DFSRecurse()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        List<int>? paths = graph.FindPath(0, 3, Graph.TraversingAlgorithm.DFSRecurse);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void Test_UndirectedGraph_DFSRecurse_Fail()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        List<int>? paths = graph.FindPath(0, 4, Graph.TraversingAlgorithm.DFSRecurse);
        Assert.IsNull(paths);
    }
    [TestMethod]
    public void Test_UndirectedGraph_DFSStack()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        List<int>? paths = graph.FindPath(0, 3, Graph.TraversingAlgorithm.DFSStack);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void Test_UndirectedGraph_BFS()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        List<int>? paths = graph.FindPath(0, 3, Graph.TraversingAlgorithm.BFS);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
}