# Binary search tree

**WARNING**: this part is not fully constructed. These classes now are in namespace `CryptoS.Misc` but will be reconstructed.

- `BinaryNode.cs`: node on binary search tree. Each node consists of $3$ field:
  
  - `Data` - data of the node;
  - `Left` - pointer to the node on the left;
  - `Right` - pointer to the node on the right.

- `BinaryTree.cs`: binary search tree, which must have an `Comparer<T>` for comparing data and implementing following features:

  1. Add element into tree:
     
     - if new element is less than current element in the current node (from above comparer) -> go to subtree on the left;
     - if new element is greater than current element in the current node (from above comparer) -> go to subtree on the right.

  2. Find element in binary search tree:

     - root of tree has index $0$;
     - if any node has index $i$, then

       - node on the left has index $2i + 1$;
       - node on the right has index $2i + 2$.

  3. Traverse through the tree by $3$ ways:

     - `InOrder` - current node => left child => right child;
     - `PreOrder` - left child => current node => right child;
     - `PostOrder` - left child => right child => current node.
     
For example:

```cs
using CryptoS.Misc;

BinaryTree<int> binaryTree = new BinaryTree<int>();
for (int i = 0; i < 5; ++i)
{
    binaryTree.Add((i + 2) % 5);
}
List<int> nodes = binaryTree.Traversal(BinaryTree<int>.TraversalType.InOrder);
var result = nodes.SequenceEqual([2, 0, 1, 3, 4]);
```
