#if DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class GraphBaseTests<TVertex, TEdge, TGraph> where TVertex : Vertex<char> where TEdge : Edge<char, TVertex> where TGraph : Graph<char, TVertex> {
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
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetWeightGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetWeightGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(CreateVertex('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(vA, CreateVertex('B'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var result = graph.GetWeight(vA, vB);
        }
        [TestMethod]
        public void GetWeightTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vB, vC, 3);
            graph.CreateEdge(vC, vA, 4);
            AssertEx.AreDoublesEqual(2, graph.GetWeight(vA, vB));
            AssertEx.AreDoublesEqual(3, graph.GetWeight(vB, vC));
            AssertEx.AreDoublesEqual(4, graph.GetWeight(vC, vA));
        }
        [TestMethod]
        public void SelfLoopWeightTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            graph.CreateEdge(vA, vA, 11);
            AssertEx.AreDoublesEqual(11, graph.GetWeight(vA, vA));
        }
        [TestMethod]
        public void CreateEdgeDefaultWeightTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            AssertEx.AreDoublesEqual(1, graph.GetWeight(vA, vB));
        }
        [TestMethod]
        public void UnweightedGraphTest() {
            var graph = CreateGraph();
            Assert.IsTrue(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            Assert.IsTrue(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
        }
        [TestMethod]
        public void WeightedGraphTest() {
            var graph = CreateGraph();
            Assert.IsTrue(graph.Properties.IsUnweighted);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC, 2);
            graph.CreateEdge(vC, vA, 3);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsTrue(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
        }
        [TestMethod]
        public void NegativeWeightedGraphTest() {
            var graph = CreateGraph();
            Assert.IsTrue(graph.Properties.IsUnweighted);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC, -2);
            graph.CreateEdge(vC, vA, -3);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsTrue(graph.Properties.IsNegativeWeighted);
        }
        [TestMethod]
        public void GraphEdgeCountTest() {
            var graph = CreateGraph();
            Assert.AreEqual(0, graph.Properties.EdgeCount);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vD);
            Assert.AreEqual(1, graph.Properties.EdgeCount);
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(3, graph.Properties.EdgeCount);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DistanceObjectGetPathToGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            distanceObj.GetPathTo(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectGetPathToGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            distanceObj.GetPathTo(CreateVertex('A'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectGetPathToIfThereIsNoAnyTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj.GetPathTo(vC);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetShortestPathFromGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var result = graph.GetShortestPathFrom(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetShortestPathFromGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var result = graph.GetShortestPathFrom(CreateVertex('A'));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DistanceObjectGetRowGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj[null];
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectGetRowGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj[CreateVertex('A')];
        }
        [TestMethod]
        public void DistanceObjectGetRowSimpleTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var rowA = new DistanceObject<char, TVertex>.Row(vA, null, 0);
            Assert.AreEqual(rowA, distanceObj[vA]);
            Assert.IsNull(distanceObj[vB]);
            Assert.IsNull(distanceObj[vC]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DistanceObjectSetRowGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            distanceObj[null] = new DistanceObject<char, TVertex>.Row(vA, null, 0);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectSetRowGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            distanceObj[CreateVertex('A')] = new DistanceObject<char, TVertex>.Row(vA, null, 0);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectSetRowGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            distanceObj[vA] = new DistanceObject<char, TVertex>.Row(vB, null, 0);
        }
        [TestMethod]
        public void DistanceObjectSetRowTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var row1 = new DistanceObject<char, TVertex>.Row(vA, null, 0);
            var row2 = new DistanceObject<char, TVertex>.Row(vB, vA, 1);
            var row3 = new DistanceObject<char, TVertex>.Row(vC, vA, 1);
            distanceObj[vA] = row1;
            distanceObj[vB] = row2;
            distanceObj[vC] = row3;
            Assert.AreSame(row1, distanceObj[vA]);
            Assert.AreSame(row2, distanceObj[vB]);
            Assert.AreSame(row3, distanceObj[vC]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DistanceObjectIsRowEmptyGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj.IsRowEmpty(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DistanceObjectIsRowEmptyGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj.IsRowEmpty(CreateVertex('A'));
        }
        [TestMethod]
        public void DistanceObjectIsRowEmptyTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            Assert.IsFalse(distanceObj.IsRowEmpty(vA));
            Assert.IsFalse(distanceObj.IsRowEmpty(vB));
            Assert.IsTrue(distanceObj.IsRowEmpty(vC));
        }
        [TestMethod]
        public void GetEdgeListSimpleTest() {
            var graph = CreateGraph();
            CollectionAssertEx.IsEmpty(graph.GetEdgeList());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetEdgeDataGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetEdgeDataGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(CreateVertex('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(vA, CreateVertex('B'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(vA, vC);
        }
        [TestMethod]
        public void GetEdgeDataSimpleTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
            edgeData = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
        }
        [TestMethod]
        public void GetEdgeDataTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vB, vC, 3);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(2, true, Color.Empty, null), edgeData);
            edgeData = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(3, true, Color.Empty, null), edgeData);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(null, vB, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, null, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, vB, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(CreateVertex('A'), vB, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, CreateVertex('B'), x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase6Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, vC, x => x);
        }
        [TestMethod]
        public void UpdateEdgeDataTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
            Color colorID = Color.CreateColor();
            graph.UpdateEdgeData(vA, vB, x => x.WithColor(colorID));
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, colorID, null), edgeData);
            TestEdgeData data = new TestEdgeData();
            graph.UpdateEdgeData(vA, vB, x => x.WithData(data));
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, colorID, data), edgeData);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetSimpleCircuitCoreGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetSimpleCircuitCore(null, (x, y) => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetSimpleCircuitCoreGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetSimpleCircuitCore(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetSimpleCircuitCoreGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetSimpleCircuitCore(CreateVertex('A'), (x, y) => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetEulerianCircuitGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetEulerianCircuit(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEulerianCircuitGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetEulerianCircuit(CreateVertex('A'));
        }
        [TestMethod]
        public void GetEulerianCircuitSimpleTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var result = graph.GetEulerianCircuit(vA);
            CollectionAssert.AreEqual(new TVertex[] { vA }, result);
        }
        [TestMethod]
        public void ContainsCycleInEmptyGraphTest() {
            var graph = CreateGraph();
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleSimpleTest1() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoDFSearchGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoDFSearch(null, (x, y) => true, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DoDFSearchGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoDFSearch(CreateVertex('A'), (x, y) => true, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoDFSearchGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoDFSearch(vA, null, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoDFSearchGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoDFSearch(vA, (x, y) => true, null, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoDFSearchGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoDFSearch(vA, (x, y) => true, (x, y) => { }, null);
        }
        [TestMethod]
        public void DoDFSearchTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vK = graph.CreateVertex('K');
            var vM = graph.CreateVertex('M');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vA, vC, 1);
            graph.CreateEdge(vC, vE, 1);
            graph.CreateEdge(vC, vF, 7);
            graph.CreateEdge(vE, vK, 3);
            graph.CreateEdge(vE, vM, 1);
            List<char> vertexList = new List<char>();
            List<char> edgeActionList = new List<char>();
            graph.DoDFSearch(vA, (x, y) => MathUtils.AreEquals(graph.GetEdgeData(x, y).Weight, 1), (x, y) => { edgeActionList.AddRange(new char[] { x.Value, y.Value }); }, x => { vertexList.Add(x.Value); return true; });
            CollectionAssert.AreEqual(new char[] { 'A', 'C', 'E', 'M' }, vertexList);
            CollectionAssert.AreEqual(new char[] { 'A', 'C', 'C', 'E', 'E', 'M' }, edgeActionList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoBFSearchGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoBFSearch(null, (x, y) => true, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DoBFSearchGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoBFSearch(CreateVertex('A'), (x, y) => true, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoBFSearchGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoBFSearch(vA, null, (x, y) => { }, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoBFSearchGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoBFSearch(vA, (x, y) => true, null, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DoBFSearchGuardCase5Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.DoBFSearch(vA, (x, y) => true, (x, y) => { }, null);
        }
        [TestMethod]
        public void DoBFSearchTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            var vI = graph.CreateVertex('I');
            var vJ = graph.CreateVertex('J');
            graph.CreateEdge(vA, vC, 3);
            graph.CreateEdge(vA, vD, 7);
            graph.CreateEdge(vA, vF, 5);
            graph.CreateEdge(vA, vH, 1);
            graph.CreateEdge(vC, vB, 2);
            graph.CreateEdge(vD, vE, 2);
            graph.CreateEdge(vF, vG, 9);
            graph.CreateEdge(vH, vI, 6);
            graph.CreateEdge(vH, vJ, 1);
            List<char> vertexList = new List<char>();
            List<char> edgeActionList = new List<char>();
            graph.DoBFSearch(vA, (x, y) => MathUtils.AreEquals(graph.GetEdgeData(x, y).Weight, 1), (x, y) => { edgeActionList.AddRange(new char[] { x.Value, y.Value }); }, x => { vertexList.Add(x.Value); return true; });
            CollectionAssert.AreEqual(new char[] { 'A', 'H', 'J' }, vertexList);
            CollectionAssert.AreEqual(new char[] { 'A', 'H', 'H', 'J' }, edgeActionList);
        }

        #region TestEdgeData
        protected class TestEdgeData : IEdgeData {
        }
        #endregion

        protected abstract TGraph CreateGraph();
        protected abstract TVertex CreateVertex(char value);
        protected abstract TEdge CreateEdgeObject(TVertex startVertex, TVertex endVertex, double weight);
    }

    public abstract class UndirectedGraphBaseTests<TVertex, TEdge, TGraph> : GraphBaseTests<TVertex, TEdge, TGraph> where TVertex : UndirectedVertex<char> where TEdge : Edge<char, TVertex> where TGraph : UndirectedGraph<char, TVertex> {
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
            graph.DFSearch(x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'E'; });
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'D', 'E' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'E'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'B', 'A', 'H', 'D', 'G' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'A'; });
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'B', 'A' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'A'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.BFSearch(x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'H', 'D', 'E', 'F', 'G' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'H', 'D' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'G', 'H', 'B', 'D', 'A' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'G', 'H', };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void VertexDegreeTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            Assert.AreEqual(0, vA.Degree);
            Assert.AreEqual(0, vB.Degree);
            Assert.AreEqual(0, vC.Degree);
            Assert.AreEqual(0, vD.Degree);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vA, vD);
            Assert.AreEqual(3, vA.Degree);
            Assert.AreEqual(1, vB.Degree);
            Assert.AreEqual(1, vC.Degree);
            Assert.AreEqual(1, vD.Degree);
        }
        [TestMethod]
        public void DistanceObjectGetPathToTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj.GetPathTo(vA);
            CollectionAssert.AreEqual(new TVertex[] { vA }, result);
            result = distanceObj.GetPathTo(vB);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB }, result);
            result = distanceObj.GetPathTo(vC);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vC }, result);
            result = distanceObj.GetPathTo(vD);
            CollectionAssert.AreEqual(new TVertex[] { vA, vD }, result);
        }
        [TestMethod]
        public void GetShortestPathFromTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vC, vF);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vD, vG);
            graph.CreateEdge(vE, vG);
            var result = graph.GetShortestPathFrom(vC);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, vC, 1),
                new DistanceObject<char, TVertex>.Row(vB, vA, 2),
                new DistanceObject<char, TVertex>.Row(vC, null, 0),
                new DistanceObject<char, TVertex>.Row(vD, vA, 2),
                new DistanceObject<char, TVertex>.Row(vE, vB, 3),
                new DistanceObject<char, TVertex>.Row(vF, vC, 1),
                new DistanceObject<char, TVertex>.Row(vG, vD, 3),
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void GetShortestPathFromTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vF);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vE);
            var result = graph.GetShortestPathFrom(vC);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, vC, 1),
                new DistanceObject<char, TVertex>.Row(vB, vA, 2),
                new DistanceObject<char, TVertex>.Row(vC, null, 0),
                new DistanceObject<char, TVertex>.Row(vD, vA, 2),
                new DistanceObject<char, TVertex>.Row(vE, vB, 3),
                new DistanceObject<char, TVertex>.Row(vF, vC, 1),
                null
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void DistanceObjectGetRowTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vA, null, 0), distanceObj[vA]);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vB, vA, 1), distanceObj[vB]);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vC, vA, 1), distanceObj[vC]);
        }
        [TestMethod]
        public void GetShortestPathFromTest3() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB, 7);
            graph.CreateEdge(vA, vC, 1);
            graph.CreateEdge(vB, vC, 2);
            graph.CreateEdge(vC, vD, 6);
            graph.CreateEdge(vB, vD, 2);
            graph.CreateEdge(vD, vE, 3);
            var result = graph.GetShortestPathFrom(vA);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, null, 0),
                new DistanceObject<char, TVertex>.Row(vB, vC, 3),
                new DistanceObject<char, TVertex>.Row(vC, vA, 1),
                new DistanceObject<char, TVertex>.Row(vD, vB, 5),
                new DistanceObject<char, TVertex>.Row(vE, vD, 8),
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void GetEdgeListTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB, 1);
            graph.CreateEdge(vB, vC, 2);
            graph.CreateEdge(vC, vD, 3);
            graph.CreateEdge(vD, vB, 4);
            graph.CreateEdge(vA, vE, 4);
            graph.CreateEdge(vE, vD, 2);
            var result = graph.GetEdgeList();
            TEdge[] expected = new TEdge[] {
                CreateEdgeObject(vA, vB, 1),
                CreateEdgeObject(vA, vE, 4),
                CreateEdgeObject(vB, vC, 2),
                CreateEdgeObject(vB, vD, 4),
                CreateEdgeObject(vC, vD, 3),
                CreateEdgeObject(vD, vE, 2),
            };
            CollectionAssert.AreEquivalent(expected, result);
        }
        [TestMethod]
        public void BuildMSFTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB, 7);
            graph.CreateEdge(vA, vD, 5);
            graph.CreateEdge(vD, vB, 9);
            graph.CreateEdge(vB, vC, 8);
            graph.CreateEdge(vB, vE, 7);
            graph.CreateEdge(vC, vE, 5);
            graph.CreateEdge(vD, vE, 15);
            graph.CreateEdge(vD, vF, 6);
            graph.CreateEdge(vE, vF, 8);
            graph.CreateEdge(vE, vG, 9);
            graph.CreateEdge(vF, vG, 11);
            var forest = CreateGraph();
            graph.DoBuildMSF(forest);
            Assert.AreEqual(7, forest.Size);
            char[] extectedValues = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            CollectionAssert.AreEquivalent(extectedValues, forest.GetVertexList().Select(x => x.Value).ToList());
            EdgeTriplet<char>[] extectedEdges = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('A', 'D', 5),
                new EdgeTriplet<char>('D', 'F', 6),
                new EdgeTriplet<char>('A', 'B', 7),
                new EdgeTriplet<char>('B', 'E', 7),
                new EdgeTriplet<char>('C', 'E', 5),
                new EdgeTriplet<char>('E', 'G', 9),
            };
            CollectionAssertEx.AreEquivalent(extectedEdges, forest.GetEdgeList().Select(x => x.CreateTriplet()));
        }
        [TestMethod]
        public void BuildMSFTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vP = graph.CreateVertex('P');
            var vQ = graph.CreateVertex('Q');
            var vL = graph.CreateVertex('L');
            var vK = graph.CreateVertex('K');
            graph.CreateEdge(vA, vB, 8);
            graph.CreateEdge(vB, vC, 5);
            graph.CreateEdge(vC, vD, 2);
            graph.CreateEdge(vD, vA, 9);
            graph.CreateEdge(vA, vC, 11);
            graph.CreateEdge(vP, vQ, 7);
            graph.CreateEdge(vQ, vL, 9);
            graph.CreateEdge(vP, vL, 3);
            var forest = CreateGraph();
            graph.DoBuildMSF(forest);
            Assert.AreEqual(8, forest.Size);
            char[] extectedValues = new char[] { 'A', 'B', 'C', 'D', 'P', 'Q', 'L', 'K' };
            CollectionAssert.AreEquivalent(extectedValues, forest.GetVertexList().Select(x => x.Value).ToList());
            EdgeTriplet<char>[] extectedEdges = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('A', 'B', 8),
                new EdgeTriplet<char>('B', 'C', 5),
                new EdgeTriplet<char>('C', 'D', 2),
                new EdgeTriplet<char>('P', 'Q', 7),
                new EdgeTriplet<char>('P', 'L', 3),
            };
            CollectionAssertEx.AreEquivalent(extectedEdges, forest.GetEdgeList().Select(x => x.CreateTriplet()));
        }
        [TestMethod]
        public void UpdateEdgeDataConsistencyTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            Color colorID = Color.CreateColor();
            TestEdgeData data = new TestEdgeData();
            graph.UpdateEdgeData(vB, vC, x => x.WithColor(colorID).WithData(data));
            EdgeData edgeData1 = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(1, true, colorID, data), edgeData1);
            EdgeData edgeData2 = graph.GetEdgeData(vC, vB);
            Assert.AreEqual(edgeData1, edgeData2);
        }
        [TestMethod]
        public void GetArticulationPointListSimpleTest1() {
            var graph = CreateGraph();
            CollectionAssertEx.IsEmpty(graph.GetArticulationPointList());
        }
        [TestMethod]
        public void GetArticulationPointListSimpleTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vB, vD);
            CollectionAssertEx.IsEmpty(graph.GetArticulationPointList());
        }
        [TestMethod]
        public void GetArticulationPointListTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vC, vB);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vB, vA);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vF, vE);
            graph.CreateEdge(vG, vC);
            CollectionAssert.AreEquivalent(new TVertex[] { vC, vD }, graph.GetArticulationPointList());
        }
        [TestMethod]
        public void GetArticulationPointListTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vE);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vE, vF);
            graph.CreateEdge(vE, vG);
            graph.CreateEdge(vG, vH);
            graph.CreateEdge(vF, vH);
            CollectionAssert.AreEquivalent(new TVertex[] { vA, vB, vE }, graph.GetArticulationPointList());
        }
        [TestMethod]
        public void GetArticulationPointListTest3() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vE, vF);
            graph.CreateEdge(vE, vG);
            CollectionAssert.AreEquivalent(new TVertex[] { vE }, graph.GetArticulationPointList());
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetSimpleCircuitCoreGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vM = graph.CreateVertex('M');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vD, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vM);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => true);
        }
        [TestMethod]
        public void GetSimpleCircuitCoreTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vN = graph.CreateVertex('N');
            var vM = graph.CreateVertex('M');
            var vT = graph.CreateVertex('T');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vN);
            graph.CreateEdge(vN, vC);
            graph.CreateEdge(vB, vM);
            graph.CreateEdge(vC, vT);
            graph.CreateEdge(vM, vT);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => true);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vC, vA }, result);
        }
        [TestMethod]
        public void GetSimpleCircuitCoreTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vN = graph.CreateVertex('N');
            var vM = graph.CreateVertex('M');
            var vT = graph.CreateVertex('T');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vN);
            graph.CreateEdge(vN, vC);
            graph.CreateEdge(vB, vM);
            graph.CreateEdge(vC, vT);
            graph.CreateEdge(vM, vT);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => {
                if(ReferenceEquals(x, vB) && ReferenceEquals(y, vC)) return false;
                if(ReferenceEquals(x, vC) && ReferenceEquals(y, vB)) return false;
                return true;
            });
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vN, vC, vA }, result);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEulerianCircuitTwoVertexTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            var result = graph.GetEulerianCircuit(vA);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEulerianCircuitGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vD, vC);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vC, vE);
            var result = graph.GetEulerianCircuit(vA);
        }
        [TestMethod]
        public void GetEulerianCircuitTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vB, vF);
            graph.CreateEdge(vE, vF);
            var result = graph.GetEulerianCircuit(vC);
            CollectionAssert.AreEqual(new TVertex[] { vC, vA, vB, vE, vF, vB, vD, vC }, result);
        }
        [TestMethod]
        public void GetEulerianCircuitTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vD, vG);
            graph.CreateEdge(vG, vF);
            graph.CreateEdge(vE, vF);
            var result = graph.GetEulerianCircuit(vD);
            CollectionAssert.AreEqual(new TVertex[] { vD, vE, vF, vG, vD }, result);
        }
        [TestMethod]
        public void ContainsCycleSimpleTest2() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            graph.CreateEdge(v1, v2);
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest1() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v3);
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            var vI = graph.CreateVertex('I');
            var vJ = graph.CreateVertex('J');
            var vK = graph.CreateVertex('K');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vC, vF);
            graph.CreateEdge(vF, vG);
            graph.CreateEdge(vG, vH);
            graph.CreateEdge(vF, vK);
            graph.CreateEdge(vK, vJ);
            graph.CreateEdge(vJ, vI);
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest3() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v3);
            graph.CreateEdge(v1, v3);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest4() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            var vI = graph.CreateVertex('I');
            var vJ = graph.CreateVertex('J');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vC, vF);
            graph.CreateEdge(vF, vG);
            graph.CreateEdge(vG, vH);
            graph.CreateEdge(vF, vJ);
            graph.CreateEdge(vJ, vI);
            graph.CreateEdge(vI, vH);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest5() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            var v4 = graph.CreateVertex('4');
            var v5 = graph.CreateVertex('5');
            graph.CreateEdge(v3, v4);
            graph.CreateEdge(v3, v5);
            graph.CreateEdge(v4, v5);
            Assert.IsTrue(graph.ContainsCycle());
        }
    }

    public abstract class DirectedGraphBaseTests<TVertex, TEdge, TGraph> : GraphBaseTests<TVertex, TEdge, TGraph> where TVertex : DirectedVertex<char> where TEdge : Edge<char, TVertex> where TGraph : DirectedGraph<char, TVertex> {
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
            graph.DFSearch(x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'D', 'H', 'E', 'G', 'F' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'D', 'H' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(x => { vertexList.Add(x.Value); return x.Value != 'H'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'D', 'G', 'H', 'A', 'B' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void DFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'G'; });
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'D', 'G' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.DFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'G'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase1Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.BFSearch(x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'H', 'D', 'E', 'G', 'F' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase2Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            ICollection expectedList = new char[] { 'A', 'B', 'C', 'H', 'D' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase3Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'G', 'D', 'H', 'A', 'B' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(vF, x => vertexList.Add(x.Value));
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void BFSearchCase4Test() {
            List<char> vertexList = new List<char>();
            var graph = CreateGraph();
            GraphTestUtils.InitializeGraph(graph);
            var vF = graph.GetVertexList().First(x => x.Value == 'F');
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            ICollection expectedList = new char[] { 'F', 'E', 'C', 'G', 'D' };
            CollectionAssert.AreEqual(expectedList, vertexList);
            vertexList.Clear();
            graph.BFSearch(vF, x => { vertexList.Add(x.Value); return x.Value != 'D'; });
            CollectionAssert.AreEqual(expectedList, vertexList);
        }
        [TestMethod]
        public void VertextInDegreeTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            Assert.AreEqual(0, vA.InDegree);
            Assert.AreEqual(0, vB.InDegree);
            Assert.AreEqual(0, vC.InDegree);
            Assert.AreEqual(0, vD.InDegree);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vA);
            Assert.AreEqual(1, vA.InDegree);
            Assert.AreEqual(1, vB.InDegree);
            Assert.AreEqual(2, vC.InDegree);
            Assert.AreEqual(1, vD.InDegree);
        }
        [TestMethod]
        public void VertextOutDegreeTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            Assert.AreEqual(0, vA.OutDegree);
            Assert.AreEqual(0, vB.OutDegree);
            Assert.AreEqual(0, vC.OutDegree);
            Assert.AreEqual(0, vD.OutDegree);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vA);
            Assert.AreEqual(2, vA.OutDegree);
            Assert.AreEqual(1, vB.OutDegree);
            Assert.AreEqual(1, vC.OutDegree);
            Assert.AreEqual(1, vD.OutDegree);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TopologicalSortGuardCase1Test() {
            var graph = CreateGraph();
            Action<TVertex> action = null;
            graph.TopologicalSort(action);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TopologicalSortGuardCase2Test() {
            var graph = CreateGraph();
            var v7 = graph.CreateVertex('7');
            var v5 = graph.CreateVertex('5');
            var v3 = graph.CreateVertex('3');
            var v1 = graph.CreateVertex('1');
            var v8 = graph.CreateVertex('8');
            var v2 = graph.CreateVertex('2');
            var v9 = graph.CreateVertex('9');
            var v4 = graph.CreateVertex('4');
            graph.CreateEdge(v7, v1);
            graph.CreateEdge(v7, v8);
            graph.CreateEdge(v5, v1);
            graph.CreateEdge(v3, v8);
            graph.CreateEdge(v3, v4);
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v1, v9);
            graph.CreateEdge(v1, v4);
            graph.CreateEdge(v8, v9);
            graph.CreateEdge(v2, v7);
            var valueList = new List<char>();
            graph.TopologicalSort(x => valueList.Add(x.Value));
        }
        [TestMethod]
        public void TopologicalSortTest() {
            var graph = CreateGraph();
            var v7 = graph.CreateVertex('7');
            var v5 = graph.CreateVertex('5');
            var v3 = graph.CreateVertex('3');
            var v1 = graph.CreateVertex('1');
            var v8 = graph.CreateVertex('8');
            var v2 = graph.CreateVertex('2');
            var v9 = graph.CreateVertex('9');
            var v4 = graph.CreateVertex('4');
            graph.CreateEdge(v7, v1);
            graph.CreateEdge(v7, v8);
            graph.CreateEdge(v5, v1);
            graph.CreateEdge(v3, v8);
            graph.CreateEdge(v3, v4);
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v1, v9);
            graph.CreateEdge(v1, v4);
            graph.CreateEdge(v8, v9);
            var valueList = new List<char>();
            graph.TopologicalSort(x => valueList.Add(x.Value));
            CollectionAssert.AreEqual(new char[] { '7', '5', '3', '1', '8', '2', '4', '9' }, valueList);
        }
        [TestMethod]
        public void DistanceObjectGetPathToTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            var result = distanceObj.GetPathTo(vA);
            CollectionAssert.AreEqual(new TVertex[] { vA }, result);
            result = distanceObj.GetPathTo(vB);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB }, result);
            result = distanceObj.GetPathTo(vC);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vC }, result);
            result = distanceObj.GetPathTo(vD);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vC, vD }, result);
        }
        [TestMethod]
        public void GetShortestPathFromTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vE, vG);
            graph.CreateEdge(vD, vG);
            graph.CreateEdge(vG, vF);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vF);
            var result = graph.GetShortestPathFrom(vC);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, vC, 1),
                new DistanceObject<char, TVertex>.Row(vB, vA, 2),
                new DistanceObject<char, TVertex>.Row(vC, null, 0),
                new DistanceObject<char, TVertex>.Row(vD, vA, 2),
                new DistanceObject<char, TVertex>.Row(vE, vB, 3),
                new DistanceObject<char, TVertex>.Row(vF, vC, 1),
                new DistanceObject<char, TVertex>.Row(vG, vD, 3),
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void GetShortestPathFromTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vG, vF);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vF);
            var result = graph.GetShortestPathFrom(vC);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, vC, 1),
                new DistanceObject<char, TVertex>.Row(vB, vA, 2),
                new DistanceObject<char, TVertex>.Row(vC, null, 0),
                new DistanceObject<char, TVertex>.Row(vD, vA, 2),
                new DistanceObject<char, TVertex>.Row(vE, vB, 3),
                new DistanceObject<char, TVertex>.Row(vF, vC, 1),
                null
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void DistanceObjectGetRowTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            var distanceObj = graph.GetShortestPathFrom(vA);
            Assert.IsNotNull(distanceObj);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vA, null, 0), distanceObj[vA]);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vB, vA, 1), distanceObj[vB]);
            Assert.AreEqual(new DistanceObject<char, TVertex>.Row(vC, vB, 2), distanceObj[vC]);
        }
        [TestMethod]
        public void GetShortestPathFromTest3() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB, 4);
            graph.CreateEdge(vA, vC, 1);
            graph.CreateEdge(vC, vB, 2);
            graph.CreateEdge(vC, vD, 4);
            graph.CreateEdge(vB, vE, 4);
            graph.CreateEdge(vD, vE, 4);
            var result = graph.GetShortestPathFrom(vA);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, null, 0),
                new DistanceObject<char, TVertex>.Row(vB, vC, 3),
                new DistanceObject<char, TVertex>.Row(vC, vA, 1),
                new DistanceObject<char, TVertex>.Row(vD, vC, 5),
                new DistanceObject<char, TVertex>.Row(vE, vB, 7),
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void GetShortestPathFromTest4() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB, 7);
            graph.CreateEdge(vA, vD, 6);
            graph.CreateEdge(vA, vG, 10);
            graph.CreateEdge(vB, vF, 2);
            graph.CreateEdge(vF, vG, 3);
            graph.CreateEdge(vD, vE, 3);
            graph.CreateEdge(vE, vB, 4);
            graph.CreateEdge(vE, vC, 9);
            graph.CreateEdge(vC, vB, -15);
            var result = graph.GetShortestPathFrom(vA);
            var expectedResult = new DistanceObject<char, TVertex>.Row[] {
                new DistanceObject<char, TVertex>.Row(vA, null, 0),
                new DistanceObject<char, TVertex>.Row(vB, vC, 3),
                new DistanceObject<char, TVertex>.Row(vC, vE, 18),
                new DistanceObject<char, TVertex>.Row(vD, vA, 6),
                new DistanceObject<char, TVertex>.Row(vE, vD, 9),
                new DistanceObject<char, TVertex>.Row(vF, vB, 5),
                new DistanceObject<char, TVertex>.Row(vG, vF, 8),
            };
            CollectionAssert.AreEqual(expectedResult, result.Rows);
        }
        [TestMethod]
        public void GetEdgeListTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vC, vB, 7);
            graph.CreateEdge(vA, vC, 1);
            graph.CreateEdge(vD, vA, 5);
            graph.CreateEdge(vD, vC, 3);
            graph.CreateEdge(vE, vA, 6);
            graph.CreateEdge(vE, vD, 2);
            graph.CreateEdge(vD, vE, 3);
            var result = graph.GetEdgeList();
            TEdge[] expected = new TEdge[] {
                CreateEdgeObject(vA, vB, 2),
                CreateEdgeObject(vC, vB, 7),
                CreateEdgeObject(vA, vC, 1),
                CreateEdgeObject(vD, vA, 5),
                CreateEdgeObject(vD, vC, 3),
                CreateEdgeObject(vE, vA, 6),
                CreateEdgeObject(vE, vD, 2),
                CreateEdgeObject(vD, vE, 3),
            };
            CollectionAssert.AreEquivalent(expected, result);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetSimpleCircuitCoreGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vN = graph.CreateVertex('N');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vN);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vN, vC);
            graph.CreateEdge(vC, vA);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => true);
        }
        [TestMethod]
        public void GetSimpleCircuitCoreTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vN = graph.CreateVertex('N');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vB);
            graph.CreateEdge(vB, vN);
            graph.CreateEdge(vN, vC);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => true);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vC, vA }, result);
        }
        [TestMethod]
        public void GetSimpleCircuitCoreTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vN = graph.CreateVertex('N');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vB);
            graph.CreateEdge(vB, vN);
            graph.CreateEdge(vN, vC);
            var result = graph.GetSimpleCircuitCore(vA, (x, y) => {
                if(ReferenceEquals(x, vB) && ReferenceEquals(y, vC)) return false;
                return true;
            });
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vN, vC, vA }, result);
        }
        [TestMethod]
        public void GetEulerianCircuitTwoVertexTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var result = graph.GetEulerianCircuit(vA);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vA }, result);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEulerianCircuitGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vD, vC);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vE);
            graph.CreateEdge(vE, vD);
            var result = graph.GetEulerianCircuit(vA);
        }
        [TestMethod]
        public void GetEulerianCircuitTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vE, vB);
            graph.CreateEdge(vE, vC);
            graph.CreateEdge(vF, vE);
            var result = graph.GetEulerianCircuit(vA);
            CollectionAssert.AreEqual(new TVertex[] { vA, vB, vD, vF, vE, vC, vD, vE, vB, vC, vA }, result);
        }
        [TestMethod]
        public void GetEulerianCircuitTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vD, vE);
            graph.CreateEdge(vE, vF);
            graph.CreateEdge(vF, vG);
            graph.CreateEdge(vG, vD);
            var result = graph.GetEulerianCircuit(vD);
            CollectionAssert.AreEqual(new TVertex[] { vD, vE, vF, vG, vD }, result);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VertexRelationAreStronglyConnectedGuardCase1Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var data = graph.GetVertexRelationData();
            bool result = data.AreStronglyConnected(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VertexRelationAreStronglyConnectedGuardCase2Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var data = graph.GetVertexRelationData();
            bool result = data.AreStronglyConnected(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void VertexRelationAreStronglyConnectedGuardCase3Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var data = graph.GetVertexRelationData();
            bool result = data.AreStronglyConnected(CreateVertex('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void VertexRelationAreStronglyConnectedGuardCase4Test() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var data = graph.GetVertexRelationData();
            bool result = data.AreStronglyConnected(vA, CreateVertex('B'));
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedEmptyGraphTest() {
            var graph = CreateGraph();
            var data = graph.GetVertexRelationData();
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedSimpleTest() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vA);
            var data = graph.GetVertexRelationData();
            Assert.IsTrue(data.AreStronglyConnected(vA, vB));
            Assert.IsTrue(data.AreStronglyConnected(vB, vA));
            Assert.IsTrue(data.AreStronglyConnected(vA, vA));
            Assert.IsTrue(data.AreStronglyConnected(vB, vB));
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var data = graph.GetVertexRelationData();
            Assert.IsFalse(data.AreStronglyConnected(vA, vB));
            Assert.IsFalse(data.AreStronglyConnected(vB, vC));
            Assert.IsFalse(data.AreStronglyConnected(vA, vC));
            Assert.IsTrue(data.AreStronglyConnected(vA, vA));
            Assert.IsTrue(data.AreStronglyConnected(vB, vB));
            Assert.IsTrue(data.AreStronglyConnected(vC, vC));
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var data = graph.GetVertexRelationData();
            Assert.IsFalse(data.AreStronglyConnected(vA, vB));
            Assert.IsFalse(data.AreStronglyConnected(vB, vC));
            Assert.IsFalse(data.AreStronglyConnected(vA, vC));
            Assert.IsTrue(data.AreStronglyConnected(vA, vA));
            Assert.IsTrue(data.AreStronglyConnected(vB, vB));
            Assert.IsTrue(data.AreStronglyConnected(vC, vC));
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedTest3() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vD);
            var data = graph.GetVertexRelationData();
            Assert.IsTrue(data.AreStronglyConnected(vA, vB));
            Assert.IsTrue(data.AreStronglyConnected(vB, vC));
            Assert.IsTrue(data.AreStronglyConnected(vA, vC));
            Assert.IsFalse(data.AreStronglyConnected(vD, vA));
            Assert.IsTrue(data.AreStronglyConnected(vD, vD));
        }
        [TestMethod]
        public void VertexRelationAreStronglyConnectedTest4() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vA);
            graph.CreateEdge(vC, vB);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vC, vH);
            graph.CreateEdge(vD, vB);
            graph.CreateEdge(vE, vB);
            graph.CreateEdge(vE, vF);
            graph.CreateEdge(vF, vE);
            graph.CreateEdge(vG, vF);
            graph.CreateEdge(vG, vH);
            graph.CreateEdge(vG, vG);
            graph.CreateEdge(vH, vC);
            graph.CreateEdge(vH, vE);
            var data = graph.GetVertexRelationData();
            Assert.IsTrue(data.AreStronglyConnected(vA, vB));
            Assert.IsTrue(data.AreStronglyConnected(vB, vD));
            Assert.IsTrue(data.AreStronglyConnected(vA, vD));
            Assert.IsTrue(data.AreStronglyConnected(vF, vE));
            Assert.IsTrue(data.AreStronglyConnected(vG, vG));
            Assert.IsTrue(data.AreStronglyConnected(vH, vC));
            Assert.IsFalse(data.AreStronglyConnected(vG, vA));
            Assert.IsFalse(data.AreStronglyConnected(vA, vE));
            Assert.IsFalse(data.AreStronglyConnected(vC, vE));
            Assert.IsFalse(data.AreStronglyConnected(vH, vG));
        }
        [TestMethod]
        public void ContainsCycleSimpleTest2() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v1);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest1() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v3);
            graph.CreateEdge(v1, v3);
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest2() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            graph.CreateEdge(vA, vG);
            graph.CreateEdge(vB, vF);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vE);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vE, vB);
            graph.CreateEdge(vE, vD);
            graph.CreateEdge(vF, vH);
            graph.CreateEdge(vG, vB);
            graph.CreateEdge(vG, vH);
            Assert.IsFalse(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest3() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v3);
            graph.CreateEdge(v3, v1);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest4() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            var vD = graph.CreateVertex('D');
            var vE = graph.CreateVertex('E');
            var vF = graph.CreateVertex('F');
            var vG = graph.CreateVertex('G');
            var vH = graph.CreateVertex('H');
            graph.CreateEdge(vA, vG);
            graph.CreateEdge(vB, vF);
            graph.CreateEdge(vC, vA);
            graph.CreateEdge(vC, vE);
            graph.CreateEdge(vD, vF);
            graph.CreateEdge(vE, vB);
            graph.CreateEdge(vE, vD);
            graph.CreateEdge(vF, vH);
            graph.CreateEdge(vG, vB);
            graph.CreateEdge(vH, vG);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void ContainsCycleTest5() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            var v4 = graph.CreateVertex('4');
            var v5 = graph.CreateVertex('5');
            graph.CreateEdge(v3, v4);
            graph.CreateEdge(v4, v5);
            graph.CreateEdge(v5, v3);
            Assert.IsTrue(graph.ContainsCycle());
        }
        [TestMethod]
        public void BuildTransposeGraphSimpeTest1() {
            var transposeGraph = CreateGraph();
            CreateGraph().FillTransposeGraph(transposeGraph);
            CollectionAssertEx.IsEmpty(transposeGraph.GetVertexValueList());
            CollectionAssertEx.IsEmpty(transposeGraph.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildTransposeGraphSimpeTest2() {
            var graph = CreateGraph();
            graph.CreateVertex('A');
            var transposeGraph = CreateGraph();
            graph.FillTransposeGraph(transposeGraph);
            CollectionAssert.AreEqual(new char[] { 'A' }, transposeGraph.GetVertexValueList());
            CollectionAssertEx.IsEmpty(transposeGraph.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildTransposeGraphTest1() {
            var graph = CreateGraph();
            var vA = graph.CreateVertex('A');
            graph.CreateEdge(vA, vA);
            var transposeGraph = CreateGraph();
            graph.FillTransposeGraph(transposeGraph);
            CollectionAssert.AreEqual(new char[] { 'A' }, transposeGraph.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('A', 'A', 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, transposeGraph.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildTransposeGraphTest2() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            graph.CreateEdge(v1, v2);
            var transposeGraph = CreateGraph();
            graph.FillTransposeGraph(transposeGraph);
            char[] extectedValues = new char[] { '1', '2' };
            CollectionAssert.AreEqual(extectedValues, transposeGraph.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('2', '1', 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, transposeGraph.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildTransposeGraphTest3() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            graph.CreateEdge(v1, v2);
            graph.CreateEdge(v2, v1);
            var transposeGraph = CreateGraph();
            graph.FillTransposeGraph(transposeGraph);
            char[] extectedValues = new char[] { '1', '2' };
            CollectionAssert.AreEqual(extectedValues, transposeGraph.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('1', '2', 1),
                new EdgeTriplet<char>('2', '1', 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, transposeGraph.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildTransposeGraphTest4() {
            var graph = CreateGraph();
            var v1 = graph.CreateVertex('1');
            var v2 = graph.CreateVertex('2');
            var v3 = graph.CreateVertex('3');
            var v4 = graph.CreateVertex('4');
            var v5 = graph.CreateVertex('5');
            var v6 = graph.CreateVertex('6');
            var v7 = graph.CreateVertex('7');
            graph.CreateEdge(v1, v2, 1);
            graph.CreateEdge(v2, v3, 2);
            graph.CreateEdge(v1, v3, 1);
            graph.CreateEdge(v2, v4, 3);
            graph.CreateEdge(v5, v4, 4);
            graph.CreateEdge(v6, v7, 5);
            var transposeGraph = CreateGraph();
            graph.FillTransposeGraph(transposeGraph);
            char[] extectedValues = new char[] { '1', '2', '3', '4', '5', '6', '7' };
            CollectionAssert.AreEqual(extectedValues, transposeGraph.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('2', '1', 1),
                new EdgeTriplet<char>('3', '1', 1),
                new EdgeTriplet<char>('3', '2', 2),
                new EdgeTriplet<char>('4', '2', 3),
                new EdgeTriplet<char>('4', '5', 4),
                new EdgeTriplet<char>('7', '6', 5),
            };
            CollectionAssert.AreEqual(extectedEdgeList, transposeGraph.GetEdgeTripletList());
        }
    }


    [TestClass]
    public class AdjMatrixGraphTests : UndirectedGraphBaseTests<AdjMatrixGraphVertex<char>, Edge<char, AdjMatrixGraphVertex<char>>, AdjMatrixGraph<char>> {
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
            Assert.IsFalse(graph.Data.Matrix[0, 0].Initialized);
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
        [TestMethod]
        public void GraphEdgeCountMultigraphCaseTest() {
            AdjMatrixGraph<char> graph = new AdjMatrixGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
        }

        protected override AdjMatrixGraph<char> CreateGraph() {
            return new AdjMatrixGraph<char>();
        }
        protected override AdjMatrixGraphVertex<char> CreateVertex(char value) {
            return new AdjMatrixGraphVertex<char>(value);
        }
        protected override Edge<char, AdjMatrixGraphVertex<char>> CreateEdgeObject(AdjMatrixGraphVertex<char> startVertex, AdjMatrixGraphVertex<char> endVertex, double weight) {
            return new Edge<char, AdjMatrixGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class AdjListGraphTests : UndirectedGraphBaseTests<AdjListGraphVertex<char>, Edge<char, AdjListGraphVertex<char>>, AdjListGraph<char>> {
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
        [TestMethod]
        public void GraphEdgeCountMultigraphCaseTest() {
            AdjListGraph<char> graph = new AdjListGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(4, graph.Properties.EdgeCount);
        }

        protected override AdjListGraph<char> CreateGraph() {
            return new AdjListGraph<char>();
        }
        protected override AdjListGraphVertex<char> CreateVertex(char value) {
            return new AdjListGraphVertex<char>(value);
        }
        protected override Edge<char, AdjListGraphVertex<char>> CreateEdgeObject(AdjListGraphVertex<char> startVertex, AdjListGraphVertex<char> endVertex, double weight) {
            return new Edge<char, AdjListGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class AdjSetGraphTests : UndirectedGraphBaseTests<AdjSetGraphVertex<char>, Edge<char, AdjSetGraphVertex<char>>, AdjSetGraph<char>> {
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
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeMultigraphCaseTest() {
            AdjSetGraph<char> graph = new AdjSetGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
        }

        protected override AdjSetGraph<char> CreateGraph() {
            return new AdjSetGraph<char>();
        }
        protected override AdjSetGraphVertex<char> CreateVertex(char value) {
            return new AdjSetGraphVertex<char>(value);
        }
        protected override Edge<char, AdjSetGraphVertex<char>> CreateEdgeObject(AdjSetGraphVertex<char> startVertex, AdjSetGraphVertex<char> endVertex, double weight) {
            return new Edge<char, AdjSetGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class DirectedAdjMatrixGraphTests : DirectedGraphBaseTests<DirectedAdjMatrixGraphVertex<char>, Edge<char, DirectedAdjMatrixGraphVertex<char>>, DirectedAdjMatrixGraph<char>> {
        [TestMethod]
        public void CreateVertexTest() {
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>();
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
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>();
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
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>();
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
        [TestMethod]
        public void GraphEdgeCountMultigraphCaseTest() {
            DirectedAdjMatrixGraph<char> graph = new DirectedAdjMatrixGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
        }

        protected override DirectedAdjMatrixGraph<char> CreateGraph() {
            return new DirectedAdjMatrixGraph<char>();
        }
        protected override DirectedAdjMatrixGraphVertex<char> CreateVertex(char value) {
            return new DirectedAdjMatrixGraphVertex<char>(value);
        }
        protected override Edge<char, DirectedAdjMatrixGraphVertex<char>> CreateEdgeObject(DirectedAdjMatrixGraphVertex<char> startVertex, DirectedAdjMatrixGraphVertex<char> endVertex, double weight) {
            return new Edge<char, DirectedAdjMatrixGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class DirectedAdjListGraphTests : DirectedGraphBaseTests<DirectedAdjListGraphVertex<char>, Edge<char, DirectedAdjListGraphVertex<char>>, DirectedAdjListGraph<char>> {
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
        [TestMethod]
        public void GraphEdgeCountMultigraphCaseTest() {
            DirectedAdjListGraph<char> graph = new DirectedAdjListGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(4, graph.Properties.EdgeCount);
        }

        protected override DirectedAdjListGraph<char> CreateGraph() {
            return new DirectedAdjListGraph<char>();
        }
        protected override DirectedAdjListGraphVertex<char> CreateVertex(char value) {
            return new DirectedAdjListGraphVertex<char>(value);
        }
        protected override Edge<char, DirectedAdjListGraphVertex<char>> CreateEdgeObject(DirectedAdjListGraphVertex<char> startVertex, DirectedAdjListGraphVertex<char> endVertex, double weight) {
            return new Edge<char, DirectedAdjListGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class DirectedAdjSetGraphTests : DirectedGraphBaseTests<DirectedAdjSetGraphVertex<char>, Edge<char, DirectedAdjSetGraphVertex<char>>, DirectedAdjSetGraph<char>> {
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
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GraphEdgeCountMultigraphCaseTest() {
            DirectedAdjSetGraph<char> graph = new DirectedAdjSetGraph<char>();
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vB);
        }

        protected override DirectedAdjSetGraph<char> CreateGraph() {
            return new DirectedAdjSetGraph<char>();
        }
        protected override DirectedAdjSetGraphVertex<char> CreateVertex(char value) {
            return new DirectedAdjSetGraphVertex<char>(value);
        }
        protected override Edge<char, DirectedAdjSetGraphVertex<char>> CreateEdgeObject(DirectedAdjSetGraphVertex<char> startVertex, DirectedAdjSetGraphVertex<char> endVertex, double weight) {
            return new Edge<char, DirectedAdjSetGraphVertex<char>>(startVertex, endVertex, weight);
        }
    }

    [TestClass]
    public class BipartiteGraphTests {
        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void CreateVertexGuardTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            graph.CreateVertex('A');
        }
        [TestMethod]
        public void CreateVertexTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            Assert.IsNotNull(vA);
            Assert.IsNotNull(vB);
            Assert.IsNotNull(vC);
            Assert.IsNotNull(vD);
            Assert.AreEqual(0, vA.Handle);
            Assert.AreEqual(1, vB.Handle);
            Assert.AreEqual(0, vC.Handle);
            Assert.AreEqual(1, vD.Handle);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>(0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(new BipartiteGraphVertex<char>('C'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, new BipartiteGraphVertex<char>('D'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase5Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase6Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vB, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase7Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vA);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CreateEdgeGuardCase8Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vB, vB);
        }
        [TestMethod]
        public void CreateEdgeSimpleTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            Assert.AreEqual(new MatrixSize(0, 0), graph.Data.Matrix.Size);
            var vA = graph.U.CreateVertex('A');
            Assert.AreEqual(new MatrixSize(0, 1), graph.Data.Matrix.Size);
            var vB = graph.V.CreateVertex('B');
            Assert.AreEqual(new MatrixSize(1, 1), graph.Data.Matrix.Size);
            Assert.IsFalse(graph.Data.Matrix[0, 0].Initialized);
            CollectionAssertEx.AreEqual(new int[,] { { 0 } }, graph.Data.GetMatrixData());
            graph.CreateEdge(vA, vB);
            Assert.IsTrue(graph.Data.Matrix[0, 0].Initialized);
            CollectionAssertEx.AreEqual(new int[,] { { 1 } }, graph.Data.GetMatrixData());
        }
        [TestMethod]
        public void CreateEdgeTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            Assert.AreEqual(new MatrixSize(3, 2), graph.Data.Matrix.Size);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vD);
            CollectionAssertEx.AreEqual(new int[,] { { 0, 1 }, { 1, 1 }, { 0, 0 } }, graph.Data.GetMatrixData());
        }
        [TestMethod]
        public void GraphSizeTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            Assert.AreEqual(0, graph.Size);
            var vA = graph.U.CreateVertex('A');
            Assert.AreEqual(1, graph.Size);
            Assert.AreEqual(1, graph.U.Size);
            Assert.AreEqual(0, graph.V.Size);
            var vB = graph.V.CreateVertex('B');
            Assert.AreEqual(2, graph.Size);
            Assert.AreEqual(1, graph.U.Size);
            Assert.AreEqual(1, graph.V.Size);
            graph.CreateEdge(vA, vB);
            Assert.AreEqual(2, graph.Size);
            var vC = graph.U.CreateVertex('C');
            Assert.AreEqual(3, graph.Size);
            Assert.AreEqual(2, graph.U.Size);
            Assert.AreEqual(1, graph.V.Size);
            var vD = graph.U.CreateVertex('D');
            Assert.AreEqual(4, graph.Size);
            Assert.AreEqual(3, graph.U.Size);
            Assert.AreEqual(1, graph.V.Size);
        }
        [TestMethod]
        public void GetVertexListTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            CollectionAssertEx.IsEmpty(graph.GetVertexList());
            CollectionAssertEx.IsEmpty(graph.U.GetVertexList());
            CollectionAssertEx.IsEmpty(graph.V.GetVertexList());
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            CollectionAssertEx.AreEquivalent(new char[] { 'A', 'C', 'B' }, graph.GetVertexList().Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'A', 'C' }, graph.U.GetVertexList().Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'B' }, graph.V.GetVertexList().Select(x => x.Value));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AreVerticesAdjacentGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AreVerticesAdjacentGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AreVerticesAdjacentGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(new BipartiteGraphVertex<char>('C'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AreVerticesAdjacentGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.AreVerticesAdjacent(vA, new BipartiteGraphVertex<char>('D'));
        }
        [TestMethod]
        public void AreVerticesAdjacentTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            Assert.IsFalse(graph.AreVerticesAdjacent(vA, vB));
            Assert.IsFalse(graph.AreVerticesAdjacent(vB, vC));
            Assert.IsTrue(graph.AreVerticesAdjacent(vA, vC));
            Assert.IsFalse(graph.AreVerticesAdjacent(vB, vA));
            Assert.IsFalse(graph.AreVerticesAdjacent(vC, vB));
            Assert.IsTrue(graph.AreVerticesAdjacent(vC, vA));
            Assert.IsFalse(graph.AreVerticesAdjacent(vA, vA));
            Assert.IsFalse(graph.AreVerticesAdjacent(vB, vB));
            Assert.IsFalse(graph.AreVerticesAdjacent(vC, vC));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetAdjacentVertextListGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.GetAdjacentVertextList(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAdjacentVertextListGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.GetAdjacentVertextList(new BipartiteGraphVertex<char>('C'));
        }
        [TestMethod]
        public void GetAdjacentVertextListTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            CollectionAssertEx.AreEquivalent(new char[] { 'C' }, graph.GetAdjacentVertextList(vA).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'C' }, graph.GetAdjacentVertextList(vB).Select(x => x.Value));
            CollectionAssertEx.AreEquivalent(new char[] { 'A', 'B' }, graph.GetAdjacentVertextList(vC).Select(x => x.Value));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetVertexGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var result = graph.GetVertex(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetVertexGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var result = graph.GetVertex(3);
        }
        [TestMethod]
        public void GetVertexTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            Assert.AreSame(vA, graph.GetVertex(0));
            Assert.AreSame(vC, graph.GetVertex(1));
            Assert.AreSame(vB, graph.GetVertex(2));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetWeightGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetWeightGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(new BipartiteGraphVertex<char>('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            graph.GetWeight(vA, new BipartiteGraphVertex<char>('B'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase5Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var result = graph.GetWeight(vA, vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase6Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetWeight(vA, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase7Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var result = graph.GetWeight(vB, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase8Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetWeight(vA, vA);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetWeightGuardCase9Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var result = graph.GetWeight(vB, vB);
        }
        [TestMethod]
        public void GetWeightTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vB, vC, 3);
            AssertEx.AreDoublesEqual(2, graph.GetWeight(vA, vB));
            AssertEx.AreDoublesEqual(2, graph.GetWeight(vB, vA));
            AssertEx.AreDoublesEqual(3, graph.GetWeight(vB, vC));
            AssertEx.AreDoublesEqual(3, graph.GetWeight(vC, vB));
        }
        [TestMethod]
        public void CreateEdgeDefaultWeightTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            AssertEx.AreDoublesEqual(1, graph.GetWeight(vA, vB));
            AssertEx.AreDoublesEqual(1, graph.GetWeight(vB, vA));
        }
        [TestMethod]
        public void GetEdgeListSimpleTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            CollectionAssertEx.IsEmpty(graph.GetEdgeList());
        }
        [TestMethod]
        public void GetEdgeListTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            graph.CreateEdge(vA, vB, 1);
            graph.CreateEdge(vA, vD, 2);
            graph.CreateEdge(vB, vC, 3);
            var result = graph.GetEdgeList();
            var expected = new Edge<char, BipartiteGraphVertex<char>>[] {
                new Edge<char, BipartiteGraphVertex<char>>(vA, vB, 1),
                new Edge<char, BipartiteGraphVertex<char>>(vA, vD, 2),
                new Edge<char, BipartiteGraphVertex<char>>(vC, vB, 3),
            };
            CollectionAssert.AreEquivalent(expected, result);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetEdgeDataGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetEdgeDataGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(new BipartiteGraphVertex<char>('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            var edgeData = graph.GetEdgeData(vA, new BipartiteGraphVertex<char>('B'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase5Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var edgeData = graph.GetEdgeData(vB, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase6Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var edgeData = graph.GetEdgeData(vA, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase7Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var edgeData = graph.GetEdgeData(vB, vC);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase8Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var edgeData = graph.GetEdgeData(vA, vA);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetEdgeDataGuardCase9Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            var edgeData = graph.GetEdgeData(vB, vB);
        }
        [TestMethod]
        public void GetEdgeDataSimpleTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
            edgeData = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
        }
        [TestMethod]
        public void GetEdgeDataTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB, 2);
            graph.CreateEdge(vB, vC, 3);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(2, true, Color.Empty, null), edgeData);
            edgeData = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(3, true, Color.Empty, null), edgeData);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(null, vB, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, null, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateEdgeDataGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, vB, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(new BipartiteGraphVertex<char>('A'), vB, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase5Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.UpdateEdgeData(vA, new BipartiteGraphVertex<char>('B'), x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase6Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.UpdateEdgeData(vB, vC, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase7Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.UpdateEdgeData(vA, vC, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase8Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.UpdateEdgeData(vB, vC, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase9Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.UpdateEdgeData(vA, vA, x => x);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEdgeDataGuardCase10Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.UpdateEdgeData(vB, vB, x => x);
        }
        [TestMethod]
        public void UpdateEdgeDataTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            EdgeData edgeData;
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, Color.Empty, null), edgeData);
            Color colorID = Color.CreateColor();
            graph.UpdateEdgeData(vA, vB, x => x.WithColor(colorID));
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, colorID, null), edgeData);
            TestEdgeData data = new TestEdgeData();
            graph.UpdateEdgeData(vA, vB, x => x.WithData(data));
            edgeData = graph.GetEdgeData(vA, vB);
            Assert.AreEqual(new EdgeData(1, true, colorID, data), edgeData);
        }
        [TestMethod]
        public void UpdateEdgeDataConsistencyTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            Color colorID = Color.CreateColor();
            TestEdgeData data = new TestEdgeData();
            graph.UpdateEdgeData(vB, vC, x => x.WithColor(colorID).WithData(data));
            EdgeData edgeData1 = graph.GetEdgeData(vB, vC);
            Assert.AreEqual(new EdgeData(1, true, colorID, data), edgeData1);
            EdgeData edgeData2 = graph.GetEdgeData(vC, vB);
            Assert.AreEqual(edgeData1, edgeData2);
        }
        [TestMethod]
        public void VertexDegreeTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            Assert.AreEqual(0, vA.Degree);
            Assert.AreEqual(0, vB.Degree);
            Assert.AreEqual(0, vC.Degree);
            Assert.AreEqual(0, vD.Degree);
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            Assert.AreEqual(2, vA.Degree);
            Assert.AreEqual(2, vB.Degree);
            Assert.AreEqual(1, vC.Degree);
            Assert.AreEqual(1, vD.Degree);
        }
        [TestMethod]
        public void BuildMSFTest1() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            graph.CreateEdge(vA, vB, 1);
            graph.CreateEdge(vA, vD, 2);
            graph.CreateEdge(vB, vC, 3);
            graph.CreateEdge(vD, vC, 7);
            graph.CreateEdge(vC, vF, 11);
            var forest = graph.BuildMSF();
            Assert.AreEqual(6, forest.Size);
            CollectionAssert.AreEqual(new char[] { 'A', 'C' }, forest.U.GetVertexValueList());
            CollectionAssert.AreEqual(new char[] { 'B', 'D', 'E', 'F' }, forest.V.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdges = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('A', 'B', 1),
                new EdgeTriplet<char>('C', 'B', 3),
                new EdgeTriplet<char>('A', 'D', 2),
                new EdgeTriplet<char>('C', 'F', 11),
            };
            CollectionAssertEx.AreEquivalent(extectedEdges, forest.GetEdgeTripletList());
        }
        [TestMethod]
        public void BuildMSFTest2() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vP = graph.U.CreateVertex('P');
            var vQ = graph.V.CreateVertex('Q');
            var vL = graph.U.CreateVertex('L');
            var vM = graph.V.CreateVertex('M');
            graph.CreateEdge(vA, vB, 7);
            graph.CreateEdge(vA, vD, 11);
            graph.CreateEdge(vB, vC, 9);
            graph.CreateEdge(vD, vC, 5);
            graph.CreateEdge(vP, vQ, 6);
            graph.CreateEdge(vQ, vL, 9);
            var forest = graph.BuildMSF();
            Assert.AreEqual(8, forest.Size);
            CollectionAssert.AreEquivalent(new char[] { 'A', 'C', 'P', 'L' }, forest.U.GetVertexValueList());
            CollectionAssert.AreEquivalent(new char[] { 'B', 'D', 'Q', 'M' }, forest.V.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdges = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('A', 'B', 7),
                new EdgeTriplet<char>('C', 'B', 9),
                new EdgeTriplet<char>('C', 'D', 5),
                new EdgeTriplet<char>('P', 'Q', 6),
                new EdgeTriplet<char>('L', 'Q', 9),
            };
            CollectionAssertEx.AreEquivalent(extectedEdges, forest.GetEdgeTripletList());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteEdgeGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DeleteEdge(null, vB);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteEdgeGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DeleteEdge(vA, null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DeleteEdgeGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DeleteEdge(new BipartiteGraphVertex<char>('A'), vB);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DeleteEdgeGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            graph.DeleteEdge(vA, new BipartiteGraphVertex<char>('B'));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DeleteEdgeGuardCase5Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            var vG = graph.V.CreateVertex('G');
            graph.CreateEdge(vB, vF);
            graph.CreateEdge(vB, vG);
            graph.CreateEdge(vC, vD);
            graph.CreateEdge(vC, vE);
            graph.DeleteEdge(vA, vG);
        }
        [TestMethod]
        public void DeleteEdgeTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            var vG = graph.V.CreateVertex('G');
            graph.CreateEdge(vB, vF, 1);
            graph.CreateEdge(vB, vG, 2);
            graph.CreateEdge(vC, vD, 3);
            graph.CreateEdge(vC, vE, 4);
            graph.DeleteEdge(vB, vG);
            graph.DeleteEdge(vD, vC);
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, graph.U.GetVertexValueList());
            CollectionAssert.AreEqual(new char[] { 'D', 'E', 'F', 'G' }, graph.V.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdges = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('B', 'F', 1),
                new EdgeTriplet<char>('C', 'E', 4),
            };
            CollectionAssert.AreEquivalent(extectedEdges, graph.GetEdgeTripletList());
        }
        [TestMethod]
        public void GraphEdgeCountTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            Assert.AreEqual(0, graph.Properties.EdgeCount);
            graph.CreateEdge(vA, vC);
            Assert.AreEqual(1, graph.Properties.EdgeCount);
            graph.CreateEdge(vA, vD);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
            graph.CreateEdge(vC, vB);
            Assert.AreEqual(3, graph.Properties.EdgeCount);
            graph.CreateEdge(vC, vB);
            Assert.AreEqual(3, graph.Properties.EdgeCount);
            graph.DeleteEdge(vA, vC);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
            graph.DeleteEdge(vB, vC);
            Assert.AreEqual(1, graph.Properties.EdgeCount);
            graph.DeleteEdge(vA, vD);
            Assert.AreEqual(0, graph.Properties.EdgeCount);
        }
        [TestMethod]
        public void DeleteEdgeGraphPropertiesTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.U.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            var vG = graph.V.CreateVertex('G');
            var vH = graph.V.CreateVertex('H');
            graph.CreateEdge(vA, vE);
            graph.CreateEdge(vB, vF, 3);
            graph.CreateEdge(vB, vG, 2);
            graph.CreateEdge(vC, vH, -7);
            graph.CreateEdge(vD, vH, -1);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsTrue(graph.Properties.IsNegativeWeighted);
            graph.DeleteEdge(vC, vH);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsTrue(graph.Properties.IsNegativeWeighted);
            graph.DeleteEdge(vD, vH);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsTrue(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
            graph.DeleteEdge(vF, vB);
            Assert.IsFalse(graph.Properties.IsUnweighted);
            Assert.IsTrue(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
            graph.DeleteEdge(vB, vG);
            Assert.IsTrue(graph.Properties.IsUnweighted);
            Assert.IsFalse(graph.Properties.IsWeighted);
            Assert.IsFalse(graph.Properties.IsNegativeWeighted);
            CollectionAssertEx.AreEquivalent(new EdgeTriplet<char>('A', 'E', 1).Yield(), graph.GetEdgeTripletList());
        }
        [TestMethod]
        public void DeleteEdgeVertexDegreeTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vC, vD);
            CollectionAssertEx.AreEqual(new int[] { 1, 1, 1, 3 }, graph.GetVertexList().Select(x => x.Degree));
            graph.DeleteEdge(vA, vD);
            graph.DeleteEdge(vC, vD);
            CollectionAssertEx.AreEqual(new int[] { 0, 1, 0, 1 }, graph.GetVertexList().Select(x => x.Degree));
        }
        [TestMethod]
        public void GetMaximalMatchingInEmptyGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var matching = graph.GetMaximalMatching();
            Assert.AreEqual(0, matching.Size);
            CollectionAssertEx.IsEmpty(matching.GetEdgeList());
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetMaximalMatchingInWeightedGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, 2);
            var matching = graph.GetMaximalMatching();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetMaximalMatchingInNegativeWeightedGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, -2);
            var matching = graph.GetMaximalMatching();
        }
        [TestMethod]
        public void GetMaximalMatchingSimpleTest1() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.V.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var matching = graph.GetMaximalMatching();
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, matching.V.GetVertexValueList());
            CollectionAssertEx.IsEmpty(matching.U.GetVertexList());
            CollectionAssertEx.IsEmpty(matching.GetEdgeList());
        }
        [TestMethod]
        public void GetMaximalMatchingSimpleTest2() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var matching = graph.GetMaximalMatching();
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, matching.U.GetVertexValueList());
            CollectionAssertEx.IsEmpty(matching.V.GetVertexList());
            CollectionAssertEx.IsEmpty(matching.GetEdgeList());
        }
        [TestMethod]
        public void GetMaximalMatchingSimpleTest3() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            var v3 = graph.V.CreateVertex("V3");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u1, v2);
            graph.CreateEdge(u1, v3);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U1", "V1", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingStructureTest() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var u2 = graph.U.CreateVertex("U2");
            var u3 = graph.U.CreateVertex("U3");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            var v3 = graph.V.CreateVertex("V3");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u2, v2);
            graph.CreateEdge(u3, v3);
            var matching = graph.GetMaximalMatching();
            CollectionAssert.AreEqual(new string[] { "U1", "U2", "U3" }, matching.U.GetVertexValueList());
            CollectionAssert.AreEqual(new string[] { "V1", "V2", "V3" }, matching.V.GetVertexValueList());
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U1", "V1", 1),
                new EdgeTriplet<string>("U2", "V2", 1),
                new EdgeTriplet<string>("U3", "V3", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingTest1() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var u2 = graph.U.CreateVertex("U2");
            var u3 = graph.U.CreateVertex("U3");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            var v3 = graph.V.CreateVertex("V3");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u1, v3);
            graph.CreateEdge(u2, v2);
            graph.CreateEdge(u2, v3);
            graph.CreateEdge(u3, v1);
            graph.CreateEdge(u3, v2);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U3", "V1", 1),
                new EdgeTriplet<string>("U2", "V2", 1),
                new EdgeTriplet<string>("U1", "V3", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingTest2() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var u2 = graph.U.CreateVertex("U2");
            var u3 = graph.U.CreateVertex("U3");
            var u4 = graph.U.CreateVertex("U4");
            var u5 = graph.U.CreateVertex("U5");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            var v3 = graph.V.CreateVertex("V3");
            var v4 = graph.V.CreateVertex("V4");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u2, v1);
            graph.CreateEdge(u2, v3);
            graph.CreateEdge(u3, v2);
            graph.CreateEdge(u3, v3);
            graph.CreateEdge(u3, v4);
            graph.CreateEdge(u4, v3);
            graph.CreateEdge(u5, v3);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U2", "V1", 1),
                new EdgeTriplet<string>("U3", "V2", 1),
                new EdgeTriplet<string>("U5", "V3", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingTest3() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var u2 = graph.U.CreateVertex("U2");
            var u3 = graph.U.CreateVertex("U3");
            var u4 = graph.U.CreateVertex("U4");
            var u5 = graph.U.CreateVertex("U5");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            var v3 = graph.V.CreateVertex("V3");
            var v4 = graph.V.CreateVertex("V4");
            var v5 = graph.V.CreateVertex("V5");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u1, v2);
            graph.CreateEdge(u2, v1);
            graph.CreateEdge(u2, v5);
            graph.CreateEdge(u3, v3);
            graph.CreateEdge(u3, v4);
            graph.CreateEdge(u4, v1);
            graph.CreateEdge(u4, v5);
            graph.CreateEdge(u5, v2);
            graph.CreateEdge(u5, v4);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U4", "V1", 1),
                new EdgeTriplet<string>("U1", "V2", 1),
                new EdgeTriplet<string>("U3", "V3", 1),
                new EdgeTriplet<string>("U5", "V4", 1),
                new EdgeTriplet<string>("U2", "V5", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingTest4() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var u1 = graph.U.CreateVertex("U1");
            var u2 = graph.U.CreateVertex("U2");
            var u3 = graph.U.CreateVertex("U3");
            var v1 = graph.V.CreateVertex("V1");
            var v2 = graph.V.CreateVertex("V2");
            graph.CreateEdge(u1, v1);
            graph.CreateEdge(u1, v2);
            graph.CreateEdge(u2, v1);
            graph.CreateEdge(u3, v1);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("U3", "V1", 1),
                new EdgeTriplet<string>("U1", "V2", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingTest5() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var vA = graph.U.CreateVertex("A");
            var vB = graph.U.CreateVertex("B");
            var vC = graph.U.CreateVertex("C");
            var vD = graph.U.CreateVertex("D");
            var vE = graph.V.CreateVertex("E");
            var vF = graph.V.CreateVertex("F");
            var vG = graph.V.CreateVertex("G");
            var vH = graph.V.CreateVertex("H");
            graph.CreateEdge(vA, vF);
            graph.CreateEdge(vA, vH);
            graph.CreateEdge(vB, vG);
            graph.CreateEdge(vC, vE);
            graph.CreateEdge(vC, vF);
            graph.CreateEdge(vD, vG);
            graph.CreateEdge(vD, vH);
            var matching = graph.GetMaximalMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("C", "E", 1),
                new EdgeTriplet<string>("A", "F", 1),
                new EdgeTriplet<string>("B", "G", 1),
                new EdgeTriplet<string>("D", "H", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetMaximalMatchingOriginGraphDataTest() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var vA = graph.U.CreateVertex("A");
            var vB = graph.U.CreateVertex("B");
            var vC = graph.U.CreateVertex("C");
            var vD = graph.V.CreateVertex("D");
            var vE = graph.V.CreateVertex("E");
            var vF = graph.V.CreateVertex("F");
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vC, vE);
            graph.CreateEdge(vC, vF);
            var matching = graph.GetMaximalMatching();
            Assert.IsNotNull(matching);
            foreach(var vertex in graph.GetVertexList()) {
                Assert.IsNull(vertex.Data);
            }
            graph.Data.Matrix.ForEach(x => Assert.IsNull(x.Data));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAssignmentMatchingInEmptyGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var matching = graph.GetAssignmentMatching();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAssignmentMatchingIfVIsBiggerTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vA, vC);
            var matching = graph.GetAssignmentMatching();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAssignmentMatchingIfUIsBiggerTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            var matching = graph.GetAssignmentMatching();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAssignmentMatchingInNegativeWeightedGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB, -1);
            var matching = graph.GetAssignmentMatching();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void GetAssignmentMatchingInNonCompleteGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vB, vC);
            var matching = graph.GetAssignmentMatching();
        }
        [TestMethod]
        public void GetAssignmentMatchingSimpleTest() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var w1 = graph.V.CreateVertex("W1");
            graph.CreateEdge(t1, w1);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T1", "W1", 1),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetAssignmentMatchingTest1() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var t2 = graph.U.CreateVertex("T2");
            var t3 = graph.U.CreateVertex("T3");
            var w1 = graph.V.CreateVertex("W1");
            var w2 = graph.V.CreateVertex("W2");
            var w3 = graph.V.CreateVertex("W3");
            graph.CreateEdge(w1, t1, 2);
            graph.CreateEdge(w1, t2, 3);
            graph.CreateEdge(w1, t3, 3);
            graph.CreateEdge(w2, t1, 3);
            graph.CreateEdge(w2, t2, 2);
            graph.CreateEdge(w2, t3, 3);
            graph.CreateEdge(w3, t1, 3);
            graph.CreateEdge(w3, t2, 3);
            graph.CreateEdge(w3, t3, 2);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T1", "W1", 2),
                new EdgeTriplet<string>("T2", "W2", 2),
                new EdgeTriplet<string>("T3", "W3", 2),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetAssignmentMatchingTest2() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var t2 = graph.U.CreateVertex("T2");
            var t3 = graph.U.CreateVertex("T3");
            var t4 = graph.U.CreateVertex("T4");
            var w1 = graph.V.CreateVertex("W1");
            var w2 = graph.V.CreateVertex("W2");
            var w3 = graph.V.CreateVertex("W3");
            var w4 = graph.V.CreateVertex("W4");
            graph.CreateEdge(w1, t1, 1);
            graph.CreateEdge(w1, t2, 7);
            graph.CreateEdge(w1, t3, 1);
            graph.CreateEdge(w1, t4, 3);
            graph.CreateEdge(w2, t1, 1);
            graph.CreateEdge(w2, t2, 6);
            graph.CreateEdge(w2, t3, 4);
            graph.CreateEdge(w2, t4, 6);
            graph.CreateEdge(w3, t1, 17);
            graph.CreateEdge(w3, t2, 1);
            graph.CreateEdge(w3, t3, 5);
            graph.CreateEdge(w3, t4, 1);
            graph.CreateEdge(w4, t1, 1);
            graph.CreateEdge(w4, t2, 6);
            graph.CreateEdge(w4, t3, 10);
            graph.CreateEdge(w4, t4, 4);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T3", "W1", 1),
                new EdgeTriplet<string>("T1", "W2", 1),
                new EdgeTriplet<string>("T2", "W3", 1),
                new EdgeTriplet<string>("T4", "W4", 4),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetAssignmentMatchingTest3() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var t2 = graph.U.CreateVertex("T2");
            var t3 = graph.U.CreateVertex("T3");
            var t4 = graph.U.CreateVertex("T4");
            var t5 = graph.U.CreateVertex("T5");
            var w1 = graph.V.CreateVertex("W1");
            var w2 = graph.V.CreateVertex("W2");
            var w3 = graph.V.CreateVertex("W3");
            var w4 = graph.V.CreateVertex("W4");
            var w5 = graph.V.CreateVertex("W5");
            graph.CreateEdge(w1, t1, 7);
            graph.CreateEdge(w1, t2, 11);
            graph.CreateEdge(w1, t3, 5);
            graph.CreateEdge(w1, t4, 7);
            graph.CreateEdge(w1, t5, 9);
            graph.CreateEdge(w2, t1, 15);
            graph.CreateEdge(w2, t2, 10);
            graph.CreateEdge(w2, t3, 5);
            graph.CreateEdge(w2, t4, 12);
            graph.CreateEdge(w2, t5, 5);
            graph.CreateEdge(w3, t1, 7);
            graph.CreateEdge(w3, t2, 12);
            graph.CreateEdge(w3, t3, 20);
            graph.CreateEdge(w3, t4, 7);
            graph.CreateEdge(w3, t5, 11);
            graph.CreateEdge(w4, t1, 9);
            graph.CreateEdge(w4, t2, 9);
            graph.CreateEdge(w4, t3, 9);
            graph.CreateEdge(w4, t4, 9);
            graph.CreateEdge(w4, t5, 9);
            graph.CreateEdge(w5, t1, 7);
            graph.CreateEdge(w5, t2, 9);
            graph.CreateEdge(w5, t3, 12);
            graph.CreateEdge(w5, t4, 13);
            graph.CreateEdge(w5, t5, 15);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T3", "W1", 5),
                new EdgeTriplet<string>("T5", "W2", 5),
                new EdgeTriplet<string>("T4", "W3", 7),
                new EdgeTriplet<string>("T2", "W4", 9),
                new EdgeTriplet<string>("T1", "W5", 7),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetAssignmentMatchingTest4() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var t2 = graph.U.CreateVertex("T2");
            var t3 = graph.U.CreateVertex("T3");
            var w1 = graph.V.CreateVertex("W1");
            var w2 = graph.V.CreateVertex("W2");
            var w3 = graph.V.CreateVertex("W3");
            graph.CreateEdge(w1, t1, 7);
            graph.CreateEdge(w1, t2, 9);
            graph.CreateEdge(w1, t3, 2);
            graph.CreateEdge(w2, t1, 3);
            graph.CreateEdge(w2, t2, 6);
            graph.CreateEdge(w2, t3, 8);
            graph.CreateEdge(w3, t1, 4);
            graph.CreateEdge(w3, t2, 12);
            graph.CreateEdge(w3, t3, 12);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T3", "W1", 2),
                new EdgeTriplet<string>("T2", "W2", 6),
                new EdgeTriplet<string>("T1", "W3", 4),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void GetAssignmentMatchingTest5() {
            BipartiteGraph<string> graph = new BipartiteGraph<string>();
            var t1 = graph.U.CreateVertex("T1");
            var t2 = graph.U.CreateVertex("T2");
            var t3 = graph.U.CreateVertex("T3");
            var t4 = graph.U.CreateVertex("T4");
            var t5 = graph.U.CreateVertex("T5");
            var w1 = graph.V.CreateVertex("W1");
            var w2 = graph.V.CreateVertex("W2");
            var w3 = graph.V.CreateVertex("W3");
            var w4 = graph.V.CreateVertex("W4");
            var w5 = graph.V.CreateVertex("W5");
            graph.CreateEdge(w1, t1, 32);
            graph.CreateEdge(w1, t2, 28);
            graph.CreateEdge(w1, t3, 4);
            graph.CreateEdge(w1, t4, 26);
            graph.CreateEdge(w1, t5, 4);
            graph.CreateEdge(w2, t1, 17);
            graph.CreateEdge(w2, t2, 19);
            graph.CreateEdge(w2, t3, 4);
            graph.CreateEdge(w2, t4, 17);
            graph.CreateEdge(w2, t5, 4);
            graph.CreateEdge(w3, t1, 4);
            graph.CreateEdge(w3, t2, 4);
            graph.CreateEdge(w3, t3, 5);
            graph.CreateEdge(w3, t4, 4);
            graph.CreateEdge(w3, t5, 4);
            graph.CreateEdge(w4, t1, 17);
            graph.CreateEdge(w4, t2, 14);
            graph.CreateEdge(w4, t3, 4);
            graph.CreateEdge(w4, t4, 14);
            graph.CreateEdge(w4, t5, 4);
            graph.CreateEdge(w5, t1, 21);
            graph.CreateEdge(w5, t2, 16);
            graph.CreateEdge(w5, t3, 4);
            graph.CreateEdge(w5, t4, 13);
            graph.CreateEdge(w5, t5, 4);
            var matching = graph.GetAssignmentMatching();
            EdgeTriplet<string>[] extectedEdgeList = new EdgeTriplet<string>[] {
                new EdgeTriplet<string>("T5", "W1", 4),
                new EdgeTriplet<string>("T3", "W2", 4),
                new EdgeTriplet<string>("T1", "W3", 4),
                new EdgeTriplet<string>("T2", "W4", 14),
                new EdgeTriplet<string>("T4", "W5", 13),
            };
            CollectionAssert.AreEqual(extectedEdgeList, matching.GetEdgeTripletList());
        }
        [TestMethod]
        public void CloneEmptyGraphTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var result = graph.Clone<BipartiteGraph<char>>(false);
            CollectionAssertEx.IsEmpty(result.U.GetVertexList());
            CollectionAssertEx.IsEmpty(result.V.GetVertexList());
            CollectionAssertEx.IsEmpty(result.GetEdgeList());
        }
        [TestMethod]
        public void CloneTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vK = graph.V.CreateVertex('K');
            var vM = graph.V.CreateVertex('M');
            graph.CreateEdge(vA, vK);
            graph.CreateEdge(vB, vK);
            graph.CreateEdge(vC, vM);
            var result = graph.Clone<BipartiteGraph<char>>(false);
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, result.U.GetVertexValueList());
            CollectionAssert.AreEqual(new char[] { 'K', 'M' }, result.V.GetVertexValueList());
            CollectionAssertEx.IsEmpty(result.GetEdgeList());
        }
        [TestMethod]
        public void CloneGraphWithEdgesSimpleTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var result = graph.Clone<BipartiteGraph<char>>(true);
            CollectionAssertEx.IsEmpty(result.U.GetVertexValueList());
            CollectionAssertEx.IsEmpty(result.V.GetVertexValueList());
            CollectionAssertEx.IsEmpty(result.GetEdgeList());
        }
        [TestMethod]
        public void CloneGraphWtihEdgesTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            var vG = graph.V.CreateVertex('G');
            graph.CreateEdge(vA, vF, 9);
            graph.CreateEdge(vB, vD, 7);
            graph.CreateEdge(vC, vE, 5);
            var result = graph.Clone<BipartiteGraph<char>>(true);
            CollectionAssertEx.AreEqual(new char[] { 'A', 'B', 'C' }, result.U.GetVertexValueList());
            CollectionAssertEx.AreEqual(new char[] { 'D', 'E', 'F', 'G' }, result.V.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('B', 'D', 7),
                new EdgeTriplet<char>('C', 'E', 5),
                new EdgeTriplet<char>('A', 'F', 9),
            };
            CollectionAssert.AreEqual(extectedEdgeList, result.GetEdgeTripletList());
        }
        [TestMethod]
        public void GraphEdgeCountMultigraphCaseTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vD);
            Assert.AreEqual(2, graph.Properties.EdgeCount);
        }
        [TestMethod]
        public void IsCompleteTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            Assert.IsFalse(graph.IsComplete());
            var vA = graph.U.CreateVertex('A');
            Assert.IsFalse(graph.IsComplete());
            var vD = graph.V.CreateVertex('D');
            Assert.IsFalse(graph.IsComplete());
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vE = graph.V.CreateVertex('E');
            Assert.IsFalse(graph.IsComplete());
            graph.CreateEdge(vA, vD);
            graph.CreateEdge(vA, vE);
            graph.CreateEdge(vB, vD);
            graph.CreateEdge(vB, vE);
            graph.CreateEdge(vC, vD);
            Assert.IsFalse(graph.IsComplete());
            graph.CreateEdge(vC, vE);
            Assert.IsTrue(graph.IsComplete());
        }
        [TestMethod]
        public void BipartiteGraphMatchingIsPerfectTest() {
            BipartiteGraphMatching<char> graph = new BipartiteGraphMatching<char>();
            Assert.IsFalse(graph.IsPerfect());
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vT = graph.U.CreateVertex('T');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            var vF = graph.V.CreateVertex('F');
            var vG = graph.V.CreateVertex('G');
            Assert.IsFalse(graph.IsPerfect());
            graph.CreateEdge(vC, vD);
            Assert.IsFalse(graph.IsPerfect());
            graph.CreateEdge(vB, vF);
            Assert.IsFalse(graph.IsPerfect());
            graph.CreateEdge(vT, vG);
            Assert.IsFalse(graph.IsPerfect());
            graph.CreateEdge(vA, vE);
            Assert.IsTrue(graph.IsPerfect());
            graph.CreateEdge(vT, vF);
            Assert.IsFalse(graph.IsPerfect());
            graph.DeleteEdge(vT, vF);
            Assert.IsTrue(graph.IsPerfect());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CloneWithEdgesGuardCase1Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.V.CreateVertex('B');
            graph.CreateEdge(vA, vB);
            var result = graph.CloneWithEdges<BipartiteGraph<char>>(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CloneWithEdgesGuardCase2Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            BipartiteGraph<char> other = new BipartiteGraph<char>();
            var otherA = other.U.CreateVertex('A');
            var otherC = other.V.CreateVertex('C');
            other.CreateEdge(otherA, otherC);
            var result = graph.CloneWithEdges(other);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CloneWithEdgesGuardCase3Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            graph.CreateEdge(vB, vC);
            BipartiteGraph<char> other = new BipartiteGraph<char>();
            var otherA = other.U.CreateVertex('A');
            var otherB = other.U.CreateVertex('B');
            var otherC = other.V.CreateVertex('C');
            var otherD = other.V.CreateVertex('D');
            other.CreateEdge(otherA, otherC);
            var result = graph.CloneWithEdges(other);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CloneWithEdgesGuardCase4Test() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.V.CreateVertex('C');
            graph.CreateEdge(vA, vC);
            BipartiteGraph<char> other = new BipartiteGraph<char>();
            var otherA = other.U.CreateVertex('A');
            var otherB = other.U.CreateVertex('B');
            var otherC = other.V.CreateVertex('C');
            other.CreateEdge(otherA, otherC);
            other.CreateEdge(otherB, otherC);
            var result = graph.CloneWithEdges(other);
        }
        [TestMethod]
        public void CloneWithEdgesTest() {
            BipartiteGraph<char> graph = new BipartiteGraph<char>();
            var vA = graph.U.CreateVertex('A');
            var vB = graph.U.CreateVertex('B');
            var vC = graph.U.CreateVertex('C');
            var vD = graph.V.CreateVertex('D');
            var vE = graph.V.CreateVertex('E');
            graph.CreateEdge(vB, vD, 7);
            graph.CreateEdge(vC, vD, 3);
            graph.CreateEdge(vC, vE, 2);
            BipartiteGraph<char> other = new BipartiteGraph<char>();
            var otherK = other.U.CreateVertex('K');
            var otherL = other.U.CreateVertex('L');
            var otherM = other.U.CreateVertex('M');
            var otherN = other.V.CreateVertex('N');
            var otherP = other.V.CreateVertex('P');
            other.CreateEdge(otherL, otherN, 2);
            other.CreateEdge(otherM, otherN, 11);
            var result = graph.CloneWithEdges(other);
            CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, result.U.GetVertexValueList());
            CollectionAssert.AreEqual(new char[] { 'D', 'E' }, result.V.GetVertexValueList());
            EdgeTriplet<char>[] extectedEdgeList = new EdgeTriplet<char>[] {
                new EdgeTriplet<char>('B', 'D', 7),
                new EdgeTriplet<char>('C', 'D', 3),
            };
            CollectionAssert.AreEqual(extectedEdgeList, result.GetEdgeTripletList());
        }
    }

    #region TestEdgeData
    class TestEdgeData : IEdgeData {
    }
    #endregion

    static class GraphTestExtensions {
        public static List<TValue> GetVertexValueList<TValue, TVertex>(this Graph<TValue, TVertex> graph) where TVertex : Vertex<TValue> {
            return graph.GetVertexList().Select(x => x.Value).ToList();
        }
        public static List<TValue> GetVertexValueList<TValue, TVertex>(this BipartiteGraphPartition<TValue, TVertex> partition) where TVertex : BipartiteGraphVertex<TValue> {
            return partition.GetVertexList().Select(x => x.Value).ToList();
        }
        public static List<EdgeTriplet<TValue>> GetEdgeTripletList<TValue, TVertex>(this Graph<TValue, TVertex> graph) where TVertex : Vertex<TValue> {
            return graph.GetEdgeList().Select(x => x.CreateTriplet()).ToList();
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
