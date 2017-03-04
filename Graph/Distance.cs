using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("DistanceObject (Vertex = {Vertex.Value}, Size = {Size})")]
    public class DistanceObject<TValue, TVertex> where TVertex : Vertex<TValue> {
        readonly int size;
        readonly Row[] rows;
        readonly TVertex vertex;
        readonly Guid id;

        internal DistanceObject(Graph<TValue, TVertex> graph, TVertex vertex) {
            this.id = graph.ID;
            this.size = graph.Size;
            this.vertex = vertex;
            this.rows = new Row[size];
        }
        public ReadOnlyCollection<TVertex> GetPathTo(TVertex targetVertex) {
            Guard.IsNotNull(targetVertex, nameof(targetVertex));
            CheckVertexOwner(targetVertex);
            Stack<TVertex> stack = new Stack<TVertex>();
            stack.Push(targetVertex);
            TVertex current = targetVertex;
            while(!ReferenceEquals(Vertex, current)) {
                if(IsRowEmpty(current))
                    throw new InvalidOperationException();
                current = this[current].Predecessor;
                stack.Push(current);
            }
            List<TVertex> list = new List<TVertex>(stack.Size);
            while(!stack.IsEmpty) {
                list.Add(stack.Pop());
            }
            return new ReadOnlyCollection<TVertex>(list);
        }

        public Row this[TVertex vertex] {
            get {
                Guard.IsNotNull(vertex, nameof(vertex));
                CheckVertexOwner(vertex);
                return Rows[vertex.Handle];
            }
            internal set {
                Guard.IsNotNull(vertex, nameof(vertex));
                CheckVertexOwner(vertex);
                if(value != null) {
                    if(!ReferenceEquals(vertex, value.Vertex)) throw new InvalidOperationException();
                }
                Rows[vertex.Handle] = value;
            }
        }
        public bool IsRowEmpty(TVertex vertex) {
            Guard.IsNotNull(vertex, nameof(vertex));
            CheckVertexOwner(vertex);
            return Rows[vertex.Handle] == null;
        }

        void CheckVertexOwner(TVertex vertex) {
            if(!vertex.OwnerID.Equals(this.id)) {
                throw new InvalidOperationException();
            }
        }

        #region Row
        [DebuggerDisplay("Vertex: {Vertex.Value}, Predecessor: {Predecessor.Value}, Distance: {Distance}")]
        public class Row {
            readonly TVertex vertex;
            readonly TVertex predecessor;
            readonly double distance;

            internal Row(TVertex vertex, TVertex predecessor, double distance) {
                this.vertex = vertex;
                this.predecessor = predecessor;
                this.distance = distance;
            }
            public override bool Equals(object obj) {
                Row other = obj as Row;
                return other != null && Equals(this, other);
            }
            static bool Equals(Row @this, Row other) {
                return ReferenceEquals(@this.Vertex, other.Vertex) && ReferenceEquals(@this.Predecessor, other.Predecessor) && MathUtils.AreDoubleEquals(@this.Distance, other.Distance);
            }
            public override int GetHashCode() {
                return base.GetHashCode();
            }

            public TVertex Vertex { get { return vertex; } }
            public TVertex Predecessor { get { return predecessor; } }
            public double Distance { get { return distance; } }
        }

        #endregion

        internal Row[] Rows {
            get { return rows; }
        }

        public int Size { get { return size; } }
        public TVertex Vertex { get { return vertex; } }
    }


    interface IPathSearch<TValue, TVertex> where TVertex : Vertex<TValue> {
        DistanceObject<TValue, TVertex> GetPath(TVertex baseVertex);
    }

    abstract class PathSearchBase<TValue, TVertex> : IPathSearch<TValue, TVertex> where TVertex : Vertex<TValue> {
        readonly Graph<TValue, TVertex> graph;

        public PathSearchBase(Graph<TValue, TVertex> graph) {
            Guard.IsNotNull(graph, nameof(graph));
            this.graph = graph;
        }

        public Graph<TValue, TVertex> Graph { get { return graph; } }
        public abstract DistanceObject<TValue, TVertex> GetPath(TVertex baseVertex);
    }


    class PathAlgorithmFactory<TValue, TVertex> where TVertex : Vertex<TValue> {
        public static IPathSearch<TValue, TVertex> Create(Graph<TValue, TVertex> graph) {
            if(graph.Properties == GraphProperties.Unweighted) {
                return new BFPathSearch<TValue, TVertex>(graph);
            }
            if(graph.Properties.HasFlag(GraphProperties.Weighted)) {
                if(graph.Properties.HasFlag(GraphProperties.NegativeWeighted)) {
                    return new BellmanFordPathSearch<TValue, TVertex>(graph);
                }
                else {
                    return new DijkstraPathSearch<TValue, TVertex>(graph);
                }
            }
            throw new ArgumentException(nameof(graph));
        }
    }

    class BFPathSearch<TValue, TVertex> : PathSearchBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        public BFPathSearch(Graph<TValue, TVertex> graph)
            : base(graph) {
        }
        public override DistanceObject<TValue, TVertex> GetPath(TVertex baseVertex) {
            DistanceObject<TValue, TVertex> result = new DistanceObject<TValue, TVertex>(Graph, baseVertex);
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(baseVertex);
            result[baseVertex] = new DistanceObject<TValue, TVertex>.Row(baseVertex, null, 0);
            while(!queue.IsEmpty) {
                TVertex vertex = queue.DeQueue();
                var adjacentList = Graph.GetAdjacentVertextList(vertex);
                foreach(TVertex adjacentVertex in adjacentList) {
                    if(result.IsRowEmpty(adjacentVertex)) {
                        result[adjacentVertex] = new DistanceObject<TValue, TVertex>.Row(adjacentVertex, vertex, result[vertex].Distance + 1);
                        queue.EnQueue(adjacentVertex);
                    }
                }
            }
            return result;
        }
    }

    class DijkstraPathSearch<TValue, TVertex> : PathSearchBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        public DijkstraPathSearch(Graph<TValue, TVertex> graph)
            : base(graph) {
        }
        public override DistanceObject<TValue, TVertex> GetPath(TVertex baseVertex) {
            DistanceObject<TValue, TVertex> result = new DistanceObject<TValue, TVertex>(Graph, baseVertex);
            VertexPriorityQueue queue = new VertexPriorityQueue();
            queue.Insert(0, baseVertex);
            result[baseVertex] = new DistanceObject<TValue, TVertex>.Row(baseVertex, null, 0);
            VertexColor colorId = VertexColor.NewColor();
            while(!queue.IsEmpty) {
                TVertex vertex = queue.DeleteMinimumValue();
                if(vertex.Tag.Color != colorId) {
                    vertex.Tag.Color = colorId;
                    var adjacentList = Graph.GetAdjacentVertextList(vertex);
                    foreach(TVertex adjacentVertex in adjacentList) {
                        double weight = result[vertex].Distance + Graph.GetWeight(vertex, adjacentVertex);
                        if(result.IsRowEmpty(adjacentVertex) || weight < result[adjacentVertex].Distance) {
                            result[adjacentVertex] = new DistanceObject<TValue, TVertex>.Row(adjacentVertex, vertex, weight);
                            queue.Insert(weight, adjacentVertex);
                        }
                    }
                }
            }
            return result;
        }
        #region VertexPriorityQueue
        class VertexPriorityQueue : AscendingPriorityQueue<double, TVertex> {
        }
        #endregion
    }

    class BellmanFordPathSearch<TValue, TVertex> : PathSearchBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        public BellmanFordPathSearch(Graph<TValue, TVertex> graph)
            : base(graph) {
        }
        public override DistanceObject<TValue, TVertex> GetPath(TVertex baseVertex) {
            DistanceObject<TValue, TVertex> result = new DistanceObject<TValue, TVertex>(Graph, baseVertex);
            Queue<TVertex> queue = new Queue<TVertex>();
            queue.EnQueue(baseVertex);
            result[baseVertex] = new DistanceObject<TValue, TVertex>.Row(baseVertex, null, 0);
            VertexColor colorId = VertexColor.NewColor();
            while(!queue.IsEmpty) {
                TVertex vertex = queue.DeQueue();
                vertex.Tag.Color = VertexColor.Empty;
                var adjacentList = Graph.GetAdjacentVertextList(vertex);
                foreach(TVertex adjacentVertex in adjacentList) {
                    double weight = result[vertex].Distance + Graph.GetWeight(vertex, adjacentVertex);
                    if(result.IsRowEmpty(adjacentVertex) || weight < result[adjacentVertex].Distance) {
                        result[adjacentVertex] = new DistanceObject<TValue, TVertex>.Row(adjacentVertex, vertex, weight);
                        if(adjacentVertex.Tag.Color != colorId) {
                            queue.EnQueue(adjacentVertex);
                            adjacentVertex.Tag.Color = colorId;
                        }
                    }
                }
            }
            return result;
        }
    }
}
