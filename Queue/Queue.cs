using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Queue (Size = {Size})")]
    public class Queue<T> {
        SinglyLinkedListNode<T> front;
        SinglyLinkedListNode<T> rear;
        int size;

        public Queue() {
            this.front = this.rear = null;
            this.size = 0;
        }

        public void EnQueue(T value) {
            DemandEnQueue(value);
            SinglyLinkedListNode<T> node = new SinglyLinkedListNode<T>(value);
            if(IsEmpty) {
                this.rear = this.front = node;
            }
            else {
                this.rear.Redirect(node);
                this.rear = node;
            }
            this.size++;
        }
        protected virtual void DemandEnQueue(T value) { }

        public T DeQueue() {
            if(IsEmpty) {
                throw new InvalidOperationException("Queue is empty");
            }
            DemandDeQueue();
            T value = this.front.Value;
            this.front = this.front.Next;
            this.size--;
            return value;
        }
        protected virtual void DemandDeQueue() { }

        public T Peek() {
            if(IsEmpty) {
                throw new InvalidOperationException("Queue is empty");
            }
            return this.front.Value;
        }
        public void Clear() { Clear(false); }
        public void Clear(bool disposing) {
            if(disposing) {
                SinglyLinkedListNode<T> node = this.front;
                while(node != null) {
                    IDisposable obj = node.Value as IDisposable;
                    if(obj != null)
                        obj.Dispose();
                    node = node.Next;
                }
            }
            this.size = 0;
            this.front = this.rear = null;
        }
        public T[] ToArray() {
            T[] result = new T[Size];
            int n = 0;
            SinglyLinkedListNode<T> node = this.front;
            while(node != null) {
                result[n++] = node.Value;
                node = node.Next;
            }
            return result;
        }
        public int Size {
            get { return size; }
        }
        public bool IsEmpty {
            get { return Size == 0; }
        }
        public virtual bool IsFull {
            get { return false; }
        }
    }
}
