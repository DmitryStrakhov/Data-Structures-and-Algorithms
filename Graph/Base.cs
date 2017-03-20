using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class Vertex<T> {
        T value;
        Guid? ownerID;
        int? handle;
        VertexTag tag;

        internal Vertex(T value) {
            this.value = value;
            this.ownerID = null;
            this.handle = null;
            this.tag = new VertexTag(0, VertexColor.Empty);
        }
        internal Guid OwnerID {
            get { return ownerID.Value; }
            set {
                if(ownerID.HasValue) {
                    throw new InvalidOperationException();
                }
                ownerID = value;
            }
        }
        internal int Handle {
            get { return handle.Value; }
            set {
                if(handle.HasValue) {
                    throw new InvalidOperationException();
                }
                handle = value;
            }
        }
        internal VertexTag Tag {
            get { return tag; }
        }

        public T Value { get { return value; } }
    }

    public abstract class UndirectedVertex<T> : Vertex<T> {
        int degree;
        internal UndirectedVertex(T value) : base(value) {
            this.degree = 0;
        }
        public int Degree {
            get { return degree; }
            internal set { degree = value; }
        }
    }

    public abstract class DirectedVertex<T> : Vertex<T> {
        int inDegree;
        int outDegree;
        internal DirectedVertex(T value) : base(value) {
            this.inDegree = this.outDegree = 0;
        }
        public int InDegree {
            get { return inDegree; }
            internal set { inDegree = value; }
        }
        public int OutDegree {
            get { return outDegree; }
            internal set { outDegree = value; }
        }
    }

    [DebuggerDisplay("StartVertex: {StartVertex.Value}, EndVertex: {EndVertex.Value}, Weight: {Weight}")]
    public class Edge<TValue, TVertex> where TVertex : Vertex<TValue> {
        readonly TVertex startVertex;
        readonly TVertex endVertex;
        readonly double weight;

        internal Edge(TVertex startVertex, TVertex endVertex, double weight) {
            this.startVertex = startVertex;
            this.endVertex = endVertex;
            this.weight = weight;
        }
        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            Edge<TValue, TVertex> other = obj as Edge<TValue, TVertex>;
            return other != null && AreEquals(this, other);
        }
        public override int GetHashCode() {
            return StartVertex.GetHashCode() ^ EndVertex.GetHashCode() ^ Weight.GetHashCode();
        }
        #endregion
        static bool AreEquals(Edge<TValue, TVertex> x, Edge<TValue, TVertex> y) {
            return ReferenceEquals(x.StartVertex, y.StartVertex) && ReferenceEquals(x.EndVertex, y.EndVertex) && MathUtils.AreDoubleEquals(x.Weight, y.Weight);
        }

        internal EdgeTriplet<TValue> CreateTriplet() {
            return new EdgeTriplet<TValue>(StartVertex.Value, EndVertex.Value, Weight);
        }

        public TVertex StartVertex { get { return startVertex; } }
        public TVertex EndVertex { get { return endVertex; } }
        public double Weight { get { return weight; } }
    }

    [DebuggerDisplay("Value1: {Value1}, Value2: {Value2}, Weight: {Weight}")]
    class EdgeTriplet<T> {
        readonly T value1;
        readonly T value2;
        readonly double weight;

        public EdgeTriplet(T value1, T value2, double weight) {
            this.value1 = value1;
            this.value2 = value2;
            this.weight = weight;
        }

        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            EdgeTriplet<T> other = obj as EdgeTriplet<T>;
            return other != null && AreEquals(this, other);
        }
        public override int GetHashCode() {
            return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Weight.GetHashCode();
        }
        #endregion
        static bool AreEquals(EdgeTriplet<T> x, EdgeTriplet<T> y) {
            return EqualityComparer<T>.Default.Equals(x.Value1, y.Value1) && EqualityComparer<T>.Default.Equals(x.Value2, y.Value2) && MathUtils.AreDoubleEquals(x.Weight, y.Weight);
        }

        public T Value1 { get { return value1; } }
        public T Value2 { get { return value2; } }
        public double Weight { get { return weight; } }
    }


    [Flags]
    public enum GraphProperties {
        Unweighted = 1,
        Weighted = 2,
        NegativeWeighted = 4
    }

    internal struct VertexColor {
        Guid guid;

        public static VertexColor NewColor() {
            return new VertexColor() { guid = Guid.NewGuid() };
        }
        public static readonly VertexColor Empty = NewColor();

        #region Operators
        public static bool operator ==(VertexColor x, VertexColor y) {
            return AreEquals(x, y);
        }
        public static bool operator !=(VertexColor x, VertexColor y) {
            return !AreEquals(x, y);
        }
        #endregion
        
        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            VertexColor other = (VertexColor)obj;
            return other != null && AreEquals(this, other);
        }
        public override int GetHashCode() {
            return guid.GetHashCode();
        }
        #endregion
        static bool AreEquals(VertexColor x, VertexColor y) {
            if(ReferenceEquals(x, null) || ReferenceEquals(y, null)) {
                return ReferenceEquals(x, null) && ReferenceEquals(y, null);
            }
            return x.guid.Equals(y.guid);
        }
    }

    internal class VertexTag {
        int nValue;
        VertexColor color;

        public VertexTag(int nValue, VertexColor color) {
            this.nValue = nValue;
            this.color = color;
        }
        internal VertexColor Color {
            get { return color; }
            set { color = value; }
        }
        public int NValue {
            get { return nValue; }
            set { nValue = value; }
        }
    }

    abstract class GraphDataBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        public GraphDataBase(int capacity) {
        }
        internal abstract void CreateEdge(TVertex vertex1, TVertex vertex2, double weight);
        internal abstract int GetSize();
        internal abstract void RegisterVertex(TVertex vertex);
        internal abstract bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2);
        internal abstract List<TVertex> GetAdjacentVertextList(TVertex vertex);
        internal abstract List<TVertex> GetVertexList();
        internal abstract TVertex GetVertex(int handle);
        internal abstract double GetWeight(TVertex vertex1, TVertex vertex2);
        internal abstract List<Edge<TValue, TVertex>> GetEdgeList();
    }

    public abstract class Graph<TValue, TVertex> where TVertex : Vertex<TValue> {
        readonly Guid id;
        GraphProperties properties;
        readonly GraphDataBase<TValue, TVertex> _data;

        public Graph(int capacity) {
            Guard.IsPositive(capacity, nameof(capacity));
            this.id = Guid.NewGuid();
            this.properties = GraphProperties.Unweighted;
            this._data = CreateDataCore(capacity);
        }
        public TVertex CreateVertex(TValue value) {
            TVertex vertex = CreateVertexCore(value);
            vertex.OwnerID = this.id;
            Data.RegisterVertex(vertex);
            return vertex;
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2) {
            CreateEdge(vertex1, vertex2, 1d);
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2, double weight) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            CreateEdgeCore(vertex1, vertex2, weight);
            UpdateProperties(weight: weight);
        }
        public double GetWeight(TVertex vertex1, TVertex vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            if(!AreVerticesAdjacent(vertex1, vertex2))
                throw new InvalidOperationException();
            return Data.GetWeight(vertex1, vertex2);
        }

        protected virtual void CreateEdgeCore(TVertex vertex1, TVertex vertex2, double weight) {
            Data.CreateEdge(vertex1, vertex2, weight);
        }

        public int Size {
            get { return Data.GetSize(); }
        }
        public ReadOnlyCollection<TVertex> GetVertexList() {
            var list = Data.GetVertexList();
            return new ReadOnlyCollection<TVertex>(list);
        }
        public bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            return Data.AreVerticesAdjacent(vertex1, vertex2);
        }
        public ReadOnlyCollection<TVertex> GetAdjacentVertextList(TVertex vertex) {
            Guard.IsNotNull(vertex, nameof(vertex));
            CheckVertexOwner(vertex);
            var list = Data.GetAdjacentVertextList(vertex);
            return new ReadOnlyCollection<TVertex>(list);
        }
        public ReadOnlyCollection<Edge<TValue, TVertex>> GetEdgeList() {
            var list = Data.GetEdgeList();
            return new ReadOnlyCollection<Edge<TValue, TVertex>>(list);
        }
        protected static readonly int DefaultCapacity = 4;

        public void DFSearch(Action<TVertex> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, x => { action(x); return true; }, DFSearchCore);
        }
        public void DFSearch(Func<TVertex, bool> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, action, DFSearchCore);
        }
        public void DFSearch(TVertex vertex, Action<TVertex> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch(vertex, x => { action(x); return true; }, DFSearchCore);
        }
        public void DFSearch(TVertex vertex, Func<TVertex, bool> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch(vertex, action, DFSearchCore);
        }

        public void BFSearch(Action<TVertex> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, x => { action(x); return true; }, BFSearchCore);
        }
        public void BFSearch(Func<TVertex, bool> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, action, BFSearchCore);
        }
        public void BFSearch(TVertex vertex, Action<TVertex> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch(vertex, x => { action(x); return true; }, BFSearchCore);
        }
        public void BFSearch(TVertex vertex, Func<TVertex, bool> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch(vertex, action, BFSearchCore);
        }
        public GraphProperties Properties { get { return properties; } }

        public DistanceObject<TValue, TVertex> GetShortestPathFrom(TVertex baseVertex) {
            Guard.IsNotNull(baseVertex, nameof(baseVertex));
            CheckVertexOwner(baseVertex);
            return PathAlgorithmFactory<TValue, TVertex>.Create(this).GetPath(baseVertex);
        }

        void DoSearch(TVertex vertex, Func<TVertex, bool> action, Func<TVertex, Func<TVertex, bool>, VertexColor, bool> searchProc) {
            if(Size == 0) return;
            int handle = vertex != null ? vertex.Handle : 0;
            VertexColor colorID = VertexColor.NewColor();
            for(int i = 0; i < Size; i++) {
                var graphVertex = GetVertex((handle + i) % Size);
                if(graphVertex.Tag.Color != colorID) {
                    if(!searchProc(graphVertex, action, colorID)) break;
                }
            }
        }

        bool DFSearchCore(TVertex vertex, Func<TVertex, bool> action, VertexColor colorID) {
            bool @continue = action(vertex);
            if(!@continue)
                return false;
            vertex.Tag.Color = colorID;
            var adjacentList = GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                if(adjacentList[i].Tag.Color != colorID) {
                    if(!DFSearchCore(adjacentList[i], action, colorID)) return false;
                }
            }
            return true;
        }
        bool BFSearchCore(TVertex vertex, Func<TVertex, bool> action, VertexColor colorID) {
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(vertex);
            vertex.Tag.Color = colorID;
            while(!queue.IsEmpty) {
                var graphVertex = queue.DeQueue();
                if(!action(graphVertex))
                    return false;
                var adjacentList = GetAdjacentVertextList(graphVertex);
                for(int i = 0; i < adjacentList.Count; i++) {
                    TVertex adjacentVertex = adjacentList[i];
                    if(adjacentVertex.Tag.Color != colorID) {
                        adjacentVertex.Tag.Color = colorID;
                        queue.EnQueue(adjacentVertex);
                    }
                }
            }
            return true;
        }

        protected internal TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return Data.GetVertex(handle);
        }

        void UpdateProperties(double? weight = null) {
            if(weight.HasValue) {
                if(!MathUtils.AreDoubleEquals(1d, weight.Value)) {
                    properties &= (~GraphProperties.Unweighted);
                    properties |= GraphProperties.Weighted;
                    if(weight.Value < 0)
                        properties |= GraphProperties.NegativeWeighted;
                }
            }
        }
        internal Guid ID { get { return id; } }

        void CheckVertexOwner(TVertex vertex) {
            if(!vertex.OwnerID.Equals(ID)) {
                throw new InvalidOperationException();
            }
        }

        internal GraphDataBase<TValue, TVertex> Data { get { return _data; } }

        #region Data & Vertex

        internal abstract GraphDataBase<TValue, TVertex> CreateDataCore(int capacity);
        internal abstract TVertex CreateVertexCore(TValue value);

        #endregion
    }

    public abstract class UndirectedGraph<TValue, TVertex> : Graph<TValue, TVertex> where TVertex : UndirectedVertex<TValue> {
        public UndirectedGraph(int capacity)
            : base(capacity) {
        }
        protected internal void DoBuildMSF(UndirectedGraph<TValue, TVertex> forest) {
            DisjointSet<TVertex> set = new DisjointSet<TVertex>();
            foreach(TVertex vertex in GetVertexList()) {
                set.MakeSet(vertex);
                forest.CreateVertex(vertex.Value);
            }
            var edgeList = Data.GetEdgeList();
            edgeList.Sort((x, y) => x.Weight.CompareTo(y.Weight));
            foreach(Edge<TValue, TVertex> edge in edgeList) {
                if(!set.AreEquivalent(edge.StartVertex, edge.EndVertex)) {
                    set.Union(edge.StartVertex, edge.EndVertex);
                    forest.CreateEdge(forest.GetVertex(edge.StartVertex.Handle), forest.GetVertex(edge.EndVertex.Handle), edge.Weight);
                }
            }
        }
        protected override void CreateEdgeCore(TVertex vertex1, TVertex vertex2, double weight) {
            base.CreateEdgeCore(vertex1, vertex2, weight);
            vertex1.Degree++;
            vertex2.Degree++;
        }
    }

    public abstract class DirectedGraph<TValue, TVertex> : Graph<TValue, TVertex> where TVertex : DirectedVertex<TValue> {
        public DirectedGraph(int capacity)
            : base(capacity) {
        }
        public void TopologicalSort(Action<TVertex> action) {
            Guard.IsNotNull(action, nameof(action));
            int vertexCount = 0;
            Queue<TVertex> queue = new Queue<TVertex>();
            foreach(var vertex in GetVertexList()) {
                if(vertex.InDegree == 0)
                    queue.EnQueue(vertex);
                vertex.Tag.NValue = vertex.InDegree;
            }
            while(!queue.IsEmpty) {
                TVertex vertex = queue.DeQueue();
                action(vertex);
                vertexCount++;
                var adjacentList = GetAdjacentVertextList(vertex);
                foreach(var adjacentVertex in adjacentList) {
                    if(--adjacentVertex.Tag.NValue == 0) queue.EnQueue(adjacentVertex);
                }
            }
            if(Size != vertexCount)
                throw new InvalidOperationException();
        }
        protected override void CreateEdgeCore(TVertex vertex1, TVertex vertex2, double weight) {
            base.CreateEdgeCore(vertex1, vertex2, weight);
            vertex1.OutDegree++;
            vertex2.InDegree++;
        }
    }
}
