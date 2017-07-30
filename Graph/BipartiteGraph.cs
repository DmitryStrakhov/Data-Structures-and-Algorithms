using System;
using System.Collections;
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
        readonly RectMatrix<EdgeData, Color> matrix;

        public BiAdjMatrixGraphDataBase(int capacity) : base(capacity) {
            this.uSize = this.vSize = 0;
            this.uCapacity = this.vCapacity = capacity;
            this.uVertexList = new TVertex[capacity];
            this.vVertexList = new TVertex[capacity];
            this.matrix = new RectMatrix<EdgeData, Color>(capacity, capacity);
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

        internal bool AreInTheSamePartition(TVertex vertex1, TVertex vertex2) {
            return vertex1.PartitionID.Equals(vertex2.PartitionID);
        }
        internal override bool IsUVertex(TVertex vertex) {
            return USize != 0 && AreInTheSamePartition(UVertexList.First(), vertex);
        }
        internal override bool IsVVertex(TVertex vertex) {
            return VSize != 0 && AreInTheSamePartition(VVertexList.First(), vertex);
        }
        internal override TVertex GetUVertex(TVertex vertex1, TVertex vertex2) {
            if(IsUVertex(vertex1))
                return vertex1;
            if(IsUVertex(vertex2))
                return vertex2;
            throw new InvalidOperationException();
        }
        internal override TVertex GetVVertex(TVertex vertex1, TVertex vertex2) {
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
        public RectMatrix<EdgeData, Color> Matrix { get { return matrix; } }
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
        internal override void DeleteEdge(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2) {
            var uVertex = GetUVertex(vertex1, vertex2);
            var vVertex = GetVVertex(vertex1, vertex2);
            Matrix[vVertex.Handle, uVertex.Handle] = EdgeData.Empty;
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
        public void DeleteEdge(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2) {
            Guard.IsNotNull(vertex1, nameof(vertex1));
            Guard.IsNotNull(vertex2, nameof(vertex2));
            CheckVertexOwner(vertex1);
            CheckVertexOwner(vertex2);
            if(!Data.AreVerticesAdjacent(vertex1, vertex2))
                throw new InvalidOperationException();
            EdgeData edgeData = Data.GetEdgeData(vertex1, vertex2);
            Data.DeleteEdge(vertex1, vertex2);
            vertex1.Degree--;
            vertex2.Degree--;
            Properties.OnEdgeDeleted(edgeData);
        }
        public BipartiteGraphMatching<T> GetMaximalMatching() {
            if(!Properties.IsUnweighted)
                throw new InvalidOperationException();
            return GetMaximalMatchingCore();
        }
        BipartiteGraphMatching<T> GetMaximalMatchingCore() {
            BipartiteGraphMatching<T> matching = Clone<BipartiteGraphMatching<T>>(false);
            if(Size != 0)
                FillMaximalMatching(matching);
            return matching;
        }
        void FillMaximalMatching(BipartiteGraphMatching<T> matching) {
            GetMaximalMatchingFSet fSet = new GetMaximalMatchingFSet(Data.VSize);
            do {
                fSet.Clear();
                var freeUVertexList = Data.GetUVertexList().Where(x => !IsMatchedVertex(x));
                foreach(var uVertex in freeUVertexList) {
                    Color bfsColor = Color.CreateColor();
                    DoBFSearch(uVertex, (x, y) =>
                        IsMatchedEdge(x, y) ? Data.IsVVertex(x) : Data.IsUVertex(x)
                    , (x, y) =>
                        Data.UpdateEdgeData(x, y, edgeData => edgeData.WithColor(bfsColor))
                    , x => {
                        if(Data.IsVVertex(x) && !IsMatchedVertex(x)) {
                            if(IsInFSet(x)) {
                                fSet.UpdateItem(x.GetData<GetMaximalMatchingVertexInFSetData>().ItemIndex, new GetMaximalMatchingFSetItem(x, bfsColor));
                            }
                            else {
                                int itemIndex = fSet.AddItem(new GetMaximalMatchingFSetItem(x, bfsColor));
                                x.Data = new GetMaximalMatchingVertexInFSetData(itemIndex);
                            }
                            return false;
                        }
                        return true;
                    });
                }
                foreach(var fSetItem in fSet) {
                    DoDFSearch(fSetItem.Vertex, (x, y) => Data.GetEdgeData(x, y).Color == fSetItem.BfsColor
                    , (x, y) => {
                        var uVertex = matching.Data.GetUVertex(Data.GetUVertex(x, y).Handle);
                        var vVertex = matching.Data.GetVVertex(Data.GetVVertex(x, y).Handle);
                        if(IsMatchedEdge(x, y)) {
                            Data.GetEdgeData(x, y).WithData(null);
                            matching.DeleteEdge(uVertex, vVertex);
                        }
                        else {
                            Data.UpdateEdgeData(x, y, edgeData => edgeData.WithData(new GetMaximalMatchingMatchedData()));
                            matching.CreateEdge(uVertex, vVertex);
                        }
                    }, x => {
                        bool stopSearch = Data.IsUVertex(x) && !IsMatchedVertex(x);
                        x.Data = new GetMaximalMatchingMatchedData();
                        return stopSearch ? false : true;
                    });
                }
            }
            while(fSet.Size != 0);
            ClearData(true, true);
        }
        public BipartiteGraphMatching<T> GetAssignmentMatching() {
            if(Data.USize != Data.VSize || Properties.IsNegativeWeighted || !IsComplete()) {
                throw new InvalidOperationException();
            }
            BipartiteGraphMatching<T> graph = Clone<BipartiteGraphMatching<T>>(false);
            Func<int, int, double, EdgeData, EdgeData> func1 = (row, column, minVal, x) => {
                var edgeData = x.WithWeight(x.Weight - minVal);
                if(MathUtils.AreEquals(edgeData.Weight, 0))
                    graph.EnsureEdge(column, row, x.Weight);
                return edgeData;
            };
            Func<int, int, double, EdgeData, EdgeData> func2 = (row, column, minVal, x) => {
                var edgeData = x.WithWeight(x.Weight + minVal);
                if(!MathUtils.AreEquals(edgeData.Weight, 0))
                    graph.EnsureNoEdge(column, row);
                return edgeData;
            };
            RectMatrix<EdgeData, Color> matrix = Data.Matrix.Clone();
            int rowCount = matrix.Size.RowCount;
            for(int n = 0; n < rowCount; n++) {
                double minVal = matrix.GetRowItemList(n).Select(x => x.Weight).Min();
                matrix.TranslateRow(n, func1, minVal);
            }
            if(graph.IsPerfect())
                return graph;
            int columnCount = matrix.Size.ColumnCount;
            for(int n = 0; n < columnCount; n++) {
                double minVal = matrix.GetColumnItemList(n).Select(x => x.Weight).Min();
                matrix.TranslateColumn(n, func1, minVal);
            }
            if(graph.IsPerfect())
                return graph;
            while(true) {
                var matching = graph.GetMaximalMatchingCore();
                if(matching.IsPerfect())
                    return CloneWithEdges(matching);
                Color color1 = Color.CreateColor();
                matching.Data.GetVVertexList()
                    .Where(x => x.Degree != 0)
                    .ForEach(x => matrix.RowAttributes[x.Handle] = color1);
                Color color2 = Color.CreateColor();
                matrix.TranslateRowAttributes((row, rowColor) => rowColor != color1, x => color2);
                matrix.ForEach((row, column, rowColor, columnColor, x) => rowColor == color2 && MathUtils.AreEquals(x.Weight, 0), (row, column, x) => {
                    matrix.ColumnAttributes[column] = color2;
                    for(int n = 0; n < matrix.Size.RowCount; n++) {
                        if(matching.AreVerticesAdjacent(column, n)) matrix.RowAttributes[n] = color2;
                    }
                });
                Color color3 = Color.CreateColor();
                matrix.TranslateColumnAttributes((column, columnColor) => columnColor == color2, x => color3);
                matrix.TranslateRowAttributes((row, rowColor) => rowColor != color2, x => color3);
                double minValue = matrix.GetItems((row, column, rowColor, columnColor, x) => rowColor != color3 && columnColor != color3).Select(x => x.Weight).Min();
                matrix.Translate((row, column, rowColor, columnColor, x) => rowColor != color3 && columnColor != color3, func1, minValue);
                matrix.Translate((row, column, rowColor, columnColor, x) => rowColor == color3 && columnColor == color3, func2, minValue);
            }
        }
        public bool IsComplete() {
            if(Data.USize == 0 || Data.VSize == 0) {
                return false;
            }
            return Properties.EdgeCount == Data.USize * Data.VSize;
        }
        internal TGraph Clone<TGraph>(bool cloneEdges) where TGraph : BipartiteGraph<T>, new() {
            TGraph graph = new TGraph();
            foreach(var vertex in Data.GetUVertexList()) {
                graph.U.CreateVertex(vertex.Value);
            }
            foreach(var vertex in Data.GetVVertexList()) {
                graph.V.CreateVertex(vertex.Value);
            }
            if(cloneEdges) {
                foreach(var edgeData in GetEdgeList()) {
                    graph.CreateEdge(edgeData.StartVertex.Handle, edgeData.EndVertex.Handle, edgeData.Weight);
                }
            }
            return graph;
        }
        internal TGraph CloneWithEdges<TGraph>(TGraph graph) where TGraph : BipartiteGraph<T>, new() {
            Guard.IsNotNull(graph, nameof(graph));
            if(Data.GetUSize() != graph.Data.GetUSize() || Data.GetVSize() != graph.Data.GetVSize()) {
                throw new InvalidOperationException();
            }
            TGraph result = Clone<TGraph>(false);
            foreach(var edge in graph.GetEdgeList()) {
                int uVertexHandle = edge.StartVertex.Handle;
                var vVertexHandle = edge.EndVertex.Handle;
                if(!AreVerticesAdjacent(uVertexHandle, vVertexHandle))
                    throw new InvalidOperationException();
                EdgeData edgeData = GetEdgeData(uVertexHandle, vVertexHandle);
                result.CreateEdge(uVertexHandle, vVertexHandle, edgeData.Weight);
            }
            return result;
        }
        #region Utils
        bool AreVerticesAdjacent(int uVertexHandle, int vVertexHandle) {
            return Data.AreVerticesAdjacent(GetUVertex(uVertexHandle), GetVVertex(vVertexHandle));
        }
        EdgeData GetEdgeData(int uVertexHandle, int vVertexHandle) {
            return Data.GetEdgeData(GetUVertex(uVertexHandle), GetVVertex(vVertexHandle));
        }
        void CreateEdge(int uVertexHandle, int vVertexHandle, double weight) {
            Data.CreateEdge(GetUVertex(uVertexHandle), GetVVertex(vVertexHandle), weight);
        }
        void DeleteEdge(int uVertexHandle, int vVertexHandle) {
            Data.DeleteEdge(GetUVertex(uVertexHandle), GetVVertex(vVertexHandle));
        }
        void EnsureEdge(int uVertexHandle, int vVertexHandle, double weight) {
            if(!AreVerticesAdjacent(uVertexHandle, vVertexHandle)) CreateEdge(uVertexHandle, vVertexHandle, weight);
        }
        void EnsureNoEdge(int uVertexHandle, int vVertexHandle) {
            if(AreVerticesAdjacent(uVertexHandle, vVertexHandle)) DeleteEdge(uVertexHandle, vVertexHandle);
        }
        BipartiteGraphVertex<T> GetUVertex(int uVertexHandle) { return Data.GetUVertex(uVertexHandle); }
        BipartiteGraphVertex<T> GetVVertex(int vVertexHandle) { return Data.GetVVertex(vVertexHandle); }
        #endregion

        public BipartiteGraphPartition<T, BipartiteGraphVertex<T>> U { get { return uPartition; } }
        public BipartiteGraphPartition<T, BipartiteGraphVertex<T>> V { get { return vPartition; } }

        protected override bool AllowEdge(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2) {
            if(Data.AreVerticesAdjacent(vertex1, vertex2)) return false;
            return true;
        }
        internal override GraphDataBase<T, BipartiteGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedBiAdjMatrixGraphData<T>(capacity);
        }
        internal override BipartiteGraphVertex<T> CreateVertexCore(T value) {
            return new BipartiteGraphVertex<T>(value);
        }
        internal new UndirectedBiAdjMatrixGraphData<T> Data { get { return (UndirectedBiAdjMatrixGraphData<T>)base.Data; } }

        #region GetMaximalMatchingMatchedData
        class GetMaximalMatchingMatchedData : IVertexData, IEdgeData {
            public GetMaximalMatchingMatchedData() {
            }
        }
        bool IsMatchedVertex(BipartiteGraphVertex<T> vertex) {
            return vertex.IsDataOfType<GetMaximalMatchingMatchedData>();
        }
        bool IsMatchedEdge(BipartiteGraphVertex<T> vertex1, BipartiteGraphVertex<T> vertex2) {
            EdgeData edgeData = Data.GetEdgeData(vertex1, vertex2);
            return edgeData.IsDataOfType<GetMaximalMatchingMatchedData>();
        }
        #endregion
        #region GetMaximalMatchingVertexInFSetData
        class GetMaximalMatchingVertexInFSetData : IVertexData {
            readonly int itemIIndex;
            public GetMaximalMatchingVertexInFSetData(int itemIIndex) {
                this.itemIIndex = itemIIndex;
            }
            public int ItemIndex { get { return itemIIndex; } }
        }
        bool IsInFSet(BipartiteGraphVertex<T> vertex) {
            return vertex.IsDataOfType<GetMaximalMatchingVertexInFSetData>();
        }
        #endregion

        #region GetMaximalMatchingFSet
        class GetMaximalMatchingFSet : SimpleSet<GetMaximalMatchingFSetItem> {
            public GetMaximalMatchingFSet(int vVertexListSize)
                : base(vVertexListSize) {
            }
        }
        #endregion

        #region GetMaximalMatchingFSetItem
        class GetMaximalMatchingFSetItem {
            readonly BipartiteGraphVertex<T> vertex;
            readonly Color bfsColor;

            public GetMaximalMatchingFSetItem(BipartiteGraphVertex<T> vertex, Color bfsColor) {
                this.vertex = vertex;
                this.bfsColor = bfsColor;
            }
            public Color BfsColor { get { return bfsColor; } }
            public BipartiteGraphVertex<T> Vertex { get { return vertex; } }
        }
        #endregion
    }

    public class BipartiteGraphMatching<T> : BipartiteGraph<T> {
        public BipartiteGraphMatching()
            : base() {
        }
        public bool IsPerfect() {
            return Size != 0 && Data.GetVertexList().All(x => x.Degree == 1);
        }
    }
}
