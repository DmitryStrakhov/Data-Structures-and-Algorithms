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
        VertexColor color;

        internal Vertex(T value) {
            this.value = value;
            this.ownerID = null;
            this.color = VertexColor.None;
            this.handle = null;
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
        internal VertexColor Color {
            get { return color; }
            set { color = value; }
        }
        public T Value { get { return value; } }
    }

    internal enum VertexColor {
        None, Gray
    }

    public abstract class Graph<TValue, TVertex> where TVertex : Vertex<TValue> {
        int capacity;
        Guid id;

        public Graph() {
            this.id = Guid.NewGuid();
        }
        public TVertex CreateVertex(TValue value) {
            TVertex vertex = CreateVertexCore(value);
            vertex.OwnerID = this.id;
            RegisterVertex(vertex);
            return vertex;
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            CreateEdgeCore(vertex1, vertex2);
        }
        public int Size {
            get { return SizeCore; }
        }
        public ReadOnlyCollection<TVertex> GetVertexList() {
            var list = GetVertexListCore();
            return new ReadOnlyCollection<TVertex>(list);
        }
        public bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            return AreVerticesAdjacentCore(vertex1, vertex2);
        }
        public ReadOnlyCollection<TVertex> GetAdjacentVertextList(TVertex vertex) {
            Guard.IsNotNull(vertex, nameof(vertex));
            CheckVertexOwner(vertex);
            var list = GetAdjacentVertextListCore(vertex);
            return new ReadOnlyCollection<TVertex>(list);
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

        void DoSearch(TVertex vertex, Func<TVertex, bool> action, Func<TVertex, Func<TVertex, bool>, bool> searchProc) {
            if(Size == 0) return;
            int handle = vertex != null ? vertex.Handle : 0;
            for(int i = 0; i < Size; i++) {
                var graphVertex = GetVertex((handle + i) % Size);
                if(graphVertex.Color == VertexColor.None) {
                    if(!searchProc(graphVertex, action)) break;
                }
            }
            for(int i = 0; i < Size; i++) {
                GetVertexCore(i).Color = VertexColor.None;
            }
        }

        bool DFSearchCore(TVertex vertex, Func<TVertex, bool> action) {
            bool @continue = action(vertex);
            if(!@continue)
                return false;
            vertex.Color = VertexColor.Gray;
            var adjacentList = GetAdjacentVertextList(vertex);
            for(int i = 0; i < adjacentList.Count; i++) {
                if(adjacentList[i].Color == VertexColor.None) {
                    if(!DFSearchCore(adjacentList[i], action)) return false;
                }
            }
            return true;
        }
        bool BFSearchCore(TVertex vertex, Func<TVertex, bool> action) {
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(vertex);
            vertex.Color = VertexColor.Gray;
            while(!queue.IsEmpty) {
                var graphVertex = queue.DeQueue();
                if(!action(graphVertex))
                    return false;
                var adjacentList = GetAdjacentVertextList(graphVertex);
                for(int i = 0; i < adjacentList.Count; i++) {
                    if(adjacentList[i].Color == VertexColor.None) {
                        adjacentList[i].Color = VertexColor.Gray;
                        queue.EnQueue(adjacentList[i]);
                    }
                }
            }
            return true;
        }

        protected internal TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return GetVertexCore(handle);
        }

        void CheckVertexOwner(TVertex vertex) {
            if(!vertex.OwnerID.Equals(this.id)) {
                throw new InvalidOperationException();
            }
        }
        #region Vertex / Edge

        protected abstract TVertex CreateVertexCore(TValue value);
        protected abstract void CreateEdgeCore(TVertex vertex1, TVertex vertex2);
        protected abstract void RegisterVertex(TVertex vertex);
        protected abstract TVertex GetVertexCore(int handle);

        protected abstract int SizeCore {
            get;
        }
        #endregion

        #region Metrics

        protected abstract bool AreVerticesAdjacentCore(TVertex vertex1, TVertex vertex2);
        protected abstract IList<TVertex> GetVertexListCore();
        protected abstract IList<TVertex> GetAdjacentVertextListCore(TVertex vertex);

        #endregion
    }
}
