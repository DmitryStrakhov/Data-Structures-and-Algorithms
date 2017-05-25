using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjMatrixGraphVertex ({Value})")]
    public class AdjMatrixGraphVertex<T> : UndirectedVertex<T> {
        internal AdjMatrixGraphVertex(T value)
            : base(value) {
        }
    }

    [DebuggerDisplay("DirectedAdjMatrixGraphVertex ({Value})")]
    public class DirectedAdjMatrixGraphVertex<T> : DirectedVertex<T> {
        internal DirectedAdjMatrixGraphVertex(T value)
            : base(value) {
        }
    }


    abstract class AdjMatrixGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        int size;
        int capacity;
        TVertex[] vertexList;
        readonly SquareMatrix<EdgeData> matrix;

        public AdjMatrixGraphDataBase(int capacity) : base(capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.vertexList = new TVertex[capacity];
            this.matrix = new SquareMatrix<EdgeData>(capacity);
        }

        public int Size { get { return size; } }
        public int Capacity { get { return capacity; } }

        internal override void RegisterVertex(TVertex vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureVertexListSize(newSize);
            Matrix.EnsureSize(newSize);
            VertexList[handle] = vertex;
            vertex.Handle = handle;
        }
        internal override List<TVertex> GetVertexList() {
            return VertexList.Take(Size).ToList();
        }
        internal override List<TVertex> GetAdjacentVertextList(TVertex vertex) {
            List<TVertex> list = new List<TVertex>();
            for(int n = 0; n < Size; n++) {
                if(Matrix[vertex.Handle, n].Initialized) list.Add(VertexList[n]);
            }
            return list;
        }
        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            return Matrix[vertex1.Handle, vertex2.Handle].Initialized;
        }
        internal override int GetSize() {
            return Size;
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return VertexList[handle];
        }
        internal override double GetWeight(TVertex vertex1, TVertex vertex2) {
            return GetEdgeDataCore(vertex1, vertex2).Weight;
        }
        internal override EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2) {
            return GetEdgeDataCore(vertex1, vertex2);
        }
        internal override List<Edge<TValue, TVertex>> GetEdgeList() {
            List<Edge<TValue, TVertex>> list = new List<Edge<TValue, TVertex>>();
            for(int row = 0; row < Size; row++) {
                for(int column = GetColumnByRow(row); column < Size; column++) {
                    if(Matrix[row, column].Initialized)
                        list.Add(new Edge<TValue, TVertex>(VertexList[row], VertexList[column], Matrix[row, column].Weight));
                }
            }
            return list;
        }
        protected abstract int GetColumnByRow(int row);

        protected EdgeData GetEdgeDataCore(TVertex vertex1, TVertex vertex2) {
            return Matrix[vertex1.Handle, vertex2.Handle];
        }
        void EnsureVertexListSize(int newSize) {
            if(newSize > this.capacity) {
                int _capacity = this.capacity * 2;
                if(newSize > _capacity)
                    _capacity = newSize * 2;
                TVertex[] _list = new TVertex[_capacity];
                Array.Copy(VertexList, _list, VertexList.Length);
                this.vertexList = _list;
                this.capacity = _capacity;
            }
        }

        internal int[,] GetMatrixData() {
            int sz = Matrix.Size;
            int[,] result = new int[sz, sz];
            for(int row = 0; row < sz; row++) {
                for(int column = 0; column < sz; column++) {
                    result[row, column] = Matrix[row, column].Initialized ? 1 : 0;
                }
            }
            return result;
        }

        public SquareMatrix<EdgeData> Matrix { get { return matrix; } }
        public TVertex[] VertexList { get { return vertexList; } }
    }

    class UndirectedAdjMatrixGraphData<T> : AdjMatrixGraphDataBase<T, AdjMatrixGraphVertex<T>> {
        public UndirectedAdjMatrixGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2, double weight) {
            Matrix[vertex1.Handle, vertex2.Handle] = new EdgeData(weight);
            Matrix[vertex2.Handle, vertex1.Handle] = new EdgeData(weight);
        }
        internal override void UpdateEdgeData(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            EdgeData edgeData = updateFunc(GetEdgeDataCore(vertex1, vertex2));
            Matrix[vertex1.Handle, vertex2.Handle] = edgeData;
            Matrix[vertex2.Handle, vertex1.Handle] = edgeData;
        }
        protected override int GetColumnByRow(int row) {
            return row;
        }
    }

    class DirectedAdjMatrixGraphData<T> : AdjMatrixGraphDataBase<T, DirectedAdjMatrixGraphVertex<T>> {
        public DirectedAdjMatrixGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(DirectedAdjMatrixGraphVertex<T> vertex1, DirectedAdjMatrixGraphVertex<T> vertex2, double weight) {
            Matrix[vertex1.Handle, vertex2.Handle] = new EdgeData(weight);
        }
        internal override void UpdateEdgeData(DirectedAdjMatrixGraphVertex<T> vertex1, DirectedAdjMatrixGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            Matrix[vertex1.Handle, vertex2.Handle] = updateFunc(GetEdgeDataCore(vertex1, vertex2));
        }
        protected override int GetColumnByRow(int row) {
            return 0;
        }
    }


    public class AdjMatrixGraph<T> : UndirectedGraph<T, AdjMatrixGraphVertex<T>> {
        public AdjMatrixGraph()
            : this(DefaultCapacity) {
        }
        public AdjMatrixGraph(int capacity)
            : base(capacity) {
        }
        public AdjMatrixGraph<T> BuildMSF() {
            AdjMatrixGraph<T> graph = new AdjMatrixGraph<T>();
            DoBuildMSF(graph);
            return graph;
        }

        internal override GraphDataBase<T, AdjMatrixGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjMatrixGraphData<T>(capacity);
        }
        internal override AdjMatrixGraphVertex<T> CreateVertexCore(T value) {
            return new AdjMatrixGraphVertex<T>(value);
        }
        internal new UndirectedAdjMatrixGraphData<T> Data { get { return (UndirectedAdjMatrixGraphData<T>)base.Data; } }
    }

    public class DirectedAdjMatrixGraph<T> : DirectedGraph<T, DirectedAdjMatrixGraphVertex<T>> {
        public DirectedAdjMatrixGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjMatrixGraph(int capacity)
            : base(capacity) {
        }
        public DirectedAdjMatrixGraph<T> BuildTransposeGraph() {
            DirectedAdjMatrixGraph<T> graph = new DirectedAdjMatrixGraph<T>();
            FillTransposeGraph(graph);
            return graph;
        }
        internal override GraphDataBase<T, DirectedAdjMatrixGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjMatrixGraphData<T>(capacity);
        }
        internal override DirectedAdjMatrixGraphVertex<T> CreateVertexCore(T value) {
            return new DirectedAdjMatrixGraphVertex<T>(value);
        }
        internal new DirectedAdjMatrixGraphData<T> Data { get { return (DirectedAdjMatrixGraphData<T>)base.Data; } }
    }
}
