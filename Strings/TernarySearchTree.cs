using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("TernarySearchTree(Size={Size})")]
    public sealed class TernarySearchTree<T> : EnumerableBase<KeyValuePair<string, T>> {
        int size;
        TernarySearchTreeNode<T> root;

        public TernarySearchTree() {
            this.size = 0;
            this.root = null;
        }
        public void Insert(string key) {
            Guard.IsNotNull(key, nameof(key));
            Insert(key, default(T));
        }
        public void Insert(string key, T tag) {
            Guard.IsNotNull(key, nameof(key));
            root = InsertCore(root, key, 0, tag);
        }
        public void Delete(string key) {
            Guard.IsNotNull(key, nameof(key));
            DeleteCore(root, key, 0);
        }
        public int Size {
            get { return size; }
        }
        public bool Contains(string key) {
            Guard.IsNotNull(key, nameof(key));
            return SearchCore(root, key, 0) != null;
        }
        public bool Search(string key, out T tag) {
            Guard.IsNotNull(key, nameof(key));
            var node = SearchCore(root, key, 0);
            tag = node.Return(x => x.Value, default(T));
            return node != null;
        }

        private TernarySearchTreeNode<T> InsertCore(TernarySearchTreeNode<T> node, string key, int index, T tag) {
            if(!key.IsIndexValid(index)) return null;
            if(node == null) node = new TernarySearchTreeNode<T>(key[index]);
            char @char = key[index];
            if(@char == node.Char) {
                if(index == key.Length - 1) {
                    if(!node.IsEos) {
                        node.IsEos = true;
                        node.Value = tag;
                        size++;
                    }
                    return node;
                }
                node.Equal = InsertCore(node.Equal, key, index + 1, tag);
            }
            else if(@char < node.Char) {
                node.Left = InsertCore(node.Left, key, index, tag);
            }
            else {
                node.Right = InsertCore(node.Right, key, index, tag);
            }
            return node;
        }
        private void DeleteCore(TernarySearchTreeNode<T> node, string key, int index) {
            if(node == null || !key.IsIndexValid(index)) return;
            char @char = key[index];
            if(@char == node.Char) {
                if(index == key.Length - 1) {
                    if(node.IsEos) {
                        size--;
                        node.IsEos = false;
                    }
                    node.Value = default(T);
                    return;
                }
                DeleteCore(node.Equal, key, index + 1);
            }
            else if(@char < node.Char) {
                DeleteCore(node.Left, key, index);
            }
            else {
                DeleteCore(node.Right, key, index);
            }
        }
        private TernarySearchTreeNode<T> SearchCore(TernarySearchTreeNode<T> node, string key, int index) {
            if(node == null || !key.IsIndexValid(index)) return null;
            char @char = key[index];
            if(@char == node.Char) {
                if(index == key.Length - 1) return node.IsEos ? node : null;
                return SearchCore(node.Equal, key, index + 1);
            }
            else if(@char < node.Char) {
                return SearchCore(node.Left, key, index);
            }
            else {
                return SearchCore(node.Right, key, index);
            }
        }

        #region IEnumerable

        protected override IEnumerator<KeyValuePair<string, T>> CreateEnumerator() {
            Stack<TernarySearchTreeNode<T>> stack = new Stack<TernarySearchTreeNode<T>>();
            StringBuilder stringBuilder = new StringBuilder(32);

            TernarySearchTreeNode<T> node = root;
            while(true) {
                while(node != null) {
                    stack.Push(node);
                    node = node.Left;
                }
                if(stack.IsEmpty) yield break;
                node = stack.Peek();
                if(node != null) {
                    stringBuilder.Append(node.Char);
                    if(node.IsEos) yield return new KeyValuePair<string, T>(stringBuilder.ToString(), node.Value);
                    stack.Push(null);
                    node = node.Equal;
                }
                else {
                    stringBuilder.RemoveLastChar();
                    stack.Pop();
                    node = stack.Pop().Right;
                }
            }
        }

        #endregion

        internal TernarySearchTreeNode<T> Root { get { return root; } }
    }


    [DebuggerDisplay("TernarySearchTreeNode(Char={Char},Value={Value},IsEos={IsEos})")]
    class TernarySearchTreeNode<T> {
        readonly char @char;
        T value;
        bool isEos;
        TernarySearchTreeNode<T> left;
        TernarySearchTreeNode<T> equal;
        TernarySearchTreeNode<T> right;

        public TernarySearchTreeNode(char @char) {
            this.@char = @char;
            this.isEos = false;
            this.value = default(T);
        }
        public char Char {
            get { return @char; }
        }
        public T Value {
            get { return value; }
            set { this.value = value; }
        }
        public bool IsEos {
            get { return isEos; }
            set { isEos = value; }
        }

        public TernarySearchTreeNode<T> Left {
            get { return left; }
            set { left = value; }
        }
        public TernarySearchTreeNode<T> Equal {
            get { return equal; }
            set { equal = value; }
        }
        public TernarySearchTreeNode<T> Right {
            get { return right; }
            set { right = value; }
        }
    }
}
