using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinarySearchTree<T> : BinaryTreeBase<T> {
        public BinarySearchTree()
            : this(null) {
        }
        public BinarySearchTree(T rootValue)
            : this(new BinarySearchTreeNode<T>(rootValue)) {
        }
        public BinarySearchTree(BinarySearchTreeNode<T> root)
            : base(root) {
        }
        public new BinarySearchTreeNode<T> Root { get { return (BinarySearchTreeNode<T>)base.Root; } }

        public BinarySearchTreeNode<T> Search(T value) {
            return (BinarySearchTreeNode<T>)DoSearch(value);
        }
        public BinarySearchTreeNode<T> Insert(T value) {
            BinarySearchTreeNode<T> root = (BinarySearchTreeNode<T>)DoInsert(new BinarySearchTreeNode<T>(value));
            SetRoot(root);
            return root;
        }
        public BinarySearchTreeNode<T> Insert(BinarySearchTreeNode<T> node) {
            return (BinarySearchTreeNode<T>)DoInsert(node);
        }
        public BinarySearchTreeNode<T> GetMinimum() {
            return DoGetMinimum(Root);
        }
        public BinarySearchTreeNode<T> GetMaximum() {
            return DoGetMaximum(Root);
        }
        public BinarySearchTreeNode<T> GetFloor(T value) {
            return GetFloorRecursive(Root, value);
        }
        public BinarySearchTreeNode<T> GetCeiling(T value) {
            return GetCeilingRecursive(Root, value);
        }

        protected override BinaryTreeNodeBase<T> DoSearch(T value) {
            return DoSearchRecursive(Root, x => BinaryTreeNodeBase<T>.Compare(x.Value, value));
        }
        protected override BinaryTreeNodeBase<T> DoInsert(BinaryTreeNodeBase<T> node) {
            return DoInsertRecursive(Root, node);
        }
        protected override bool DoDelete(T value) {
            bool result = true;
            BinaryTreeNodeBase<T> root = DoDeleteRecursive(value, Root, ref result);
            SetRoot(root);
            return result;
        }

        protected BinarySearchTreeNode<T> DoGetMinimum(BinarySearchTreeNode<T> root) {
            BinarySearchTreeNode<T> left = root;
            if(left == null) return null;
            while(left.Left != null) {
                left = left.Left;
            }
            return left;
        }
        protected BinarySearchTreeNode<T> DoGetMaximum(BinarySearchTreeNode<T> root) {
            BinarySearchTreeNode<T> right = root;
            if(right == null) return null;
            while(right.Right != null) {
                right = right.Right;
            }
            return right;
        }

        BinaryTreeNodeBase<T> DoInsertRecursive(BinarySearchTreeNode<T> root, BinaryTreeNodeBase<T> node) {
            if(root == null) return node;
            int comparisonResult = BinaryTreeNode<T>.Compare(node.Value, root.Value);
            if(comparisonResult == 0) return root;
            if(comparisonResult < 0) {
                root.SetLeft(DoInsertRecursive(root.Left, node));
            }
            else {
                root.SetRight(DoInsertRecursive(root.Right, node));
            }
            return root;
        }
        BinaryTreeNodeBase<T> DoSearchRecursive(BinaryTreeNodeBase<T> root, Func<BinaryTreeNodeBase<T>, int> comparerFunc) {
            if(root == null) return null;
            int comparisonResult = comparerFunc(root);
            if(comparisonResult == 0)
                return root;
            if(comparisonResult > 0) {
                return DoSearchRecursive(root.Left, comparerFunc);
            }
            return DoSearchRecursive(root.Right, comparerFunc);
        }
        protected virtual BinarySearchTreeNode<T> DoDeleteRecursive(T value, BinarySearchTreeNode<T> root, ref bool result) {
            if(root == null) {
                result = false;
                return root;
            }
            int comparisonResult = BinaryTreeNode<T>.Compare(root.Value, value);
            if(comparisonResult == 0) {
                if(root.IsFull) {
                    BinarySearchTreeNode<T>.ExchangeValues(root, DoGetMaximum(root.Left));
                    root.SetLeft(DoDeleteRecursive(value, root.Left, ref result));
                }
                else {
                    root = root.Left ?? root.Right;
                }
            }
            else if(comparisonResult < 0) {
                root.SetRight(DoDeleteRecursive(value, root.Right, ref result));
            }
            else {
                root.SetLeft(DoDeleteRecursive(value, root.Left, ref result));
            }
            return root;
        }
        BinarySearchTreeNode<T> GetFloorRecursive(BinarySearchTreeNode<T> root, T value) {
            if(root == null) return null;
            var right = GetFloorRecursive(root.Right, value);
            if(right != null)
                return right;
            if(BinarySearchTreeNode<T>.Compare(root.Value, value) <= 0) 
                return root;
            return GetFloorRecursive(root.Left, value);
        }
        BinarySearchTreeNode<T> GetCeilingRecursive(BinarySearchTreeNode<T> root, T value) {
            if(root == null) return null;
            var left = GetCeilingRecursive(root.Left, value);
            if(left != null)
                return left;
            if(BinarySearchTreeNode<T>.Compare(root.Value, value) >= 0)
                return root;
            return GetCeilingRecursive(root.Right, value);
        }

        #region Traversal
        public void PreOrderTraverse(Action<BinarySearchTreeNode<T>> action) {
            DoPreOrderTraverse(x => action((BinarySearchTreeNode<T>)x));
        }
        public void PreOrderTraverseRecursive(Action<BinarySearchTreeNode<T>> action) {
            DoPreOrderTraverseRecursive(x => action((BinarySearchTreeNode<T>)x));
        }
        public void InOrderTraverse(Action<BinarySearchTreeNode<T>> action) {
            DoInOrderTraverse(x => action((BinarySearchTreeNode<T>)x));
        }
        public void InOrderTraverseRecursive(Action<BinarySearchTreeNode<T>> action) {
            DoInOrderTraverseRecursive(x => action((BinarySearchTreeNode<T>)x));
        }
        public void PostOrderTraverse(Action<BinarySearchTreeNode<T>> action) {
            DoPostOrderTraverse(x => action((BinarySearchTreeNode<T>)x));
        }
        public void PostOrderTraverseRecursive(Action<BinarySearchTreeNode<T>> action) {
            DoPostOrderTraverseRecursive(x => action((BinarySearchTreeNode<T>)x));
        }
        public void LevelOrderTraverse(Action<BinarySearchTreeNode<T>> action) {
            DoLevelOrderTraverse(x => action((BinarySearchTreeNode<T>)x));
        }
        #endregion

        #region Metrics
        public BinarySearchTreeNode<T> GetDeepestNode() {
            return (BinarySearchTreeNode<T>)DoGetDeepestNode();
        }
        public BinarySearchTreeNode<T> GetLeastCommonAncestor(BinarySearchTreeNode<T> x, BinarySearchTreeNode<T> y) {
            return (BinarySearchTreeNode<T>)DoGetLeastCommonAncestor(x, y);
        }
        public ReadOnlyCollection<BinarySearchTreeNode<T>> GetAncestors(BinarySearchTreeNode<T> node) {
            var ancestorList = DoGetAncestors(node).OfType<BinarySearchTreeNode<T>>().ToList();
            return new ReadOnlyCollection<BinarySearchTreeNode<T>>(ancestorList);
        }
        #endregion

        protected override BinaryTreeNodeBase<T> DoGetLeastCommonAncestor(BinaryTreeNodeBase<T> node, BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            BinaryTreeNodeBase<T> n = node;
            int comparisonResult = BinaryTreeNodeBase<T>.Compare(x.Value, y.Value);
            BinaryTreeNodeBase<T> minN = (comparisonResult > 0 ? y : x);
            BinaryTreeNodeBase<T> maxN = (comparisonResult > 0 ? x : y);
            while(n != null) {
                int minCompResult = BinaryTreeNodeBase<T>.Compare(n.Value, minN.Value);
                int maxCompResult = BinaryTreeNodeBase<T>.Compare(n.Value, maxN.Value);
                if(minCompResult > 0 && maxCompResult < 0)
                    return n;
                if(maxCompResult == 0)
                    return maxN;
                n = (minCompResult < 0) ? n.Right : n.Left;
            }
            return null;
        }
    }
}
