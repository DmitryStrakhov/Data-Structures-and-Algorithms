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
        int valueCore;
        Color color;
        IVertexData data;

        internal Vertex(T value) {
            this.value = value;
            this.ownerID = null;
            this.handle = null;
            this.valueCore = 0;
            this.color = Color.Empty;
            this.data = null;
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
        internal int ValueCore {
            get { return valueCore; }
            set { valueCore = value; }
        }
        internal Color Color {
            get { return color; }
            set { color = value; }
        }
        internal IVertexData Data {
            get { return data; }
            set { data = value; }
        }
        internal TData GetData<TData>() where TData : IVertexData {
            return (TData)this.Data;
        }

        public T Value { get { return value; } }
    }

    internal interface IVertexData {
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

    interface IEdgeData {
    }

    [DebuggerDisplay("Initialized: {Initialized}, Weight: {Weight}, Color: {Color}, Data: {Data}")]
    struct EdgeData : ICloneable {
        readonly double weight;
        readonly bool initialized;
        Color color;
        IEdgeData data;

        public EdgeData(double weight)
            : this(weight, true, Color.Empty, null) {
        }
        internal EdgeData(double weight, bool initialized, Color color, IEdgeData data) {
            this.weight = weight;
            this.initialized = initialized;
            this.color = color;
            this.data = data;
        }

        public Color Color {
            get { return color; }
        }
        public IEdgeData Data {
            get { return data; }
        }
        public double Weight {
            get { return weight; }
        }
        public bool Initialized {
            get { return initialized; }
        }

        public EdgeData WithColor(Color color) {
            EdgeData result = Clone();
            result.color = color;
            return result;
        }
        public EdgeData WithData(IEdgeData data) {
            EdgeData result = Clone();
            result.data = data;
            return result;
        }

        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            EdgeData other = (EdgeData)obj;
            return Equals(this, other);
        }
        static bool Equals(EdgeData x, EdgeData y) {
            return x.Initialized == y.Initialized && MathUtils.AreDoubleEquals(x.Weight, y.Weight) && x.Color.Equals(y.Color) && ReferenceEquals(x.Data, y.Data);
        }
        public override int GetHashCode() {
            int hashCode = Initialized.GetHashCode() ^ Weight.GetHashCode() ^ Color.GetHashCode();
            if(Data != null)
                hashCode ^= Data.GetHashCode();
            return hashCode;
        }
        #endregion
        #region ICloneable
        object ICloneable.Clone() {
            return Clone();
        }
        EdgeData Clone() {
            return new EdgeData(Weight, Initialized, Color, Data);
        }
        #endregion
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

    internal struct Color {
        readonly Guid guid;

        Color(Guid guid) {
            this.guid = guid;
        }

        public static Color CreateColor() {
            return new Color(Guid.NewGuid());
        }
        public static readonly Color Empty = new Color(Guid.Empty);

        #region Operators
        public static bool operator ==(Color x, Color y) {
            return AreEquals(x, y);
        }
        public static bool operator !=(Color x, Color y) {
            return !AreEquals(x, y);
        }
        #endregion
        
        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            Color other = (Color)obj;
            return other != null && AreEquals(this, other);
        }
        public override int GetHashCode() {
            return guid.GetHashCode();
        }
        #endregion
        static bool AreEquals(Color x, Color y) {
            if(ReferenceEquals(x, null) || ReferenceEquals(y, null)) {
                return ReferenceEquals(x, null) && ReferenceEquals(y, null);
            }
            return x.guid.Equals(y.guid);
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
        internal abstract EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2);
        internal abstract void UpdateEdgeData(TVertex vertex1, TVertex vertex2, Func<EdgeData, EdgeData> updateFunc);
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
            if(!Data.AreVerticesAdjacent(vertex1, vertex2))
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
        public ReadOnlyCollection<TVertex> GetEulerianCircuit(TVertex vertex) {
            Guard.IsNotNull(vertex, nameof(vertex));
            CheckVertexOwner(vertex);
            var list = GetEulerianCircuitCore(vertex);
            return new ReadOnlyCollection<TVertex>(list);
        }
        internal EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            if(!Data.AreVerticesAdjacent(vertex1, vertex2))
                throw new InvalidOperationException();
            return Data.GetEdgeData(vertex1, vertex2);
        }
        internal void UpdateEdgeData(TVertex vertex1, TVertex vertex2, Func<EdgeData, EdgeData> updateFunc) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            Guard.IsNotNull(updateFunc, nameof(updateFunc));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            if(!Data.AreVerticesAdjacent(vertex1, vertex2))
                throw new InvalidOperationException();
            Data.UpdateEdgeData(vertex1, vertex2, updateFunc);
        }
        internal ReadOnlyCollection<TVertex> GetSimpleCircuitCore(TVertex vertex, Func<TVertex, TVertex, bool> acceptEdge) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(acceptEdge, nameof(acceptEdge));
            CheckVertexOwner(vertex);
            IList<TVertex> list = DoGetSimpleCircuit(vertex, acceptEdge);
            return new ReadOnlyCollection<TVertex>(list);
        }
        IList<TVertex> DoGetSimpleCircuit(TVertex vertex, Func<TVertex, TVertex, bool> acceptEdge) {
            return DoGetSimpleCircuit(vertex, Color.CreateColor(), acceptEdge);
        }
        IList<TVertex> DoGetSimpleCircuit(TVertex vertex, Color colorID, Func<TVertex, TVertex, bool> acceptEdge) {
            List<TVertex> list = new List<TVertex>();
            FillSimpleCircuit(list, vertex, colorID, acceptEdge);
            return list;
        }
        bool FillSimpleCircuit(IList<TVertex> list, TVertex vertex, Color colorID, Func<TVertex, TVertex, bool> acceptEdge) {
            if(!CanBePartOfEulerianCircuit(vertex)) throw new InvalidOperationException();
            bool exit = list.Count != 0 && ReferenceEquals(vertex, list.First());
            list.Add(vertex);
            if(exit) return true;
            IList<TVertex> adjacentList = Data.GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                TVertex adjVertex = adjacentList[i];
                if(Data.GetEdgeData(vertex, adjVertex).Color != colorID && acceptEdge(vertex, adjVertex)) {
                    Data.UpdateEdgeData(vertex, adjVertex, x => x.WithColor(colorID));
                    if(FillSimpleCircuit(list, adjVertex, colorID, acceptEdge)) return true;
                }
            }
            return false;
        }
        protected abstract bool CanBePartOfEulerianCircuit(TVertex vertex);

        void DoSearch(TVertex vertex, Func<TVertex, bool> action, Func<TVertex, Func<TVertex, bool>, Color, bool> searchProc) {
            if(Size == 0) return;
            int handle = vertex != null ? vertex.Handle : 0;
            Color colorID = Color.CreateColor();
            for(int i = 0; i < Size; i++) {
                var graphVertex = GetVertex((handle + i) % Size);
                if(graphVertex.Color != colorID) {
                    if(!searchProc(graphVertex, action, colorID)) break;
                }
            }
        }

        bool DFSearchCore(TVertex vertex, Func<TVertex, bool> action, Color colorID) {
            bool @continue = action(vertex);
            if(!@continue)
                return false;
            vertex.Color = colorID;
            var adjacentList = GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                if(adjacentList[i].Color != colorID) {
                    if(!DFSearchCore(adjacentList[i], action, colorID)) return false;
                }
            }
            return true;
        }
        bool BFSearchCore(TVertex vertex, Func<TVertex, bool> action, Color colorID) {
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(vertex);
            vertex.Color = colorID;
            while(!queue.IsEmpty) {
                var graphVertex = queue.DeQueue();
                if(!action(graphVertex))
                    return false;
                var adjacentList = GetAdjacentVertextList(graphVertex);
                for(int i = 0; i < adjacentList.Count; i++) {
                    TVertex adjacentVertex = adjacentList[i];
                    if(adjacentVertex.Color != colorID) {
                        adjacentVertex.Color = colorID;
                        queue.EnQueue(adjacentVertex);
                    }
                }
            }
            return true;
        }
        IList<TVertex> GetEulerianCircuitCore(TVertex vertex) {
            Color colorID = Color.CreateColor();
            List<TVertex> list = new List<TVertex>();
            list.Add(vertex);
            for(int i = 0; i < list.Count; i++) {
                TVertex vertexCore = list[i];
                var circuitList = DoGetSimpleCircuit(vertexCore, colorID, (x, y) => GetEdgeData(x, y).Color != colorID);
                if(circuitList.Count > 1) {
                    list.InsertRange(i + 1, circuitList.Skip(1));
                }
            }
            return list;
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
        public ReadOnlyCollection<TVertex> GetArticulationPointList() {
            if(Size == 0) return CollectionUtils.ReadOnly<TVertex>();
            List<TVertex> vertexList = new List<TVertex>();
            FillArticulationPointList(vertexList);
            return new ReadOnlyCollection<TVertex>(vertexList);
        }
        void FillArticulationPointList(List<TVertex> vertexList) {
            int dfsNum = 1;
            Color colorID = Color.CreateColor();
            for(int handle = 0; handle < Size; handle++) {
                var vertex = GetVertex(handle);
                if(vertex.Color != colorID) FillArticulationPointListCore(vertex, vertex, colorID, vertexList, ref dfsNum);
            }
        }
        void FillArticulationPointListCore(TVertex vertex, TVertex root, Color colorID, List<TVertex> vertexList, ref int dfsNum) {
            GetCutVertexListData data = new GetCutVertexListData(dfsNum++);
            vertex.Color = colorID;
            vertex.Data = data;
            var adjacentList = Data.GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                TVertex adjVertex = adjacentList[i];
                if(adjVertex.Color != colorID) {
                    data.ChildCount++;
                    FillArticulationPointListCore(adjVertex, root, colorID, vertexList, ref dfsNum);
                    if((ReferenceEquals(vertex, root) && data.ChildCount > 1) || (!ReferenceEquals(vertex, root) && adjVertex.GetData<GetCutVertexListData>().DfsLow >= data.DfsNum)) {
                        if(!data.IsInList)
                            vertexList.Add(vertex);
                        data.IsInList = true;
                    }
                    data.DfsLow = Math.Min(data.DfsLow, adjVertex.GetData<GetCutVertexListData>().DfsLow);
                }
                else {
                    data.DfsLow = Math.Min(data.DfsLow, adjVertex.GetData<GetCutVertexListData>().DfsNum);
                }
            }
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
        protected override bool CanBePartOfEulerianCircuit(TVertex vertex) {
            return (vertex.Degree & 0x1) == 0;
        }
        #region GetCutVertexListData
        [DebuggerDisplay("DfsNum = {DfsNum}, DfsLow = {DfsLow}, ChildCount = {ChildCount}, IsInList = {IsInList}")]
        class GetCutVertexListData : IVertexData {
            int dfsNum;
            int dfsLow;
            int childCount;
            bool isInList;

            public GetCutVertexListData(int num) {
                this.dfsNum = this.dfsLow = num;
                this.childCount = 0;
                this.isInList = false;
            }
            public int DfsNum { get { return dfsNum; } set { dfsNum = value; } }
            public int DfsLow { get { return dfsLow; } set { dfsLow = value; } }
            public int ChildCount { get { return childCount; } set { childCount = value; } }
            public bool IsInList { get { return isInList; } set { isInList = value; } }
        }
        #endregion
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
                vertex.ValueCore = vertex.InDegree;
            }
            while(!queue.IsEmpty) {
                TVertex vertex = queue.DeQueue();
                action(vertex);
                vertexCount++;
                var adjacentList = GetAdjacentVertextList(vertex);
                foreach(var adjacentVertex in adjacentList) {
                    if(--adjacentVertex.ValueCore == 0) queue.EnQueue(adjacentVertex);
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
        protected override bool CanBePartOfEulerianCircuit(TVertex vertex) {
            return vertex.InDegree == vertex.OutDegree;
        }
    }
}
