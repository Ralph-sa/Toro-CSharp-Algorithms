using System;
namespace DataStructures.Lists
{
    public class SkipListNode<T> : IComparable<SkipListNode<T>> where T : IComparable<T>
    {
        private T _value;
        private SkipListNode<T>[] _forwards;

        public SkipListNode(T value, int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException("Invalid value for level");
            Value = value;
            Forwards = new SkipListNode<T>[level];
        }

        public virtual T Value
        {
            get { return this._value; }
            private set { this._value = value; }
        }
        public virtual SkipListNode<T>[] Forwards
        {
            get { return this._forwards; }
            private set { this._forwards = value; }
        }
        public virtual int Level
        {
            get { return Forwards.Length; }
        }



        public int CompareTo(SkipListNode<T> other)
        {
            if (other == null)
            {
                return -1;
            }
            else
            {
                return this.Value.CompareTo(other.Value);
            }
        }
    }
}