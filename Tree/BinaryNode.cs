using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinaryTreeNode<T> : BinaryTreeNodeBase<T> {
        public BinaryTreeNode(T value)
            : base(value) {
        }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
            : base(value, left, right) {
        }

        public BinaryTreeNode<T> InsertLeft(T value) {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            node.SetLeft(Left);
            SetLeft(node);
            return node;
        }
        public BinaryTreeNode<T> InsertRight(T value) {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            node.SetRight(Right);
            SetRight(node);
            return node;
        }
        public BinaryTreeNode<T> RemoveLeft() {
            BinaryTreeNode<T> leftNode = Left;
            SetLeft(null);
            return leftNode;
        }
        public BinaryTreeNode<T> RemoveRight() {
            BinaryTreeNode<T> rightNode = Right;
            SetRight(null);
            return rightNode;
        }

        internal void AddChild(T value) {
            AddChild(new BinaryTreeNode<T>(value));
        }

        public new BinaryTreeNode<T> Left {
            get { return (BinaryTreeNode<T>)base.Left; }
        }
        public new BinaryTreeNode<T> Right {
            get { return (BinaryTreeNode<T>)base.Right; }
        }
    }
}
