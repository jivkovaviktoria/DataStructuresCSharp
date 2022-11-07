namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(T element)
            {
                this.Element = element;
            }
        }

        private Node head;
        private Node tail;
        
        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node(item);
            if (this.head == null) this.head = this.tail = newNode;
            else
            {
                this.head.Previous = newNode;
                newNode.Next = this.head;
                this.head = newNode;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);
            if (this.head == null) this.head = this.tail = newNode;
            else
            {
                this.tail.Next = newNode;
                newNode.Previous = this.tail;
                this.tail = newNode;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.Count == 0) throw new InvalidOperationException();
            return this.tail.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null) throw new InvalidOperationException();
            var node = this.head;
            
            if (this.head.Next == null)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.Next;
                this.head.Previous = null;
            }

            this.Count--;
            return node.Element;
        }

        public T RemoveLast()
        {
            if (this.tail == null) throw new InvalidOperationException();
            
            var node = this.tail;
            if (this.tail.Previous == null)
            {
                this.tail = this.head = null;
            }
            else
            {
                this.tail = this.tail.Previous;
                this.tail.Next = null;
            }

            this.Count--;
            return node.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;
            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}