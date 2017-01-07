#if DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class GraphBaseTests<TVertex, TGraph> where TVertex : Vertex<char> where TGraph : Graph<char, TVertex> {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(CreateVertex('C'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, CreateVertex('D'));
        }
        [TestMethod]
        public void GraphSizeTest() {
            var graph = CreateGraph();
            Assert.AreEqual(0, graph.Size);
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.Size);
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Size);
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Size);
        }
        [TestMethod]
        public void GetVertexListTest() {
            var graph = CreateGraph();
            CollectionAssertEx.IsEmpty(graph.GetVertexList());
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            CollectionAssertEx.AreEquivalent(new char[] { 'B', 'A', 'C' }, graph.GetVertexList().Select(x => x.Value));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AreVerticesAdjacentGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AreVerticesAdjacentGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AreVerticesAdjacentGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(CreateVertex('C'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AreVerticesAdjacentGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(vA, CreateVertex('D'));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetAdjacentVertextListGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.GetAdjacentVertextList(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAdjacentVertextListGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.GetAdjacentVertextList(CreateVertex('C'));
        }

        protected abstract TGraph CreateGraph();
        protected abstract TVertex CreateVertex(char value);
    }

    public abstract class UndirectedGraphBaseTests<TVertex, TGraph> : GraphBaseTests<TVertex, TGraph> where TVertex : Vertex<char> where TGraph : Graph<char, TVertex> {
        [TestMethod]
        public void AreVerticesAdjacentTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            Assert.IsTrue(graph.AreVerticesAdjacent(vA, vB));
            Assert.IsTrue(graph.AreVerticesAdjacent(vB, vC));
            Assert.IsFalse(graph.AreVerticesAdjacent(vA, vC));
            Assert.IsTrue(graph.AreVerticesAdjacent(vB, vA));
            Assert.IsTrue(graph.AreVerticesAdjacent(vC, vB));
            Assert.IsFalse(graph.AreVerticesAdjacent(vC, vA));
        }
        [TestMethod]
        public void GetAdjacentVertextListTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vA, vC);
            CollectionAssertEx.AreEquivalent(new char[] { 'B', 'C' }, graph.GetAdjacentVertextList(vA).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'A', 'C' }, graph.GetAdjacentVertextList(vB).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'A', 'B' }, graph.GetAdjacentVertextList(vC).Select(x => x.Value));
        }
    }

    public abstract class DirectedGraphBaseTests<TVertex, TGraph> : GraphBaseTests<TVertex, TGraph> where TVertex : Vertex<char> where TGraph : Graph<char, TVertex> {
        [TestMethod]
        public void AreVerticesAdjacentTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            Assert.IsTrue(graph.AreVerticesAdjacent(vA, vB));
            Assert.IsTrue(graph.AreVerticesAdjacent(vB, vC));
            Assert.IsFalse(graph.AreVerticesAdjacent(vA, vC));
            Assert.IsFalse(graph.AreVerticesAdjacent(vB, vA));
            Assert.IsFalse(graph.AreVerticesAdjacent(vC, vB));
            Assert.IsFalse(graph.AreVerticesAdjacent(vC, vA));
        }
        [TestMethod]
        public void GetAdjacentVertextListTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            CollectionAssertEx.AreEquivalent(new char[] { 'B' }, graph.GetAdjacentVertextList(vA).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'C' }, graph.GetAdjacentVertextList(vB).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'A' }, graph.GetAdjacentVertextList(vC).Select(x => x.Value));
        }
    }


    [TestClass]
    public class AdjMatrixGraphTests : UndirectedGraphBaseTests<AdjMatrixGraphVertex<char>, AdjMatrixGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            AdjMatrixGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            AdjMatrixGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.Matrix.Size);
            Assert.IsFalse(graph.Matrix[0, 0]);
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 0 }, { 0, 0 } }, graph.GetMatrixData());
            graph.CreateEdge(vA, vA);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 0 }, { 0, 0 } }, graph.GetMatrixData());
            graph.CreateEdge(vA, vB);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 1 }, { 1, 0 } }, graph.GetMatrixData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjMatrixGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 1, 1, 1 }, { 1, 0, 1, 0 }, { 1, 1, 0, 1 }, { 1, 0, 1, 0 } }, graph.GetMatrixData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            AdjMatrixGraph<char> graph = new AdjMatrixGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            AdjMatrixGraph<char> graph = new AdjMatrixGraph<char>(0);
        }

        protected override AdjMatrixGraph<char> CreateGraph() {
            return new AdjMatrixGraph<char>();
        }
        protected override AdjMatrixGraphVertex<char> CreateVertex(char value) {
            return new AdjMatrixGraphVertex<char>(value);
        }
    }

    [TestClass]
    public class AdjListGraphTests : UndirectedGraphBaseTests<AdjListGraphVertex<char>, AdjListGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            AdjListGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            AdjListGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A', 'B' }, new char[] { 'B', 'A' } } , graph.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjListGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.GetData().Length);
            Assert.AreEqual(4, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'B', 'C', 'D' }, new char[] { 'B', 'A', 'C' }, new char[] { 'C', 'A', 'B', 'D' }, new char[] { 'D', 'A', 'C' } }, graph.GetData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            AdjListGraph<char> graph = new AdjListGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            AdjListGraph<char> graph = new AdjListGraph<char>(0);
        }

        protected override AdjListGraph<char> CreateGraph() {
            return new AdjListGraph<char>();
        }
        protected override AdjListGraphVertex<char> CreateVertex(char value) {
            return new AdjListGraphVertex<char>(value);
        }
    }

    [TestClass]
    public class AdjSetGraphTests : UndirectedGraphBaseTests<AdjSetGraphVertex<char>, AdjSetGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            AdjSetGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            AdjSetGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A' } }, graph.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'B', 'A' }, new char[] { 'A', 'B' } }, graph.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjSetGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.GetData().Length);
            Assert.AreEqual(4, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'B', 'C', 'A', 'D' }, new char[] { 'C', 'B', 'A' }, new char[] { 'A', 'B', 'D', 'C' }, new char[] { 'D', 'A', 'C' } }, graph.GetData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            AdjSetGraph<char> graph = new AdjSetGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            AdjSetGraph<char> graph = new AdjSetGraph<char>(0);
        }

        protected override AdjSetGraph<char> CreateGraph() {
            return new AdjSetGraph<char>();
        }
        protected override AdjSetGraphVertex<char> CreateVertex(char value) {
            return new AdjSetGraphVertex<char>(value);
        }
    }

    [TestClass]
    public class DirectedAdjMatrixGraphTests : DirectedGraphBaseTests<AdjMatrixGraphVertex<char>, DirectedAdjMatrixGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            DirectedAdjMatrixGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            DirectedAdjMatrixGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0 } }, graph.GetMatrixData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 0 }, { 0, 0 } }, graph.GetMatrixData());
            graph.CreateEdge(vA, vA);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 0 }, { 0, 0 } }, graph.GetMatrixData());
            graph.CreateEdge(vA, vB);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 1 }, { 0, 0 } }, graph.GetMatrixData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjMatrixGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 1, 1, 1 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 0 } }, graph.GetMatrixData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>(0);
        }

        protected override DirectedAdjMatrixGraph<char> CreateGraph() {
            return new DirectedAdjMatrixGraph<char>();
        }
        protected override AdjMatrixGraphVertex<char> CreateVertex(char value) {
            return new AdjMatrixGraphVertex<char>(value);
        }
    }

    [TestClass]
    public class DirectedAdjListGraphTests : DirectedGraphBaseTests<AdjListGraphVertex<char>, DirectedAdjListGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            DirectedAdjListGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            DirectedAdjListGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A', 'B' }, new char[] { 'B' } }, graph.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjListGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.GetData().Length);
            Assert.AreEqual(4, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'B', 'C', 'D' }, new char[] { 'B', 'C' }, new char[] { 'C', 'D' }, new char[] { 'D' } }, graph.GetData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            DirectedAdjListGraph<char> graph = new DirectedAdjListGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            DirectedAdjListGraph<char> graph = new DirectedAdjListGraph<char>(0);
        }

        protected override DirectedAdjListGraph<char> CreateGraph() {
            return new DirectedAdjListGraph<char>();
        }
        protected override AdjListGraphVertex<char> CreateVertex(char value) {
            return new AdjListGraphVertex<char>(value);
        }
    }

    [TestClass]
    public class DirectedAdjSetGraphTests : DirectedGraphBaseTests<AdjSetGraphVertex<char>, DirectedAdjSetGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            DirectedAdjSetGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(2, vC.Handle);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            DirectedAdjSetGraph<char> graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            Assert.AreEqual(1, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'B', 'A' }, new char[] { 'B' } }, graph.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjSetGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            Assert.AreEqual(4, graph.GetData().Length);
            Assert.AreEqual(4, graph.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'D', 'C', 'B', 'A' }, new char[] { 'C', 'B' }, new char[] { 'D', 'C' }, new char[] { 'D' } }, graph.GetData());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            DirectedAdjSetGraph<char> graph = new DirectedAdjSetGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            DirectedAdjSetGraph<char> graph = new DirectedAdjSetGraph<char>(0);
        }

        protected override DirectedAdjSetGraph<char> CreateGraph() {
            return new DirectedAdjSetGraph<char>();
        }
        protected override AdjSetGraphVertex<char> CreateVertex(char value) {
            return new AdjSetGraphVertex<char>(value);
        }
    }


    class GraphTestUtils {
        #region Test Graph
        internal static void InitializeGraph<TVertex>(Graph<char, TVertex> graph) where TVertex : Vertex<char> {
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
        }
        #endregion
    }
}
#endif