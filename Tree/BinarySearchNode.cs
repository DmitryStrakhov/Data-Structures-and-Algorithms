using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class BinarySearchTreeNode<T> : BinaryTreeNodeBase<T> {
        int height;

        public BinarySearchTreeNode(T value) : base(value) {
            this.height = -1;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Height { get { return height; } }

        public new BinarySearchTreeNode<T> Left {
            get { return (BinarySearchTreeNode<T>)base.Left; }
        }
        public new BinarySearchTreeNode<T> Right {
            get { return (BinarySearchTreeNode<T>)base.Right; }
        }
        
        internal void SetHeight(int height) { this.height = height; }
    }
}
