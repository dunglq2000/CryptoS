namespace Misc;

/// <summary>
/// Binary search tree.
/// </summary>
/// <typeparam name="T">Data in each node.</typeparam>
public class BinaryTree<T>
{
    /// <summary>
    /// Traversal binary search tree.
    /// </summary>
    public enum TraversalType
    {
        /// <summary>
        /// Traversal by PreOrder: left node => current node => right node.
        /// </summary>
        PreOrder,
        /// <summary>
        /// Traversal by InOrder: current node => left node => right node.
        /// </summary>
        InOrder,
        /// <summary>
        /// Traversal by PostOrder: left node => right node => current node.
        /// </summary>
        PostOrder
    }
    /// <summary>
    /// Root node of the tree.
    /// </summary>
    public BinaryNode<T>? _root;
    private List<T> _paths;
    private readonly IComparer<T> _comparer;
    /// <summary>
    /// Constructor of binary search tree with a comparer for comparing data in nodes.
    /// </summary>
    /// <param name="comparer"></param>
    public BinaryTree(IComparer<T>? comparer = null)
    {
        _root = null;
        _comparer = comparer ?? Comparer<T>.Default;
        _paths = new List<T>();
    }
    /// <summary>
    /// Create a node with <paramref name="data"/> and add to binary tree.
    /// </summary>
    /// <param name="data">Data to be added.</param>
    public void Add(T data)
    {
        BinaryNode<T> node = new BinaryNode<T>(data);
        if (_root is null)
        {
            _root = node;
        }
        else
        {
            BinaryNode<T> currentNode = _root;
            while (currentNode is not null)
            {
                int compare = _comparer.Compare(node.Data, currentNode.Data);
                // Found the node with given data, do nothing!
                if (compare == 0)
                {
                    return;
                }
                // The given data is less than data in current node, move to left.
                if (_comparer.Compare(node.Data, currentNode.Data) < 0)
                {
                    if (currentNode.Left is null)
                    {
                        currentNode.Left = node;
                    }
                    else
                    {
                        currentNode = currentNode.Left;
                    }
                }
                // The given data is greater than data in current node, move to right.
                else
                {
                    if (currentNode.Right is null)
                    {
                        currentNode.Right = node;
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                    }
                }
            }
        }
    }
    /// <summary>
    /// <para>Find the index of <paramref name="data"/> in the binary tree.</para>
    /// <para>If index of any node is <c>i</c>, then left child node has index <c>2i+1</c> and right child node has index <c>2i+2</c>.</para>
    /// <para>Index of the root is 0.</para>
    /// </summary>
    /// <param name="data">Data to be found.</param>
    /// <returns>Index of node containing data, -1 if node is not found.</returns>
    public int Find(T data)
    {
        if (_root is null)
        {
            return -1;
        }
        BinaryNode<T>? currentNode = _root;
        int index = 0;
        while (currentNode is not null)
        {
            int compare = _comparer.Compare(data, currentNode.Data);
            // Found the node with given data, do nothing!
            if (compare == 0)
            {
                return index;
            }
            // The given data is less than data in current node, move to left.
            if (_comparer.Compare(data, currentNode.Data) < 0)
            {
                // end of tree => not found
                if (currentNode.Left is null)
                {
                    return -1;
                }
                // go deeper
                else
                {
                    index = index * 2 + 1;
                    currentNode = currentNode.Left;
                }
            }
            // The given data is greater than data in current node, move to right.
            else
            {
                // end of tree => not found
                if (currentNode.Right is null)
                {
                    return -1;
                }
                // go deeper
                else
                {
                    index = index * 2 + 2;
                    currentNode = currentNode.Right;
                }
            }
        }
        return index;
    }
    /// <summary>
    /// Traversal over binary tree with type traversal from <paramref name="traversalType"/>.
    /// </summary>
    /// <param name="traversalType">PreOrder, InOrder or PostOrder.</param>
    /// <returns>List of nodes from traversal path.</returns>
    public List<T> Traversal(TraversalType traversalType = TraversalType.PreOrder)
    {
        _paths.Clear();
        if (_root is null)
        {
            return _paths;
        }
        switch (traversalType)
        {
            case TraversalType.PreOrder:
                PreOrderTraversal(_root);
                break;
            case TraversalType.InOrder:
                InOrderTraversal(_root);
                break;
            case TraversalType.PostOrder:
                PostOrderTraversal(_root);
                break;
        }
        return _paths;
    }
    /// <summary>
    /// Traversal with pre-order.
    /// </summary>
    /// <param name="node">Node to be traversal.</param>
    private void PreOrderTraversal(BinaryNode<T> node)
    {
        if (node.Left is not null)
        {
            PreOrderTraversal(node.Left);
        }
        _paths.Add(node.Data);
        if (node.Right is not null)
        {
            PreOrderTraversal(node.Right);
        }
    }
    /// <summary>
    /// Traversal with in-order.
    /// </summary>
    /// <param name="node">Node to be traversal.</param>
    private void InOrderTraversal(BinaryNode<T> node)
    {
        _paths.Add(node.Data);
        if (node.Left is not null)
        {
            InOrderTraversal(node.Left);
        }
        if (node.Right is not null)
        {
            InOrderTraversal(node.Right);
        }
    }
    /// <summary>
    /// Traversal with post-order.
    /// </summary>
    /// <param name="node">Node to be traversal.</param>
    private void PostOrderTraversal(BinaryNode<T> node)
    {
        if (node.Left is not null)
        {
            PostOrderTraversal(node.Left);
        }
        if (node.Right is not null)
        {
            PostOrderTraversal(node.Right);
        }
        _paths.Add(node.Data);
    }
}
