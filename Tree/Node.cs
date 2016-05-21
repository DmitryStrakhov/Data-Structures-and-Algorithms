using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Value: {Value}")]
    public class BinaryTreeNode<T> {
        T value;
        BinaryTreeNode<T> left;
        BinaryTreeNode<T> right;

        public BinaryTreeNode(T value)
            : this(value, null, null) {
        }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right) {
            this.value = value;
            this.left = left;
            this.right = right;
        }
        
        public BinaryTreeNode<T> Left {
            get { return left; }
        }
        public BinaryTreeNode<T> Right {
            get { return right; }
        }
        public T Value { get { return value; } }
    }
}
