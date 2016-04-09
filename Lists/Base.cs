using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface ILinkedListNode<T> {
        T Value { get; }
    }

    public interface ILinkedList<T> {
        void Insert(T value, int position);
        T RemoveAt(int position);
        void Clear(bool dispose);
        int GetLenght();
        T GetValue(int position);
        T GetLastValue(int position);
        void Traverse(Action<T> action);
        void Reverse();
        void ReverseRecursive();
    }
}
