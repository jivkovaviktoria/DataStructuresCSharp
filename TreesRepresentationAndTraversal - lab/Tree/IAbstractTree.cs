namespace Tree
{
    using System.Collections.Generic;

    interface IAbstractTree<T>
    {
        List<T> OrderBfs();

        List<T> OrderDfs();

        void AddChild(T parentKey, Tree<T> child);

        void RemoveNode(T nodeKey);

        void Swap(T firstKey, T secondKey);
    }
}
