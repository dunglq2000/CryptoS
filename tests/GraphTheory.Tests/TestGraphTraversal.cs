namespace GraphTheory.Traversal.Tests;
using GraphTheory.Traversal;

[TestClass]
public class TestGraphTraversal
{
    [TestMethod]
    public void TestStrategy_DFSRecurse()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphDFSRecurse(graph);
        List<int>? paths = graphTraversal.FindPath(0, 3);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestStrategy_DFSRecurse_NoPath()
    {
        UndirectedGraph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphDFSRecurse(graph);
        List<int>? paths = graphTraversal.FindPath(0, 4);
        Assert.IsNull(paths);
    }
    [TestMethod]
    public void TestStrategy_DFSStack()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphDFSStack(graph);
        List<int>? paths = graphTraversal.FindPath(0, 3);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void TestStrategy_DFSStack_NoPath()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphDFSStack(graph);
        List<int>? paths = graphTraversal.FindPath(0, 4);
        Assert.IsNull(paths);
    }
    [TestMethod]
    public void TestStrategy_BFS()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphBFS(graph);
        List<int>? paths = graphTraversal.FindPath(0, 4);
        Assert.IsNull(paths);
    }
    [TestMethod]
    public void TestStrategy_BFS_NoPath()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>{
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );

        GraphTraversal graphTraversal = new GraphBFS(graph);
        List<int>? paths = graphTraversal.FindPath(0, 3);
        var result = paths is not null && paths.SequenceEqual([0, 1, 2, 3]);
        Assert.IsTrue(result);
    }
}
