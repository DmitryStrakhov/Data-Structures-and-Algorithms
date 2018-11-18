using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [
    DebuggerDisplay("Trie({typeof(T)})"),
    DebuggerTypeProxy(typeof(TrieDebugView<>))
    ]
    public class Trie<T> {
        readonly TrieRoot<T> root;

        public Trie() {
            this.root = new TrieRoot<T>();
        }

        public void Insert(string key) {
            Insert(key, default(T));
        }
        public void Insert(string key, T tag) {
            Guard.IsNotNull(key, nameof(key));
            TrieKeyReader keyReader = new TrieKeyReader(key);

            TrieNode<T> node = root;
            while(!keyReader.IsFinished) {
                int index = keyReader.ReadChunk();
                var child = node[index];
                if(child == null || (keyReader.IsFinished && !child.IsTerminal)) {
                    node[index] = child = keyReader.IsFinished ? new TrieNode<T>(tag, true) : new TrieNode<T>();
                }
                node = child;
            }
        }
        public void Delete(string key) {
            Guard.IsNotNull(key, nameof(key));
            TrieKeyReader keyReader = new TrieKeyReader(key);

            TrieNode<T> node = root;
            TrieNode<T> prev = null;
            int index = -1;
            while(node != null && !keyReader.IsFinished) {
                prev = node;
                index = keyReader.ReadChunk();
                node = node[index];
            }
            if(node != null && node.IsTerminal) {
                prev[index] = new TrieNode<T>();
            }
        }
        public bool Contains(string key) {
            Guard.IsNotNull(key, nameof(key));
            return SearchNode(key) != null;
        }
        public bool Search(string key, out T tag) {
            Guard.IsNotNull(key, nameof(key));
            TrieNode<T> node = SearchNode(key);
            if(node != null)
                tag = node.Tag;
            else
                tag = default(T);
            return node != null;
        }

        internal TrieNode<T> SearchNode(string key) {
            TrieKeyReader keyReader = new TrieKeyReader(key);
            TrieNode<T> node = root;
            while(node != null && !keyReader.IsFinished) {
                node = node[keyReader.ReadChunk()];
            }
            return node != null && node.IsTerminal ? node : null;
        }
        internal TrieRoot<T> GetRoot() { return root; }
    }


    [DebuggerDisplay("TrieNode(Tag={tag},IsTerminal={isTerminal})")]
    class TrieNode<T> {
        readonly T tag;
        readonly bool isTerminal;
        TrieNode<T>[] children;

        public TrieNode(T tag, bool isTerminal) {
            this.tag = tag;
            this.children = null;
            this.isTerminal = isTerminal;
        }
        public TrieNode()
            : this(default(T), false) {
        }
        public TrieNode<T> this[int index] {
            get {
                CheckIndex(index, nameof(index));
                return children != null ? children[index] : null;
            }
            set {
                CheckIndex(index, nameof(index));
                EnsureChildren();
                children[index] = value;
            }
        }
        public bool ContainsNode(int index) {
            CheckIndex(index, nameof(index));
            return children != null && children[index] != null;
        }

        void EnsureChildren() {
            if(children == null)
                children = new TrieNode<T>[TrieConstants.AlphabetSize];
        }
        void CheckIndex(int index, string argument) {
            Guard.IsInRange(index, 0, TrieConstants.AlphabetSize - 1, nameof(index));
        }
        public T Tag { get { return tag; } }
        public bool IsTerminal { get { return isTerminal; } }
    }


    [DebuggerDisplay("TrieRoot")]
    class TrieRoot<T> : TrieNode<T> {
        public TrieRoot() : base(default(T), true) { }
    }


    static class TrieConstants {
        public const int ChunkSize = 4;
        public const int AlphabetSize = (1 << ChunkSize);
        public const int ChunkMask = (1 << ChunkSize) - 1;
        public const int ChunksPerSymbol = sizeof(char) * 8 / ChunkSize;
    }


    class TrieKeyReader {
        readonly string key;
        int charIndex;
        int chunkIndex;
        bool isFinished;

        public TrieKeyReader(string key) {
            this.key = key;
            this.charIndex = 0;
            this.chunkIndex = 0;
            this.isFinished = string.IsNullOrEmpty(key);
        }
        public int ReadChunk() {
            if(isFinished) return -1;

            if(AreNoChunks) {
                chunkIndex = 0;
                charIndex++;
            }
            int result = (key[charIndex] >> (TrieConstants.ChunkSize * chunkIndex)) & TrieConstants.ChunkMask;
            chunkIndex++;
            if(AreNoChunks && IsLastChar) isFinished = true;
            return result;
        }
        public bool IsFinished {
            get { return isFinished; }
        }

        bool IsLastChar {
            get { return charIndex == key.Length - 1; }
        }
        bool AreNoChunks {
            get { return chunkIndex == TrieConstants.ChunksPerSymbol; }
        }
    }


    #region Trie DebugView

    sealed class TrieDebugView<T> {
        readonly Trie<T> trie;
        TrieNodeDebugView<T> root;

        public TrieDebugView(Trie<T> trie) {
            Guard.IsNotNull(trie, nameof(trie));
            this.trie = trie;
        }
        public TrieNodeDebugView<T> Root {
            get { return root ?? (root = new TrieNodeDebugView<T>(-1, this.trie.GetRoot())); }
        }
    }


    [DebuggerDisplay("TrieNode(Chunk={Chunk},Tag={Tag},IsTerminal={IsTerminal})")]
    sealed class TrieNodeDebugView<T> {
        readonly int chunk;
        readonly TrieNode<T> node;

        public TrieNodeDebugView(int chunk, TrieNode<T> node) {
            this.chunk = chunk;
            this.node = node;
        }
        public int Chunk { get { return chunk; } }
        public T Tag { get { return node.Tag; } }
        public bool IsTerminal { get { return node.IsTerminal; } }

        public TrieNodeDebugView<T>[] Children {
            get {
                List<TrieNodeDebugView<T>> children = new List<TrieNodeDebugView<T>>(64);
                for(int n = 0; n < TrieConstants.AlphabetSize; n++) {
                    if(node[n] != null) children.Add(new TrieNodeDebugView<T>(n, node[n]));
                }
                return children.ToArray();
            }
        }
    }

    #endregion
}
