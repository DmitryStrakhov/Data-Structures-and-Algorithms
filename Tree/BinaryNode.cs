using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Value: {Value}")]
    public class BinaryTreeNode<T> {
        T value;
        BinaryTreeNode<T> left;
        BinaryTreeNode<T> right;

        public BinaryTreeNode(T value)
            : this(value, null, null) {
        }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right) {
            this.value = value;
            this.left = left;
            this.right = right;
        }

        public BinaryTreeNode<T> InsertLeft(T value) {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            node.left = this.Left;
            this.left = node;
            return node;
        }
        public BinaryTreeNode<T> InsertRight(T value) {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            node.right = Right;
            this.right = node;
            return node;
        }
        public BinaryTreeNode<T> RemoveLeft() {
            BinaryTreeNode<T> leftNode = Left;
            this.left = null;
            return leftNode;
        }
        public BinaryTreeNode<T> RemoveRight() {
            BinaryTreeNode<T> rightNode = Right;
            this.right = null;
            return rightNode;
        }

        internal void AddChild(T value) {
            AddChild(new BinaryTreeNode<T>(value));
        }
        internal void AddChild(BinaryTreeNode<T> node) {
            if(IsFull) return;
            if(Left == null) {
                this.left = node;
            }
            else {
                this.right = node;
            }
        }
        internal void RemoveChild(BinaryTreeNode<T> node) {
            if(ReferenceEquals(Left, node)) {
                this.left = null;
            }
            if(ReferenceEquals(Right, node)) {
                this.right = null;
            }
        }
        internal bool IsFull {
            get { return Left != null && Right != null; }
        }
        internal static bool AreEquals(BinaryTreeNode<T> x, BinaryTreeNode<T> y) {
            if(x == null) return (y == null);
            if(y == null) return (x == null);
            return AreEquals(x.Value, y.Value);
        }
        internal static bool AreEquals(T x, T y) {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
        internal static void ExchangeValues(BinaryTreeNode<T> x, BinaryTreeNode<T> y) {
            if(x == null || y == null) return;
            T temp = x.Value;
            x.value = y.Value;
            y.value = temp;
        }

        public BinaryTreeNode<T> Left {
            get { return left; }
        }
        public BinaryTreeNode<T> Right {
            get { return right; }
        }
        public T Value { get { return value; } }
    }
}
