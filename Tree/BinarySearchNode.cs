using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinarySearchTreeNode<T> : BinaryTreeNodeBase<T> {
        public BinarySearchTreeNode(T value)
            : base(value) {
        }

        public new BinarySearchTreeNode<T> Left {
            get { return (BinarySearchTreeNode<T>)base.Left; }
        }
        public new BinarySearchTreeNode<T> Right {
            get { return (BinarySearchTreeNode<T>)base.Right; }
        }
    }
}
