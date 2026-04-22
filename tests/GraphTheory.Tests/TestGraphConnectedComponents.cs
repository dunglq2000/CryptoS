using GraphTheory.Traversal.ConnectedComponents;

namespace GraphTheory.Traversal.Tests;

[TestClass]
public class TestGraphConnectedComponents
{
    [TestMethod]
    public void Test_UndirectedGraph_ConnectedComponents_DFSRecurse()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        GraphTraversal graphConnectedComponents = new GraphDFSRecurse(graph);
        List<List<int>> connectedComponents = graphConnectedComponents.GetConnectedComponents();
        Assert.HasCount(2, connectedComponents);
    }
    [TestMethod]
    public void Test_UndirectedGraph_ConnectedComponents_DFSStack()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        GraphTraversal graphConnectedComponents = new GraphDFSStack(graph);
        List<List<int>> connectedComponents = graphConnectedComponents.GetConnectedComponents();
        Assert.HasCount(2, connectedComponents);
    }
    [TestMethod]
    public void Test_UndirectedGraph_ConnectedComponents_BFS()
    {
        Graph graph = new UndirectedGraph(
            new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (4, 5)
            }, 6
        );
        GraphTraversal graphConnectedComponents = new GraphBFS(graph);
        List<List<int>> connectedComponents = graphConnectedComponents.GetConnectedComponents();
        Assert.HasCount(2, connectedComponents);
    }
}
