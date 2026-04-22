namespace GraphTheory.Traversal;

/// <summary>
/// Abstract class for algorithms traversing graph.
/// </summary>
public abstract class GraphTraversal
{
    protected bool[] _isVisited;
    protected int[] _trace;
    protected readonly Graph _graph;
    protected List<int> _currentConnectedComponent;
    public GraphTraversal(Graph graph)
    {
        _graph = graph;
        _isVisited = new bool[graph.NumberOfVertices];
        _trace = new int[graph.NumberOfVertices];
        Array.Fill(_trace, -1);
        _currentConnectedComponent = new List<int>();
    }
    public abstract void Traverse(int u);
    /// <summary>
    /// Get path from vertex <c>start</c> to vertex <c>end</c>.
    /// </summary>
    /// <param name="start">Starting vertex of the path.</param>
    /// <param name="end">Ending vertex of the path.</param>
    /// <returns>If there is no path from <c>start</c> to <c>end</c>, return null. Else return a path as a list of vertices from <c>start</c> to <c>end</c>.</returns>
    public List<int>? FindPath(int start, int end)
    {
        if (_isVisited[start] == false)
        {
            Traverse(start);   
        }
        List<int> paths = [end];
        for (int i = 1; i < _trace.Length; ++i)
        {
            int last = paths[paths.Count - 1];
            if (last == -1)
            {
                break;
            }
            paths.Add(_trace[last]);
            if (_trace[last] == start)
            {
                break;
            }
        }
        if (paths[paths.Count - 1] != start)
        {
            return null;
        }
        paths.Reverse();
        return paths;
    }
    public List<List<int>> GetConnectedComponents()
    {
        List<List<int>> connectedComponents = new List<List<int>>();
        Array.Fill(_isVisited, false);
        for (int v = 0; v < _graph.NumberOfVertices; ++v)
        {
            if (_isVisited[v] == false)
            {
                Traverse(v);
                // after having traversed all vertices from vertex v, 
                // those vertices are in _currentConnectedComponent
                connectedComponents.Add(_currentConnectedComponent);
                _currentConnectedComponent.Clear();
            }
        }
        return connectedComponents;
    }
}
