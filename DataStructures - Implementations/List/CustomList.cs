using System.Collections;

namespace List;

public class CustomList<T> : IEnumerable<T>
{
    private T[] _elements;
    private int _count;

    public CustomList() { this._elements = new T[4]; }
    public CustomList(int capacity) { this._elements = new T[capacity]; }

    public int Count => this._count;
    
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this._elements.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            return this._elements[index];
        }
        set
        {
            if (index < 0 || index >= this._elements.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            this._elements[index] = value;
        }
    }

    public bool Contains(T element) => this.IndexOf(element) > 0;

    public void Add(T element)
    {
        if(this._count == this._elements.Length) this.Resize();
        this._elements[this._count++] = element;
    }

    public int IndexOf(T element)
    {
        for (int i = 0; i < this._count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(element, this._elements[i])) return i;
        }

        return -1;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= this._count)
            throw new ArgumentOutOfRangeException(nameof(index));
        
        Array.Copy(this._elements, index+1, this._elements, index, this._count-index-1);
        this._count--;
        this._elements[this._count] = default(T);
    }

    public bool Remove(T element)
    {
        var index = this.IndexOf(element);

        if (index >= 0)
        {
            this.RemoveAt(index);
            return true;
        }

        return false;
    }

    private void Resize()
    {
        int newCapacity = this._elements.Length * 2;
        T[] newArray = new T[newCapacity];
        
        Array.Copy(this._elements, newArray, _count);
        this._elements = newArray;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this._count; i++)
            yield return this._elements[i];
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}