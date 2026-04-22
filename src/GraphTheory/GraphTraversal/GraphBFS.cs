namespace CryptoS.GraphTheory.Traversal;

/// <summary>
/// Class for traversing graph with BFS algorithm.
/// </summary>
public class GraphBFS : GraphTraversal
{
    /// <summary>
    /// Constructor for traversing through graph.
    /// </summary>
    /// <param name="graph"></param>
    public GraphBFS(Graph graph) : base(graph)
    {
        
    }
    /// <summary>
    /// Traverse graph at the node <paramref name="u"/> with BFS algorithm.
    /// </summary>
    /// <param name="u">Current node.</param>
    public override void Traverse(int u)
    {
        Queue<int> verticesQueue = new Queue<int>();
        verticesQueue.Enqueue(u);
        _isVisited[u] = true;
        _currentConnectedComponent.Add(u);
        do
        {
            int i = verticesQueue.Peek();
            verticesQueue.Dequeue();
            for (int v = 0; v < _graph.NumberOfVertices; ++v)
            {
                if (_isVisited[v] == false && _graph.Matrix[i * _graph.NumberOfVertices + v] == 1)
                {
                    _trace[v] = i;
                    _isVisited[v] = true;
                    verticesQueue.Enqueue(v);
                    _currentConnectedComponent.Add(u);
                }
            }
        }
        while (verticesQueue.Count > 0);
    }
}
