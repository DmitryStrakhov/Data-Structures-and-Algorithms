﻿#if DEBUG

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
            Assert.AreEqual(GraphProperties.Unweighted, graph.Properties);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC);
            graph.CreateEdge(vC, vA);
            Assert.AreEqual(GraphProperties.Unweighted, graph.Properties);
        }
        [TestMethod]
        public void WeightedGraphTest() {
            var graph = CreateGraph();
            Assert.AreEqual(GraphProperties.Unweighted, graph.Properties);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC, 2);
            graph.CreateEdge(vC, vA, 3);
            Assert.AreEqual(GraphProperties.Weighted, graph.Properties);
        }
        [TestMethod]
        public void NegativeWeightedGraphTest() {
            var graph = CreateGraph();
            Assert.AreEqual(GraphProperties.Unweighted, graph.Properties);
            var vA = graph.CreateVertex('A');
            var vB = graph.CreateVertex('B');
            var vC = graph.CreateVertex('C');
            graph.CreateEdge(vA, vB);
            graph.CreateEdge(vB, vC, -2);
            graph.CreateEdge(vC, vA, -3);
            Assert.AreEqual(GraphProperties.Weighted | GraphProperties.NegativeWeighted, graph.Properties);
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
            CollectionAssert.AreEqual(new char[] { '7', '5', '3', '1', '8', '2', '4', '9'  }, valueList);
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
