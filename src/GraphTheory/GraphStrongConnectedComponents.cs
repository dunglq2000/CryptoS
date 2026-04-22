namespace GraphTheory.Traversal.ConnectedComponents;

/// <summary>
/// Strong connected components for directed graph.
/// </summary>
public abstract class GraphStrongConnectedComponents : GraphTraversal
{
    /// <summary>
    /// Temporary variables for Tarjan algorithm.
    /// </summary>
    protected int[] _number;
    /// <summary>
    /// Temporary variable for counting the number of strong connected components.
    /// </summary>
    protected int _cnt;
    /// <summary>
    /// Temporary variables for Tarjan algorithm.
    /// </summary>
    protected int[] _low;
    /// <summary>
    /// Count the number of strong connected components.
    /// </summary>
    protected int _countStrongConnectedComponents;
    /// <summary>
    /// Trace vertices in the same strong connected components.
    /// </summary>
    protected Stack<int> _traceStrongConnectedComponents;
    /// <summary>
    /// List of strong connected components.
    /// </summary>
    protected List<List<int>> _strongConnectedComponents;
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="graph"></param>
    public GraphStrongConnectedComponents(Graph graph) : base(graph)
    {
        _low = new int[graph.NumberOfVertices];
        _number = new int[graph.NumberOfVertices];
        _cnt = 0;
        _countStrongConnectedComponents = 0;
        _traceStrongConnectedComponents = new Stack<int>();
        _strongConnectedComponents = new List<List<int>>();
    }
    /// <summary>
    /// Get all strong connected components.
    /// </summary>
    /// <returns></returns>
    public List<List<int>> GetStrongConnectedComponents()
    {
        if (_strongConnectedComponents.Count == 0)
        {
            for (int u = 0; u < _graph.NumberOfVertices; ++u)
            {
                if (_number[u] == 0)
                {
                    Traverse(u);
                }
            }
        }
        return _strongConnectedComponents;
    }
}

/// <summary>
/// Strong connected components for directed graph with DFS.
/// </summary>
public class GraphStrongConnectedComponentsDFS : GraphStrongConnectedComponents
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="graph"></param>
    public GraphStrongConnectedComponentsDFS(UndirectedGraph graph) : base(graph)
    {
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm and trace connected component.
    /// </summary>
    /// <param name="u">Current node.</param>
    public override void Traverse(int u)
    {
        _cnt += 1;
        _number[u] = _cnt;
        _low[u] = _number[u];
        _traceStrongConnectedComponents.Push(u);
        _isVisited[u] = true;
        for (int v = 0; v < _graph.NumberOfVertices; ++v)
        {
            if (_graph.Matrix[u * _graph.NumberOfVertices + v] == 1)
            {
                if (_number[v] != 0 && _isVisited[v] == true)
                {
                    _low[u] = _low[u] < _number[v] ? _low[u] : _number[v];
                }
                else
                {
                    Traverse(v);
                    _low[u] = _low[u] < _low[v] ? _low[u] : _low[v];
                }
            }
        }
        if (_number[u] == _low[u])
        {
            _countStrongConnectedComponents += 1;
            int v;
            List<int> _strongConnectedComponent = new List<int>();
            do
            {
                v = _traceStrongConnectedComponents.Peek();
                _traceStrongConnectedComponents.Pop();
                _strongConnectedComponent.Add(v);
                _number[v] = _low[v] = int.MaxValue;
            }
            while (u != v);
            _strongConnectedComponent.Reverse();
            _strongConnectedComponents.Add(_strongConnectedComponent);
        }
    }
}
