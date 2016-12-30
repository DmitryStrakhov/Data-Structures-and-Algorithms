#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class StackTests {
        [TestMethod]
        public void StackSimpleTest() {
            Stack<int> stack = new Stack<int>();
            Assert.IsNull(stack.GetHead());
            Assert.IsTrue(stack.IsEmpty);
        }
        [TestMethod]
        public void StackPushTest() {
            Stack<int> stack = new Stack<int>();
            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Size);
            stack.Push(1);
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Size);
            stack.Push(1);
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(2, stack.Size);
        }
        [TestMethod]
        public void StackIsFullTest() {
            Stack<int> stack = new Stack<int>();
            Assert.IsFalse(stack.IsFull);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void StackPopGuardTest() {
            Stack<int> stack = new Stack<int>();
            int value = stack.Pop();
        }
        [TestMethod]
        public void StackPopTest() {
            Stack<int> stack = new Stack<int>();
            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Size);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(3, stack.Pop());
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(2, stack.Pop());
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, stack.Pop());
            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Size);
            Assert.IsNull(stack.GetHead());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void StackPeekGuardTest() {
            Stack<int> stack = new Stack<int>();
            int value = stack.Peek();
        }
        [TestMethod]
        public void StackPeekTest() {
            Stack<int> stack = new Stack<int>();
            stack.Push(11);
            Assert.AreEqual(11, stack.Peek());
            stack.Push(12);
            Assert.AreEqual(12, stack.Peek());
        }
        [TestMethod]
        public void StackClearTest() {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            Assert.AreEqual(5, stack.Size);
            stack.Clear();
            Assert.AreEqual(0, stack.Size);
            Assert.IsNull(stack.GetHead());
        }
    }
}

#endif