using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class ThreadedBinaryTreeNode<T> : BinaryTreeNodeBase<T> {
        bool isLeftThreaded;
        bool isRightThreaded;

        public ThreadedBinaryTreeNode(T value)
            : this(value, null, null) {
        }
        public ThreadedBinaryTreeNode(T value, ThreadedBinaryTreeNode<T> left, ThreadedBinaryTreeNode<T> right)
            : this(value, false, left, false, right) {
        }
        public ThreadedBinaryTreeNode(T value, bool isLeftThreaded, ThreadedBinaryTreeNode<T> left, bool isRightThreaded, ThreadedBinaryTreeNode<T> right) : base(value, left, right) {
            this.isLeftThreaded = isLeftThreaded;
            this.isRightThreaded = isRightThreaded;
        }
        internal ThreadedBinaryTreeNode(T value, ThreadedBinaryTreeNode<T> left) : base(value, left, null) {
            SetRight(this);
        }

        public bool IsLeftThreaded { get { return isLeftThreaded; } }
        public new ThreadedBinaryTreeNode<T> Left {
            get { return (ThreadedBinaryTreeNode<T>)base.Left; }
        }

        public bool IsRightThreaded { get { return isRightThreaded; } }
        public new ThreadedBinaryTreeNode<T> Right {
            get { return (ThreadedBinaryTreeNode<T>)base.Right; }
        }

        public bool IsThreaded { get { return IsLeftThreaded || IsRightThreaded; } }

        public ThreadedBinaryTreeNode<T> InsertLeft(T value) {
            ThreadedBinaryTreeNode<T> node = new ThreadedBinaryTreeNode<T>(value);
            ThreadedBinaryTreeNode<T> left = Left;
            bool isLT = IsLeftThreaded;
            SetLeft(node);
            this.isLeftThreaded = false;
            node.SetLeft(left);
            node.isLeftThreaded = isLT;
            node.isRightThreaded = true;
            node.SetRight(this);
            if(!isLT) {
                ThreadedBinaryTreeNode<T> n = left;
                while(!n.IsRightThreaded) {
                    n = n.Right;
                }
                n.SetRight(node);
            }
            return node;
        }
        public ThreadedBinaryTreeNode<T> InsertRight(T value) {
            ThreadedBinaryTreeNode<T> node = new ThreadedBinaryTreeNode<T>(value);
            ThreadedBinaryTreeNode<T> right = Right;
            bool isRT = IsRightThreaded;
            SetRight(node);
            this.isRightThreaded = false;
            node.SetRight(right);
            node.isRightThreaded = isRT;
            node.isLeftThreaded = true;
            node.SetLeft(this);
            if(!isRT) {
                ThreadedBinaryTreeNode<T> n = right;
                while(!n.isLeftThreaded) {
                    n = n.Left;
                }
                n.SetLeft(node);
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
            SetLeft(left.Left);
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
            SetRight(right.Right);
            return result;
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
