using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjSetGraphVertex ({Value})")]
    public class AdjSetGraphVertex<T> : UndirectedVertex<T> {
        internal AdjSetGraphVertex(T value)
            : base(value) {
        }
    }

    [DebuggerDisplay("DirectedAdjSetGraphVertex ({Value})")]
    public class DirectedAdjSetGraphVertex<T> : DirectedVertex<T> {
        internal DirectedAdjSetGraphVertex(T value)
            : base(value) {
        }
    }

    abstract class AdjSetGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        int size;
        int capacity;
        VertexDisjointSet[] list;

        public AdjSetGraphDataBase(int capacity) : base(capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.list = new VertexDisjointSet[capacity];
        }

        public int Size { get { return size; } }
        public int Capacity { get { return capacity; } }

        internal override void RegisterVertex(TVertex vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureListSize(newSize);
            List[handle] = new VertexDisjointSet(vertex);
            vertex.Handle = handle;
        }
        internal override List<TVertex> GetVertexList() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        internal override List<TVertex> GetAdjacentVertextList(TVertex vertex) {
            return List[vertex.Handle].GetVertices(false).ToList();
        }
        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            return List[vertex1.Handle].IsVertexAdjacent(vertex2);
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return List[handle].Vertex;
        }
        
        internal override int GetSize() {
            return Size;
        }
        internal override double GetWeight(TVertex vertex1, TVertex vertex2) {
            return GetVertexSetItem(vertex1, vertex2).EdgeData.Weight;
        }
        internal override EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2) {
            return GetVertexSetItem(vertex1, vertex2).EdgeData;
        }
        internal override List<Edge<TValue, TVertex>> GetEdgeList() {
            List<Edge<TValue, TVertex>> list = new List<Edge<TValue, TVertex>>();
            for(int n = 0; n < Size; n++) {
                VertexDisjointSet set = List[n];
                foreach(TVertex vertex in set.GetVertices(false)) {
                    if(AllowEdge(set, vertex))
                        list.Add(new Edge<TValue, TVertex>(set.Vertex, vertex, set.GetItem(vertex).EdgeData.Weight));
                }
            }
            return list;
        }
        protected abstract bool AllowEdge(VertexDisjointSet set, TVertex vertex);

        protected VertexSetItem GetVertexSetItem(TVertex vertex1, TVertex vertex2) {
            return List[vertex1.Handle].GetItem(vertex2);
        }
        void EnsureListSize(int newSize) {
            if(newSize > this.capacity) {
                int _capacity = this.capacity * 2;
                if(newSize > _capacity)
                    _capacity = newSize * 2;
                VertexDisjointSet[] _list = new VertexDisjointSet[_capacity];
                Array.Copy(List, _list, List.Length);
                this.list = _list;
                this.capacity = _capacity;
            }
        }

        #region VertexDisjointSet
        [DebuggerDisplay("VertexDisjointSet ({Vertex.Value})")]
        internal class VertexDisjointSet : DisjointSet<TVertex> {
            bool selfLooped;
            readonly TVertex vertex;

            public VertexDisjointSet(TVertex vertex) {
                this.vertex = MakeSet(vertex);
            }
            public void AddVertex(TVertex other, double weight) {
                if(ReferenceEquals(other, Vertex)) {
                    this.selfLooped = true;
                }
                else {
                    MakeSet(other);
                    Union(Vertex, other);
                }
                GetItem(other).EdgeData = new EdgeData(weight);
            }
            public bool IsVertexAdjacent(TVertex other) {
                return Exists(other) && AreEquivalent(Vertex, other);
            }
            public IEnumerable<TVertex> GetVertices(bool includeBaseVertex = true) {
                if(includeBaseVertex) {
                    yield return Vertex;
                }
                if(this.selfLooped) yield return Vertex;
                foreach(var item in Items) {
                    if(!ReferenceEquals(item, Vertex)) yield return item;
                }
            }
            sealed protected override SetItem CreateSetItem(TVertex item, int rank) {
                return new VertexSetItem(item, rank);
            }

            internal VertexSetItem GetItem(TVertex item) {
                return (VertexSetItem)GetItemCore(item);
            }
            public TVertex Vertex { get { return vertex; } }
        }
        #region VertexSetItem
        internal class VertexSetItem : DisjointSet<TVertex>.SetItem {
            EdgeData edgeData;
            public VertexSetItem(TVertex parent, int rank)
                : base(parent, rank) {
            }
            public EdgeData EdgeData {
                get { return edgeData; }
                set { edgeData = value; }
            }
        }
        #endregion
        #endregion

        internal IEnumerable<TValue>[] GetData() {
            IEnumerable<TValue>[] result = new IEnumerable<TValue>[Size];
            for(int i = 0; i < Size; i++) {
                result[i] = List[i].GetVertices().Select(x => x.Value).ToList();
            }
            return result;
        }

        internal VertexDisjointSet[] List { get { return list; } }

    }

    class UndirectedAdjSetGraphData<T> : AdjSetGraphDataBase<T, AdjSetGraphVertex<T>> {
        public UndirectedAdjSetGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2, double weight) {
            List[vertex1.Handle].AddVertex(vertex2, weight);
            if(!ReferenceEquals(vertex1, vertex2)) {
                List[vertex2.Handle].AddVertex(vertex1, weight);
            }
        }
        internal override void UpdateEdgeData(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            VertexSetItem setItem1 = GetVertexSetItem(vertex1, vertex2);
            VertexSetItem setItem2 = GetVertexSetItem(vertex2, vertex1);
            setItem1.EdgeData = setItem2.EdgeData = updateFunc(setItem1.EdgeData);
        }
        protected override bool AllowEdge(VertexDisjointSet set, AdjSetGraphVertex<T> vertex) {
            return vertex.Handle >= set.Vertex.Handle;
        }
    }

    class DirectedAdjSetGraphData<T> : AdjSetGraphDataBase<T, DirectedAdjSetGraphVertex<T>> {
        public DirectedAdjSetGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(DirectedAdjSetGraphVertex<T> vertex1, DirectedAdjSetGraphVertex<T> vertex2, double weight) {
            List[vertex1.Handle].AddVertex(vertex2, weight);
        }
        internal override void UpdateEdgeData(DirectedAdjSetGraphVertex<T> vertex1, DirectedAdjSetGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            VertexSetItem setItem = GetVertexSetItem(vertex1, vertex2);
            setItem.EdgeData = updateFunc(setItem.EdgeData);
        }
        protected override bool AllowEdge(VertexDisjointSet set, DirectedAdjSetGraphVertex<T> vertex) {
            return true;
        }
    }


    public class AdjSetGraph<T> : UndirectedGraph<T, AdjSetGraphVertex<T>> {
        public AdjSetGraph()
            : this(DefaultCapacity) {
        }
        public AdjSetGraph(int capacity)
            : base(capacity) {
        }
        public AdjSetGraph<T> BuildMSF() {
            return DoBuildMSF(new AdjSetGraph<T>());
        }
        protected override void CreateEdgeCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2, double weight) {
            if(!ReferenceEquals(vertex1, vertex2) && Data.AreVerticesAdjacent(vertex1, vertex2)) {
                throw new InvalidOperationException();
            }
            base.CreateEdgeCore(vertex1, vertex2, weight);
        }
        internal override GraphDataBase<T, AdjSetGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjSetGraphData<T>(capacity);
        }
        internal override AdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new AdjSetGraphVertex<T>(value);
        }
        internal new UndirectedAdjSetGraphData<T> Data { get { return (UndirectedAdjSetGraphData<T>)base.Data; } }
    }

    public class DirectedAdjSetGraph<T> : DirectedGraph<T, DirectedAdjSetGraphVertex<T>> {
        public DirectedAdjSetGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjSetGraph(int capacity)
            : base(capacity) {
        }
        public DirectedAdjSetGraph<T> BuildTransposeGraph() {
            DirectedAdjSetGraph<T> graph = new DirectedAdjSetGraph<T>();
            FillTransposeGraph(graph);
            return graph;
        }
        protected override void CreateEdgeCore(DirectedAdjSetGraphVertex<T> vertex1, DirectedAdjSetGraphVertex<T> vertex2, double weight) {
            if(!ReferenceEquals(vertex1, vertex2) && Data.AreVerticesAdjacent(vertex1, vertex2)) {
                throw new InvalidOperationException();
            }
            base.CreateEdgeCore(vertex1, vertex2, weight);
        }
        internal override GraphDataBase<T, DirectedAdjSetGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjSetGraphData<T>(capacity);
        }
        internal override DirectedAdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new DirectedAdjSetGraphVertex<T>(value);
        }
        internal new DirectedAdjSetGraphData<T> Data { get { return (DirectedAdjSetGraphData<T>)base.Data; } }
    }
}
