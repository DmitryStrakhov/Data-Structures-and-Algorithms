using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjMatrixGraphVertex (Value: {Value})")]
    public class AdjMatrixGraphVertex<T> : Vertex<T> {
        internal AdjMatrixGraphVertex(T value)
            : base(value) {
        }
    }

    public abstract class AdjMatrixGraphBase<T> : Graph<T, AdjMatrixGraphVertex<T>> {
        int size;
        int capacity;
        AdjMatrixGraphVertex<T>[] vertexList;
        BitMatrix matrix;

        public AdjMatrixGraphBase()
            : this(DefaultCapacity) {
        }
        public AdjMatrixGraphBase(int capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.vertexList = new AdjMatrixGraphVertex<T>[capacity];
            this.matrix = new BitMatrix(capacity);
        }

        protected override int SizeCore {
            get { return size; }
        }
        protected override void RegisterVertex(AdjMatrixGraphVertex<T> vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureVertexListSize(newSize);
            Matrix.EnsureSize(newSize);
            VertexList[handle] = vertex;
            vertex.Handle = handle;
        }
        protected override AdjMatrixGraphVertex<T> CreateVertexCore(T value) {
            return new AdjMatrixGraphVertex<T>(value);
        }
        protected override ReadOnlyCollection<AdjMatrixGraphVertex<T>> GetVertexListCore() {
            var list = VertexList.Take(Size).ToList();
            return new ReadOnlyCollection<AdjMatrixGraphVertex<T>>(list);
        }
        protected override IList<AdjMatrixGraphVertex<T>> GetAdjacentVertextListCore(AdjMatrixGraphVertex<T> vertex) {
            List<AdjMatrixGraphVertex<T>> list = new List<AdjMatrixGraphVertex<T>>();
            for(int n = 0; n < Size; n++) {
                if(Matrix[vertex.Handle, n]) list.Add(VertexList[n]);
            }
            return list;
        }
        protected override bool AreVerticesAdjacentCore(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2) {
            return Matrix[vertex1.Handle, vertex2.Handle];
        }

        void EnsureVertexListSize(int newSize) {
            if(newSize > this.capacity) {
                int _capacity = this.capacity * 2;
                if(size > _capacity)
                    _capacity = size * 2;
                AdjMatrixGraphVertex<T>[] _list = new AdjMatrixGraphVertex<T>[_capacity];
                Array.Copy(VertexList, _list, VertexList.Length);
                this.vertexList = _list;
                this.capacity = _capacity;
            }
        }
        internal int[,] GetMatrixData() {
            int sz = Matrix.Size;
            int[,] result = new int[sz, sz];
            for(int i = 0; i < sz; i++) {
                for(int j = 0; j < sz; j++) {
                    result[i, j] = Matrix[i, j] ? 1 : 0;
                }
            }
            return result;
        }

        internal AdjMatrixGraphVertex<T>[] VertexList {
            get { return vertexList; }
        }
        internal BitMatrix Matrix { get { return matrix; } }
    }


    public class AdjMatrixGraph<T> : AdjMatrixGraphBase<T> {
        public AdjMatrixGraph() {
        }
        public AdjMatrixGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2) {
            Matrix[vertex1.Handle, vertex2.Handle] = true;
            Matrix[vertex2.Handle, vertex1.Handle] = true;
        }
    }

    public class DirectedAdjMatrixGraph<T> : AdjMatrixGraphBase<T> {
        public DirectedAdjMatrixGraph() {
        }
        public DirectedAdjMatrixGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2) {
            Matrix[vertex1.Handle, vertex2.Handle] = true;
        }
    }
}
