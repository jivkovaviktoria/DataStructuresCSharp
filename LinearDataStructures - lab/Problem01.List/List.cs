using System.Linq;

namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List() : this(DEFAULT_CAPACITY){}

        public List(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException();
            this.items = new T[capacity];
        }
        
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count - 1) throw new IndexOutOfRangeException();
                return this.items[index];
            }
            set
            {
                if (index < 0 || index > this.Count - 1) throw new IndexOutOfRangeException();
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            AddRange();
            this.items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            foreach (var el in this.items)
            {
                if (el.Equals(item)) return true;
            }

            return false;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item)) return i;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            AddRange();
            if (index < 0 || index > this.Count - 1) throw new IndexOutOfRangeException();
            
            for (int i = this.Count; i > index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            if (!this.Contains(item)) return false;
            
            var index = this.IndexOf(item);
            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > this.Count - 1) throw new IndexOutOfRangeException();
            for (int i = index; i <= this.Count; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void AddRange()
        {
            if (this.Count == this.items.Length)
            {
                T[] itemsCopy = new T[this.items.Length * 2];
                for (int i = 0; i < this.items.Length; i++)
                {
                    itemsCopy[i] = this.items[i];
                }

                this.items = itemsCopy;
            }
        }
    }
}