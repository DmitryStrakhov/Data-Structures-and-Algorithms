using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("TreeSorter(Size = {treeSize})")]
    public class TreeSorter<T> {
        readonly AVLTree<Node> tree;
        int treeSize;
        readonly Comparison<T> comparison;

        public TreeSorter() {
            Guard.IsAssignableFrom<IComparable, T>();
            this.comparison = ComparisonCore.Compare;
            this.tree = new AVLTree<Node>(new NodeComparer(ComparisonCore.Compare));
        }
        public TreeSorter(Comparison<T> comparison) {
            Guard.IsNotNull(comparison, nameof(comparison));
            this.comparison = comparison;
            this.tree = new AVLTree<Node>(new NodeComparer(comparison));
        }
        public void AddItem(T item) {
            Guard.IsNotNull(item, nameof(item));
            this.tree.Insert(new Node(item), (x, node) => {
                if(AreEqual(x.Value, item))
                    x.Tail = x.Tail.Next = node;
            });
            this.treeSize++;
        }
        public void AddBlock(IEnumerable<T> items) {
            Guard.IsNotNull(items, nameof(items));
            items.ForEach(x => AddItem(x));
        }
        public T[] Sort() {
            return new TreeWalker(this.tree, this.treeSize).Walk();
        }

        #region AreEqual

        bool AreEqual(T x, T y) {
            return this.comparison(x, y) == 0;
        }

        #endregion

        #region Node

        [DebuggerDisplay("Node({Value})")]
        class Node {
            readonly T value;
            Node next;
            Node tail;

            public Node(T value) {
                this.value = value;
                this.next = null;
                this.tail = this;
            }
            internal Node Next {
                get { return next; }
                set { next = value; }
            }
            internal Node Tail {
                get { return tail; }
                set { tail = value; }
            }
            public T Value { get { return value; } }
        }

        #endregion

        #region NodeComparer

        class NodeComparer : IComparer<Node> {
            readonly Comparison<T> comparison;

            public NodeComparer(Comparison<T> comparison) {
                this.comparison = comparison;
            }
            public int Compare(Node x, Node y) {
                return this.comparison(x.Value, y.Value);
            }
        }

        #endregion

        #region TreeWalker

        class TreeWalker {
            readonly AVLTree<Node> tree;
            readonly int size;

            public TreeWalker(AVLTree<Node> tree, int size) {
                this.size = size;
                this.tree = tree;
            }
            int walkIndex;
            public T[] Walk() {
                T[] result = new T[size];
                this.walkIndex = 0;
                this.tree.InOrderTraverse(x => {
                    Node head = x.Value;
                    Node current = head;
                    do {
                        result[walkIndex++] = current.Value;
                        current = current.Next;
                    }
                    while(current != null);
                });
                return result;
            }
        }

        #endregion
    }
}
