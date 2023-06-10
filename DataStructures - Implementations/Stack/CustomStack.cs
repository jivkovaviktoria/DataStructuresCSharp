namespace Stack;

public class CustomStack<T>
{
    private T[] _elements;
    private int _count;

    public CustomStack()
    {
        this._elements = new T[4];
        this._count = 0;
    }

    public int Count => this._count;

    public void Push(T element)
    {
        if(this._count == this._elements.Length) this.Resize();
        this._elements[this._count++] = element;
    }

    public T Pop()
    {
        if (this._count == 0)
            throw new InvalidOperationException("The stack is empty");

        var element = this._elements[--this._count];
        this._elements[this._count] = default(T);

        return element;
    }

    public T Peek()
    {
        if (this._count == 0)
            throw new InvalidOperationException("The stack is empty");

        return this._elements[this._count-1];
    }

    public void Clear()
    {
        Array.Clear(this._elements, 0, this._count);
        this._count = 0;
    }
    
    private void Resize()
    {
        int newSize = this._elements.Length * 2;
        T[] newElements = new T[newSize];
        
        Array.Copy(this._elements, newElements, this._count);
        this._elements = newElements;
    }
}