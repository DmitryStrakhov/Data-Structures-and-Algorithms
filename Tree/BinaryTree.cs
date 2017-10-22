using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinaryTree<T> : BinaryTreeBase<T> {
        public BinaryTree()
            : this(null) {
        }
        public BinaryTree(T rootValue)
            : this(new BinaryTreeNode<T>(rootValue)) {
        }
        public BinaryTree(BinaryTreeNode<T> root)
            : base(root) {
        }
        public BinaryTree(BinaryTreeNode<T> root, IComparer<T> comparer)
            : base(root, comparer) {
        }
        public new BinaryTreeNode<T> Root { get { return (BinaryTreeNode<T>)base.Root; } }

        #region Traversal
        public void PreOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoPreOrderTraverse(x => action((BinaryTreeNode<T>)x));
        }
        public void PreOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            DoPreOrderTraverseRecursive(x => action((BinaryTreeNode<T>)x));
        }
        public void InOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoInOrderTraverse(x => action((BinaryTreeNode<T>)x));
        }
        public void InOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            DoInOrderTraverseRecursive(x => action((BinaryTreeNode<T>)x));
        }
        public void PostOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoPostOrderTraverse(x => action((BinaryTreeNode<T>)x));
        }
        public void PostOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            DoPostOrderTraverseRecursive(x => action((BinaryTreeNode<T>)x));
        }
        public void LevelOrderTraverse(Action<BinaryTreeNode<T>> action) {
            DoLevelOrderTraverse(x => action((BinaryTreeNode<T>)x));
        }
        #endregion

        #region Insert
        public BinaryTreeNode<T> Insert(T value) {
            return Insert(value, null);
        }
        public BinaryTreeNode<T> Insert(BinaryTreeNode<T> node) {
            return Insert(node, null);
        }
        public BinaryTreeNode<T> Insert(T value, Action<T, T> visitAction) {
            return (BinaryTreeNode<T>)DoInsert(new BinaryTreeNode<T>(value), visitAction);
        }
        public BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, Action<T, T> visitAction) {
            return (BinaryTreeNode<T>)DoInsert(node, visitAction);
        }
        #endregion

        #region Metrics
        public BinaryTreeNode<T> GetDeepestNode() {
            return (BinaryTreeNode<T>)DoGetDeepestNode();
        }
        public BinaryTreeNode<T> GetLeastCommonAncestor(BinaryTreeNode<T> x, BinaryTreeNode<T> y) {
            return (BinaryTreeNode<T>)DoGetLeastCommonAncestor(x, y);
        }
        public ReadOnlyCollection<BinaryTreeNode<T>> GetAncestors(BinaryTreeNode<T> node) {
            var ancestorList = DoGetAncestors(node).OfType<BinaryTreeNode<T>>().ToList();
            return new ReadOnlyCollection<BinaryTreeNode<T>>(ancestorList);
        }
        #endregion

        #region Search
        public BinaryTreeNode<T> Search(T value) {
            return (BinaryTreeNode<T>)DoSearch(value);
        }
        #endregion

        protected override BinaryTreeNodeBase<T> DoSearch(T value) {
            return DoLevelOrderTraverse((x, level) => AreEqual(x.Value, value));
        }
        protected override BinaryTreeNodeBase<T> DoInsert(BinaryTreeNodeBase<T> node, Action<T, T> visitAction) {
            Guard.IsNotNull(node, nameof(node));
            if(Root == null) {
                SetRoot(node);
                return node;
            }
            DoLevelOrderTraverse((n, level) => {
                if(!n.IsFull) {
                    n.AddChild(node);
                    return true;
                }
                return false;
            });
            return node;
        }
        protected override bool DoDelete(T value) {
            if(Root == null)
                return false;
            BinaryTreeNodeBase<T> nodeToDelete = null;
            BinaryTreeNodeBase<T> nodeDeepest = Root;
            BinaryTreeNodeBase<T> itParent = null;
            int maxLevel = 0;
            return DoPostOrderTraverse(n => {
                if(nodeToDelete == null && AreEqual(n.Value, value)) nodeToDelete = n;
            },
            () => {
                if(nodeToDelete == null || nodeDeepest == null) return false;
                if(ReferenceEquals(Root, nodeDeepest)) {
                    SetRoot(null);
                }
                else {
                    BinaryTreeNodeBase<T>.ExchangeValues(nodeDeepest, nodeToDelete);
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
    }
}
