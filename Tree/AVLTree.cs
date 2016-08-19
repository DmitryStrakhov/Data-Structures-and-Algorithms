using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class AVLTree<T> : BinarySearchTree<T> {
        public AVLTree() {
        }
        public AVLTree(BinarySearchTreeNode<T> root)
            : base(root) {
        }
        public AVLTree(T rootValue)
            : base(rootValue) {
        }

        protected override BinaryTreeNodeBase<T> DoInsert(BinaryTreeNodeBase<T> node) {
            return DoInsertRecursive(Root, (BinarySearchTreeNode<T>)node);
        }
        
        BinaryTreeNodeBase<T> DoInsertRecursive(BinarySearchTreeNode<T> root, BinarySearchTreeNode<T> node) {
            if(root == null) {
                node.SetHeight(0);
                return node;
            }
            int comparisonResult = BinaryTreeNode<T>.Compare(node.Value, root.Value);
            if(comparisonResult == 0) return root;
            if(comparisonResult < 0) {
                root.SetLeft(DoInsertRecursive(root.Left, node));
                root = EnsureBalanced(root, x => BinaryTreeNode<T>.Compare(x.Left.Value, node.Value) > 0, RotateLL, RotateLR);
            }
            else {
                root.SetRight(DoInsertRecursive(root.Right, node));
                root = EnsureBalanced(root, x => BinaryTreeNode<T>.Compare(x.Right.Value, node.Value) > 0, RotateRL, RotateRR);
            }
            root.SetHeight(Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1);
            return root;
        }
        protected override BinarySearchTreeNode<T> DoDeleteRecursive(T value, BinarySearchTreeNode<T> root, ref bool result) {
            if(root == null) {
                result = false;
                return root;
            }
            int comparisonResult = BinaryTreeNode<T>.Compare(root.Value, value);
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
