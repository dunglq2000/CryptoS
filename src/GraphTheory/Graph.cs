using System.Text;

namespace GraphTheory;
public class Graph
{
    public int NumberOfVertices { get; }
    private int[] _matrixEdges;
    private bool[] _isVisited;
    private int[] _trace;
    private int[] _low;
    private int[] _number;
    private int _cnt;
    private int _countStrongConnectedComponents;
    private Stack<int> _traceStrongConnectedComponents;
    private List<List<int>> _strongConnectedComponents;
    public Graph(List<(int, int)> edges, int numberOfVertices = 0)
    {
        if (numberOfVertices == 0)
        {
            int max1 = edges.Max(item => item.Item1);
            int max2 = edges.Max(item => item.Item2);
            NumberOfVertices = max1 > max2 ? max1 : max2;
            NumberOfVertices += 1;
            int min1 = edges.Min(item => item.Item1);
            int min2 = edges.Min(item => item.Item2);
            if (min1 < 0 || min2 < 0)
            {
                throw new ArgumentException($"Vertex's index must be in range [0, {NumberOfVertices - 1}].");
            }
        }
        else
        {
            NumberOfVertices = numberOfVertices;
            foreach (var edge in edges)
            {
                if (
                    (edge.Item1 >= NumberOfVertices) || (edge.Item2 >= NumberOfVertices) ||
                    (edge.Item1 < 0) || (edge.Item2 < 0)
                )
                {
                    throw new ArgumentException($"Vertex's index must be in range [0, {NumberOfVertices - 1}].");
                }
            }
        }
        // construct matrix adjacency
        _matrixEdges = new int[NumberOfVertices * NumberOfVertices];
        foreach (var edge in edges)
        {
            _matrixEdges[edge.Item1 * NumberOfVertices + edge.Item2] = 1;
            _matrixEdges[edge.Item2 * NumberOfVertices + edge.Item1] = 1;
        }
        _isVisited = new bool[NumberOfVertices]; Array.Fill(_isVisited, false);
        _trace = new int[NumberOfVertices]; Array.Fill(_trace, -1);
        _low = new int[NumberOfVertices];
        _number = new int[NumberOfVertices];
        _cnt = 0;
        _countStrongConnectedComponents = 0;
        _traceStrongConnectedComponents = new Stack<int>();
        _strongConnectedComponents = new List<List<int>>();
    }
    /// <summary>
    /// Get path from vertex <c>start</c> to vertex <c>end</c>.
    /// </summary>
    /// <param name="start">Starting vertex of the path.</param>
    /// <param name="end">Ending vertex of the path.</param>
    /// <returns>If there is no path from <c>start</c> to <c>end</c>, return null. Else return a path as a list of vertices from <c>start</c> to <c>end</c>.</returns>
    public List<int>? FindPath(int start, int end)
    {
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
    public List<List<int>> GetStrongConnectedComponents()
    {
        if (_strongConnectedComponents.Count == 0)
        {
            for (int u = 0; u < NumberOfVertices; ++u)
            {
                if (_number[u] == 0)
                {
                    DFSWithConnectedComponents(u);   
                }
            }
        }
        return _strongConnectedComponents;
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm using recursive.
    /// </summary>
    /// <param name="u">Current node.</param>
    public void DFSUseRecursive(int u)
    {
        _isVisited[u] = true;
        for (int v = 0; v < NumberOfVertices; ++v)
        {
            if (_isVisited[v] == false && _matrixEdges[u * NumberOfVertices + v] == 1)
            {
                _trace[v] = u;
                DFSUseRecursive(v);
            }
        }
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm using stack.
    /// </summary>
    /// <param name="u">Current node.</param>
    public void DFSUseStack(int u)
    {
        Stack<int> vertices = new Stack<int>();
        vertices.Push(u);
        _isVisited[u] = true;
        while (vertices.Count != 0)
        {
            int s = vertices.Peek();
            vertices.Pop();
            for (int v = 0; v < NumberOfVertices; ++v)
            {
                if (_isVisited[v] == false && _matrixEdges[s * NumberOfVertices + v] == 1)
                {
                    _trace[v] = s;
                    _isVisited[v] = true;
                    vertices.Push(v);
                }
            }
        }
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with DFS algorithm and trace connected component.
    /// </summary>
    /// <param name="u">Current node.</param>
    public void DFSWithConnectedComponents(int u)
    {
        _cnt += 1;
        _number[u] = _cnt;
        _low[u] = _number[u];
        _traceStrongConnectedComponents.Push(u);
        _isVisited[u] = true;
        for (int v = 0; v < NumberOfVertices; ++v)
        {
            if (_matrixEdges[u * NumberOfVertices + v] == 1)
            {
                if (_number[v] != 0 && _isVisited[v] == true)
                {
                    _low[u] = _low[u] < _number[v] ? _low[u] : _number[v];
                }
                else
                {
                    DFSWithConnectedComponents(v);
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
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with BFS algorithm.
    /// </summary>
    /// <param name="u">Current node.</param>
    public void BFS(int u)
    {
        Queue<int> verticesQueue = new Queue<int>();
        verticesQueue.Append(u);
        _isVisited[u] = true;
        do
        {
            int i = verticesQueue.Peek();
            verticesQueue.Dequeue();
            for (int v = 0; v < NumberOfVertices; ++v)
            {
                if (_isVisited[v] == false && _matrixEdges[i * NumberOfVertices + v] == 1)
                {
                    _trace[v] = i;
                    _isVisited[v] = true;
                    verticesQueue.Enqueue(v);
                }
            }
        }
        while (verticesQueue.Count != 0);
    }
    /// <summary>
    /// Print graph as matrix adjacency:
    /// <list type="bullet">
    ///     <item>
    ///         <description><c>M[i, j] = 1</c> if there is edge from vertex <c>i</c> to vertex <c>j</c>;</description>
    ///     </item>
    ///     <item>
    ///         <description><c>M[i, j] = 0</c> if there is no edge from vertex <c>i</c> to vertex <c>j</c>.</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <returns>String of matrix adjacency.</returns>
    public override string ToString()
    {
        StringBuilder matrixStringBuilder = new StringBuilder();
        List<string> matrixStrings = new List<string>();
        for (int i = 0; i < NumberOfVertices; ++i)
        {
            StringBuilder rowStringBuilder = new StringBuilder();
            List<string> rowStrings = new List<string>();
            for (int j = 0; j < NumberOfVertices; ++j)
            {
                rowStrings.Add(_matrixEdges[i * NumberOfVertices + j].ToString());
            }
            rowStringBuilder.AppendJoin(" ", rowStrings);
            matrixStrings.Add(rowStringBuilder.ToString());
        }
        matrixStringBuilder.AppendJoin("\n", matrixStrings);
        return matrixStringBuilder.ToString();
    }
}
