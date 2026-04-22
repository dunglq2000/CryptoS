namespace CryptoS.GraphTheory.Traversal;

/// <summary>
/// Class for traversing graph with DFS algorithm using stack.
/// </summary>
public class GraphDFSStack : GraphTraversal
{
    /// <summary>
    /// Constructor for traversing through graph.
    /// </summary>
    /// <param name="graph"></param>
    public GraphDFSStack(Graph graph) : base(graph)
    {
        
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm using stack.
    /// </summary>
    /// <param name="u">Current node.</param>
    public override void Traverse(int u)
    {
        Stack<int> vertices = new Stack<int>();
        vertices.Push(u);
        _isVisited[u] = true;
        _currentConnectedComponent.Add(u);
        while (vertices.Count > 0)
        {
            int s = vertices.Peek();
            vertices.Pop();
            for (int v = 0; v < _graph.NumberOfVertices; ++v)
            {
                if (_isVisited[v] == false && _graph.Matrix[s * _graph.NumberOfVertices + v] == 1)
                {
                    _trace[v] = s;
                    _isVisited[v] = true;
                    _currentConnectedComponent.Add(u);
                    vertices.Push(v);
                }
            }
        }
    }
}
