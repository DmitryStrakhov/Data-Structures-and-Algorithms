﻿using System;
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
            if(!IsDataOfType<TData>()) return default(TData);
            return (TData)this.Data;
        }
        internal bool IsDataOfType<TData>() where TData : IVertexData {
            return Data != null && Data.GetType() == typeof(TData);
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
        double weight;
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

        public bool IsDefault {
            get { return MathUtils.AreEqual(1d, Weight); }
        }
        public bool IsWeighed {
            get { return !IsDefault && Weight >= 0; }
        }
        public bool IsNegativeWeighed {
            get { return !IsDefault && Weight < 0; }
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
        public EdgeData WithWeight(double weight) {
            EdgeData result = Clone();
            result.weight = weight;
            return result;
        }
        internal bool IsDataOfType<TData>() where TData : IEdgeData {
            return Data != null && Data.GetType() == typeof(TData);
        }
        internal static readonly EdgeData Empty = default(EdgeData);

        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            EdgeData other = (EdgeData)obj;
            return Equals(this, other);
        }
        static bool Equals(EdgeData x, EdgeData y) {
            return x.Initialized == y.Initialized && MathUtils.AreEqual(x.Weight, y.Weight) && x.Color.Equals(y.Color) && ReferenceEquals(x.Data, y.Data);
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
            return other != null && AreEqual(this, other);
        }
        public override int GetHashCode() {
            return StartVertex.GetHashCode() ^ EndVertex.GetHashCode() ^ Weight.GetHashCode();
        }
        #endregion
        static bool AreEqual(Edge<TValue, TVertex> x, Edge<TValue, TVertex> y) {
            return ReferenceEquals(x.StartVertex, y.StartVertex) && ReferenceEquals(x.EndVertex, y.EndVertex) && MathUtils.AreEqual(x.Weight, y.Weight);
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
            return other != null && AreEqual(this, other);
        }
        public override int GetHashCode() {
            return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Weight.GetHashCode();
        }
        #endregion
        static bool AreEqual(EdgeTriplet<T> x, EdgeTriplet<T> y) {
            return EqualityComparer<T>.Default.Equals(x.Value1, y.Value1) && EqualityComparer<T>.Default.Equals(x.Value2, y.Value2) && MathUtils.AreEqual(x.Weight, y.Weight);
        }

        public T Value1 { get { return value1; } }
        public T Value2 { get { return value2; } }
        public double Weight { get { return weight; } }
    }


    public class GraphProperties {
        EdgeProperties edgeProperties;

        internal GraphProperties() {
            this.edgeProperties = new EdgeProperties();
        }
        internal void OnEdgeAdded(EdgeData edgeData) {
            EdgeProperties.Count++;
            if(edgeData.IsWeighed)
                EdgeProperties.WCount++;
            if(edgeData.IsNegativeWeighed)
                EdgeProperties.NCount++;
        }
        internal void OnEdgeDeleted(EdgeData edgeData) {
            EdgeProperties.Count--;
            if(edgeData.IsWeighed)
                EdgeProperties.WCount--;
            if(edgeData.IsNegativeWeighed)
                EdgeProperties.NCount--;
        }
        public bool IsUnweighted {
            get { return EdgeProperties.WCount == 0 && EdgeProperties.NCount == 0; }
        }
        public bool IsWeighted {
            get { return EdgeProperties.WCount != 0 && EdgeProperties.NCount == 0; }
        }
        public bool IsNegativeWeighted {
            get { return EdgeProperties.NCount != 0; }
        }
        public int EdgeCount {
            get { return EdgeProperties.Count; }
        }
        internal EdgeProperties EdgeProperties { get { return edgeProperties; } }
    }

    #region EdgeProperties

    class EdgeProperties {
        int count;
        int wCount;
        int nCount;

        public EdgeProperties() {
            this.count = this.wCount = this.nCount = 0;
        }
        public int Count {
            get { return count; }
            set { count = value; }
        }
        public int WCount {
            get { return wCount; }
            set { wCount = value; }
        }
        public int NCount {
            get { return nCount; }
            set { nCount = value; }
        }
    }
    #endregion

    [DebuggerDisplay("Color ({guid})")]
    public struct Color {
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
            return AreEqual(x, y);
        }
        public static bool operator !=(Color x, Color y) {
            return !AreEqual(x, y);
        }
        #endregion

        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            Color other = (Color)obj;
            return other != null && AreEqual(this, other);
        }
        public override int GetHashCode() {
            return guid.GetHashCode();
        }
        #endregion
        static bool AreEqual(Color x, Color y) {
            if(ReferenceEquals(x, null) || ReferenceEquals(y, null)) {
                return ReferenceEquals(x, null) && ReferenceEquals(y, null);
            }
            return x.guid.Equals(y.guid);
        }
    }

    public interface IVertexRelation<TValue, TVertex> where TVertex : Vertex<TValue> {
        bool AreStronglyConnected(TVertex vertex1, TVertex vertex2);
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

    abstract class BipartiteGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : BipartiteGraphVertex<TValue> {
        public BipartiteGraphDataBase(int capacity)
            : base(capacity) {
        }
        internal abstract void RegisterUVertex(TVertex vertex);
        internal abstract void RegisterVVertex(TVertex vertex);
        internal abstract List<TVertex> GetUVertexList();
        internal abstract List<TVertex> GetVVertexList();
        internal abstract int GetUSize();
        internal abstract int GetVSize();
        internal abstract TVertex GetUVertex(int handle);
        internal abstract TVertex GetVVertex(int handle);
        internal abstract TVertex GetUVertex(TVertex vertex1, TVertex vertex2);
        internal abstract TVertex GetVVertex(TVertex vertex1, TVertex vertex2);
        internal abstract bool IsUVertex(TVertex vertex);
        internal abstract bool IsVVertex(TVertex vertex);
        internal abstract void DeleteEdge(TVertex vertex1, TVertex vertex2);
    }

    public abstract class Graph<TValue, TVertex> where TVertex : Vertex<TValue> {
        readonly Guid id;
        GraphProperties properties;
        readonly GraphDataBase<TValue, TVertex> _data;

        public Graph(int capacity) {
            Guard.IsPositive(capacity, nameof(capacity));
            this.id = Guid.NewGuid();
            this.properties = new GraphProperties();
            this._data = CreateDataCore(capacity);
        }
        public virtual TVertex CreateVertex(TValue value) {
            return CreateVertex(value, Data.RegisterVertex);
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2) {
            CreateEdge(vertex1, vertex2, 1d);
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2, double weight) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            if(AllowEdge(vertex1, vertex2)) {
                CreateEdgeCore(vertex1, vertex2, weight);
                Properties.OnEdgeAdded(Data.GetEdgeData(vertex1, vertex2));
            }
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

        protected virtual bool AllowEdge(TVertex vertex1, TVertex vertex2) {
            return true;
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
            DoSearch<Func<TVertex, bool>>(null, x => { action(x); return true; }, DFSearchCore);
        }
        public void DFSearch(Func<TVertex, bool> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, action, DFSearchCore);
        }
        public void DFSearch(TVertex vertex, Action<TVertex> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch<Func<TVertex, bool>>(vertex, x => { action(x); return true; }, DFSearchCore);
        }
        public void DFSearch(TVertex vertex, Func<TVertex, bool> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch(vertex, action, DFSearchCore);
        }

        public void BFSearch(Action<TVertex> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch<Func<TVertex, bool>>(null, x => { action(x); return true; }, BFSearchCore);
        }
        public void BFSearch(Func<TVertex, bool> action) {
            Guard.IsNotNull(action, nameof(action));
            DoSearch(null, action, BFSearchCore);
        }
        public void BFSearch(TVertex vertex, Action<TVertex> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            DoSearch<Func<TVertex, bool>>(vertex, x => { action(x); return true; }, BFSearchCore);
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
        public bool ContainsCycle() {
            if(Size == 0) return false;
            Color colorID = Color.CreateColor();
            for(int n = 0; n < Size; n++) {
                var vertex = GetVertex(n);
                if(vertex.Color != colorID) {
                    if(ContainsCycle(vertex, colorID)) return true;
                }
            }
            return false;
        }
        protected abstract bool ContainsCycle(TVertex vertex, Color colorID);

        internal void ClearData(bool vertexData, bool edgeData) {
            if(vertexData) {
                Data.GetVertexList().ForEach(x => x.Data = null);
            }
            if(edgeData) {
                Data.GetEdgeList().ForEach(x => Data.UpdateEdgeData(x.StartVertex, x.EndVertex, edge => edge.WithData(null)));
            }
        }
        protected TVertex CreateVertex(TValue value, Action<TVertex> dataRegisterAction) {
            TVertex vertex = CreateVertexCore(value);
            vertex.OwnerID = this.id;
            dataRegisterAction(vertex);
            return vertex;
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

        protected void DoSearch<T>(TVertex vertex, T data, Func<TVertex, Color, T, bool> searchProc) {
            if(Size == 0) return;
            int handle = vertex != null ? vertex.Handle : 0;
            Color colorID = Color.CreateColor();
            for(int i = 0; i < Size; i++) {
                var graphVertex = GetVertex((handle + i) % Size);
                if(graphVertex.Color != colorID) {
                    if(!searchProc(graphVertex, colorID, data)) break;
                }
            }
        }

        protected internal bool DoDFSearch(TVertex vertex, Func<TVertex, TVertex, bool> acceptEdge, Action<TVertex, TVertex> edgeAction, Func<TVertex, bool> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(acceptEdge, nameof(acceptEdge));
            Guard.IsNotNull(edgeAction, nameof(edgeAction));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            return DFSearchCore(vertex, Color.CreateColor(), acceptEdge, edgeAction, action);
        }
        protected internal bool DoBFSearch(TVertex vertex, Func<TVertex, TVertex, bool> acceptEdge, Action<TVertex, TVertex> edgeAction, Func<TVertex, bool> action) {
            Guard.IsNotNull(vertex, nameof(vertex));
            Guard.IsNotNull(acceptEdge, nameof(acceptEdge));
            Guard.IsNotNull(edgeAction, nameof(edgeAction));
            Guard.IsNotNull(action, nameof(action));
            CheckVertexOwner(vertex);
            return BFSearchCore(vertex, Color.CreateColor(), acceptEdge, edgeAction, action);
        }

        bool DFSearchCore(TVertex vertex, Color colorID, Func<TVertex, bool> action) {
            return DFSearchCore(vertex, colorID, (x, y) => true, (x, y) => { }, action);
        }
        bool DFSearchCore(TVertex vertex, Color colorID, Func<TVertex, TVertex, bool> acceptEdge, Action<TVertex, TVertex> edgeAction, Func<TVertex, bool> action) {
            bool @continue = action(vertex);
            if(!@continue)
                return false;
            vertex.Color = colorID;
            var adjacentList = GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                if(adjacentList[i].Color != colorID && acceptEdge(vertex, adjacentList[i])) {
                    edgeAction(vertex, adjacentList[i]);
                    if(!DFSearchCore(adjacentList[i], colorID, acceptEdge, edgeAction, action)) return false;
                }
            }
            return true;
        }
        bool BFSearchCore(TVertex vertex, Color colorID, Func<TVertex, bool> action) {
            return BFSearchCore(vertex, colorID, (x, y) => true, (x, y) => { }, action);
        }
        bool BFSearchCore(TVertex vertex, Color colorID, Func<TVertex, TVertex, bool> acceptEdge, Action<TVertex, TVertex> edgeAction, Func<TVertex, bool> action) {
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(vertex);
            vertex.Color = colorID;
            while(!queue.IsEmpty) {
                var graphVertex = queue.DeQueue();
                if(!action(graphVertex)) {
                    return false;
                }
                var adjacentList = GetAdjacentVertextList(graphVertex);
                for(int i = 0; i < adjacentList.Count; i++) {
                    TVertex adjacentVertex = adjacentList[i];
                    if(adjacentVertex.Color != colorID && acceptEdge(graphVertex, adjacentVertex)) {
                        edgeAction(graphVertex, adjacentVertex);
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
        internal Guid ID { get { return id; } }

        protected void CheckVertexOwner(TVertex vertex) {
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
        protected internal TGraph DoBuildMSF<TGraph>(TGraph forest) where TGraph : UndirectedGraph<TValue, TVertex> {
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
            return forest;
        }
        protected override void CreateEdgeCore(TVertex vertex1, TVertex vertex2, double weight) {
            base.CreateEdgeCore(vertex1, vertex2, weight);
            vertex1.Degree++;
            vertex2.Degree++;
        }
        protected override bool CanBePartOfEulerianCircuit(TVertex vertex) {
            return (vertex.Degree & 0x1) == 0;
        }
        protected override bool ContainsCycle(TVertex vertex, Color colorID) {
            return ContainsCycleCore(vertex, null, colorID);
        }
        bool ContainsCycleCore(TVertex vertex, TVertex ancestor, Color colorID) {
            vertex.Color = colorID;
            List<TVertex> adjacentList = Data.GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                TVertex adjVertex = adjacentList[i];
                if(!ReferenceEquals(adjVertex, ancestor)) {
                    if(adjVertex.Color == colorID || ContainsCycleCore(adjVertex, vertex, colorID)) return true;
                }
            }
            return false;
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
        public IVertexRelation<TValue, TVertex> GetVertexRelationData() {
            DisjointSet<TVertex> vertexDisjointSet = new DisjointSet<TVertex>();
            FillVertexRelationDataCore(vertexDisjointSet);
            return new VertexRelationDataObj(this, vertexDisjointSet);
        }
        protected internal void FillTransposeGraph(DirectedGraph<TValue, TVertex> graph) {
            foreach(TVertex vertex in Data.GetVertexList()) {
                graph.CreateVertex(vertex.Value);
            }
            foreach(var edge in Data.GetEdgeList()) {
                graph.CreateEdge(graph.GetVertex(edge.EndVertex.Handle), graph.GetVertex(edge.StartVertex.Handle), edge.Weight);
            }
        }
        protected override void CreateEdgeCore(TVertex vertex1, TVertex vertex2, double weight) {
            base.CreateEdgeCore(vertex1, vertex2, weight);
            vertex1.OutDegree++;
            vertex2.InDegree++;
        }
        protected override bool CanBePartOfEulerianCircuit(TVertex vertex) {
            return vertex.InDegree == vertex.OutDegree;
        }
        protected override bool ContainsCycle(TVertex vertex, Color colorID) {
            return ContainsCycleCore(vertex, colorID);
        }
        bool ContainsCycleCore(TVertex vertex, Color colorID) {
            vertex.Color = colorID;
            vertex.Data = ContainsCycleData.Default;
            List<TVertex> adjacentList = Data.GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                TVertex adjVertex = adjacentList[i];
                if((adjVertex.Color == colorID && adjVertex.IsDataOfType<ContainsCycleData>()) || ContainsCycleCore(adjVertex, colorID)) {
                    vertex.Data = null;
                    return true;
                }
            }
            vertex.Data = null;
            return false;
        }

        void FillVertexRelationDataCore(DisjointSet<TVertex> set) {
            if(Size == 0) return;
            Stack<TVertex> stack = new Stack<TVertex>();
            DoSearch(GetVertex(0), new FillVertexRelationDataContext(stack, set), FillVertexRelationDataCore);
        }
        bool FillVertexRelationDataCore(TVertex vertex, Color colorID, FillVertexRelationDataContext context) {
            vertex.Color = colorID;
            context.Stack.Push(vertex);
            FillVertexRelationData _data = new FillVertexRelationData(context.DfsNum++, true);
            vertex.Data = _data;
            var adjacentList = GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                TVertex adjVertex = adjacentList[i];
                if(adjVertex.Color != colorID) {
                    FillVertexRelationDataCore(adjVertex, colorID, context);
                    _data.LowNum = Math.Min(_data.LowNum, adjVertex.GetData<FillVertexRelationData>().LowNum);
                }
                else if(adjVertex.GetData<FillVertexRelationData>().IsInStack) {
                    _data.LowNum = Math.Min(_data.LowNum, adjVertex.GetData<FillVertexRelationData>().DfsNum);
                }
            }
            if(_data.DfsNum == _data.LowNum) {
                context.Set.MakeSet(vertex);
                while(true) {
                    TVertex vertexOnStack = context.Stack.Pop();
                    vertexOnStack.GetData<FillVertexRelationData>().IsInStack = false;
                    if(ReferenceEquals(vertexOnStack, vertex))
                        break;
                    context.Set.MakeSet(vertexOnStack);
                    context.Set.Union(vertex, vertexOnStack);
                }
            }
            return true;
        }

        #region ContainsCycleData
        class ContainsCycleData : IVertexData {
            public ContainsCycleData() {
            }
            public static readonly ContainsCycleData Default = new ContainsCycleData();
        }
        #endregion

        #region FillVertexRelationData
        [DebuggerDisplay("DfsNum = {DfsNum}, LowNum = {LowNum}, IsInStack = {IsInStack}")]
        class FillVertexRelationData : IVertexData {
            int dfsNum;
            int lowNum;
            bool isInStack;

            public FillVertexRelationData(int dfsNum, bool isInStack) {
                this.dfsNum = this.lowNum = dfsNum;
                this.isInStack = isInStack;
            }
            public int DfsNum { get { return dfsNum; } set { dfsNum = value; } }
            public int LowNum { get { return lowNum; } set { lowNum = value; } }
            public bool IsInStack { get { return isInStack; } set { isInStack = value; } }
        }
        #endregion

        #region FillVertexRelationDataContext
        class FillVertexRelationDataContext {
            readonly Stack<TVertex> stack;
            readonly DisjointSet<TVertex> set;
            int dfsNumber;

            public FillVertexRelationDataContext(Stack<TVertex> stack, DisjointSet<TVertex> set) {
                this.stack = stack;
                this.set = set;
                this.dfsNumber = 1;
            }
            public Stack<TVertex> Stack { get { return stack; } }
            public DisjointSet<TVertex> Set { get { return set; } }
            public int DfsNum { get { return dfsNumber; } set { dfsNumber = value; } }
        }
        #endregion

        #region VertexRelationDataObj
        class VertexRelationDataObj : IVertexRelation<TValue, TVertex> {
            readonly DirectedGraph<TValue, TVertex> graph;
            readonly DisjointSet<TVertex> vertexDisjointSet;

            public VertexRelationDataObj(DirectedGraph<TValue, TVertex> graph, DisjointSet<TVertex> vertexDisjointSet) {
                this.graph = graph;
                this.vertexDisjointSet = vertexDisjointSet;
            }
            public bool AreStronglyConnected(TVertex vertex1, TVertex vertex2) {
                Guard.IsNotNull(vertex1, nameof(vertex1));
                Guard.IsNotNull(vertex2, nameof(vertex2));
                this.graph.CheckVertexOwner(vertex1);
                this.graph.CheckVertexOwner(vertex2);
                return this.vertexDisjointSet.AreEquivalent(vertex1, vertex2);
            }
        }
        #endregion
    }
}
