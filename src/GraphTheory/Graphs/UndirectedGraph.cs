using System.Text;
namespace GraphTheory;

/// <summary>
/// Để khởi tạo đồ thị (vô hướng) chương trình hiện tại cung cấp constructor:
/// </summary>
public class UndirectedGraph : Graph
{
    /// <summary>
    /// Constructor cho đồ thị vô hướng.
    /// </summary>
    /// <param name="edges">Danh sách cạnh của đồ thị vô hướng.</param>
    /// <param name="numberOfVertices">Số lượng đỉnh của đồ thị.</param>
    /// <remarks>
    /// <para>Đối với đồ thị vô hướng, nếu đồ thị có cạnh <c>(u, v)</c> thì cũng có cạnh <c>(v, u)</c>. Do đó chỉ cần truyền một trong hai cạnh.</para>
    /// <para>Đỉnh của đồ thị được đánh số từ 0 tới <paramref name="numberOfVertices"/> - 1.</para>
    /// <para>Nếu <paramref name="numberOfVertices"/> không được chỉ định thì dựa trên index cao nhất của đỉnh để xác định đỉnh.</para>
    /// </remarks>
    /// <example>
    /// Để tạo đồ thị cần danh sách các cạnh dưới dạng <c>List&lt;(int, int)&gt;</c>:
    /// <code>
    /// using GraphTheory;
    /// Graph graph = new UndirectedGraph(
    ///     new List&lt;(int, int)&gt;{
    ///         (0, 1), (1, 2), (2, 3)
    ///     }, 6
    /// );
    /// </code>
    /// </example>
    public UndirectedGraph(List<(int, int)> edges, int numberOfVertices = 0) : base(edges, numberOfVertices)
    {
        foreach (var edge in edges)
        {
            Matrix[edge.Item1 * NumberOfVertices + edge.Item2] = 1;
            Matrix[edge.Item2 * NumberOfVertices + edge.Item1] = 1;
        }
        foreach (var edge in edges)
        {
            AdjacencyMatrix[edge.Item1].Add(edge.Item2);
            AdjacencyMatrix[edge.Item2].Add(edge.Item1);
        }
    }
    /// <summary>
    /// Generate spanning tree of graph.
    /// </summary>
    /// <param name="u">Current vertex.</param>
    /// <returns>New graph which is spanning tree for this graph.</returns>
    public UndirectedGraph? GetSpanningTree(int u)
    {
        bool[] seen = new bool[NumberOfVertices];
        int[] trace = new int[NumberOfVertices];
        Array.Fill(trace, -1);
        List<(int, int)> edgesSpanningTree = new List<(int, int)>();
        Stack<int> vertices = new Stack<int>();
        vertices.Push(u);
        while (vertices.Count > 0)
        {
            int s = vertices.Peek();
            vertices.Pop();
            for (int v = 0; v < NumberOfVertices; ++v)
            {
                if (seen[v] == false && Matrix[s * NumberOfVertices + v] == 1)
                {
                    trace[v] = s;
                    edgesSpanningTree.Add((s, v));
                    seen[v] = true;
                    vertices.Push(v);
                }
            }
        }
        if (edgesSpanningTree.Count == 0)
        {
            return null;
        }
        else
        {
            return new UndirectedGraph(edgesSpanningTree);
        }
    }
    /// <summary>
    /// Find path between two vertices.
    /// </summary>
    /// <param name="start">Starting vertex.</param>
    /// <param name="end">Ending vertex.</param>
    /// <param name="algorithm">Algorithm for traversing graph.</param>
    /// <returns>A list contains the path from starting vertex <paramref name="start"/> to ending vertex <paramref name="end"/>. Returns null if such path cannot be found.</returns>
    /// <exception cref="ArgumentException">Algorithm for traversing graph is not implemented.</exception>
    /// <example>
    /// Ví dụ
    /// <code>
    /// Graph graph = new UndirectedGraph(
    ///     new List&lt;(int, int)&gt;
    ///     {
    ///         (0, 1), (1, 2), (2, 3), (4, 5)
    ///     }, 6
    /// );
    /// List&lt;int&gt;? paths = graph.FindPath(0, 3, Graph.TraversingAlgorithm.DFSRecurse);
    /// bool result = paths is not null &amp;&amp; paths.SequenceEqual([0, 1, 2, 3]);
    /// </code>
    /// trả về true vì có đường đi từ đỉnh 0 tới đỉnh 3 là 0 -> 1 -> 2 -> 3.
    /// </example>
    public override List<int>? FindPath(int start, int end, TraversingAlgorithm algorithm)
    {
        if (_algorithmTraversal.ContainsKey(algorithm) == false)
        {
            throw new ArgumentException("Algorithm not implemented.");
        }
        return _algorithmTraversal[algorithm].FindPath(start, end);
    }
    /// <summary>
    /// Find connected components of the undirected graph.
    /// </summary>
    /// <param name="algorithm">Algorithm for traversing graph.</param>
    /// <returns>A list of lists, each list contains all vertices lies in the same connected component.</returns>
    /// <exception cref="ArgumentException">Algorithm for traversing graph is not implemented.</exception>
    public override List<List<int>> GetConnectedComponents(TraversingAlgorithm algorithm = TraversingAlgorithm.DFSRecurse)
    {
        if (_algorithmTraversal.ContainsKey(algorithm) == false)
        {
            throw new ArgumentException("Algorithm not implemented.");
        }
        return _algorithmTraversal[algorithm].GetConnectedComponents();
    }
    /// <summary>
    /// Print graph as matrix adjacency:
    /// <list type="bullet">
    /// <item>
    ///     <description><c>M[i, j] = 1</c> if there is edge from vertex <c>i</c> to vertex <c>j</c>;</description>
    /// </item>
    /// <item>
    ///     <description><c>M[i, j] = 0</c> if there is no edge from vertex <c>i</c> to vertex <c>j</c>.</description>
    /// </item>
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
                rowStrings.Add(Matrix[i * NumberOfVertices + j].ToString());
            }
            rowStringBuilder.AppendJoin(" ", rowStrings);
            matrixStrings.Add(rowStringBuilder.ToString());
        }
        matrixStringBuilder.AppendJoin("\n", matrixStrings);
        return matrixStringBuilder.ToString();
    }
}
