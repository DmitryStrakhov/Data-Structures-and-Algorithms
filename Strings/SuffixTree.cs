using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("SuffixTree()")]
    public sealed class SuffixTree {
        readonly string text;
        readonly SuffixTreeNode root;
        readonly char terminalSymbol;
        readonly SuffixTreeNodeResolver resolver;

        public SuffixTree(string text)
            : this((char)0, text) {
        }
        public SuffixTree(char terminalSymbol, string text) {
            Guard.IsNotNull(text, nameof(text));
            this.terminalSymbol = terminalSymbol;
            this.text = text;
            this.root = SuffixTreeBuilder.Build(terminalSymbol, text);
            this.resolver = new SuffixTreeNodeResolver(root);
        }
        public bool Contains(string pattern) {
            Guard.IsNotNull(pattern, nameof(pattern));
            return resolver.Resolve(pattern) != null;
        }
        internal SuffixTreeNode Root { get { return root; } }
        public string Text { get { return text; } }
    }


    [DebuggerDisplay("SuffixTreeNode(IsTerminal={IsTerminal})")]
    [DebuggerTypeProxy(typeof(SuffixTreeNodeDebugView))]
    sealed class SuffixTreeNode {
        readonly bool isTerminal;
        readonly int index;
        HashMap<string, SuffixTreeEdge> keyToEdgeMap;
        HashMap<char, SuffixTreeEdge> symbolToEdgeMap;

        public SuffixTreeNode(int index, bool isTerminal) {
            this.index = index;
            this.isTerminal = isTerminal;
            this.keyToEdgeMap = null;
            this.symbolToEdgeMap = null;
        }

        public void AddChild(string label, SuffixTreeNode node) {
            Guard.IsNotNullOrEmpty(label);
            SuffixTreeEdge edge = new SuffixTreeEdge(label, this, node);
            KeyToEdgeMap.Add(label, edge);
            SymbolToEdgeMap.Add(label[0], edge);
        }
        public string[] GetKeys() {
            return GetOrderedKeys().ToArray();
        }
        public SuffixTreeNode[] GetChildren() {
            return GetOrderedKeys().Select(x => keyToEdgeMap[x].EndNode).ToArray();
        }
        public SuffixTreeEdge GetEdge(char symbol) {
            if(symbolToEdgeMap == null) return null;
            return SymbolToEdgeMap.GetValueOrDefault(symbol);
        }

        private IEnumerable<string> GetOrderedKeys() {
            if(keyToEdgeMap == null) return new string[0];
            return keyToEdgeMap.Keys.OrderBy(x => x);
        }
        public bool IsTerminal { get { return isTerminal; } }
        public int Index { get { return index; } }

        private HashMap<string, SuffixTreeEdge> KeyToEdgeMap {
            get { return keyToEdgeMap ?? (keyToEdgeMap = new HashMap<string, SuffixTreeEdge>()); }
        }
        private HashMap<char, SuffixTreeEdge> SymbolToEdgeMap {
            get { return symbolToEdgeMap ?? (symbolToEdgeMap = new HashMap<char, SuffixTreeEdge>()); }
        }
    }


    [DebuggerDisplay("SuffixTreeEdge(Label={Label})")]
    sealed class SuffixTreeEdge : EquatableObject<SuffixTreeEdge> {
        readonly string label;
        readonly SuffixTreeNode startNode;
        readonly SuffixTreeNode endNode;

        public SuffixTreeEdge(string label, SuffixTreeNode startNode, SuffixTreeNode endNode) {
            this.label = label;
            this.startNode = startNode;
            this.endNode = endNode;
        }

        #region EqualsTo

        protected override bool EqualsTo(SuffixTreeEdge other) {
            return string.Equals(label, other.label, StringComparison.Ordinal) && ReferenceEquals(startNode, other.startNode) && ReferenceEquals(endNode, other.endNode);
        }

        #endregion

        public SuffixTreeNode StartNode { get { return startNode; } }
        public SuffixTreeNode EndNode { get { return endNode; } }
        public string Label { get { return label; } }
    }


    sealed class SuffixTreeNodeResolver {
        readonly SuffixTreeNode root;

        public SuffixTreeNodeResolver(SuffixTreeNode root) {
            Guard.IsNotNull(root, nameof(root));
            this.root = root;
        }
        public SuffixTreeNode Resolve(string @string) {
            int index = -1;
            SuffixTreeNode node = root;
            SuffixTreeEdge edge = null;

            for(int n = 0; n < @string.Length; n++) {
                char symbol = @string[n];
                if(index == -1) {
                    edge = node.GetEdge(symbol);
                    if(edge == null) return null;
                    node = edge.EndNode;
                    if(edge.Label.Length > 1) index = 1;
                }
                else {
                    if(symbol != edge.Label[index]) return null;
                    if(++index == edge.Label.Length) {
                        index = -1;
                    }
                }
            }
            return node;
        }
    }


    static class SuffixTreeBuilder {
        public static SuffixTreeNode Build(char terminalSymbol, string text) {
            string fullText = text + terminalSymbol;
            return BuildCore(BuildSuffixGroup(fullText), fullText.Length);
        }
        static SuffixTreeNode BuildCore(SuffixGroup group, int index) {
            if(group.Size == 1) return new SuffixTreeNode(index, true);

            SuffixTreeNode node = new SuffixTreeNode(-1, false);
            foreach(SuffixGroup subGroup in group.BuildGroups()) {
                string prefix = null;
                SuffixGroup reducedGroup = subGroup.Reduce(out prefix);
                SuffixTreeNode child = BuildCore(reducedGroup, index - prefix.Length);
                node.AddChild(prefix, child);
            }
            return node;
        }
        internal static SuffixGroup BuildSuffixGroup(string text) {
            if(text.Length == 1) return new SuffixGroup(text);
            int groupSz = text.Length - 1;

            string[] suffixes = new string[groupSz];
            for(int n = 0; n < groupSz; n++) {
                suffixes[n] = text.Substring(groupSz - n - 1);
            }
            return new SuffixGroup(suffixes);
        }
    }


    [DebuggerDisplay("SuffixGroup(Size={Size})")]
    sealed class SuffixGroup {
        readonly string[] suffixes;

        public SuffixGroup(params string[] suffixes) {
            if(suffixes.Length == 0) Guard.Fail();
            this.suffixes = suffixes;
        }

        public IEnumerable<SuffixGroup> BuildGroups() {
            var groups = suffixes.GroupBy(x => x[0]);

            foreach(var group in groups) {
                yield return new SuffixGroup(group.OrderBy(x => x.Length).ToArray());
            }
        }
        public SuffixGroup Reduce(out string prefix) {
            if(Size == 1) {
                prefix = suffixes[0];
                return this;
            }
            prefix = StringUtils.CalculateLargePrefix(suffixes, false);
            int prefixSz = prefix.Length;
            return new SuffixGroup(suffixes.Transform(x => x.Substring(prefixSz)));
        }

        #region Equals & GetHashCode

        public override bool Equals(object obj) {
            SuffixGroup other = obj as SuffixGroup;
            return other != null && Enumerable.SequenceEqual(suffixes, other.suffixes);
        }
        public override int GetHashCode() {
            int hashCode = 0;
            for(int n = 0; n < suffixes.Length; n++) {
                hashCode ^= suffixes[n].GetHashCode();
            }
            return hashCode;
        }

        #endregion

        public int Size {
            get { return suffixes.Length; }
        }
        public string[] Suffixes { get { return suffixes; } }
    }


    #region Debug Views

    sealed class SuffixTreeNodeDebugView {
        readonly SuffixTreeNode node;
        readonly string[] keys;
        readonly SuffixTreeNode[] children;

        public SuffixTreeNodeDebugView(SuffixTreeNode node) {
            Guard.IsNotNull(node, nameof(node));
            this.node = node;
            this.keys = node.GetKeys();
            this.children = node.GetChildren();
        }
        public bool IsTerminal { get { return node.IsTerminal; } }
        public int Index { get { return node.Index; } }
        public string[] Keys { get { return keys; } }
        public SuffixTreeNode[] Children { get { return children; } }
    }

    #endregion
}
