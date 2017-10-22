using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class AVLTree<T> : BinarySearchTree<T> {
        public AVLTree() {
        }
        public AVLTree(IComparer<T> comparer)
            : base(null, comparer) {
        }
        public AVLTree(BinarySearchTreeNode<T> root)
            : base(root) {
        }
        public AVLTree(BinarySearchTreeNode<T> root, IComparer<T> comparer)
            : base(root, comparer) {
        }
        public AVLTree(T rootValue)
            : base(rootValue) {
        }
        public AVLTree(T rootValue, IComparer<T> comparer)
            : base(new BinarySearchTreeNode<T>(rootValue), comparer) {
        }

        public static bool IsAVLTree(BinarySearchTree<T> tree) {
            Guard.IsNotNull(tree, nameof(tree));
            return GetAVLTreeHeight(tree.Root) != int.MinValue;
        }
        static int GetAVLTreeHeight(BinarySearchTreeNode<T> root) {
            if(root == null) return -1;
            int lHeight = GetAVLTreeHeight(root.Left);
            if(lHeight == int.MinValue)
                return int.MinValue;
            int rHeight = GetAVLTreeHeight(root.Right);
            if(rHeight == int.MinValue)
                return int.MinValue;
            if(Math.Abs(lHeight - rHeight) > 1) return int.MinValue;
            return Math.Max(lHeight, rHeight) + 1;
        }

        protected override BinaryTreeNodeBase<T> DoInsert(BinaryTreeNodeBase<T> node, Action<T, T> visitAction) {
            return DoInsertRecursive(Root, (BinarySearchTreeNode<T>)node, visitAction);
        }
        
        BinaryTreeNodeBase<T> DoInsertRecursive(BinarySearchTreeNode<T> root, BinarySearchTreeNode<T> node, Action<T, T> visitAction) {
            if(root == null) {
                node.SetHeight(0);
                return node;
            }
            if(visitAction != null) visitAction(root.Value, node.Value);
            int comparisonResult = Compare(node.Value, root.Value);
            if(comparisonResult == 0) return root;
            if(comparisonResult < 0) {
                root.SetLeft(DoInsertRecursive(root.Left, node, visitAction));
                root = EnsureBalanced(root, x => Compare(x.Left.Value, node.Value) > 0, RotateLL, RotateLR);
            }
            else {
                root.SetRight(DoInsertRecursive(root.Right, node, visitAction));
                root = EnsureBalanced(root, x => Compare(x.Right.Value, node.Value) > 0, RotateRL, RotateRR);
            }
            root.SetHeight(Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1);
            return root;
        }
        protected override BinarySearchTreeNode<T> DoDeleteRecursive(T value, BinarySearchTreeNode<T> root, ref bool result) {
            if(root == null) {
                result = false;
                return root;
            }
            int comparisonResult = Compare(root.Value, value);
            if(comparisonResult == 0) {
                if(root.IsFull) {
                    BinarySearchTreeNode<T>.ExchangeValues(root, DoGetMaximum(root.Left));
                    root.SetLeft(DoDeleteRecursive(value, root.Left, ref result));
                    root = EnsureBalanced(root, x => GetHeight(x.Right.Left) > GetHeight(x.Right.Right), RotateRL, RotateRR);
                }
                else {
                    root = root.Left ?? root.Right;
                }
            }
            else if(comparisonResult < 0) {
                root.SetRight(DoDeleteRecursive(value, root.Right, ref result));
                root = EnsureBalanced(root, x => GetHeight(x.Left.Left) > GetHeight(x.Left.Right), RotateLL, RotateLR);
            }
            else {
                root.SetLeft(DoDeleteRecursive(value, root.Left, ref result));
                root = EnsureBalanced(root, x => GetHeight(x.Right.Left) > GetHeight(x.Right.Right), RotateRL, RotateRR);
            }
            if(root != null) root.SetHeight(Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1);
            return root;
        }

        internal static BinarySearchTreeNode<T> EnsureBalanced(BinarySearchTreeNode<T> node, Func<BinarySearchTreeNode<T>, bool> getIsLeftViolation, AVLTreeRotateDelegate doResolveLeftViolation, AVLTreeRotateDelegate doResolveRightViolation) {
            if(Math.Abs(GetHeight(node.Right) - GetHeight(node.Left)) == 2) {
                if(getIsLeftViolation(node))
                    node = doResolveLeftViolation(node);
                else
                    node = doResolveRightViolation(node);
            }
            return node;
        }
        internal static int GetHeight(BinarySearchTreeNode<T> node) {
            return node != null ? node.Height : -1;
        }
        internal static BinarySearchTreeNode<T> RotateLL(BinarySearchTreeNode<T> node) {
            BinarySearchTreeNode<T> left = node.Left;
            BinarySearchTreeNode<T> r = left.Right;
            left.SetRight(node);
            node.SetLeft(r);
            node.SetHeight(Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1);
            left.SetHeight(Math.Max(GetHeight(left.Left), GetHeight(left.Right)) + 1);
            return left;
        }
        internal static BinarySearchTreeNode<T> RotateRR(BinarySearchTreeNode<T> node) {
            BinarySearchTreeNode<T> right = node.Right;
            BinarySearchTreeNode<T> l = right.Left;
            right.SetLeft(node);
            node.SetRight(l);
            node.SetHeight(Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1);
            right.SetHeight(Math.Max(GetHeight(right.Left), GetHeight(right.Right)) + 1);
            return right;
        }
        internal static BinarySearchTreeNode<T> RotateLR(BinarySearchTreeNode<T> node) {
            BinarySearchTreeNode<T> left = RotateRR(node.Left);
            node.SetLeft(left);
            return RotateLL(node);
        }
        internal static BinarySearchTreeNode<T> RotateRL(BinarySearchTreeNode<T> node) {
            BinarySearchTreeNode<T> right = RotateLL(node.Right);
            node.SetRight(right);
            return RotateRR(node);
        }

        internal delegate BinarySearchTreeNode<T> AVLTreeRotateDelegate(BinarySearchTreeNode<T> node);
    }
}
