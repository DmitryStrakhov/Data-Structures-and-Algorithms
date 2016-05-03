using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class Stack<T> {
        int size;
        SinglyLinkedListNode<T> head;

        public Stack() {
            this.size = 0;
            this.head = null;
        }
        public void Push(T value) {
            this.head = new SinglyLinkedListNode<T>(value, this.head);
            this.size++;
        }
        public T Pop() {
            if(IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            T value = this.head.Value;
            this.head = this.head.Next;
            this.size--;
            return value;
        }
        public T Peek() {
            if(IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            return this.head.Value;
        }
        public void Clear() { Clear(false); }
        public void Clear(bool dispose) {
            if(this.head == null) return;
            if(dispose) {
                SinglyLinkedListNode<T> node = this.head;
                while(node != null) {
                    IDisposable obj = node.Value as IDisposable;
                    if(obj != null)
                        obj.Dispose();
                    node = node.Next;
                }
            }
            this.head = null;
            this.size = 0;
        }
        public int Size {
            get { return size; }
        }
        public bool IsEmpty {
            get { return Size == 0; }
        }
        public bool IsFull {
            get { return false; }
        }

        internal SinglyLinkedListNode<T> GetHead() {
            return this.head;
        }
    }
}
