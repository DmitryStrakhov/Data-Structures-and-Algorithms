using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Value: {Value}")]
    public class ThreadedBinaryTreeNode<T> {
        T value;
        ThreadedBinaryTreeNode<T> left;
        ThreadedBinaryTreeNode<T> right;

        bool isLeftThreaded;
        bool isRightThreaded;

        public ThreadedBinaryTreeNode(T value)
            : this(value, null, null) {
        }
        public ThreadedBinaryTreeNode(T value, ThreadedBinaryTreeNode<T> left, ThreadedBinaryTreeNode<T> right)
            : this(value, false, left, false, right) {
        }
        public ThreadedBinaryTreeNode(T value, bool isLeftThreaded, ThreadedBinaryTreeNode<T> left, bool isRightThreaded, ThreadedBinaryTreeNode<T> right) {
            this.value = value;
            this.isLeftThreaded = isLeftThreaded;
            this.left = left;
            this.isRightThreaded = isRightThreaded;
            this.right = right;
        }
        internal ThreadedBinaryTreeNode(T value, ThreadedBinaryTreeNode<T> left) : this(value) {
            this.left = left;
            this.right = this;
        }

        public bool IsLeftThreaded { get { return isLeftThreaded; } }
        public ThreadedBinaryTreeNode<T> Left {
            get { return this.left; }
        }

        public bool IsRightThreaded { get { return isRightThreaded; } }
        public ThreadedBinaryTreeNode<T> Right {
            get { return this.right; }
        }

        public bool IsThreaded { get { return IsLeftThreaded || IsRightThreaded; } }

        public ThreadedBinaryTreeNode<T> InsertLeft(T value) {
            ThreadedBinaryTreeNode<T> node = new ThreadedBinaryTreeNode<T>(value);
            ThreadedBinaryTreeNode<T> left = Left;
            bool isLT = IsLeftThreaded;
            this.left = node;
            this.isLeftThreaded = false;
            node.left = left;
            node.isLeftThreaded = isLT;
            node.isRightThreaded = true;
            node.right = this;
            if(!isLT) {
                ThreadedBinaryTreeNode<T> n = left;
                while(!n.IsRightThreaded) {
                    n = n.Right;
                }
                n.right = node;
            }
            return node;
        }
        public ThreadedBinaryTreeNode<T> InsertRight(T value) {
            ThreadedBinaryTreeNode<T> node = new ThreadedBinaryTreeNode<T>(value);
            ThreadedBinaryTreeNode<T> right = Right;
            bool isRT = IsRightThreaded;
            this.right = node;
            this.isRightThreaded = false;
            node.right = right;
            node.isRightThreaded = isRT;
            node.isLeftThreaded = true;
            node.left = this;
            if(!isRT) {
                ThreadedBinaryTreeNode<T> n = right;
                while(!n.isLeftThreaded) {
                    n = n.Left;
                }
                n.left = node;
            }
            return node;
        }

        public ThreadedBinaryTreeNode<T> RemoveLeft() {
            if(IsLeftThreaded) throw new InvalidOperationException();
            ThreadedBinaryTreeNode<T> result = Left;
            ThreadedBinaryTreeNode<T> left = Left;
            this.isLeftThreaded = true;
            while(!left.IsLeftThreaded) {
                left = left.Left;
            }
            this.left = left.left;
            return result;
        }
        public ThreadedBinaryTreeNode<T> RemoveRight() {
            if(IsRightThreaded) throw new InvalidOperationException();
            ThreadedBinaryTreeNode<T> result = Right;
            ThreadedBinaryTreeNode<T> right = Right;
            this.isRightThreaded = true;
            while(!right.IsRightThreaded) {
                right = right.Right;
            }
            this.right = right.Right;
            return result;
        }
        public T Value { get { return value; } }

        internal void AddChild(ThreadedBinaryTreeNode<T> node) {
            if(IsFull) return;
            if(Left == null) {
                this.left = node;
            }
            else {
                this.right = node;
            }
        }
        internal bool IsFull {
            get { return Left != null && Right != null; }
        }
    }

    class ThreadedBinaryTreeDummyNode<T> : ThreadedBinaryTreeNode<T> {
        public ThreadedBinaryTreeDummyNode()
            : this(null) {
        }
        public ThreadedBinaryTreeDummyNode(ThreadedBinaryTreeNode<T> root)
            : base(default(T), root) {
        }
    }
}
