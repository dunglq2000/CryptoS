namespace GraphTheory.Traversal.ConnectedComponents;

public abstract class GraphStrongConnectedComponents : GraphTraversal
{
    protected int[] _number;
    protected int _cnt;
    protected int[] _low;
    protected int _countStrongConnectedComponents;
    protected Stack<int> _traceStrongConnectedComponents;
    protected List<List<int>> _strongConnectedComponents;
    public GraphStrongConnectedComponents(UndirectedGraph graph) : base(graph)
    {
        _low = new int[graph.NumberOfVertices];
        _number = new int[graph.NumberOfVertices];
        _cnt = 0;
        _countStrongConnectedComponents = 0;
        _traceStrongConnectedComponents = new Stack<int>();
        _strongConnectedComponents = new List<List<int>>();
    }
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

public class GraphStrongConnectedComponentsDFS : GraphStrongConnectedComponents
{
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
