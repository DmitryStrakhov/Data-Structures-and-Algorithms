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

        internal Vertex(T value) {
            this.value = value;
            this.ownerID = null;
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

        public T Value { get { return value; } }
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

        void CheckVertexOwner(TVertex vertex) {
            if(!vertex.OwnerID.Equals(this.id)) {
                throw new InvalidOperationException();
            }
        }
        #region Vertex / Edge

        protected abstract TVertex CreateVertexCore(TValue value);
        protected abstract void CreateEdgeCore(TVertex vertex1, TVertex vertex2);
        protected abstract void RegisterVertex(TVertex vertex);

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
