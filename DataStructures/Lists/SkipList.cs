using System;
using System.Collections.Generic;
using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Lists
{
    public class SkipList<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T>
    {
        private int _count { get; set; }
        private int _currentMaxLevel { get; set; }
        private Random _randomizer { get; set; }
        private SkipListNode<T> _firstNode { get; set; }
        private readonly int MaxLevel = 32;
        private readonly double probability = 0.5;



        private int _getNextLevel()
        {
            int lvl = 0;
            while (_randomizer.NextDouble() < probability && lvl <= _currentMaxLevel && lvl < MaxLevel)
            {
                ++lvl;
            }
            return lvl;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SkipList()
        {
            _count = 0;
            _currentMaxLevel = 1;
            _randomizer = new Random();
            _firstNode = new SkipListNode<T>(default(T), MaxLevel);
            for (int i = 0; i < MaxLevel; ++i)
            {
                _firstNode.Forwards[i] = _firstNode;
            }
        }

        public SkipListNode<T> Root
        {
            get { return _firstNode; }
        }
        public bool IsEmpty
        {
            get { return Count == 0; }
        }
        public int Count
        {
            get { return _count; }
        }
        public int Level
        {
            get { return _currentMaxLevel; }
        }
        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        public void Add(T item)
        {
            var current = _firstNode;
            var toBeUpdated = new SkipListNode<T>[MaxLevel];
            for (int i = _currentMaxLevel - 1; i >= 0; --i)
            {
                while (current.Forwards[i] != _firstNode && current.Forwards[i].Value.IsLessThan(item))
                {
                    current = current.Forwards[i];
                }
                toBeUpdated[i] = current;
            }
            current = current.Forwards[0];

            int lvl = _getNextLevel();
            if (lvl > _currentMaxLevel)
            {
                for (int i = _currentMaxLevel; i < lvl; ++i)
                {
                    toBeUpdated[i] = _firstNode;
                }
                _currentMaxLevel = lvl;
            }

            var newNode = new SkipListNode<T>(item, lvl);
            for (int i = 0; i < lvl; ++i)
            {
                newNode.Forwards[i] = toBeUpdated[i].Forwards[i];
                toBeUpdated[i].Forwards[i] = newNode;
            }
            ++_count;
        }

        public bool Remove(T item)
        {
            return false;
        }

        public bool Contains(T item)
        {
            T result;
            return Find(item, out result);
        }
        public bool Find(T item, out T result)
        {
            var current = _firstNode;
            for (int i = _currentMaxLevel - 1; i >= 0; --i)
            {
                while (current.Forwards[i] != _firstNode && current.Forwards[i].Value.IsLessThan(item))
                {
                    current = current.Forwards[i];
                }
            }
            current = current.Forwards[0];
            if (current.Value.IsEqualTo(item))
            {
                result = current.Value;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }


        #region IEnumerable<T> Implementation
        public IEnumerator<T> GetEnumerator()
        {
            var node = _firstNode;
            while (node.Forwards[0] != null && node.Forwards[0] != _firstNode)
            {
                node = node.Forwards[0];
                yield return node.Value;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable<T> Implementation

        #region ICollection<T> Implementation
        public bool IsReadOnly
        {
            get { return false; }
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            // Validate the array and arrayIndex
            if (array == null)
                throw new ArgumentNullException();
            else if (array.Length == 0 || arrayIndex >= array.Length || arrayIndex < 0)
                throw new IndexOutOfRangeException();

            // Get enumerator
            var enumarator = this.GetEnumerator();

            // Copy elements as long as there is any in the list and as long as the index is within the valid range
            for (int i = arrayIndex; i < array.Length; ++i)
            {
                if (enumarator.MoveNext())
                    array[i] = enumarator.Current;
                else
                    break;
            }
        }
        public void Clear()
        {
            _count = 0;
            _currentMaxLevel = 1;
            _randomizer = new Random();
            _firstNode = new SkipListNode<T>(default(T), MaxLevel);

            for (int i = 0; i < MaxLevel; ++i)
                _firstNode.Forwards[i] = _firstNode;
        }

        #endregion
    }
}