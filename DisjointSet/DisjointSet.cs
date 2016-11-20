using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class DisjointSet<T> {
        bool handleIsValueType;
        Dictionary<T, SetItem> values;

        public DisjointSet() {
            this.handleIsValueType = typeof(T).IsValueType;
            this.values = new Dictionary<T, SetItem>();
        }

        public T MakeSet(T item) {
            if(HandleIsReferenceType) {
                Guard.IsNotNull(item, nameof(item));
            }
            if(Values.ContainsKey(item)) {
                ThrowHandleIsAlreadyReserved(nameof(item));
            }
            Values[item] = new SetItem(item, 0);
            return item;
        }
        public T Find(T item) {
            if(HandleIsReferenceType) {
                Guard.IsNotNull(item, nameof(item));
            }
            if(!Values.ContainsKey(item)) {
                ThrowHandleIsUnknown(nameof(item));
            }
            return DoFind(item);
        }
        protected T DoFind(T item) {
            SetItem coreItem = GetItemCore(item);
            if(coreItem.Parent.Equals(item))
                return item;
            var result = DoFind(coreItem.Parent);
            coreItem.UpdateParent(result);
            return result;
        }

        public T Union(T x, T y) {
            if(HandleIsReferenceType) {
                Guard.IsNotNull(x, nameof(x));
                Guard.IsNotNull(y, nameof(y));
            }
            if(!Values.ContainsKey(x)) {
                ThrowHandleIsUnknown(nameof(x));
            }
            if(!Values.ContainsKey(y)) {
                ThrowHandleIsUnknown(nameof(y));
            }
            T xSetId = Find(x);
            T ySetId = Find(y);
            if(xSetId.Equals(ySetId)) return xSetId;
            SetItem xSetIdCore = GetItemCore(xSetId);
            SetItem ySetIdCore = GetItemCore(ySetId);
            T result;
            if(xSetIdCore.Rank < ySetIdCore.Rank) {
                xSetIdCore.UpdateParent(ySetId);
                result = ySetId;
            }
            else {
                ySetIdCore.UpdateParent(xSetId);
                if(xSetIdCore.Rank == ySetIdCore.Rank) xSetIdCore.PromoteRank();
                result = xSetId;
            }
            return result;
        }
        public bool AreEquivalent(T x, T y) {
            if(HandleIsReferenceType) {
                Guard.IsNotNull(x, nameof(x));
                Guard.IsNotNull(y, nameof(y));
            }
            if(!Values.ContainsKey(x)) {
                ThrowHandleIsUnknown(nameof(x));
            }
            if(!Values.ContainsKey(y)) {
                ThrowHandleIsUnknown(nameof(y));
            }
            return Find(x).Equals(Find(y));
        }
        public IEnumerable<T> Items {
            get {
                foreach(var key in Values.Keys) {
                    yield return key;
                }
            }
        }
        public int Size {
            get { return Values.Count; }
        }
        public void Clear() {
            Values.Clear();
        }
        public void RemoveSet(T item) {
            if(HandleIsReferenceType) {
                Guard.IsNotNull(item, nameof(item));
            }
            if(!Values.ContainsKey(item)) {
                ThrowHandleIsUnknown(nameof(item));
            }
            T setId = Find(item);
            var itemList = Items.Where(x => Find(x).Equals(setId)).ToList();
            itemList.ForEach(x => Values.Remove(x));
        }

        Dictionary<T, SetItem> Values { get { return values; } }

        internal bool HandleIsValueType {
            get { return handleIsValueType; }
        }
        internal bool HandleIsReferenceType {
            get { return !HandleIsValueType; }
        }

        static void ThrowHandleIsUnknown(string argument) {
            throw new InvalidOperationException(string.Format("Set doesn't contain the item: {0}", argument));
        }
        static void ThrowHandleIsAlreadyReserved(string argument) {
            throw new InvalidOperationException(string.Format("Set already contains the item: {0}", argument));
        }

        [DebuggerDisplay("SetItem (Parent = {Parent}, Rank = {Rank})")]
        internal class SetItem {
            T parent;
            int rank;
            public SetItem(T parent, int rank) {
                this.parent = parent;
                this.rank = rank;
            }
            public void UpdateParent(T parent) {
                this.parent = parent;
            }
            public void PromoteRank() {
                this.rank++;
            }

            public T Parent { get { return parent; } }
            public int Rank { get { return rank; } }
        }

        internal SetItem GetItemCore(T item) {
            return Values[item];
        }
        internal List<T> GetPath(T item) {
            List<T> parentList = new List<T>(16);
            T key = item;
            while(true) {
                T parent = GetItemCore(key).Parent;
                if(parent.Equals(key))
                    break;
                parentList.Add(parent);
                key = parent;
            }
            return parentList;
        }
    }
}
