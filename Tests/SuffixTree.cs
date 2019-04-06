#if DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class SuffixTreeTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            new SuffixTree(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            new SuffixTree('$', null);
        }
        [TestMethod]
        public void EmptyTreeTest() {
            CollectionAssertEx.AreEqual(Terminal(1).Yield(), new SuffixTree("").BFS());
        }
        [TestMethod]
        public void SuffixTreeTest1() {
            SuffixTree tree = new SuffixTree('$', "tatat");
            SuffixTreeNodeTriplet[] expected = new SuffixTreeNodeTriplet[] {
                NonTerminal("at", "t"),
                NonTerminal("$", "at$"),
                NonTerminal("$", "at"),
                Terminal(3),
                Terminal(1),
                Terminal(4),
                NonTerminal("$", "at$"),
                Terminal(2),
                Terminal(0),
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS());
        }
        [TestMethod]
        public void SuffixTreeTest2() {
            SuffixTree tree = new SuffixTree('$', "BANANA");
            SuffixTreeNodeTriplet[] expected = new SuffixTreeNodeTriplet[] {
                NonTerminal("A", "BANANA$", "NA"),
                NonTerminal("$", "NA"),
                Terminal(0),
                NonTerminal("$", "NA$"),
                Terminal(5),
                NonTerminal("$", "NA$"),
                Terminal(4),
                Terminal(2),
                Terminal(3),
                Terminal(1),
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS());
        }
        [TestMethod]
        public void SuffixTreeTest3() {
            SuffixTree tree = new SuffixTree('$', "SUFFIX");
            SuffixTreeNodeTriplet[] expected = new SuffixTreeNodeTriplet[] {
                NonTerminal("F", "IX$", "SUFFIX$", "UFFIX$", "X$"),
                NonTerminal("FIX$", "IX$"),
                Terminal(4),
                Terminal(0),
                Terminal(1),
                Terminal(5),
                Terminal(2),
                Terminal(3),
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS());
        }
        [TestMethod]
        public void SuffixTreeTest4() {
            SuffixTree tree = new SuffixTree('$', "PREFIX");
            SuffixTreeNodeTriplet[] expected = new SuffixTreeNodeTriplet[] {
                NonTerminal("EFIX$", "FIX$", "IX$", "PREFIX$", "REFIX$", "X$"),
                Terminal(2),
                Terminal(3),
                Terminal(4),
                Terminal(0),
                Terminal(1),
                Terminal(5),
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ContainsGuardTest() {
            new SuffixTree("test").Contains(null);
        }
        [TestMethod]
        public void ContainsTest1() {
            SuffixTree tree = new SuffixTree("");
            Assert.IsTrue(tree.Contains(""));
            Assert.IsFalse(tree.Contains("1"));
            Assert.IsFalse(tree.Contains("test"));
        }
        [TestMethod]
        public void ContainsTest2() {
            SuffixTree tree = new SuffixTree("BANANA");
            Assert.IsTrue(tree.Contains(""));
            Assert.IsTrue(tree.Contains("A"));
            Assert.IsTrue(tree.Contains("NA"));
            Assert.IsTrue(tree.Contains("ANA"));
            Assert.IsTrue(tree.Contains("NANA"));
            Assert.IsTrue(tree.Contains("ANANA"));
            Assert.IsTrue(tree.Contains("BANANA"));
            Assert.IsTrue(tree.Contains("BAN"));
            Assert.IsTrue(tree.Contains("BANAN"));

            Assert.IsFalse(tree.Contains("BANANA1"));
            Assert.IsFalse(tree.Contains("NAB"));
            Assert.IsFalse(tree.Contains("x"));
            Assert.IsFalse(tree.Contains("Na"));
            Assert.IsFalse(tree.Contains("Tt"));
            Assert.IsFalse(tree.Contains("ANAu"));
        }
        [TestMethod]
        public void ContainsTest3() {
            SuffixTree tree = new SuffixTree("SUF FIX");
            Assert.IsTrue(tree.Contains("F "));
            Assert.IsTrue(tree.Contains(" "));
            Assert.IsTrue(tree.Contains("SUF FIX"));
            Assert.IsTrue(tree.Contains("X"));

            Assert.IsFalse(tree.Contains("Y"));
            Assert.IsFalse(tree.Contains("IXX"));
            Assert.IsFalse(tree.Contains("  "));
            Assert.IsFalse(tree.Contains("T"));
        }
        [TestMethod]
        public void SuffixTreeBuilder_CreateRootSuffixGroupTest1() {
            SuffixTreeBuilder.BuildSuffixGroup("$").Assert("$");
            SuffixTreeBuilder.BuildSuffixGroup("X$").Assert("X$");
        }
        [TestMethod]
        public void SuffixTreeBuilder_CreateRootSuffixGroupTest2() {
            SuffixGroup group = SuffixTreeBuilder.BuildSuffixGroup("sample$");
            group.Assert("e$", "le$", "ple$", "mple$", "ample$", "sample$");
        }
        [TestMethod]
        public void SuffixGroup_BuildGroupsTest1() {
            SuffixGroup group = new SuffixGroup("$");
            CollectionAssertEx.AreEqual(group.Yield(), group.BuildGroups());
        }
        [TestMethod]
        public void SuffixGroup_BuildGroupsTest2() {
            SuffixGroup group = new SuffixGroup("a$", "na$", "ana$", "nana$", "anana$", "banana$");
            SuffixGroup[] expected = new SuffixGroup[] {
                new SuffixGroup("a$", "ana$", "anana$"),
                new SuffixGroup("na$", "nana$"),
                new SuffixGroup("banana$"),
            };
            CollectionAssertEx.AreEquivalent(expected, group.BuildGroups());
        }
        [TestMethod]
        public void SuffixGroup_ReduceTest1() {
            SuffixGroup group = new SuffixGroup("$");
            string prefix;
            Assert.AreEqual(group, group.Reduce(out prefix));
            Assert.AreEqual("$", prefix);
        }
        [TestMethod]
        public void SuffixGroup_ReduceTest2() {
            SuffixGroup group = new SuffixGroup("test$");
            string prefix;
            Assert.AreEqual(group, group.Reduce(out prefix));
            Assert.AreEqual("test$", prefix);
        }
        [TestMethod]
        public void SuffixGroup_ReduceTest3() {
            SuffixGroup group = new SuffixGroup("a$", "ana$", "anana$");
            string prefix;
            Assert.AreEqual(new SuffixGroup("$", "na$", "nana$"), group.Reduce(out prefix));
            Assert.AreEqual("a", prefix);
        }
        [TestMethod]
        public void SuffixGroup_ReduceTest4() {
            SuffixGroup group = new SuffixGroup("nan$", "nana$", "naa$");
            string prefix;
            Assert.AreEqual(new SuffixGroup("n$", "na$", "a$"), group.Reduce(out prefix));
            Assert.AreEqual("na", prefix);
        }
        [TestMethod]
        public void SuffixTreeNode_DefaultsTest() {
            SuffixTreeNode node = new SuffixTreeNode(-1, false);
            Assert.AreEqual(-1, node.Index);
            Assert.IsFalse(node.IsTerminal);
            CollectionAssertEx.IsEmpty(node.GetKeys());
            CollectionAssertEx.IsEmpty(node.GetChildren());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SuffixTreeNode_AddChildGuardCase1Test() {
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            root.AddChild("", new SuffixTreeNode(1, true));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SuffixTreeNode_AddChildGuardCase2Test() {
            SuffixTreeNode child = new SuffixTreeNode(1, true);
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            root.AddChild("key", child);
            root.AddChild("key", child);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SuffixTreeNode_AddChildGuardCase3Test() {
            SuffixTreeNode child = new SuffixTreeNode(1, true);
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            root.AddChild("key1", child);
            root.AddChild("key2", child);
        }
        [TestMethod]
        public void SuffixTreeNode_AddChildTest() {
            SuffixTreeNode child1 = new SuffixTreeNode(1, true);
            SuffixTreeNode child2 = new SuffixTreeNode(2, true);
            SuffixTreeNode child3 = new SuffixTreeNode(3, true);
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            root.AddChild("zkey3", child3);
            root.AddChild("ykey2", child2);
            root.AddChild("xkey1", child1);
            CollectionAssert.AreEqual(new string[] { "xkey1", "ykey2", "zkey3" }, root.GetKeys());
            CollectionAssert.AreEqual(new SuffixTreeNode[] { child1, child2, child3 }, root.GetChildren());
        }
        [TestMethod]
        public void SuffixTreeNode_GetEdgeTest() {
            SuffixTreeNode child1 = new SuffixTreeNode(1, true);
            SuffixTreeNode child2 = new SuffixTreeNode(2, true);
            SuffixTreeNode child3 = new SuffixTreeNode(3, true);
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            root.AddChild("xkey", child1);
            root.AddChild("ykey", child2);
            root.AddChild("zkey", child3);

            Assert.AreEqual(new SuffixTreeEdge("xkey", root, child1), root.GetEdge('x'));
            Assert.AreEqual(new SuffixTreeEdge("ykey", root, child2), root.GetEdge('y'));
            Assert.AreEqual(new SuffixTreeEdge("zkey", root, child3), root.GetEdge('z'));
            Assert.IsNull(root.GetEdge('a'));
            Assert.IsNull(root.GetEdge('b'));
            Assert.IsNull(root.GetEdge('c'));
        }
        [TestMethod]
        public void SuffixTreeNodeResolver_ResolveSimpleTest() {
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            SuffixTreeNodeResolver resolver = new SuffixTreeNodeResolver(root);
            Assert.AreSame(root, resolver.Resolve(""));
            Assert.IsNull(resolver.Resolve("A"));
            Assert.IsNull(resolver.Resolve("B"));
            Assert.IsNull(resolver.Resolve("TEST"));
        }
        [TestMethod]
        public void SuffixTreeNodeResolver_ResolveTest1() {
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            SuffixTreeNodeResolver resolver = new SuffixTreeNodeResolver(root);

            SuffixTreeNode aChild = new SuffixTreeNode(1, true);
            SuffixTreeNode xyzChild = new SuffixTreeNode(2, true);
            SuffixTreeNode zzChild = new SuffixTreeNode(3, true);
            root.AddChild("A", aChild);
            root.AddChild("XYZ$", xyzChild);
            root.AddChild("ZZ", zzChild);

            Assert.AreSame(aChild, resolver.Resolve("A"));
            Assert.IsNull(resolver.Resolve("AA"));
            Assert.IsNull(resolver.Resolve("B"));

            Assert.AreSame(xyzChild, resolver.Resolve("X"));
            Assert.AreSame(xyzChild, resolver.Resolve("XY"));
            Assert.AreSame(xyzChild, resolver.Resolve("XYZ"));
            Assert.AreSame(xyzChild, resolver.Resolve("XYZ$"));
            Assert.IsNull(resolver.Resolve("XYZ$$"));

            Assert.AreSame(zzChild, resolver.Resolve("Z"));
            Assert.AreSame(zzChild, resolver.Resolve("ZZ"));
            Assert.IsNull(resolver.Resolve("ZZZ"));
            Assert.AreSame(root, resolver.Resolve(""));
        }
        [TestMethod]
        public void SuffixTreeNodeResolver_ResolveTest2() {
            SuffixTreeNode root = new SuffixTreeNode(-1, false);
            SuffixTreeNodeResolver resolver = new SuffixTreeNodeResolver(root);

            SuffixTreeNode xxChild = new SuffixTreeNode(-1, false);
            SuffixTreeNode yyChild = new SuffixTreeNode(2, true);
            SuffixTreeNode zzChild = new SuffixTreeNode(3, true);
            root.AddChild("XX", xxChild);
            xxChild.AddChild("YY", yyChild);
            xxChild.AddChild("ZZ", zzChild);
            Assert.AreSame(xxChild, resolver.Resolve("X"));
            Assert.AreSame(xxChild, resolver.Resolve("XX"));
            Assert.AreSame(yyChild, resolver.Resolve("XXY"));
            Assert.AreSame(yyChild, resolver.Resolve("XXYY"));
            Assert.AreSame(zzChild, resolver.Resolve("XXZ"));
            Assert.AreSame(zzChild, resolver.Resolve("XXZZ"));
            Assert.IsNull(resolver.Resolve("XXYZ"));
        }

        static SuffixTreeNodeTriplet Terminal(int index) {
            return new SuffixTreeNodeTriplet(index, true, new string[0]);
        }
        static SuffixTreeNodeTriplet NonTerminal(params string[] labels) {
            return new SuffixTreeNodeTriplet(-1, false, labels);
        }
    }


    [DebuggerDisplay("SuffixTreeNodeTriplet(IsTerminal={IsTerminal},Index={Index})")]
    sealed class SuffixTreeNodeTriplet : EquatableObject<SuffixTreeNodeTriplet> {
        readonly string[] labels;
        readonly int index;
        readonly bool isTerminal;

        public SuffixTreeNodeTriplet(int index, bool isTerminal, params string[] labels) {
            this.index = index;
            this.labels = labels;
            this.isTerminal = isTerminal;
        }

        #region EqualsTo

        protected override bool EqualsTo(SuffixTreeNodeTriplet other) {
            return index == other.index && isTerminal == other.isTerminal && Enumerable.SequenceEqual(labels, other.labels);
        }

        #endregion
    }


    static class SuffixGroupTestExtensions {
        public static void Assert(this SuffixGroup @this, params string[] expectedSuffixes) {
            CollectionAssert.AreEqual(expectedSuffixes, @this.Suffixes);
        }
    }


    static class SuffixTreeTestExtensions {
        public static IEnumerable<SuffixTreeNodeTriplet> BFS(this SuffixTree @this) {
            Queue<SuffixTreeNode> queue = new Queue<SuffixTreeNode>();
            queue.EnQueue(@this.Root);
            while(!queue.IsEmpty) {
                SuffixTreeNode node = queue.DeQueue();
                yield return new SuffixTreeNodeTriplet(node.Index, node.IsTerminal, node.GetKeys());
                queue.Fill(node.GetChildren());
            }
        }
    }
}
#endif