using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.Common;
/// <summary>
/// 二叉查找树特点: 对于每一个节点n 1.左子树的每一个后代节点的值都小于节点n的值 2.右子树每个后代节点的值都大于节点n的值
/// </summary>
namespace DataStructures.Trees
{
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        public enum TraversalMode
        {
            InOrder = 0,
            PreOrder = 1,
            PostOrder = 2,
        }
        public BinarySearchTree(int _count, bool _allowDuplicates)
        {
            this._count = _count;
            this._allowDuplicates = _allowDuplicates;

        }
        protected int _count { get; set; }
        protected bool _allowDuplicates { get; set; }
        protected virtual BSTNode<T> _root { get; set; }


        public virtual BSTNode<T> Root
        {
            get { return this._root; }
            set { this._root = value; }
        }
        public BinarySearchTree()
        {
            _count = 0;
            _allowDuplicates = true;
            _root = null;
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }
        /// <summary>
        /// 节点替换
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        protected virtual void _replaceNodeInParent(BSTNode<T> node, BSTNode<T> newNode = null)
        {
            if (node.Parent != null)
            {
                if (node.IsLeftChild)
                {
                    node.Parent.LeftChild = newNode;
                }
                else
                {
                    node.Parent.RightChild = newNode;
                }
            }
            else
            {
                if (newNode != null)
                {
                    node = newNode;
                }
            }
        }
        /// <summary>
        /// 删除 remove
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual bool _remove(BSTNode<T> node)
        {
            if (node == null)
            {
                return false;
            }
            var parent = node.Parent;
            if (node.ChildCount == 0)
            {
                _replaceNodeInParent(node, null);
                _count--;
            }
            else if (node.ChildCount == 2)
            {
                var sucessor = node.RightChild;
                node.Value = sucessor.Value;
                _count--;
                return (true && _remove(sucessor));
            }
            else if (node.HasLeftChild)
            {
                _replaceNodeInParent(node, node.LeftChild);
                _count--;
                return true;
            }
            else if (node.HasRightChild)
            {
                _replaceNodeInParent(node, node.RightChild);
                _count--;
                return true;
            }
            return false;
        }


        protected virtual bool _insertNode(BSTNode<T> newNode)
        {
            if (Root == null)
            {
                Root = newNode;
                _count++;
                return true;
            }
            else
            {
                if (newNode.Parent == null)
                {
                    newNode.Parent = this.Root;
                    _count++;
                }
                if (_allowDuplicates == false && newNode.Value.IsEqualTo(Root.Value))
                {
                    return false;
                }
                if (newNode.Parent.Value.IsGreaterThan(newNode.Value))
                {
                    if (newNode.Parent.HasLeftChild == false)
                    {
                        newNode.Parent.LeftChild = newNode;
                        _count++;
                    }
                    else
                    {
                        newNode.Parent = newNode.Parent.LeftChild;
                        return _insertNode(newNode);
                    }
                }
                else
                {
                    if (newNode.Parent.HasRightChild == false)
                    {
                        newNode.Parent.RightChild = newNode;
                        _count++;
                    }
                    else
                    {
                        newNode.Parent = newNode.Parent.RightChild;
                        return _insertNode(newNode);
                    }
                }

            }
            return false;
        }

        protected virtual int _getHeight(BSTNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                if (node.ChildCount == 0)
                {
                    return 1;
                }
                else if (node.HasLeftChild)
                {
                    return (1 + _getHeight(node.LeftChild));
                }
                else if (node.HasRightChild)
                {
                    return (1 + _getHeight(node.RightChild));
                }
            }
            return 0;
        }

        protected virtual BSTNode<T> _find(BSTNode<T> currentNode, T item)
        {
            if (currentNode == null)
            {
                return null;
            }
            else
            {
                if (currentNode.Value.IsEqualTo(item))
                {
                    return currentNode;
                }
                else if (currentNode.HasLeftChild && currentNode.Value.IsGreaterThan(item))
                {
                    return _find(currentNode.LeftChild, item);
                }
                else if (currentNode.HasRightChild && currentNode.Value.IsLessThan(item))
                {
                    return _find(currentNode.RightChild, item);
                }
            }
            return null;
        }

        protected virtual BSTNode<T> _findMinNode(BSTNode<T> currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            else
            {
                while (currentNode.HasLeftChild)
                {
                    currentNode = currentNode.LeftChild;
                }
                return currentNode;
            }
        }

        protected virtual BSTNode<T> _findMaxNode(BSTNode<T> currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            else
            {
                while (currentNode.HasRightChild)
                {
                    currentNode = currentNode.RightChild;
                }
                return currentNode;
            }
        }
        protected virtual BSTNode<T> _findNextLarger(BSTNode<T> currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            else
            {
                if (currentNode.HasRightChild)
                {
                    return _findMinNode(currentNode.RightChild);
                }

                while (currentNode.Parent != null && currentNode.IsRightChild)
                {
                    currentNode = currentNode.Parent;
                }
                return currentNode.Parent;

            }
        }
        //in-order
        protected virtual void _findAll(BSTNode<T> currentNode, Predicate<T> match, ref List<T> list)
        {
            if (currentNode == null)
            { return; }
            _findAll(currentNode.LeftChild, match, ref list);
            if (match(currentNode.Value))
            {
                list.Add(currentNode.Value);
            }
            _findAll(currentNode.RightChild, match, ref list);
        }
        protected virtual void _inOrderTraverse(BSTNode<T> currentNode, ref List<T> list)
        {
            if (currentNode == null)
                return;
            _inOrderTraverse(currentNode.LeftChild, ref list);
            list.Add(currentNode.Value);
            _inOrderTraverse(currentNode.RightChild, ref list);
        }

        public bool IsEmpty
        {
            get
            {
                return (_count == 0);
            }
        }
        public int Height
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                return _getHeight(_root);
            }
        }
        public bool AllowsDuplicates
        {
            get
            {
                return _allowDuplicates;
            }
        }
        public void Insert(T item)
        {
            var newNode = new BSTNode<T>(item);
            bool sucess = _insertNode(newNode);
            if (sucess == false && _allowDuplicates == false)
            {
                throw new Exception("tree dones not  allow inerting duplicate elements");
            }
        }
        public void Insert(T[] collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            if (collection.Length != 0)
            {
                for (int i = 0; i < collection.Length; i++)
                {
                    this.Insert(collection[i]);
                }
            }
        }

        public void Insert(List<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            if (collection.Count != 0)
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    this.Insert(collection[i]);
                }
            }
        }
        public void RemoveMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Ttree is empty.");
            }
            else
            {
                var node = _findMinNode(_root);
                _remove(node);
            }

        }
        public void RemoveMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Ttree is empty.");
            }
            else
            {
                var node = _findMaxNode(_root);
                _remove(node);
            }
        }
        public void Remove(T item)
        {
            if (IsEmpty)
            {
                throw new Exception("Ttree is empty.");
            }
            else
            {
                var node = _find(Root, item);
                bool status = _remove(node);
                if (status == false)
                {
                    throw new Exception("Item was not found");
                }
            }
        }
        public bool Contains(T item)
        {
            var node = _find(_root, item);
            return (node != null);
        }
        public T FindMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Tress is empty.");
            }
            var node = _findMinNode(_root);
            if (node == null)
            {
                return default(T);
            }
            else
            {
                return node.Value;
            }
        }
        public T FindMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Tress is empty.");
            }
            var node = _findMaxNode(_root);
            if (node == null)
            {
                return default(T);
            }
            else
            {
                return node.Value;
            }
        }
        public T Find(T item)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = _find(Root, item);

            if (node != null)
                return node.Value;
            else
                throw new Exception("Item was not found.");
        }
        public IEnumerable<T> FindAll(System.Predicate<T> searchPredicate)
        {
            var list = new List<T>();
            _findAll(_root, searchPredicate, ref list);
            return list;
        }
        public T[] ToArray()
        {
            return this.ToList().ToArray();
        }
        public List<T> ToList()
        {
            var list = new List<T>();
            _inOrderTraverse(_root, ref list);
            return list;
        }
        public void Clear()
        {
            _root = null;
            _count = 0;
        }
















        /*********************************************************************/

        /// <summary>
        /// Returns an preorder-traversal enumerator for the tree values
        /// </summary>
        public virtual IEnumerator<T> GetPreOrderEnumerator()
        {
            return new BinarySearchTreePreOrderEnumerator(this);
        }
        public virtual IEnumerator<T> GetInOrderEnumerator()
        {
            return new BinarySearchTreeInOrderEnumerator(this);
        }
        public virtual IEnumerator<T> GetPostOrderEnumerator()
        {
            return new BinarySearchTreePostOrderEnumerator(this);
        }
        /*********************************************************************/

        internal class BinarySearchTreePreOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public T Current
            {
                get
                {
                    return current.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public BinarySearchTreePreOrderEnumerator(BinarySearchTree<T> _tree)
            {
                this.tree = _tree;
                this.traverseQueue = new Queue<BSTNode<T>>();
                VisitNode(this.tree._root);
            }

            private void VisitNode(BSTNode<T> node)
            {
                if (node == null)
                {
                    return;
                }
                else
                {
                    traverseQueue.Enqueue(node);
                    VisitNode(node.LeftChild);
                    VisitNode(node.RightChild);
                }
            }
            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                {
                    current = traverseQueue.Dequeue();
                }
                else
                {
                    current = null;
                }
                return (current != null);
            }
            public void Reset()
            {
                current = null;
            }
            public void Dispose()
            {
                current = null;
                tree = null;
            }
        }

        /// <summary>
        /// Returns an inorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreeInOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public BinarySearchTreeInOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTNode<T>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTNode<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    visitNode(node.LeftChild);
                    traverseQueue.Enqueue(node);
                    visitNode(node.RightChild);
                }
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset()
            {
                current = null;
            }

            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    current = traverseQueue.Dequeue();
                else
                    current = null;

                return (current != null);
            }
        }

        /// <summary>
        /// Returns a postorder-traversal enumerator for the tree values
        /// </summary>
        internal class BinarySearchTreePostOrderEnumerator : IEnumerator<T>
        {
            private BSTNode<T> current;
            private BinarySearchTree<T> tree;
            internal Queue<BSTNode<T>> traverseQueue;

            public BinarySearchTreePostOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<BSTNode<T>>();
                visitNode(this.tree.Root);
            }

            private void visitNode(BSTNode<T> node)
            {
                if (node == null)
                    return;
                else
                {
                    visitNode(node.LeftChild);
                    visitNode(node.RightChild);
                    traverseQueue.Enqueue(node);
                }
            }

            public T Current
            {
                get { return current.Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset()
            {
                current = null;
            }

            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    current = traverseQueue.Dequeue();
                else
                    current = null;

                return (current != null);
            }


        }//end-of-binary-search-tree
    }
}