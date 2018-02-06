using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.Common;

namespace DataStructures.Trees
{
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        public BSTNode<T> Root { get; }
        public int Count { get; }
        public bool IsEmpty { get; }
        public int Height { get; }
        public bool AllowsDuplicates { get; }
        public void Insert(T item)
        {
        }
        public void Insert(T[] collection)
        {

        }
        public void RemoveMin()
        {

        }
        public void RemoveMax()
        {

        }
        public void Remove(T item)
        {

        }
        public void Contains(T item)
        {

        }
        public T FindMin()
        {
            return default(T);
        }
        public T FindMax()
        {
            return default(T);
        }
        public T Find(T item)
        {
            return default(T);
        }
        public IEnumerable<T> FindAll(System.Predicate<T> searchPredicate)
        {
            return null;
        }
        public T[] ToArray()
        {
            return null;
        }
        public List<T> ToList()
        {
            return null;
        }
        public IEnumerable<T> GetPreOrderEnumerator()
        {
            return null;
        }
        public IEnumerable<T> GetInOrderEnumerator()
        {
            return null;
        }
        public IEnumerable<T> GetPostOrderEnumerator()
        {
            return null;
        }
        public void Clear()
        {
        }
    }
}