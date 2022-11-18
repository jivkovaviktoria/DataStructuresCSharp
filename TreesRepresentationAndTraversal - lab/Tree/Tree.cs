using System.Collections;
using System.Linq;

namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        public List<Tree<T>> Children = new List<Tree<T>>();
        public T Value;
        public Tree<T> Parent;

        public Tree(T value)
        {
            this.Value = value;
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var node = FindNode(parentKey);
            if (node != null)
            {
                node.Children.Add(child);
                child.Parent = node;
            }
            else throw new ArgumentNullException();
        }

        private Tree<T> FindNode(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(parentKey)) return subtree;
                foreach (var child in subtree.Children) queue.Enqueue(child);
            }

            return null;
        }

        public List<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();
                result.Add(subtree.Value);

                foreach (var child in subtree.Children) queue.Enqueue(child);
            }

            return result;
        }

        public List<T> OrderDfs()
        {
            var result = new List<T>();
            Dfs(this, result);

            return result;
        }

        private void Dfs(Tree<T> node, IList<T> result)
        {
            foreach (var child in node.Children) Dfs(child, result);
            result.Add(node.Value);
        }

        public void RemoveNode(T nodeKey)
        {
            var node = FindNode(nodeKey);
            if (node is null) throw new ArgumentNullException();

            if (node.Parent is null) throw new ArgumentException();

            node.Parent.Children.Remove(node);
            node.Parent = null;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = FindNode(firstKey);
            var secondNode = FindNode(secondKey);

            if (firstNode is null || secondNode is null) throw new ArgumentNullException();
            if (firstNode.Parent is null || secondNode.Parent is null) throw new ArgumentException();


            var parentFirstNode = firstNode.Parent;
            var parentSecondNode = secondNode.Parent;

            var indexOfFirstNode = parentFirstNode.Children.IndexOf(firstNode);
            var indexOfSecondNode = parentSecondNode.Children.IndexOf(secondNode);

            if (firstNode.IsParentOf(secondNode))
            {
                parentFirstNode.Children[indexOfFirstNode] = secondNode;
                secondNode.Parent = parentFirstNode;
                firstNode.Parent = null;
            }
            else if (secondNode.IsParentOf(firstNode))
            {
                parentSecondNode.Children[indexOfSecondNode] = firstNode;
                firstNode.Parent = parentSecondNode;
                secondNode.Parent = null;
            }
            else
            {
                parentFirstNode.Children[indexOfFirstNode] = secondNode;
                parentSecondNode.Children[indexOfSecondNode] = firstNode;

                secondNode.Parent = parentFirstNode;
                firstNode.Parent = parentSecondNode;
            }
        }

        private bool IsParentOf(Tree<T> other)
        {
            Tree<T> iter = other;

            while (iter != null)
            {
                if (iter == this) return true;
                iter = iter.Parent;
            }

            return false;
        }
    }
}
