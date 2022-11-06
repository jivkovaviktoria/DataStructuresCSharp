namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Element = element;
            }

            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }
            
            public T Element { get; set; }
            public Node Next { get; set; }
        }
        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = this.head;
            this.head = new Node(item, node);
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);
            if (this.head == null)
            {
                this.head = newNode;
            }
            else
            {
                var node = this.head;
                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            if (this.head == null) throw new InvalidOperationException();
            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.head == null) throw new InvalidOperationException();

            var node = this.head;
            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null) throw new InvalidOperationException();

            var oldHead = this.head;
            this.head = oldHead.Next;

            this.Count--;
            return oldHead.Element;
        }

        public T RemoveLast()
        {
            if (this.head == null) throw new InvalidOperationException();
            if (this.head.Next == null)
            {
                var x = this.head;
                this.head = null;
                this.Count--;
                return x.Element;
            }
            
            var node = this.head;

            while (node.Next.Next != null)
            {
                node = node.Next;
            }

            var nodeToRemove = node.Next;
            node.Next = null;

            this.Count--;
            return nodeToRemove.Element;
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

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}