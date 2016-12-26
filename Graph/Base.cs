using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class Vertex<T> {
        T value;
        internal Vertex(T value) {
            this.value = value;
        }

        public T Value { get { return value; } }
    }

    public abstract class Graph<TValue, TVertex> where TVertex : Vertex<TValue> {
        int capacity;

        public Graph()
            : this(4) {
        }
        public Graph(int capacity) {
            this.capacity = capacity;
        }

        public TVertex CreateVertex(TValue value) {
            throw new NotImplementedException();
        }
        public void CreateEdge(TVertex vertex1, TVertex vertex2) {
            throw new NotImplementedException();
        }
        public int Size {
            get { throw new NotImplementedException(); }
        }
        public ReadOnlyCollection<TVertex> GetVertexList() {
            throw new NotImplementedException();
        }
        public bool AreVerticesAdjacent(TVertex vertext1, TVertex vertex2) {
            throw new NotImplementedException();
        }
        public ReadOnlyCollection<TVertex> GetAdjacentVertextList(TVertex vertex) {
            throw new NotImplementedException();
        }

        internal int Capacity { get { return capacity; } }
    }
}
