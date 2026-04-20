namespace Misc;

public class BinaryNode<T>
{
    public T Data { get; }
    public BinaryNode<T>? Left { get; set; }
    public BinaryNode<T>? Right { get; set; }
    public BinaryNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
