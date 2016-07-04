using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class ThreadedBinaryTree<T> {
        ThreadedBinaryTreeDummyNode<T> dummy;

        public ThreadedBinaryTree(T value)
            : this(new ThreadedBinaryTreeNode<T>(value)) {
        }
        public ThreadedBinaryTree(ThreadedBinaryTreeNode<T> root)
            : this(new ThreadedBinaryTreeDummyNode<T>(root)) {
        }
        internal ThreadedBinaryTree(ThreadedBinaryTreeDummyNode<T> dummy) {
            this.dummy = dummy;
        }

        public void PreOrderTraverse(Action<ThreadedBinaryTreeNode<T>> action) {
            ThreadedBinaryTreeNode<T> successor = GetPreOrderSuccessor(this.dummy);
            while(!ReferenceEquals(successor, this.dummy)) {
                action(successor);
                successor = GetPreOrderSuccessor(successor);
            }
        }
        public void InOrderTraverse(Action<ThreadedBinaryTreeNode<T>> action) {
            ThreadedBinaryTreeNode<T> successor = GetInOrderSuccessor(this.dummy);
            while(!ReferenceEquals(successor, this.dummy)) {
                action(successor);
                successor = GetInOrderSuccessor(successor);
            }
        }

        protected ThreadedBinaryTreeNode<T> GetPreOrderSuccessor(ThreadedBinaryTreeNode<T> node) {
            if(!node.IsLeftThreaded) return node.Left;
            while(node.IsRightThreaded) {
                node = node.Right;
            }
            return node.Right;
        }
        protected ThreadedBinaryTreeNode<T> GetInOrderSuccessor(ThreadedBinaryTreeNode<T> node) {
            if(node.IsRightThreaded) return node.Right;
            node = node.Right;
            while(!node.IsLeftThreaded) {
                node = node.Left;
            }
            return node;
        }
        public ThreadedBinaryTreeNode<T> Root { get { return this.dummy.Left; } }
    }
}

