namespace GraphTheory;

/// <summary>
/// Abstract class for graph.
/// </summary>
public abstract class Graph
{
    /// <summary>
    /// Enum for traversing algorithm.
    /// </summary>
    public enum TraversingAlgorithm
    {
        /// <summary>
        /// Traverse graph with recursive DFS.
        /// </summary>
        DFSRecurse,
        /// <summary>
        /// Traverse graph with DFS and stack.
        /// </summary>
        DFSStack,
        /// <summary>
        /// Traverse graph with BFS.
        /// </summary>
        BFS
    }
    /// <summary>
    /// The number of vertices in the graph.
    /// </summary>
    public int NumberOfVertices { get; }
    /// <summary>
    /// Adjacency matrix for graph.
    /// </summary>
    public int[] Matrix { get; }
    /// <summary>
    /// Adjacency list for graph (has not been used in graph traversal).
    /// </summary>
    public List<HashSet<int>> AdjacencyMatrix;
    protected Dictionary<TraversingAlgorithm, Traversal.GraphTraversal> _algorithmTraversal;
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
        // construct adjacency matrix
        Matrix = new int[NumberOfVertices * NumberOfVertices];
        // construct adjacency list
        AdjacencyMatrix = new List<HashSet<int>>();
        for (int i = 0; i < NumberOfVertices; ++i)
        {
            AdjacencyMatrix.Add(new HashSet<int>());
        }
        // init algorithm for graph traversal
        _algorithmTraversal = new Dictionary<TraversingAlgorithm, Traversal.GraphTraversal>
        {
            { TraversingAlgorithm.DFSRecurse, new Traversal.GraphDFSRecurse(this) },
            { TraversingAlgorithm.DFSStack, new Traversal.GraphDFSStack(this) },
            { TraversingAlgorithm.BFS, new Traversal.GraphBFS(this) }
        };
    }
    /// <summary>
    /// Find path from vertex <paramref name="start"/> to vertex <paramref name="end"/> using algorithm in <paramref name="algorithm"/>.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public abstract List<int>? FindPath(int start, int end, TraversingAlgorithm algorithm = TraversingAlgorithm.DFSRecurse);
    public abstract List<List<int>> GetConnectedComponents(TraversingAlgorithm algorithm = TraversingAlgorithm.DFSRecurse);
}
