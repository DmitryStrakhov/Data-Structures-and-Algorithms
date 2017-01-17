using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjSetGraphVertex ({Value})")]
    public class AdjSetGraphVertex<T> : Vertex<T> {
        bool isSelfLooped;
        internal AdjSetGraphVertex(T value)
            : base(value) {
        }
        internal bool IsSelfLooped {
            get { return isSelfLooped; }
            set { isSelfLooped = value; }
        }
    }

    public abstract class AdjSetGraphBase<T> : Graph<T, AdjSetGraphVertex<T>> {
        int size;
        int capacity;
        VertexDisjointSet[] list;

        public AdjSetGraphBase()
            : this(DefaultCapacity) {
        }
        public AdjSetGraphBase(int capacity) {
            Guard.IsPositive(capacity, nameof(capacity));
            this.size = 0;
            this.capacity = capacity;
            this.list = new VertexDisjointSet[capacity];
        }
        protected override void RegisterVertex(AdjSetGraphVertex<T> vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureListSize(newSize);
            List[handle] = new VertexDisjointSet(vertex);
            vertex.Handle = handle;
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
        protected override int SizeCore {
            get { return size; }
        }
        protected override AdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new AdjSetGraphVertex<T>(value);
        }
        protected override IList<AdjSetGraphVertex<T>> GetVertexListCore() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        protected override IList<AdjSetGraphVertex<T>> GetAdjacentVertextListCore(AdjSetGraphVertex<T> vertex) {
            return List[vertex.Handle].GetVertices(false).ToList();
        }
        protected override bool AreVerticesAdjacentCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            return List[vertex1.Handle].IsVertexAdjacent(vertex2);
        }
        protected override AdjSetGraphVertex<T> GetVertexCore(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return List[handle].Vertex;
        }

        #region VertexDisjointSet
        [DebuggerDisplay("VertexDisjointSet ({Vertex.Value})")]
        internal class VertexDisjointSet : DisjointSet<AdjSetGraphVertex<T>> {
            readonly AdjSetGraphVertex<T> vertex;

            public VertexDisjointSet(AdjSetGraphVertex<T> vertex) {
                this.vertex = MakeSet(vertex);
            }
            public void AddVertex(AdjSetGraphVertex<T> other) {
                MakeSet(other);
                Union(Vertex, other);
            }
            public bool IsVertexAdjacent(AdjSetGraphVertex<T> other) {
                return Exists(other) && AreEquivalent(Vertex, other);
            }
            public IEnumerable<AdjSetGraphVertex<T>> GetVertices(bool includeBaseVertex = true) {
                if(includeBaseVertex) {
                    yield return Vertex;
                    if(Vertex.IsSelfLooped) yield return Vertex;
                }
                foreach(var item in Items) {
                    if(!ReferenceEquals(item, Vertex)) yield return item;
                }
            }
            public AdjSetGraphVertex<T> Vertex { get { return vertex; } }
        }

        #endregion

        internal IEnumerable<T>[] GetData() {
            IEnumerable<T>[] result = new IEnumerable<T>[Size];
            for(int i = 0; i < Size; i++) {
                result[i] = List[i].GetVertices().Select(x => x.Value).ToList();
            }
            return result;
        }

        internal VertexDisjointSet[] List { get { return list; } }
    }

    public class AdjSetGraph<T> : AdjSetGraphBase<T> {
        public AdjSetGraph() {
        }
        public AdjSetGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            if(ReferenceEquals(vertex1, vertex2)) {
                vertex1.IsSelfLooped = true;
            }
            else {
                List[vertex1.Handle].AddVertex(vertex2);
                List[vertex2.Handle].AddVertex(vertex1);
            }
            
        }
    }

    public class DirectedAdjSetGraph<T> : AdjSetGraphBase<T> {
        public DirectedAdjSetGraph() {
        }
        public DirectedAdjSetGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            if(ReferenceEquals(vertex1, vertex2)) {
                vertex1.IsSelfLooped = true;
            }
            else {
                List[vertex1.Handle].AddVertex(vertex2);
            }
        }
    }
}
