﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjMatrixGraphVertex ({Value})")]
    public class AdjMatrixGraphVertex<T> : Vertex<T> {
        internal AdjMatrixGraphVertex(T value)
            : base(value) {
        }
    }

    abstract class AdjMatrixGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        int size;
        int capacity;
        TVertex[] vertexList;
        readonly BitMatrix matrix;

        public AdjMatrixGraphDataBase(int capacity) : base(capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.vertexList = new TVertex[capacity];
            this.matrix = new BitMatrix(capacity);
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
        internal override IList<TVertex> GetVertexList() {
            return VertexList.Take(Size).ToList();
        }
        internal override IList<TVertex> GetAdjacentVertextList(TVertex vertex) {
            List<TVertex> list = new List<TVertex>();
            for(int n = 0; n < Size; n++) {
                if(Matrix[vertex.Handle, n]) list.Add(VertexList[n]);
            }
            return list;
        }
        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            return Matrix[vertex1.Handle, vertex2.Handle];
        }
        internal override int GetSize() {
            return Size;
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return VertexList[handle];
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
            for(int i = 0; i < sz; i++) {
                for(int j = 0; j < sz; j++) {
                    result[i, j] = Matrix[i, j] ? 1 : 0;
                }
            }
            return result;
        }

        public BitMatrix Matrix { get { return matrix; } }
        public TVertex[] VertexList { get { return vertexList; } }
    }

    class UndirectedAdjMatrixGraphData<T> : AdjMatrixGraphDataBase<T, AdjMatrixGraphVertex<T>> {
        public UndirectedAdjMatrixGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2) {
            Matrix[vertex1.Handle, vertex2.Handle] = true;
            Matrix[vertex2.Handle, vertex1.Handle] = true;
        }
    }

    class DirectedAdjMatrixGraphData<T> : AdjMatrixGraphDataBase<T, AdjMatrixGraphVertex<T>> {
        public DirectedAdjMatrixGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjMatrixGraphVertex<T> vertex1, AdjMatrixGraphVertex<T> vertex2) {
            Matrix[vertex1.Handle, vertex2.Handle] = true;
        }
    }


    public class AdjMatrixGraph<T> : UndirectedGraph<T, AdjMatrixGraphVertex<T>> {
        public AdjMatrixGraph()
            : this(DefaultCapacity) {
        }
        public AdjMatrixGraph(int capacity)
            : base(capacity) {
        }

        internal override GraphDataBase<T, AdjMatrixGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjMatrixGraphData<T>(capacity);
        }
        internal override AdjMatrixGraphVertex<T> CreateVertexCore(T value) {
            return new AdjMatrixGraphVertex<T>(value);
        }
        internal new UndirectedAdjMatrixGraphData<T> Data { get { return (UndirectedAdjMatrixGraphData<T>)base.Data; } }
    }

    public class DirectedAdjMatrixGraph<T> : DirectedGraph<T, AdjMatrixGraphVertex<T>> {
        public DirectedAdjMatrixGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjMatrixGraph(int capacity)
            : base(capacity) {
        }

        internal override GraphDataBase<T, AdjMatrixGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjMatrixGraphData<T>(capacity);
        }
        internal override AdjMatrixGraphVertex<T> CreateVertexCore(T value) {
            return new AdjMatrixGraphVertex<T>(value);
        }
        internal new DirectedAdjMatrixGraphData<T> Data { get { return (DirectedAdjMatrixGraphData<T>)base.Data; } }
    }
}
