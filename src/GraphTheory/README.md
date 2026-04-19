# Lý thuyết đồ thị

Để khởi tạo đồ thị hiện tại cung cấp constructor:

- truyền vào danh sách cạnh: danh sách các cặp $(u, v)$ chỉ cạnh từ đỉnh $u$ tới đỉnh $v$. Nếu đồ thị có $n$ đỉnh (tương ứng với tham số `numberOfVertices`) thì các đỉnh được đánh số từ $0$ tới $n-1$.

Để sử dụng thuật toán DFS cho đỉnh $i$:

```cs
using GraphTheory;

Graph graph = new Graph(
    new List<(int, int)>{
        (0, 1), (1, 2), (2, 3)
    }, 6
);

graph.DFS(0);
```

Để tìm đường đi từ đỉnh $u$ tới đỉnh $v$, đầu tiên cần chỉ định một thuật toán tìm đường từ $u$ (DFS hoặc BFS), sau đó gọi `FindPath` để nhận được đường đi từ đỉnh $u$ tới đỉnh $v$:

```cs
using System.Text;
using GraphTheory;

Graph graph = new Graph(
    new List<(int, int)>{
        (0, 1), (1, 2), (2, 3)
    }, 6
);

int start = 0, end = 3;
graph.DFS(start);
List<int>? paths = graph.FindPath(start, end);
if (paths == null)
{
    Console.WriteLine($"No path found from vertex {start} to {end}.");
}
else
{
    // Print path as start = u_1 -> u_2 -> ... -> u_k = end
    StringBuilder stringBuilder = new StringBuilder();
    List<string> verticesString = paths.Select(item => item.ToString()).ToList();
    stringBuilder.AppendJoin(" -> ", verticesString);
    Console.WriteLine($"Path from {start} to {end}: {stringBuilder}");
}
```

Để tìm tất cả thành phần liên thông mạnh của đồ thị gọi hàm `GetStrongConnectedComponents`:

```cs
using System.Text;
using GraphTheory;

Graph graph = new Graph(
    new List<(int, int)>{
        (0, 1), (1, 2), (2, 3)
    }, 6
);

List<List<int>> connectedComponents = graph.GetStrongConnectedComponents();

// Print each connected component with its own vertices
for (int i = 0; i < connectedComponents.Count; ++i)
{
    StringBuilder stringBuilder = new StringBuilder();
    List<string> verticesInConnectedComponent = connectedComponents[i].Select(item => item.ToString()).ToList();
    stringBuilder.AppendJoin(", ", verticesInConnectedComponent);
    Console.WriteLine($"Strong connected component: {i + 1}");
    Console.WriteLine(stringBuilder.ToString());
}
```
