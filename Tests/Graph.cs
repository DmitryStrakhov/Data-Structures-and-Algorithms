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
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Action<TVertex> action = null;
            graph.DFSearch(action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Func<TVertex, bool> action = null;
            graph.DFSearch(action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DFSearch(null, x => { });
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DFSearchGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DFSearch(CreateVertex('C'), x => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Action<TVertex> action = null;
            graph.DFSearch(vA, action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase6Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DFSearch(null, x => true);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DFSearchGuardCase7Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DFSearch(CreateVertex('C'), x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DFSearchGuardCase8Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Func<TVertex, bool> action = null;
            graph.DFSearch(vA, action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Action<TVertex> action = null;
            graph.BFSearch(action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Func<TVertex, bool> action = null;
            graph.BFSearch(action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.BFSearch(null, x => { });
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void BFSearchGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.BFSearch(CreateVertex('C'), x => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Action<TVertex> action = null;
            graph.BFSearch(vA, action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase6Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.BFSearch(null, x => true);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void BFSearchGuardCase7Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.BFSearch(CreateVertex('C'), x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BFSearchGuardCase8Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            Func<TVertex, bool> action = null;
            graph.BFSearch(vA, action);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetVertexGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var result = graph.GetVertex(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetVertexGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var result = graph.GetVertex(3);
        }
        [TestMethod]
        public void GetVertexTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            Assert.AreSame(vA, graph.GetVertex(0));
            Assert.AreSame(vB, graph.GetVertex(1));
            Assert.AreSame(vC, graph.GetVertex(2));
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
        [TestMethod]
        public void DFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.DFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'E'; });
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'D', 'E' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'B', 'A', 'H', 'D', 'G' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'A'; });
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'B', 'A' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.BFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'H', 'D', 'E', 'F', 'G' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'H', 'D' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'G', 'H', 'B', 'D', 'A' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'G', 'H', }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
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
        [TestMethod]
        public void DFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.DFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'D', 'H', 'E', 'G', 'F' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'D', 'H' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'D', 'G', 'H', 'A', 'B' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void DFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'G'; });
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'D', 'G' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.BFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'H', 'D', 'E', 'G', 'F' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C', 'H', 'D' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'G', 'D', 'H', 'A', 'B' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
        }
        [TestMethod]
        public void BFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(new char[] { 'F', 'E', 'C', 'G', 'D' }, vertexList);
            CollectionAssertEx.TrueForAllItems(graph.GetVertexList(), x => x.Color == VertexColor.None);
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
            Assert.AreEqual(1, graph.Data.Matrix.Size);
            Assert.IsFalse(graph.Data.Matrix[0, 0]);
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 0 }, { 0, 0 } }, graph.Data.GetMatrixData());
            graph.CreateEdge(vA, vA);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 0 }, { 0, 0 } }, graph.Data.GetMatrixData());
            graph.CreateEdge(vA, vB);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 1 }, { 1, 0 } }, graph.Data.GetMatrixData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjMatrixGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 1, 1, 1 }, { 1, 0, 1, 0 }, { 1, 1, 0, 1 }, { 1, 0, 1, 0 } }, graph.Data.GetMatrixData());
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
            Assert.AreEqual(1, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.Data.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A', 'B' }, new char[] { 'B', 'A' } }, graph.Data.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjListGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'B', 'C', 'D' }, new char[] { 'B', 'A', 'C' }, new char[] { 'C', 'A', 'B', 'D' }, new char[] { 'D', 'A', 'C' } }, graph.Data.GetData());
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
            Assert.AreEqual(1, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A' } }, graph.Data.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'B', 'A' }, new char[] { 'A', 'B' } }, graph.Data.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            AdjSetGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'B', 'C', 'A', 'D' }, new char[] { 'C', 'B', 'A' }, new char[] { 'A', 'B', 'D', 'C' }, new char[] { 'D', 'A', 'C' } }, graph.Data.GetData());
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
            Assert.AreEqual(1, graph.Data.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0 } }, graph.Data.GetMatrixData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 0 }, { 0, 0 } }, graph.Data.GetMatrixData());
            graph.CreateEdge(vA, vA);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 0 }, { 0, 0 } }, graph.Data.GetMatrixData());
            graph.CreateEdge(vA, vB);
            CollectionAssertEx.AreEqual(new int[,] { { 1, 1 }, { 0, 0 } }, graph.Data.GetMatrixData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjMatrixGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.Matrix.Size);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 1, 1, 1 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 }, { 0, 0, 0, 0 } }, graph.Data.GetMatrixData());
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
            Assert.AreEqual(1, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.Data.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A', 'B' }, new char[] { 'B' } }, graph.Data.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjListGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'B', 'C', 'D' }, new char[] { 'B', 'C' }, new char[] { 'C', 'D' }, new char[] { 'D' } }, graph.Data.GetData());
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
            Assert.AreEqual(1, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' } }, graph.Data.GetData());
            var vB = graph.CreateVertex('B');
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vA);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEqual(new char[][] { new char[] { 'A', 'A' }, new char[] { 'B' } }, graph.Data.GetData());
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'A', 'B', 'A' }, new char[] { 'B' } }, graph.Data.GetData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            DirectedAdjSetGraph<char> graph = CreateGraph();
            GraphTestUtils.InitializeSimpleGraph(graph);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            Assert.AreEqual(4, graph.Data.GetData().Length);
            CollectionAssertEx.AreEquivalent(new char[][] { new char[] { 'D', 'C', 'B', 'A' }, new char[] { 'C', 'B' }, new char[] { 'D', 'C' }, new char[] { 'D' } }, graph.Data.GetData());
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
        internal static void InitializeSimpleGraph<TVertex>(Graph<char, TVertex> graph) where TVertex : Vertex<char> {
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
        internal static void InitializeGraph<TVertex>(Graph<char, TVertex> graph) where TVertex : Vertex<char> {
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vB, vH);
            graph.CreateEdge(vE, vC);
            graph.CreateEdge(vF, vE);
            graph.CreateEdge(vE, vG);
            graph.CreateEdge(vH, vE);
        }
        #endregion
    }
}
#endif
