using System.Collections.Generic;
using System;
namespace DataStructures.Trees
{
    public interface IBinarySearchTree<T> where T : System.IComparable<T>
    {
        BSTNode<T> Root { get; }
        int Count { get; }
        bool IsEmpty { get; }
        int Height { get; }
        bool AllowsDuplicates { get; }
        void Insert(T item);
        void Insert(T[] collection);
        void Insert(List<T> collection);
        void RemoveMin();
        void RemoveMax();
        void Remove(T item);
        bool Contains(T item);
        T FindMin();
        T FindMax();
        T Find(T item);
        IEnumerable<T> FindAll(System.Predicate<T> searchPredicate);
        T[] ToArray();
        List<T> ToList();
        IEnumerable<T> GetPreOrderEnumerator();
        IEnumerable<T> GetInOrderEnumerator();
        IEnumerable<T> GetPostOrderEnumerator();
        void Clear();
    }

}