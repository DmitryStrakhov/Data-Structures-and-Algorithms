using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class BinaryTreeBase<T> {
        BinaryTreeNodeBase<T> root;
        readonly IComparer<T> comparer;
        readonly EqualityComparer<T> equalityComparer;

        public BinaryTreeBase()
            : this(null) {
        }
        public BinaryTreeBase(BinaryTreeNodeBase<T> root)
            : this(root, null) {
        }
        public BinaryTreeBase(BinaryTreeNodeBase<T> root, IComparer<T> comparer) {
            this.root = root;
            this.comparer = comparer ?? Comparer<T>.Default;
            this.equalityComparer = EqualityComparer<T>.Default;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public BinaryTreeNodeBase<T> Root { get { return root; } }

        protected void SetRoot(BinaryTreeNodeBase<T> root) {
            this.root = root;
        }

        #region Traversal
        protected void DoPreOrderTraverse(Action<BinaryTreeNodeBase<T>> action) {
            BinaryTreeNodeBase<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNodeBase<T>> stack = new Stack<BinaryTreeNodeBase<T>>();
            while(true) {
                while(node != null) {
                    action(node);
                    stack.Push(node);
                    node = node.Left;
                }
                if(stack.IsEmpty)
                    return;
                node = stack.Pop().Right;
            }
        }
        protected void DoPreOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action) {
            if(Root == null) return;
            DoPreOrderTraverseRecursive(action, Root);
        }
        void DoPreOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action, BinaryTreeNodeBase<T> node) {
            if(node == null) return;
            action(node);
            DoPreOrderTraverseRecursive(action, node.Left);
            DoPreOrderTraverseRecursive(action, node.Right);
        }
        protected void DoInOrderTraverse(Action<BinaryTreeNodeBase<T>> action) {
            BinaryTreeNodeBase<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNodeBase<T>> stack = new Stack<BinaryTreeNodeBase<T>>();
            while(true) {
                while(node != null) {
                    stack.Push(node);
                    node = node.Left;
                }
                if(stack.IsEmpty)
                    return;
                action(stack.Peek());
                node = stack.Pop().Right;
            }
        }
        protected void DoInOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action) {
            if(Root == null) return;
            DoInOrderTraverseRecursive(action, Root);
        }
        void DoInOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action, BinaryTreeNodeBase<T> node) {
            if(node == null) return;
            DoInOrderTraverseRecursive(action, node.Left);
            action(node);
            DoInOrderTraverseRecursive(action, node.Right);
        }
        protected void DoPostOrderTraverse(Action<BinaryTreeNodeBase<T>> action) {
            DoPostOrderTraverse(action, null, null);
        }
        protected void DoPostOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action) {
            if(Root == null) return;
            DoPostOrderTraverseRecursive(action, Root);
        }
        void DoPostOrderTraverseRecursive(Action<BinaryTreeNodeBase<T>> action, BinaryTreeNodeBase<T> node) {
            if(node == null) return;
            DoPostOrderTraverseRecursive(action, node.Left);
            DoPostOrderTraverseRecursive(action, node.Right);
            action(node);
        }
        protected void DoLevelOrderTraverse(Action<BinaryTreeNodeBase<T>> action) {
            DoLevelOrderTraverse((n, level) => { action(n); return false; });
        }
        #endregion

        #region Compare
        protected internal bool AreEqual(BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            if(x == null) return (y == null);
            if(y == null) return (x == null);
            return AreEqual(x.Value, y.Value);
        }
        protected internal bool AreEqual(T x, T y) {
            return this.equalityComparer.Equals(x, y);
        }
        protected internal int Compare(T x, T y) {
            return this.comparer.Compare(x, y);
        }
        #endregion

        #region Utils
        protected BinaryTreeNodeBase<T> DoLevelOrderTraverse(Func<BinaryTreeNodeBase<T>, int, bool> predicate) {
            if(Root == null) return null;
            int level = 0;
            Queue<BinaryTreeNodeBase<T>> queue = new Queue<BinaryTreeNodeBase<T>>();
            queue.EnQueue(Root);
            queue.EnQueue(null);
            while(!queue.IsEmpty) {
                BinaryTreeNodeBase<T> node = queue.DeQueue();
                if(node == null) {
                    if(!queue.IsEmpty)
                        queue.EnQueue(null);
                    level++;
                }
                else {
                    if(predicate(node, level))
                        return node;
                    if(node.Left != null) {
                        queue.EnQueue(node.Left);
                    }
                    if(node.Right != null) {
                        queue.EnQueue(node.Right);
                    }
                }
            }
            return null;
        }
        protected bool DoPostOrderTraverse(Action<BinaryTreeNodeBase<T>> action, Func<bool> traversalFinished = null, Action<BinaryTreeNodeBase<T>, BinaryTreeNodeBase<T>, int> visitingNode = null) {
            BinaryTreeNodeBase<T> node = Root;
            if(node == null) return true;
            Stack<BinaryTreeNodeBase<T>> stack = new Stack<BinaryTreeNodeBase<T>>();
            while(true) {
                if(node != null) {
                    if(visitingNode != null) {
                        visitingNode(node, stack.IsEmpty ? null : stack.Peek(), stack.Size);
                    }
                    stack.Push(node);
                    node = node.Left;
                }
                else {
                    if(stack.IsEmpty) {
                        if(traversalFinished != null) return traversalFinished();
                        return true;
                    }
                    if(stack.Peek().Right == null) {
                        node = stack.Pop();
                        action(node);
                        while(!stack.IsEmpty && node == stack.Peek().Right) {
                            action(node = stack.Pop());
                        }
                    }
                    if(!stack.IsEmpty) {
                        node = stack.Peek().Right;
                    }
                    else node = null;
                }
            }
        }
        #endregion

        #region Delete
        public bool DeleteNode(T value) {
            return DoDelete(value);
        }
        #endregion

        #region Metrics
        protected BinaryTreeNodeBase<T> DoGetDeepestNode() {
            BinaryTreeNodeBase<T> node = null;
            DoLevelOrderTraverse((n, level) => { node = n; return false; });
            return node;
        }
        public int GetTreeHeight() {
            if(Root == null) return 0;
            int level = 0;
            DoLevelOrderTraverse((n, lvl) => { level = lvl; return false; });
            return level;
        }
        public int GetTreeWidth() {
            int result = 0;
            GetTreeWidth(Root, ref result);
            return result;
        }
        int GetTreeWidth(BinaryTreeNodeBase<T> node, ref int result) {
            if(node == null) return 0;
            int lHeight = GetTreeWidth(node.Left, ref result);
            int rHeight = GetTreeWidth(node.Right, ref result);
            if(lHeight + rHeight + 1 > result)
                result = lHeight + rHeight + 1;
            return Math.Max(lHeight, rHeight) + 1;
        }
        protected BinaryTreeNodeBase<T> DoGetLeastCommonAncestor(BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            Guard.IsNotNull(x, nameof(x));
            Guard.IsNotNull(y, nameof(y));
            return DoGetLeastCommonAncestor(Root, x, y);
        }
        protected virtual BinaryTreeNodeBase<T> DoGetLeastCommonAncestor(BinaryTreeNodeBase<T> node, BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            return DoGetLeastCommonAncestorRecursive(node, x, y);
        }
        protected BinaryTreeNodeBase<T> DoGetLeastCommonAncestorRecursive(BinaryTreeNodeBase<T> node, BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            if(node == null || ReferenceEquals(node, x) || ReferenceEquals(node, y)) return node;
            BinaryTreeNodeBase<T> left = DoGetLeastCommonAncestor(node.Left, x, y);
            BinaryTreeNodeBase<T> right = DoGetLeastCommonAncestor(node.Right, x, y);
            if(left != null && right != null) return node;
            return left ?? right;
        }

        static readonly ReadOnlyCollection<BinaryTreeNodeBase<T>> EmptyNodeCollection = new ReadOnlyCollection<BinaryTreeNodeBase<T>>(new BinaryTreeNodeBase<T>[0]);

        protected ReadOnlyCollection<BinaryTreeNodeBase<T>> DoGetAncestors(BinaryTreeNodeBase<T> node) {
            Guard.IsNotNull(node, nameof(node));
            if(Root == null) return EmptyNodeCollection;
            Stack<BinaryTreeNodeBase<T>> stack = new Stack<BinaryTreeNodeBase<T>>();
            DoGetAncestors(Root, node, stack);
            if(stack.IsEmpty) return EmptyNodeCollection;
            List<BinaryTreeNodeBase<T>> list = new List<BinaryTreeNodeBase<T>>(stack.Size);
            while(!stack.IsEmpty) {
                list.Add(stack.Pop());
            }
            return new ReadOnlyCollection<BinaryTreeNodeBase<T>>(list);
        }
        bool DoGetAncestors(BinaryTreeNodeBase<T> root, BinaryTreeNodeBase<T> node, Stack<BinaryTreeNodeBase<T>> stack) {
            if(root == null) return false;
            if(ReferenceEquals(root, node)) return true;
            if(DoGetAncestors(root.Left, node, stack) || DoGetAncestors(root.Right, node, stack)) {
                stack.Push(root);
                return true;
            }
            return false;
        }
        #endregion

        protected abstract BinaryTreeNodeBase<T> DoSearch(T value);
        protected abstract BinaryTreeNodeBase<T> DoInsert(BinaryTreeNodeBase<T> node, Action<T, T> visitAction);
        protected abstract bool DoDelete(T value);

        #region ThreadedTree Building
        public ThreadedBinaryTree<T> BuildThreadedTree() {
            ThreadedBinaryTreeDummyNode<T> dummy = new ThreadedBinaryTreeDummyNode<T>();
            ThreadedBinaryTreeNode<T> root = BuildThreadedTree(Root, dummy, dummy);
            dummy.AddChild(root);
            return new ThreadedBinaryTree<T>(dummy);
        }
        internal ThreadedBinaryTreeNode<T> BuildThreadedTree(BinaryTreeNodeBase<T> root, ThreadedBinaryTreeNode<T> nodeParent, ThreadedBinaryTreeNode<T> nodeRecentVisited) {
            if(root == null) return null;
            bool isLeftThreaded = (root.Left == null);
            bool isRightThreaded = (root.Right == null);
            ThreadedBinaryTreeNode<T> node = new ThreadedBinaryTreeNode<T>(root.Value, isLeftThreaded, null, isRightThreaded, null);
            ThreadedBinaryTreeNode<T> left = BuildThreadedTree(root.Left, node, nodeRecentVisited);
            ThreadedBinaryTreeNode<T> right = BuildThreadedTree(root.Right, nodeParent, node);
            if(isLeftThreaded) {
                left = nodeRecentVisited;
            }
            if(isRightThreaded) {
                right = nodeParent;
            }
            node.AddChild(left);
            node.AddChild(right);
            return node;
        }
        #endregion
    }
}
