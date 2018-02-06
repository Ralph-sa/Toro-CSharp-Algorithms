using System;
namespace DataStructures.Trees
{
    public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
    {

        private T _value;
        private BSTNode<T> _parent;
        private BSTNode<T> _left;
        private BSTNode<T> _right;

        public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
        {
            _value = value;
            _parent = parent;
            _left = left;
            _right = right;
        }
        public BSTNode() : this(default(T), 0, null, null, null) { }
        public BSTNode(T value) : this(value, 0, null, null, null) { }
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public BSTNode<T> Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public BSTNode<T> LeftChild
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }
        public BSTNode<T> RightChild
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
            }
        }
        public virtual bool HasLeftChild
        {
            get
            {
                return (_left != null);
            }
        }
        public virtual bool HasChild
        {
            get
            {
                return (this.ChildCount > 0);
            }
        }

        public virtual bool HasRightChild
        {
            get
            {
                return (_right != null);
            }
        }
        public virtual bool IsLeftChild
        {
            get
            {
                return (this.Parent != null && this.Parent.LeftChild == this);
            }
        }
        public virtual bool IsRightChild
        {
            get
            {
                return (this.Parent != null && this.Parent.RightChild == this);
            }
        }
        /// <summary>
        /// Returns number of direct descendents: 0, 1, 2 (none, left or right, or both).
        /// </summary>
        /// <returns>Number (0,1,2)</returns>
        public virtual int ChildCount
        {
            get
            {
                int count = 0;
                if (this.IsLeftChild == true)
                {
                    count++;
                }
                if (this.IsRightChild == true)
                {
                    count++;
                }
                return count;
            }
        }











        public int CompareTo(BSTNode<T> other)
        {
            if (other == null)
            {
                return -1;
            }
            return 0;//this.Value.CompareTo(other.Value);
        }
    }
}