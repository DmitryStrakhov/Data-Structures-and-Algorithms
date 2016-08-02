using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Value: {Value}")]
    public class SinglyLinkedListNode<T> {
        T value;
        SinglyLinkedListNode<T> next;

        public SinglyLinkedListNode(T value)
            : this(value, null) {
        }
        public SinglyLinkedListNode(T value, SinglyLinkedListNode<T> next) {
            this.value = value;
            this.next = next;
        }
        public void Redirect(SinglyLinkedListNode<T> node) {
            this.next = node;
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public T Value { get { return value; } }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SinglyLinkedListNode<T> Next { get { return next; } }
    }

    public class SinglyLinkedList<T> {
        SinglyLinkedListNode<T> head;

        public SinglyLinkedList() {
            this.head = null;
        }
        public void Insert(T value, int position) {
            Guard.IsPositive(position, nameof(position));
            if(position == 1) {
                this.head = new SinglyLinkedListNode<T>(value, this.head);
            }
            else {
                SinglyLinkedListNode<T> prev = GetNode(position - 1);
                prev.Redirect(new SinglyLinkedListNode<T>(value, prev.Next));
            }
        }
        public T RemoveAt(int position) {
            Guard.IsPositive(position, nameof(position));
            Guard.IsNotNull(this.head, nameof(position));
            T value = default(T);
            if(position == 1) {
                value = this.head.Value;
                this.head = this.head.Next;
            }
            else {
                SinglyLinkedListNode<T> node = GetNode(position - 1);
                Guard.IsNotNull(node.Next, nameof(position));
                value = node.Next.Value;
                node.Redirect(node.Next.Next);
            }
            return value;
        }
        public void Clear(bool dispose) {
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
        }
        public int GetLenght() {
            if(this.head == null) return 0;
            int itemCount = 1;
            SinglyLinkedListNode<T> node = this.head.Next;
            while(node != null) {
                itemCount++;
                node = node.Next;
            }
            return itemCount;
        }
        public T GetValue(int position) {
            Guard.IsPositive(position, nameof(position));
            return GetNode(position).Value;
        }
        public void Traverse(Action<T> action) {
            SinglyLinkedListNode<T> node = this.head;
            while(node != null) {
                action(node.Value);
                node = node.Next;
            }
        }
        public T GetLastValue(int position) {
            Guard.IsPositive(position, nameof(position));
            Guard.IsNotNull(this.head, nameof(position));
            SinglyLinkedListNode<T> node = this.head;
            SinglyLinkedListNode<T> target = this.head;
            int place = position;
            while(node != null) {
                if(place <= 0) {
                    target = target.Next;
                }
                place--;
                node = node.Next;
            }
            if(place > 0) {
                throw new ArgumentException("position");
            }
            return target.Value;
        }
        public void Reverse() {
            if(this.head == null) return;
            SinglyLinkedListNode<T> prev = null;
            SinglyLinkedListNode<T> node = this.head;
            while(node != null) {
                SinglyLinkedListNode<T> next = node.Next;
                node.Redirect(prev);
                prev = node;
                node = next;
            }
            this.head = prev;
        }
        public void ReverseRecursive() {
            ReverseRecursive(this.head);
        }
        public void TraverseReversive(Action<T> action) {
            if(this.head == null) return;
            TraverseReversive(this.head, action);
        }

        internal void TraverseReversive(SinglyLinkedListNode<T> listHead, Action<T> action) {
            if(listHead == null) return;
            TraverseReversive(listHead.Next, action);
            action(listHead.Value);
        }
        internal SinglyLinkedListNode<T> ReverseRecursive(SinglyLinkedListNode<T> node) {
            if(node == null)
                return null;
            if(node.Next == null)
                return node;
            this.head = node.Next;
            SinglyLinkedListNode<T> listHead = ReverseRecursive(this.head);
            node.Redirect(null);
            listHead.Redirect(node);
            return node;
        }
        internal SinglyLinkedListNode<T> GetNode(int position) {
            Guard.IsPositive(position, nameof(position));
            Guard.IsNotNull(this.head, nameof(position));
            int nodePos = 1;
            SinglyLinkedListNode<T> node = this.head;
            while(node != null && nodePos++ < position) {
                node = node.Next;
            }
            if(node == null) {
                throw new ArgumentException("position");
            }
            return node;
        }
        internal SinglyLinkedListNode<T> GetHead() { return head; }
    }
}
