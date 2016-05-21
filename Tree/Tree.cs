using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinaryTree<T> {
        BinaryTreeNode<T> root;

        public BinaryTree(BinaryTreeNode<T> root) {
            this.root = root;
        }

        public BinaryTreeNode<T> Root { get { return root; } }

        public void PreOrderTraverse(Action<BinaryTreeNode<T>> action) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            while(true) {
                while(node != null) {
                    action(node);
                    stack.Push(node);
                    node = node.Left;
                }
                if(stack.IsEmpty)
                    return;
                node = stack.Pop().Right;
            }
        }
        public void PreOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            PreOrderTraverseRecursive(action, Root);
        }
        protected void PreOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            action(node);
            PreOrderTraverseRecursive(action, node.Left);
            PreOrderTraverseRecursive(action, node.Right);
        }
        public void InOrderTraverse(Action<BinaryTreeNode<T>> action) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            while(true) {
                while(node != null) {
                    stack.Push(node);
                    node = node.Left;
                }
                if(stack.IsEmpty)
                    return;
                action(stack.Peek());
                node = stack.Pop().Right;
            }
        }
        public void InOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            InOrderTraverseRecursive(action, Root);
        }
        protected void InOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            InOrderTraverseRecursive(action, node.Left);
            action(node);
            InOrderTraverseRecursive(action, node.Right);
        }
        public void PostOrderTraverse(Action<BinaryTreeNode<T>> action) {
            BinaryTreeNode<T> node = Root;
            if(node == null) return;
            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            while(true) {
                if(node != null) {
                    stack.Push(node);
                    node = node.Left;
                }
                else {
                    if(stack.IsEmpty)
                        return;
                    if(stack.Peek().Right == null) {
                        node = stack.Pop();
                        action(node);
                        while(!stack.IsEmpty && node == stack.Peek().Right) {
                            action(node = stack.Pop());
                        }
                    }
                    if(!stack.IsEmpty) {
                        node = stack.Peek().Right;
                    }
                    else node = null;
                }
            }
        }
        public void PostOrderTraverseRecursive(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            PostOrderTraverseRecursive(action, Root);
        }
        protected void PostOrderTraverseRecursive(Action<BinaryTreeNode<T>> action, BinaryTreeNode<T> node) {
            if(node == null) return;
            PostOrderTraverseRecursive(action, node.Left);
            PostOrderTraverseRecursive(action, node.Right);
            action(node);
        }
        public void LevelOrderTraverse(Action<BinaryTreeNode<T>> action) {
            if(Root == null) return;
            Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();
            queue.EnQueue(Root);
            while(!queue.IsEmpty) {
                BinaryTreeNode<T> node = queue.DeQueue();
                action(node);
                if(node.Left != null)
                    queue.EnQueue(node.Left);
                if(node.Right != null)
                    queue.EnQueue(node.Right);
            }
        }

    }
}
