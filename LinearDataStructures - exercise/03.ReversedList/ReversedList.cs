namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                if (!IsValid(index)) throw new IndexOutOfRangeException();
                return this.items[this.Count-1-index];
            }
            set
            {
                if (!IsValid(index)) throw new IndexOutOfRangeException();
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.items.Length == this.Count) AddRange();
            this.items[this.Count++] = item;
        }

        private void AddRange()
        {
            var newItemsArray = new T[this.items.Length * 2];
            for (int i = 0; i < this.Count; i++) newItemsArray[i] = this.items[i];

            this.items = newItemsArray;
        }

        public bool Contains(T item)
        {
            if (this.Count == 0) return false;
            return this.IndexOf(item) != -1;
        }
        

        public int IndexOf(T item)
        {
            var index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item)) index = i;
            }

            if (index == -1) return -1;
            
            return this.Count-1-index;
        }

        public void Insert(int index, T item)
        {
            if (!IsValid(index)) throw new IndexOutOfRangeException();
            AddRange();

            for (int i = this.Count; i >= this.Count - index; i--)
            {
                items[i] = items[i - 1];
            }

            items[Count - index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if (index != -1)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (!IsValid(index)) throw new IndexOutOfRangeException();

            index = this.Count - 1 - index;
            for (int i = index; i < this.Count; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.items[this.Count-1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count-1; i >= 0; i--) yield return this.items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private bool IsValid(int index) => index >= 0 && index < this.Count;
    }
}