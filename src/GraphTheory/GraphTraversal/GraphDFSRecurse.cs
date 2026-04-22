namespace GraphTheory.Traversal;

/// <summary>
/// Class for traversing graph with DFS algorithm using recursive.
/// </summary>
public class GraphDFSRecurse : GraphTraversal
{
    public GraphDFSRecurse(Graph graph) : base(graph)
    {
        
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm using recursive.
    /// </summary>
    /// <param name="u">Current node.</param>
    public override void Traverse(int u)
    {
        _isVisited[u] = true;
        _currentConnectedComponent.Add(u);
        for (int v = 0; v < _graph.NumberOfVertices; ++v)
        {
            if (_isVisited[v] == false && _graph.Matrix[u * _graph.NumberOfVertices + v] == 1)
            {
                _trace[v] = u;
                Traverse(v);
            }
        }
    }
}
