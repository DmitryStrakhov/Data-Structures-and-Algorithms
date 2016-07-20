using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Value: {Value}")]
    public abstract class BinaryTreeNodeBase<T> {
        T value;
        BinaryTreeNodeBase<T> left;
        BinaryTreeNodeBase<T> right;

        public BinaryTreeNodeBase(T value) {
            this.value = value;
            this.left = this.right = null;
        }
        internal BinaryTreeNodeBase(T value, BinaryTreeNodeBase<T> left, BinaryTreeNodeBase<T> right) {
            this.value = value;
            this.left = left;
            this.right = right;
        }

        internal void AddChild(BinaryTreeNodeBase<T> node) {
            if(IsFull) return;
            if(Left == null) {
                this.left = node;
            }
            else {
                this.right = node;
            }
        }
        internal void RemoveChild(BinaryTreeNodeBase<T> node) {
            if(ReferenceEquals(Left, node)) {
                this.left = null;
            }
            if(ReferenceEquals(Right, node)) {
                this.right = null;
            }
        }
        
        internal void SetLeft(BinaryTreeNodeBase<T> left) {
            this.left = left;
        }
        internal void SetRight(BinaryTreeNodeBase<T> right) {
            this.right = right;
        }

        internal static bool AreEquals(BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            if(x == null) return (y == null);
            if(y == null) return (x == null);
            return AreEquals(x.Value, y.Value);
        }
        internal static bool AreEquals(T x, T y) {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
        internal static int Compare(T x, T y) {
            return Comparer<T>.Default.Compare(x, y);
        }
        internal static void ExchangeValues(BinaryTreeNodeBase<T> x, BinaryTreeNodeBase<T> y) {
            if(x == null || y == null) return;
            T temp = x.Value;
            x.value = y.Value;
            y.value = temp;
        }

        public bool IsFull {
            get { return Left != null && Right != null; }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public BinaryTreeNodeBase<T> Left {
            get { return left; }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public BinaryTreeNodeBase<T> Right {
            get { return right; }
        }
        public T Value { get { return value; } }
    }
}
