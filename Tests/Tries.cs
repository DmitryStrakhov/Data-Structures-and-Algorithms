#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrieNode = System.Collections.Generic.KeyValuePair<string, int?>;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class TrieTests {
        Trie<int?> trie;

        [TestInitialize]
        public void SetUp() {
            this.trie = new Trie<int?>();
        }
        [TestCleanup]
        public void TearDown() {
            this.trie = null;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InsertGuardCase1Test() {
            trie.Insert(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InsertGuardCase2Test() {
            trie.Insert(null, 0);
        }
        [TestMethod]
        public void InsertTest() {
            trie.Insert("Word1");
            trie.Insert("OtherWord1", 111);
            trie.Insert("Word2", 22);
            trie.Insert("Word2", 33);
            trie.Insert("OtherWord2", 222);
            trie.Insert("Word3");
            trie.Insert("OtherWord3");

            trie.AssertNodes(
                new TrieNode("Word1", null),
                new TrieNode("OtherWord1", 111),
                new TrieNode("Word2", 22),
                new TrieNode("OtherWord2", 222),
                new TrieNode("Word3", null),
                new TrieNode("OtherWord3", null));
        }
        [TestMethod]
        public void InsertTest2() {
            trie.Insert("Word1", 1);
            trie.Insert("Word2", 2);
            trie.Insert("Word3", 3);
            trie.Delete("Word1");
            trie.AssertNodes(new TrieNode("Word2", 2), new TrieNode("Word3", 3));
            trie.Insert("Word1", 11);
            trie.AssertNodes(new TrieNode("Word1", 11), new TrieNode("Word2", 2), new TrieNode("Word3", 3));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteGuardTest() {
            trie.Insert("Word1");
            trie.Delete(null);
        }
        [TestMethod]
        public void DeleteTest() {
            trie.Insert("Word3", null);
            trie.Insert("Word2", 22);
            trie.Insert("Word1", null);
            trie.Insert("OtherWord3");
            trie.Insert("OtherWord2");
            trie.Insert("OtherWord1");

            trie.Delete("Word2");
            trie.Delete("OtherWord2");

            trie.AssertNodes(
                new TrieNode("Word3", null),
                new TrieNode("Word1", null),
                new TrieNode("OtherWord3", null),
                new TrieNode("OtherWord1", null));

            trie.Delete("OtherWord1");
            trie.Delete("Word3");
            trie.AssertNodes(
                new TrieNode("Word1", null),
                new TrieNode("OtherWord3", null));

            trie.Delete("OtherWord3");
            trie.Delete("OtherWord3");
            trie.AssertNodes(new TrieNode("Word1", null));

            trie.Delete("Word1");
            CollectionAssertEx.IsEmpty(TrieTestHelper.CollectKeys(trie));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ContainsGuardTest() {
            trie.Insert("Word1");
            trie.Contains(null);
        }
        [TestMethod]
        public void ContainsTest1() {
            trie.Insert("Word1", 1);
            trie.Insert("Word2", 2);
            trie.Insert("OtherWord1", 3);
            trie.Insert("OtherWord2", 4);

            Assert.IsTrue(trie.Contains("Word1"));
            Assert.IsTrue(trie.Contains("Word2"));
            Assert.IsTrue(trie.Contains("OtherWord1"));
            Assert.IsTrue(trie.Contains("OtherWord2"));
            Assert.IsTrue(trie.Contains(""));
            Assert.IsFalse(trie.Contains("X"));
        }
        [TestMethod]
        public void ContainsTest2() {
            trie.Insert("Word1", 1);
            trie.Insert("Word2", 2);
            trie.Insert("Word3", 3);
            trie.Delete("Word1");
            trie.Delete("Word3");
            Assert.IsFalse(trie.Contains("Word1"));
            Assert.IsTrue(trie.Contains("Word2"));
            Assert.IsFalse(trie.Contains("Word3"));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardTest() {
            trie.Insert("Word1", 1);
            int? tag;
            trie.Search(null, out tag);
        }
        [TestMethod]
        public void SearchTest() {
            trie.Insert("Word1", 111);
            trie.Insert("Word2");
            trie.Insert("OtherWord1", 22);
            trie.Insert("OtherWord2", 33);

            trie.AssertSearch("OtherWord2", true, 33);
            trie.AssertSearch("OtherWord1", true, 22);
            trie.AssertSearch("Word2", true, null);
            trie.AssertSearch("Word1", true, 111);

            trie.AssertSearch("", true, null);
            trie.AssertSearch("NEW", false, null);
        }
    }


    [TestClass]
    public class TrieKeyReaderTests {
        [TestMethod]
        public void ReadChunkSimpleTest() {
            Assert.IsTrue(new TrieKeyReader("").IsFinished);
        }
        [TestMethod]
        public void ReadChunkTest() {
            TrieKeyReader keyReader = new TrieKeyReader("Other1");

            List<int> result = new List<int>(64);
            while(!keyReader.IsFinished) {
                result.Add(keyReader.ReadChunk());
            }
            int[] expected = new int[] {
                0xf, 0x4, 0x0, 0x0,
                0x4, 0x7, 0x0, 0x0,
                0x8, 0x6, 0x0, 0x0,
                0x5, 0x6, 0x0, 0x0,
                0x2, 0x7, 0x0, 0x0,
                0x1, 0x3, 0x0, 0x0,
            };
            CollectionAssert.AreEqual(expected, result);
        }
    }



    static class TrieTestExtensions {

        public static void AssertNodes(this Trie<int?> @this, params TrieNode[] expected) {
            CollectionAssert.AreEquivalent(expected, TrieTestHelper.CollectKeys(@this));
        }
        public static void AssertSearch(this Trie<int?> @this, string key, bool expectedResult, int? expectedTag) {
            int? tag;
            Assert.AreEqual(expectedResult, @this.Search(key, out tag));
            Assert.AreEqual(expectedTag, tag);
        }
    }


    static class TrieTestHelper {
        public static TrieNode[] CollectKeys(Trie<int?> trie) {
            List<TrieNode> keyList = new List<TrieNode>(16);
            FillKeys(trie.GetRoot(), new List<int>(), keyList);
            return keyList.ToArray();
        }
        static void FillKeys(TrieNode<int?> node, List<int> chunkList, List<TrieNode> keyList) {
            for(int n = 0; n < TrieConstants.AlphabetSize; n++) {
                TrieNode<int?> child = node[n];
                if(child != null) {
                    chunkList.Add(n);
                    FillKeys(child, chunkList, keyList);
                    if(child.IsTerminal) {
                        keyList.Add(new TrieNode(CreateKey(chunkList), child.Tag));
                    }
                    chunkList.RemoveLastItem();
                }
            }
        }
        static string CreateKey(List<int> chunkList) {
            Guard.IsTrue((chunkList.Count & 3) == 0);

            char[] chars = new char[chunkList.Count >> 2];
            for(int n = 0; n < chunkList.Count; n += 4) {
                chars[n / 4] = (char)(chunkList[n] | (chunkList[n + 1] << 4) | (chunkList[n + 2] << 8) | (chunkList[n + 3] << 12));
            }
            return new string(chars);
        }
    }
}
#endif