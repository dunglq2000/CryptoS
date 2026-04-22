namespace Misc;

/// <summary>
/// Node for binary search tree.
/// </summary>
/// <typeparam name="T">Type of data in node.</typeparam>
public class BinaryNode<T>
{
    /// <summary>
    /// Data of the node.
    /// </summary>
    public T Data { get; }
    /// <summary>
    /// Pointer to node on the left, whose data is less than data in current node.
    /// </summary>
    public BinaryNode<T>? Left { get; set; }
    /// <summary>
    /// Pointer to node on the right, whose data is greater than data in current node.
    /// </summary>
    public BinaryNode<T>? Right { get; set; }
    /// <summary>
    /// Constructor for creating node with given data.
    /// </summary>
    /// <param name="data">Data in the node.</param>
    public BinaryNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
