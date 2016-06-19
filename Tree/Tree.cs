using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinaryTree<T> {
        BinaryTreeNode<T> root;

        public BinaryTree()
            : this(null) {
        }
        public BinaryTree(BinaryTreeNode<T> root) {
            this.root = root;
        }

        public BinaryTreeNode<T> Root { get { return root; } }

        #region Traversal
        public void PreOrderTraverse(Action<BinaryTreeNode<T>> action) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
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
        public void PreOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            PreOrderTraverseRecursive(action, Root);
        }
        protected void PreOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            action(node);
            PreOrderTraverseRecursive(action, node.Left);
            PreOrderTraverseRecursive(action, node.Right);
        }
        public void InOrderTraverse(Action<BinaryTreeNode<T>> action) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
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
        public void InOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            InOrderTraverseRecursive(action, Root);
        }
        protected void InOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            InOrderTraverseRecursive(action, node.Left);
            action(node);
            InOrderTraverseRecursive(action, node.Right);
        }
        public void PostOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoPostOrderTraverse(action);
        }
        public void PostOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            PostOrderTraverseRecursive(action, Root);
        }
        protected void PostOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            PostOrderTraverseRecursive(action, node.Left);
            PostOrderTraverseRecursive(action, node.Right);
            action(node);
        }
        public void LevelOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoLevelOrderTraverse((n, level) => { action(n); return false; });
        }
        #endregion

        #region Utils
        protected BinaryTreeNode<T> DoLevelOrderTraverse(Func<BinaryTreeNode<T>, int, bool> predicate) {
            if(Root == null) return null;
            int level = 0;
            Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();
            queue.EnQueue(Root);
            queue.EnQueue(null);
            while(!queue.IsEmpty) {
                BinaryTreeNode<T> node = queue.DeQueue();
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
        protected bool DoPostOrderTraverse(Action<BinaryTreeNode<T>> action, Func<bool> traversalFinished = null, Action<BinaryTreeNode<T>, BinaryTreeNode<T>, int> visitingNode = null) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return true;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
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

        #region Insert
        public void Insert(T value) {
            Insert(new BinaryTreeNode<T>(value));
        }
        public virtual void Insert(BinaryTreeNode<T> node) {
            Guard.IsNotNull(node, nameof(node));
            if(Root == null) {
                this.root = node;
                return;
            }
            DoLevelOrderTraverse((n, level) => {
                if(!n.IsFull) {
                    n.AddChild(node);
                    return true;
                }
                return false;
            });
        }
        #endregion

        #region Delete
        public virtual bool DeleteNode(T value) {
            if(Root == null)
                return false;
            BinaryTreeNode<T> nodeToDelete = null;
            BinaryTreeNode<T> nodeDeepest = Root;
            BinaryTreeNode<T> itParent = null;
            int maxLevel = 0;
            return DoPostOrderTraverse(n => {
                if(nodeToDelete == null && BinaryTreeNode<T>.AreEquals(n.Value, value)) nodeToDelete = n;
            },
            () => {
                if(nodeToDelete == null || nodeDeepest == null) return false;
                if(ReferenceEquals(Root, nodeDeepest)) {
                    this.root = null;
                }
                else {
                    BinaryTreeNode<T>.ExchangeValues(nodeDeepest, nodeToDelete);
                    itParent.RemoveChild(nodeDeepest);
                }
                return true;
            },
            (n, parent, level) => {
                if(level > maxLevel) {
                    nodeDeepest = n;
                    itParent = parent;
                }
            }
            );
        }
        #endregion

        #region Metrics
        public BinaryTreeNode<T> GetDeepestNode() {
            BinaryTreeNode<T> node = null;
            DoLevelOrderTraverse((n, level) => { node = n; return false; });
            return node;
        }
        public virtual int GetTreeHeight() {
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
        protected int GetTreeWidth(BinaryTreeNode<T> node, ref int result) {
            if(node == null) return 0;
            int lHeight = GetTreeWidth(node.Left, ref result);
            int rHeight = GetTreeWidth(node.Right, ref result);
            if(lHeight + rHeight + 1 > result)
                result = lHeight + rHeight + 1;
            return Math.Max(lHeight, rHeight) + 1;
        }
        public BinaryTreeNode<T> GetLeastCommonAncestor(BinaryTreeNode<T> x, BinaryTreeNode<T> y) {
            Guard.IsNotNull(x, nameof(x));
            Guard.IsNotNull(y, nameof(y));
            return GetLeastCommonAncestor(Root, x, y);
        }
        protected BinaryTreeNode<T> GetLeastCommonAncestor(BinaryTreeNode<T> node, BinaryTreeNode<T> x, BinaryTreeNode<T> y) {
            if(node == null || ReferenceEquals(node, x) || ReferenceEquals(node, y)) return node;
            BinaryTreeNode<T> left = GetLeastCommonAncestor(node.Left, x, y);
            BinaryTreeNode<T> right = GetLeastCommonAncestor(node.Right, x, y);
            if(left != null && right != null) return node;
            return left ?? right;
        }

        static readonly ReadOnlyCollection<BinaryTreeNode<T>> EmptyNodeCollection = new ReadOnlyCollection<BinaryTreeNode<T>>(new BinaryTreeNode<T>[0]);

        public ReadOnlyCollection<BinaryTreeNode<T>> GetAncestors(BinaryTreeNode<T> node) {
            Guard.IsNotNull(node, nameof(node));
            if(Root == null) return EmptyNodeCollection;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            GetAncestors(Root, node, stack);
            if(stack.IsEmpty) return EmptyNodeCollection;
            List<BinaryTreeNode<T>> list = new List<BinaryTreeNode<T>>(stack.Size);
            while(!stack.IsEmpty) {
                list.Add(stack.Pop());
            }
            return new ReadOnlyCollection<BinaryTreeNode<T>>(list);
        }
        protected bool GetAncestors(BinaryTreeNode<T> root, BinaryTreeNode<T> node, Stack<BinaryTreeNode<T>> stack) {
            if(root == null) return false;
            if(ReferenceEquals(root, node)) return true;
            if(GetAncestors(root.Left, node, stack) || GetAncestors(root.Right, node, stack)) {
                stack.Push(root);
                return true;
            }
            return false;
        }
        #endregion

        public virtual BinaryTreeNode<T> Search(Func<BinaryTreeNode<T>, bool> predicate) {
            Guard.IsNotNull(predicate, nameof(predicate));
            return DoLevelOrderTraverse((x, level) => predicate(x));
        }
    }
}
