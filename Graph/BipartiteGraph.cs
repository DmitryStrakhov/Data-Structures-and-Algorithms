using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("BipartiteGraphVertex ({Value})")]
    public class BipartiteGraphVertex<T> : UndirectedVertex<T> {
        Guid? partitionID;

        internal BipartiteGraphVertex(T value) : base(value) {
            this.partitionID = null;
        }
        internal Guid PartitionID {
            get { return partitionID.Value; }
            set {
                if(partitionID.HasValue) {
                    throw new InvalidOperationException();
                }
                partitionID = value;
            }
        }
    }

    public class BipartiteGraphPartition<TValue, TVertex> where TVertex : BipartiteGraphVertex<TValue> {
        readonly Guid id;
        readonly Func<TValue, TVertex> createVertex;
        readonly Func<IList<TVertex>> getVertexList;
        readonly Func<int> getSize;

        internal BipartiteGraphPartition(Func<TValue, TVertex> createVertex, Func<IList<TVertex>> getVertexList, Func<int> getSize) {
            this.createVertex = createVertex;
            this.getVertexList = getVertexList;
            this.getSize = getSize;
            this.id = Guid.NewGuid();
        }

        public TVertex CreateVertex(TValue value) {
            TVertex vertex = createVertex(value);
            vertex.PartitionID = ID;
            return vertex;
        }
        public int Size {
            get { return getSize(); }
        }
        public ReadOnlyCollection<TVertex> GetVertexList() {
            IList<TVertex> list = getVertexList();
            return new ReadOnlyCollection<TVertex>(list);
        }
        internal Guid ID { get { return id; } }
    }


    abstract class BiAdjMatrixGraphDataBase<TValue, TVertex> : BipartiteGraphDataBase<TValue, TVertex> where TVertex : BipartiteGraphVertex<TValue> {
        int uSize;
        int vSize;
        int uCapacity;
        int vCapacity;
        TVertex[] uVertexList;
        TVertex[] vVertexList;
        readonly RectMatrix<EdgeData> matrix;

        public BiAdjMatrixGraphDataBase(int capacity) : base(capacity) {
            this.uSize = this.vSize = 0;
            this.uCapacity = this.vCapacity = capacity;
            this.uVertexList = new TVertex[capacity];
            this.vVertexList = new TVertex[capacity];
            this.matrix = new RectMatrix<EdgeData>(capacity, capacity);
        }
        public int USize { get { return uSize; } }
        public int VSize { get { return vSize; } }
        public int UCapacity { get { return uCapacity; } }
        public int VCapacity { get { return vCapacity; } }

        internal override void RegisterVertex(TVertex vertex) {
            throw new NotImplementedException();
        }
        internal override void RegisterUVertex(TVertex vertex) {
            int handle = USize;
            int newSize = ++this.uSize;
            EnsureVertexListSize(ref this.uVertexList, ref uCapacity, newSize);
            Matrix.EnsureSize(VSize, USize);
            UVertexList[handle] = vertex;
            vertex.Handle = handle;
        }
        internal override void RegisterVVertex(TVertex vertex) {
            int handle = VSize;
            int newSize = ++this.vSize;
            EnsureVertexListSize(ref this.vVertexList, ref vCapacity, newSize);
            Matrix.EnsureSize(VSize, USize);
            VVertexList[handle] = vertex;
            vertex.Handle = handle;
        }

        internal override List<TVertex> GetVertexList() {
            List<TVertex> list = new List<TVertex>(USize + VSize);
            list.AddRange(UVertexList.Take(USize));
            list.AddRange(VVertexList.Take(VSize));
            return list;
        }
        internal override List<TVertex> GetUVertexList() {
            List<TVertex> list = new List<TVertex>(USize);
            list.AddRange(UVertexList.Take(USize));
            return list;
        }
        internal override List<TVertex> GetVVertexList() {
            List<TVertex> list = new List<TVertex>(VSize);
            list.AddRange(VVertexList.Take(VSize));
            return list;
        }
        internal override TVertex GetUVertex(int handle) {
            Guard.IsInRange(handle, 0, USize - 1, nameof(handle));
            return UVertexList[handle];
        }
        internal override TVertex GetVVertex(int handle) {
            Guard.IsInRange(handle, 0, VSize - 1, nameof(handle));
            return VVertexList[handle];
        }
        internal override List<TVertex> GetAdjacentVertextList(TVertex vertex) {
            if(IsUVertex(vertex))
                return GetVAdjacentVertextList(vertex);
            if(IsVVertex(vertex))
                return GetUAdjacentVertextList(vertex);
            throw new InvalidOperationException();
        }

        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            if(AreInTheSamePartition(vertex1, vertex2))
                return false;
            return GetEdgeDataCore(vertex1, vertex2).Initialized;
        }
        internal override int GetSize() {
            return USize + VSize;
        }
        internal override int GetUSize() {
            return USize;
        }
        internal override int GetVSize() {
            return VSize;
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, USize + VSize - 1, nameof(handle));
            if(handle < USize)
                return UVertexList[handle];
            return VVertexList[handle - USize];
        }
        internal override double GetWeight(TVertex vertex1, TVertex vertex2) {
            return GetEdgeDataCore(vertex1, vertex2).Weight;
        }
        internal override EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2) {
            return GetEdgeDataCore(vertex1, vertex2);
        }
        internal override List<Edge<TValue, TVertex>> GetEdgeList() {
            List<Edge<TValue, TVertex>> list = new List<Edge<TValue, TVertex>>();
            for(int row = 0; row < VSize; row++) {
                for(int column = 0; column < USize; column++) {
                    if(Matrix[row, column].Initialized)
                        list.Add(new Edge<TValue, TVertex>(UVertexList[column], VVertexList[row], Matrix[row, column].Weight));
                }
            }
            return list;
        }
        protected EdgeData GetEdgeDataCore(TVertex vertex1, TVertex vertex2) {
            TVertex uVertex = GetUVertex(vertex1, vertex2);
            TVertex vVertex = GetVVertex(vertex1, vertex2);
            return Matrix[vVertex.Handle, uVertex.Handle];
        }

        bool AreInTheSamePartition(TVertex vertex1, TVertex vertex2) {
            return vertex1.PartitionID.Equals(vertex2.PartitionID);
        }
        bool IsUVertex(TVertex vertex) {
            return USize != 0 && AreInTheSamePartition(UVertexList.First(), vertex);
        }
        bool IsVVertex(TVertex vertex) {
            return VSize != 0 && AreInTheSamePartition(VVertexList.First(), vertex);
        }
        protected TVertex GetUVertex(TVertex vertex1, TVertex vertex2) {
            if(IsUVertex(vertex1))
                return vertex1;
            if(IsUVertex(vertex2))
                return vertex2;
            throw new InvalidOperationException();
        }
        protected TVertex GetVVertex(TVertex vertex1, TVertex vertex2) {
            if(IsVVertex(vertex1))
                return vertex1;
            if(IsVVertex(vertex2))
                return vertex2;
            throw new InvalidOperationException();
        }
        internal List<TVertex> GetUAdjacentVertextList(TVertex vVertex) {
            if(!IsVVertex(vVertex))
                throw new InvalidOperationException();
            List<TVertex> list = new List<TVertex>();
            for(int n = 0; n < USize; n++) {
                if(Matrix[vVertex.Handle, n].Initialized) list.Add(UVertexList[n]);
            }
            return list;
        }
        internal List<TVertex> GetVAdjacentVertextList(TVertex uVertex) {
            if(!IsUVertex(uVertex))
                throw new InvalidOperationException();
            List<TVertex> list = new List<TVertex>();
            for(int n = 0; n < VSize; n++) {
                if(Matrix[n, uVertex.Handle].Initialized) list.Add(VVertexList[n]);
            }
            return list;
        }
        static void EnsureVertexListSize(ref TVertex[] vertexList, ref int capacity, int newSize) {
            if(newSize > capacity) {
                int _capacity = capacity * 2;
                if(newSize > _capacity)
                    _capacity = newSize * 2;
                TVertex[] _list = new TVertex[_capacity];
                Array.Copy(vertexList, _list, vertexList.Length);
                vertexList = _list;
                capacity = _capacity;
            }
        }
        internal int[,] GetMatrixData() {
            var size = Matrix.Size;
            int[,] result = new int[size.RowCount, size.ColumnCount];
            for(int row = 0; row < size.RowCount; row++) {
                for(int column = 0; column < size.ColumnCount; column++) {
                    result[row, column] = Matrix[row, column].Initialized ? 1 : 0;
                }
            }
            return result;
        }
        public TVertex[] UVertexList { get { return uVertexList; } }
        public TVertex[] VVertexList { get { return vVertexList; } }
        public RectMatrix<EdgeData> Matrix { get { return matrix; } }
    }

    class UndirectedBiAdjMatrixGraphData<T> : BiAdjMatrixGraphDataBase<T, BipartiteGraphVertex<T>> {
        public UndirectedBiAdjMatrixGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2, double weight) {
            var uVertex = GetUVertex(vertex1, vertex2);
            var vVertex = GetVVertex(vertex1, vertex2);
            Matrix[vVertex.Handle, uVertex.Handle] = new EdgeData(weight);
        }
        internal override void UpdateEdgeData(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            var uVertex = GetUVertex(vertex1, vertex2);
            var vVertex = GetVVertex(vertex1, vertex2);
            EdgeData edgeData = updateFunc(GetEdgeDataCore(vertex1, vertex2));
            Matrix[vVertex.Handle, uVertex.Handle] = edgeData;
        }
    }

    public class BipartiteGraph<T> : UndirectedGraph<T, BipartiteGraphVertex<T>> {
        BipartiteGraphPartition<T, BipartiteGraphVertex<T>> uPartition;
        BipartiteGraphPartition<T, BipartiteGraphVertex<T>> vPartition;

        public BipartiteGraph()
            : this(DefaultCapacity) {
        }
        public BipartiteGraph(int capacity) : base(capacity) {
            this.uPartition = new BipartiteGraphPartition<T, BipartiteGraphVertex<T>>(value => CreateVertex(value, Data.RegisterUVertex), Data.GetUVertexList, Data.GetUSize);
            this.vPartition = new BipartiteGraphPartition<T, BipartiteGraphVertex<T>>(value => CreateVertex(value, Data.RegisterVVertex), Data.GetVVertexList, Data.GetVSize);
        }

        #region To hide something
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override BipartiteGraphVertex<T> CreateVertex(T value) {
            throw new NotImplementedException("Use 'U' and 'V' partitions instead.");
        }
        #endregion

        public BipartiteGraph<T> BuildMSF() {
            return DoBuildMSF(new BipartiteGraph<T>());
        }
        new TGraph DoBuildMSF<TGraph>(TGraph forest) where TGraph : BipartiteGraph<T> {
            DisjointSet<BipartiteGraphVertex<T>> set = new DisjointSet<BipartiteGraphVertex<T>>();
            foreach(var vertex in Data.GetUVertexList()) {
                set.MakeSet(vertex);
                forest.U.CreateVertex(vertex.Value);
            }
            foreach(var vertex in Data.GetVVertexList()) {
                set.MakeSet(vertex);
                forest.V.CreateVertex(vertex.Value);
            }
            var edgeList = Data.GetEdgeList();
            edgeList.Sort((x, y) => x.Weight.CompareTo(y.Weight));
            foreach(var edge in edgeList) {
                if(!set.AreEquivalent(edge.StartVertex, edge.EndVertex)) {
                    set.Union(edge.StartVertex, edge.EndVertex);
                    forest.CreateEdge(forest.Data.GetUVertex(edge.StartVertex.Handle), forest.Data.GetVVertex(edge.EndVertex.Handle), edge.Weight);
                }
            }
            return forest;
        }

        public BipartiteGraphPartition<T, BipartiteGraphVertex<T>> U { get { return uPartition; } }
        public BipartiteGraphPartition<T, BipartiteGraphVertex<T>> V { get { return vPartition; } }

        internal override GraphDataBase<T, BipartiteGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedBiAdjMatrixGraphData<T>(capacity);
        }
        internal override BipartiteGraphVertex<T> CreateVertexCore(T value) {
            return new BipartiteGraphVertex<T>(value);
        }
        internal new UndirectedBiAdjMatrixGraphData<T> Data { get { return (UndirectedBiAdjMatrixGraphData<T>)base.Data; } }
    }
}
