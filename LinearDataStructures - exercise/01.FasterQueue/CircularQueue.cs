namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private T[] items;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = 4)
        {
            this.items = new T[capacity];
        }
        public int Count { get; private set; }
        public T Dequeue()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            
            var item = this.items[this.startIndex];
            this.startIndex = (this.startIndex + 1) % this.items.Length;
            
            this.Count--;

            return item;
        }

        public void Enqueue(T item)
        {
            if(this.Count >= this.items.Length) AddRange();
            
            this.items[endIndex] = item;
            this.endIndex = (this.endIndex + 1) % this.items.Length;
            
            this.Count++;
        }

        public T Peek()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return this.items[this.endIndex&this.items.Length];
        }

        public T[] ToArray()
        {
            if (this.Count == 0) return new T[0];
            return this.CopyElements(new T[this.Count]);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                var index = (startIndex + i) % this.items.Length;
                yield return this.items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void AddRange()
        {
            this.items = this.CopyElements(new T[this.items.Length*2]);
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private T[] CopyElements(T[] elements)
        {
            var oldStartIndex = this.startIndex;
            
            for (int i = 0; i < this.Count; i++)
            {
                elements[i] = this.items[(this.startIndex+i)%this.items.Length];
            }

            return elements;
        }
    }

}
