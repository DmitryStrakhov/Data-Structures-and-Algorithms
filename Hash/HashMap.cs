using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerTypeProxy(typeof(HashMapDebugView<,>))]
    [DebuggerDisplay("HashMap(Count={Count})")]
    public class HashMap<TKey, TValue> {
        Bucket[] buckets;
        int count;
        int capacity;
        int version;
        KeyCollection keys;
        ValueCollection values;
        IEqualityComparer<TKey> keyComparer;

        public HashMap()
            : this(17) {
        }
        public HashMap(int capacity)
            : this(capacity, null) {
        }
        public HashMap(int capacity, IEqualityComparer<TKey> keyComparer) {
            Guard.IsPositive(capacity, nameof(capacity));
            int size = HashHelper.GetPrime(capacity);
            this.count = 0;
            this.capacity = size;
            this.version = 0;
            this.keys = null;
            this.values = null;
            this.buckets = new Bucket[size];
            this.keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
        }

        public void Add(TKey key, TValue value) {
            Guard.IsNotNull(key, nameof(key));
            if(IsResizeRequired) {
                Resize();
            }
            int hashCode = GetHashCode(key);
            int bucket = this.FindBucket(key, hashCode, (xKey, xHashCode, xBucket) => {
                if(Bucket.IsUnused(xBucket)) return true;
                if(xHashCode == xBucket.HashCode && AreKeysEqual(xKey, xBucket.Key)) {
                    throw new ArgumentException();
                }
                return false;
            });
            buckets[bucket] = new Bucket(hashCode, key, value);
            count++;
            version++;
        }
        public bool Remove(TKey key) {
            Guard.IsNotNull(key, nameof(key));
            int bucket = this.FindBucket(key);
            if(bucket != -1) {
                buckets[bucket] = null;
                count--;
                version++;
                return true;
            }
            return false;
        }
        public bool TryGetValue(TKey key, out TValue value) {
            Guard.IsNotNull(key, nameof(key));
            int bucket = this.FindBucket(key);
            if(bucket != -1) {
                value = buckets[bucket].Value;
                return true;
            }
            value = default(TValue);
            return false;
        }
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory) {
            Guard.IsNotNull(key, nameof(key));
            Guard.IsNotNull(valueFactory, nameof(valueFactory));
            int bucket = this.FindBucket(key);
            if(bucket == -1) {
                TValue value = valueFactory(key);
                Add(key, value);
                return value;
            }
            return buckets[bucket].Value;
        }
        public TValue this[TKey key] {
            get {
                Guard.IsNotNull(key, nameof(key));
                int bucket = this.FindBucket(key);
                if(bucket != -1) {
                    return buckets[bucket].Value;
                }
                throw new KeyNotFoundException();
            }
            set {
                Guard.IsNotNull(key, nameof(key));
                int bucket = this.FindBucket(key);
                if(bucket != -1) {
                    version++;
                    buckets[bucket].SetValue(value);
                }
                else {
                    Add(key, value);
                }
            }
        }
        public bool ContainsKey(TKey key) {
            Guard.IsNotNull(key, nameof(key));
            return this.FindBucket(key) != -1;
        }
        public bool ContainsValue(TValue value) {
            for(int n = 0; n < buckets.Length; n++) {
                if(Bucket.IsUsed(buckets[n]) && AreValuesEqual(buckets[n].Value, value)) return true;
            }
            return false;
        }
        public void Clear() {
            if(count != 0) {
                buckets.Clear();
                count = 0;
                version++;
            }
        }

        public int Count {
            get { return count; }
        }
        public bool IsEmpty {
            get { return count == 0; }
        }
        public ICollection<TKey> Keys {
            get { return keys ?? (keys = new KeyCollection(this)); }
        }
        public ICollection<TValue> Values {
            get { return values ?? (values = new ValueCollection(this)); }
        }

        bool IsResizeRequired {
            get { return (double)count / capacity >= 0.7; }
        }
        void Resize() {
            int newCapacity = HashHelper.GetPrime(capacity * 2);
            Bucket[] newBuckets = new Bucket[newCapacity];
            for(int n = 0; n < buckets.Length; n++) {
                if(Bucket.IsUnused(buckets[n])) continue;
                int bIndex = this.FindUnusedBucket(newBuckets, newCapacity, buckets[n].Key);
                newBuckets[bIndex] = buckets[n];
            }
            this.buckets = newBuckets;
            this.capacity = newCapacity;
        }
        internal bool AreKeysEqual(TKey x, TKey y) {
            return keyComparer.Equals(x, y);
        }
        internal int GetHashCode(TKey key) {
            return keyComparer.GetHashCode(key);
        }
        bool AreValuesEqual(TValue x, TValue y) {
            if(ReferenceEquals(x, y)) return true;
            if(x == null || x == null) {
                return false;
            }
            return EqualityComparer<TValue>.Default.Equals(x, y);
        }
        internal int Capacity {
            get { return capacity; }
        }
        internal Bucket[] Buckets {
            get { return buckets; }
        }

        #region HashMapCollectionBase

        abstract class HashMapCollectionBase<T> : CollectionBase<T> {
            readonly HashMap<TKey, TValue> owner;

            public HashMapCollectionBase(HashMap<TKey, TValue> owner) {
                this.owner = owner;
            }
            public void ForEachBucket(T[] array, int arrayIndex, Action<T[], int, Bucket> action) {
                for(int n = 0; n < owner.buckets.Length; n++) {
                    if(Bucket.IsUsed(owner.buckets[n])) action(array, arrayIndex++, owner.buckets[n]);
                }
            }
            protected sealed override int Count {
                get { return owner.Count; }
            }
            protected sealed override bool IsReadOnly {
                get { return true; }
            }
            public HashMap<TKey, TValue> Owner { get { return owner; } }

            protected sealed override EnumeratorBase<T> CreateEnumerator() {
                return CreateEnumeratorCore();
            }
            protected abstract HashMapEnumeratorBase CreateEnumeratorCore();

            #region HashMapEnumeratorBase

            protected abstract class HashMapEnumeratorBase : EnumeratorBase<T> {
                readonly HashMap<TKey, TValue> owner;
                readonly int version;
                int bucket;

                public HashMapEnumeratorBase(HashMap<TKey, TValue> owner) {
                    this.owner = owner;
                    this.version = owner.version;
                    this.bucket = -1;
                }
                protected Bucket CurrentBucket {
                    get {
                        if(bucket == -1 || bucket == owner.capacity) {
                            throw new InvalidOperationException();
                        }
                        return owner.buckets[bucket];
                    }
                }
                protected sealed override bool MoveNext() {
                    if(version != owner.version) {
                        throw new InvalidOperationException();
                    }
                    if(bucket == owner.capacity) return false;
                    bucket = owner.buckets.GetIndexOf(bucket + 1, x => Bucket.IsUsed(x), owner.capacity);
                    return bucket != owner.capacity;
                }
            }

            #endregion
        }

        #endregion

        #region KeyCollection

        class KeyCollection : HashMapCollectionBase<TKey> {
            public KeyCollection(HashMap<TKey, TValue> owner)
                : base(owner) {
            }
            protected override bool Contains(TKey item) {
                return Owner.ContainsKey(item);
            }
            protected override void CopyTo(TKey[] array, int arrayIndex) {
                ForEachBucket(array, arrayIndex, (xArray, xArrayIndex, bucket) => xArray[xArrayIndex] = bucket.Key);
            }
            protected override HashMapEnumeratorBase CreateEnumeratorCore() {
                return new KeyEnumerator(Owner);
            }

            class KeyEnumerator : HashMapEnumeratorBase {
                public KeyEnumerator(HashMap<TKey, TValue> owner)
                    : base(owner) {
                }
                protected override TKey CurrentValue { get { return CurrentBucket.Key; } }
            }
        }

        #endregion

        #region ValueCollection

        class ValueCollection : HashMapCollectionBase<TValue> {
            public ValueCollection(HashMap<TKey, TValue> owner)
                : base(owner) {
            }
            protected override bool Contains(TValue item) {
                return Owner.ContainsValue(item);
            }
            protected override void CopyTo(TValue[] array, int arrayIndex) {
                ForEachBucket(array, arrayIndex, (xArray, xArrayIndex, bucket) => xArray[xArrayIndex] = bucket.Value);
            }
            protected override HashMapEnumeratorBase CreateEnumeratorCore() {
                return new ValueEnumerator(Owner);
            }

            class ValueEnumerator : HashMapEnumeratorBase {
                public ValueEnumerator(HashMap<TKey, TValue> owner)
                    : base(owner) {
                }
                protected override TValue CurrentValue { get { return CurrentBucket.Value; } }
            }
        }

        #endregion

        #region Bucket

        [DebuggerDisplay("Bucket(HashCode={HashCode},Key={Key},Value={Value})")]
        internal class Bucket {
            readonly int hashCode;
            readonly TKey key;
            TValue value;

            public Bucket(int hashCode, TKey key, TValue value) {
                this.hashCode = hashCode;
                this.key = key;
                this.value = value;
            }
            public void SetValue(TValue value) {
                this.value = value;
            }
            public static bool IsUsed(Bucket bucket) {
                return bucket != null;
            }
            public static bool IsUnused(Bucket bucket) {
                return bucket == null;
            }
            public int HashCode { get { return hashCode; } }
            public TKey Key { get { return key; } }
            public TValue Value { get { return value; } }
        }

        #endregion

    }


    #region HashMapExtensions

    static class HashMapExtensions {
        public static int FindBucket<TKey, TValue>(this HashMap<TKey, TValue> @this, TKey key) {
            int bIndex = @this.FindBucket(@this.Buckets, @this.Capacity, key, @this.GetHashCode(key),(xKey, xHashCode, xBucket) => HashMap<TKey, TValue>.Bucket.IsUnused(xBucket) || (HashMap<TKey, TValue>.Bucket.IsUsed(xBucket) && xHashCode == xBucket.HashCode && @this.AreKeysEqual(xKey, xBucket.Key)));
            return HashMap<TKey, TValue>.Bucket.IsUsed(@this.Buckets[bIndex]) ? bIndex : -1;
        }
        public static int FindBucket<TKey, TValue>(this HashMap<TKey, TValue> @this, TKey key, int hashCode, Func<TKey, int, HashMap<TKey, TValue>.Bucket, bool> predicate) {
            return @this.FindBucket(@this.Buckets, @this.Capacity, key, hashCode, predicate);
        }
        public static int FindUnusedBucket<TKey, TValue>(this HashMap<TKey, TValue> @this, HashMap<TKey, TValue>.Bucket[] buckets, int capacity, TKey key) {
            return @this.FindBucket(buckets, capacity, key, @this.GetHashCode(key),(xKey, xHashCode, xBucket) => HashMap<TKey, TValue>.Bucket.IsUnused(xBucket));
        }

        const int CollisionThreshold = int.MaxValue;
        static int FindBucket<TKey, TValue>(this HashMap<TKey, TValue> @this, HashMap<TKey, TValue>.Bucket[] buckets, int capacity, TKey key, int hashCode, Func<TKey, int, HashMap<TKey, TValue>.Bucket, bool> predicate) {
            int bIndex = GetBucket(hashCode, capacity);
            for(int n = 0; n < CollisionThreshold; n++) {
                if(predicate(key, hashCode, buckets[bIndex])) return bIndex;
                bIndex = GetNextBucket(hashCode, bIndex, capacity);
            }
            throw new InvalidOperationException();
        }
        static int GetBucket(int hashCode, int length) {
            uint hash = FNV1A.GetHash(hashCode);
            return unchecked((int)(hash % length));
        }
        static int GetNextBucket(int hashCode, int bucket, int length) {
            long increment = 1 + unchecked((uint)hashCode * 163) % (length - 1);
            return (int)((bucket + increment) % length);
        }
    } 

    #endregion


    #region HashMapDebugView

    sealed class HashMapDebugView<TKey, TValue> {
        readonly HashMap<TKey, TValue> owner;

        public HashMapDebugView(HashMap<TKey, TValue> owner) {
            Guard.IsNotNull(owner, nameof(owner));
            this.owner = owner;
        }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public HashMap<TKey, TValue>.Bucket[] Buckets {
            get { return owner.Buckets; }
        }
    }

    #endregion
}
