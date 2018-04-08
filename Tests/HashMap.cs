#if DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class HashMapTests {
        HashMap<HashMapDataKey, HashMapDataValue> hashMap;

        [TestInitialize]
        public void SetUp() {
            this.hashMap = new HashMap<HashMapDataKey, HashMapDataValue>();
        }
        [TestCleanup]
        public void TearDown() {
            this.hashMap = null;
        }
        [TestMethod]
        public void DefaultsTest() {
            CollectionAssertEx.IsEmpty(this.hashMap.Keys);
            CollectionAssertEx.IsEmpty(this.hashMap.Values);
            Assert.AreEqual(0, this.hashMap.Count);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            new HashMap<int, int>(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            new HashMap<int, int>(0);
        }
        [TestMethod]
        public void CtorGuardCase3Test() {
            HashMap<int, int> map = new HashMap<int, int>(1, null);
            map[1] = 1;
            map[2] = 2;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddGuardCase1Test() {
            this.hashMap.Add(null, new HashMapDataValue("1"));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddGuardCase2Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("2"));
        }
        [TestMethod]
        public void AddTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.Add(key3, val3);
            this.hashMap.AssertKeys(key1, key2, key3).AssertValues(val1, val2, val3);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RemoveGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap.Remove(null);
        }
        [TestMethod]
        public void RemoveTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.Add(key3, val3);

            Assert.IsTrue(this.hashMap.Remove(new HashMapDataKey(3)));
            this.hashMap.AssertKeys(key1, key2).AssertValues(val1, val2);

            Assert.IsFalse(this.hashMap.Remove(new HashMapDataKey(4)));
            this.hashMap.AssertKeys(key1, key2).AssertValues(val1, val2);

            Assert.IsTrue(this.hashMap.Remove(key1));
            this.hashMap.AssertKeys(key2).AssertValues(val2);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetIndexerGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            HashMapDataValue result = this.hashMap[null];
        }
        [TestMethod, ExpectedException(typeof(KeyNotFoundException))]
        public void GetIndexerGuardCase2Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("2"));
            HashMapDataValue result = this.hashMap[new HashMapDataKey(3)];
        }
        [TestMethod]
        public void GetIndexerTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);

            Assert.AreEqual(val1, this.hashMap[new HashMapDataKey(1)]);
            Assert.AreEqual(val2, this.hashMap[key2]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetIndexerGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap[null] = new HashMapDataValue("2");
        }
        [TestMethod]
        public void SetIndexerTest1() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap[key1] = val3;
            this.hashMap.AssertKeys(key1, key2).AssertValues(val3, val2);
            Assert.AreEqual(val3, this.hashMap[key1]);
        }
        [TestMethod]
        public void SetIndexerTest2() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.AssertKeys(key1, key2).AssertValues(val1, val2);
            this.hashMap[key3] = val3;
            this.hashMap.AssertKeys(key1, key2, key3).AssertValues(val1, val2, val3);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TryGetValueGuardCase1Test() {
            HashMapDataValue value;
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap.TryGetValue(null, out value);
        }
        [TestMethod]
        public void TryGetValueTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);

            HashMapDataValue value;
            Assert.IsTrue(this.hashMap.TryGetValue(key2, out value));
            Assert.AreSame(val2, value);
            Assert.IsTrue(this.hashMap.TryGetValue(key2, out value));
            Assert.AreSame(val2, value);
            Assert.IsFalse(this.hashMap.TryGetValue(key3, out value));
            Assert.IsNull(value);
            Assert.IsTrue(this.hashMap.TryGetValue(new HashMapDataKey(1), out value));
            Assert.AreSame(val1, value);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetOrAddGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("1"));
            this.hashMap.GetOrAdd(null, x => new HashMapDataValue("2"));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetOrAddGuardCase2Test() {
            HashMapDataKey key = new HashMapDataKey(1);
            this.hashMap.Add(key, new HashMapDataValue("1"));
            this.hashMap.GetOrAdd(key, null);
        }
        [TestMethod]
        public void GetOrAddTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.AssertKeys(key1, key2).AssertValues(val1, val2);

            Assert.AreSame(val1, this.hashMap.GetOrAdd(key1, x => val3));
            Assert.AreSame(val2, this.hashMap.GetOrAdd(key2, x => val3));
            this.hashMap.AssertKeys(key1, key2).AssertValues(val1, val2);

            Assert.AreSame(val3, this.hashMap.GetOrAdd(key3, x => val3));
            this.hashMap.AssertKeys(key1, key2, key3).AssertValues(val1, val2, val3);
        }
        [TestMethod]
        public void ClearTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            CollectionAssertEx.IsNotEmpty(this.hashMap.Keys);
            CollectionAssertEx.IsNotEmpty(this.hashMap.Values);

            this.hashMap.Clear();
            CollectionAssertEx.IsEmpty(this.hashMap.Keys);
            CollectionAssertEx.IsEmpty(this.hashMap.Values);
        }
        [TestMethod]
        public void CountTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.Add(key3, val3);
            Assert.AreEqual(3, this.hashMap.Count);

            this.hashMap.Remove(new HashMapDataKey(2));
            Assert.AreEqual(2, this.hashMap.Count);
            this.hashMap.Clear();
            Assert.AreEqual(0, this.hashMap.Count);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ContainsKeyGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.ContainsKey(null);
        }
        [TestMethod]
        public void ContainsKeyTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            Assert.IsTrue(this.hashMap.ContainsKey(new HashMapDataKey(1)));
            Assert.IsFalse(this.hashMap.ContainsKey(new HashMapDataKey(3)));
            Assert.IsTrue(this.hashMap.ContainsKey(key2));
            Assert.IsFalse(this.hashMap.ContainsKey(key3));
        }
        [TestMethod]
        public void ContainsValueTest() {
            HashMapDataKey key1 = new HashMapDataKey(1);
            HashMapDataKey key2 = new HashMapDataKey(2);
            HashMapDataKey key3 = new HashMapDataKey(3);

            HashMapDataValue val1 = new HashMapDataValue("val1");
            HashMapDataValue val2 = new HashMapDataValue("val2");
            HashMapDataValue val3 = new HashMapDataValue("val3");

            this.hashMap.Add(key1, val1);
            this.hashMap.Add(key2, val2);
            this.hashMap.Add(key3, null);
            Assert.IsTrue(this.hashMap.ContainsValue(new HashMapDataValue("val1")));
            Assert.IsFalse(this.hashMap.ContainsValue(new HashMapDataValue("val4")));
            Assert.IsTrue(this.hashMap.ContainsValue(null));
            Assert.IsFalse(this.hashMap.ContainsValue(val3));
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void KeysCollectionAddGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Keys.Add(new HashMapDataKey(2));
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void KeysCollectionRemoveGuardTest() {
            HashMapDataKey key = new HashMapDataKey(1);
            this.hashMap.Add(key, new HashMapDataValue("val1"));
            this.hashMap.Keys.Remove(key);
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void KeysCollectionClearGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Keys.Clear();
        }
        [TestMethod]
        public void KeysCollectionContainsTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            Assert.IsTrue(this.hashMap.ContainsKey(new HashMapDataKey(1)));
            Assert.IsFalse(this.hashMap.ContainsKey(new HashMapDataKey(3)));
        }
        [TestMethod]
        public void KeysCollectionIsReadOnlyTest() {
            Assert.IsTrue(this.hashMap.Keys.IsReadOnly);
        }
        [TestMethod]
        public void KeysCollectionCountTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            Assert.AreEqual(1, this.hashMap.Keys.Count);
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            Assert.AreEqual(2, this.hashMap.Keys.Count);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void KeysCollectionCopyToGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Keys.CopyTo(null, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void KeysCollectionCopyToGuardCase2Test() {
            HashMapDataKey[] keys = new HashMapDataKey[4];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Keys.CopyTo(keys, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void KeysCollectionCopyToGuardCase3Test() {
            HashMapDataKey[] keys = new HashMapDataKey[4];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Keys.CopyTo(keys, 4);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void KeysCollectionCopyToGuardCase4Test() {
            HashMapDataKey[] keys = new HashMapDataKey[8];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            this.hashMap.Keys.CopyTo(keys, 6);
        }
        [TestMethod]
        public void KeysCollectionCopyToTest() {
            HashMapDataKey[] keys = new HashMapDataKey[8];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            this.hashMap.Keys.CopyTo(keys, 5);
            HashMapDataKey[] expectedKeys = new HashMapDataKey[] {
                null, null, null, null, null,
                new HashMapDataKey(1),
                new HashMapDataKey(2),
                new HashMapDataKey(3)
            };
            CollectionAssertEx.AreEquivalent(expectedKeys, keys);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void KeysCollectionEnumeratorGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            foreach(var key in this.hashMap.Keys) {
                this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            }
        }
        [TestMethod]
        public void KeysCollectionEnumeratorSimpleTest() {
            List<HashMapDataKey> keyList = new List<HashMapDataKey>();
            foreach(var key in this.hashMap.Keys) {
                keyList.Add(key);
            }
            CollectionAssertEx.IsEmpty(keyList);
        }
        [TestMethod]
        public void KeysCollectionEnumeratorTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            List<HashMapDataKey> keyList = new List<HashMapDataKey>(4);
            foreach(var key in this.hashMap.Keys) {
                keyList.Add(key);
            }
            HashMapDataKey[] expectedKeys = new HashMapDataKey[] {
                new HashMapDataKey(1),
                new HashMapDataKey(2),
                new HashMapDataKey(3)
            };
            CollectionAssertEx.AreEquivalent(expectedKeys, keyList);
        }
        [TestMethod]
        public void KeysCollectionEnumeratorHeavyTest() {
            var numberList = Enumerable.Range(1, 200);
            HashMapDataKey[] keys = numberList
                .Select(x => new HashMapDataKey(x))
                .ToArray();
            HashMapDataValue[] values = numberList
                .Select(x => new HashMapDataValue(x.ToString()))
                .ToArray();
            for(int n = 0; n < keys.Length; n++) {
                this.hashMap.Add(keys[n], values[n]);
            }
            CollectionAssertEx.AreEquivalent(keys, this.hashMap.Keys);
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ValuesCollectionAddGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Values.Add(new HashMapDataValue("value2"));
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ValuesCollectionRemoveGuardTest() {
            HashMapDataValue value = new HashMapDataValue("val1");
            this.hashMap.Add(new HashMapDataKey(1), value);
            this.hashMap.Values.Remove(value);
        }
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void ValuesCollectionClearGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Values.Clear();
        }
        [TestMethod]
        public void ValuesCollectionContainsTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            Assert.IsTrue(this.hashMap.ContainsValue(new HashMapDataValue("val1")));
            Assert.IsFalse(this.hashMap.ContainsValue(new HashMapDataValue("val3")));
        }
        [TestMethod]
        public void ValuesCollectionIsReadOnlyTest() {
            Assert.IsTrue(this.hashMap.Values.IsReadOnly);
        }
        [TestMethod]
        public void ValuesCollectionCountTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            Assert.AreEqual(1, this.hashMap.Values.Count);
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            Assert.AreEqual(2, this.hashMap.Values.Count);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ValuesCollectionCopyToGuardCase1Test() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Values.CopyTo(null, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValuesCollectionCopyToGuardCase2Test() {
            HashMapDataValue[] values = new HashMapDataValue[4];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Values.CopyTo(values, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValuesCollectionCopyToGuardCase3Test() {
            HashMapDataValue[] values = new HashMapDataValue[4];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Values.CopyTo(values, 4);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValuesCollectionCopyToGuardCase4Test() {
            HashMapDataValue[] values = new HashMapDataValue[8];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            this.hashMap.Values.CopyTo(values, 6);
        }
        [TestMethod]
        public void ValuesCollectionCopyToTest() {
            HashMapDataValue[] values = new HashMapDataValue[8];
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            this.hashMap.Values.CopyTo(values, 5);
            HashMapDataValue[] expectedValues = new HashMapDataValue[] {
                null, null, null, null, null,
                new HashMapDataValue("val1"),
                new HashMapDataValue("val2"),
                new HashMapDataValue("val3")
            };
            CollectionAssertEx.AreEquivalent(expectedValues, values);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ValuesCollectionEnumeratorGuardTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            foreach(var value in this.hashMap.Values) {
                this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            }
        }
        [TestMethod]
        public void ValuesCollectionEnumeratorSimpleTest() {
            List<HashMapDataValue> valueList = new List<HashMapDataValue>();
            foreach(var value in this.hashMap.Values) {
                valueList.Add(value);
            }
            CollectionAssertEx.IsEmpty(valueList);
        }
        [TestMethod]
        public void ValuesCollectionEnumeratorTest() {
            this.hashMap.Add(new HashMapDataKey(1), new HashMapDataValue("val1"));
            this.hashMap.Add(new HashMapDataKey(2), new HashMapDataValue("val2"));
            this.hashMap.Add(new HashMapDataKey(3), new HashMapDataValue("val3"));
            List<HashMapDataValue> valueList = new List<HashMapDataValue>(4);
            foreach(var value in this.hashMap.Values) {
                valueList.Add(value);
            }
            HashMapDataValue[] expectedValues = new HashMapDataValue[] {
                new HashMapDataValue("val1"),
                new HashMapDataValue("val2"),
                new HashMapDataValue("val3")
            };
            CollectionAssertEx.AreEquivalent(expectedValues, valueList);
        }
        [TestMethod]
        public void ValuesCollectionEnumeratorHeavyTest() {
            var numberList = Enumerable.Range(1, 100);
            HashMapDataKey[] keys = numberList
                .Select(x => new HashMapDataKey(x))
                .ToArray();
            HashMapDataValue[] values = numberList
                .Select(x => new HashMapDataValue(x.ToString()))
                .ToArray();
            for(int n = 0; n < keys.Length; n++) {
                this.hashMap.Add(keys[n], values[n]);
            }
            CollectionAssertEx.AreEquivalent(values, this.hashMap.Values);
        }
        [TestMethod]
        public void DefaultEqualityComparerTest1() {
            HashMap<TestSimpleTracebleHashMapKey, string> map = new HashMap<TestSimpleTracebleHashMapKey, string>();
            TestSimpleTracebleHashMapKey key1 = new TestSimpleTracebleHashMapKey(1);
            TestSimpleTracebleHashMapKey key2 = new TestSimpleTracebleHashMapKey(2);
            map.Add(key1, "1");
            map.Add(key2, "2");
            Assert.AreEqual("->GetHashCode;", key1.Trace);
            Assert.AreEqual("->GetHashCode;->Equals;", key2.Trace);
        }
        [TestMethod]
        public void DefaultEqualityComparerTest2() {
            HashMap<TestIIEquatableTracebleHashMapKey, string> map = new HashMap<TestIIEquatableTracebleHashMapKey, string>();
            TestIIEquatableTracebleHashMapKey key1 = new TestIIEquatableTracebleHashMapKey(1);
            TestIIEquatableTracebleHashMapKey key2 = new TestIIEquatableTracebleHashMapKey(2);
            map.Add(key1, "1");
            map.Add(key2, "2");
            Assert.AreEqual("->Object.GetHashCode;", key1.Trace);
            Assert.AreEqual("->Object.GetHashCode;->IEquatable.Equals;", key2.Trace);
        }
        [TestMethod]
        public void CustomEqualityComparerTest() {
            TestHashMapKeyEqualityComparer keyComparer = new TestHashMapKeyEqualityComparer();
            HashMap<TestSimpleTracebleHashMapKey, string> map = new HashMap<TestSimpleTracebleHashMapKey, string>(17, keyComparer);
            TestSimpleTracebleHashMapKey key1 = new TestSimpleTracebleHashMapKey(1);
            TestSimpleTracebleHashMapKey key2 = new TestSimpleTracebleHashMapKey(2);
            map.Add(key1, "1");
            map.Add(key2, "2");
            StringAssertEx.IsNullOrEmpty(key1.Trace);
            StringAssertEx.IsNullOrEmpty(key2.Trace);
            Assert.AreEqual("->GetHashCode;->GetHashCode;->Equals;", keyComparer.Trace);
        }
    }

    #region HashMapAssert

    static class HashMapAssert {
        public static HashMap<HashMapDataKey, HashMapDataValue> AssertKeys(this HashMap<HashMapDataKey, HashMapDataValue> @this, params HashMapDataKey[] keys) {
            CollectionAssertEx.AreEquivalent(keys, @this.Keys);
            return @this;
        }
        public static HashMap<HashMapDataKey, HashMapDataValue> AssertValues(this HashMap<HashMapDataKey, HashMapDataValue> @this, params HashMapDataValue[] values) {
            CollectionAssertEx.AreEquivalent(values, @this.Values);
            return @this;
        }
    }

    #endregion

    #region HashMapDataKey

    [DebuggerDisplay("HashMapDataKey(ID={ID})")]
    public class HashMapDataKey {
        readonly int id;

        public HashMapDataKey(int id) {
            this.id = id;
        }
        public override bool Equals(object obj) {
            HashMapDataKey other = obj as HashMapDataKey;
            return other != null && ID == other.ID;
        }
        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        public int ID { get { return id; } }
    }

    #endregion

    #region HashMapDataValue

    [DebuggerDisplay("HashMapDataValue(Value={Value})")]
    public class HashMapDataValue {
        readonly string value;

        public HashMapDataValue(string value) {
            this.value = value;
        }
        public override bool Equals(object obj) {
            HashMapDataValue other = obj as HashMapDataValue;
            return other != null && Equals(Value, other.Value);
        }
        public override int GetHashCode() {
            return Value.GetHashCode();
        }
        public string Value { get { return value; } }
    }

    #endregion


    #region TestSimpleTracebleHashMapKey

    [DebuggerDisplay("TestSimpleTracebleHashMapKey(Key={Key})")]
    class TestSimpleTracebleHashMapKey {
        readonly int key;
        string trace;

        public TestSimpleTracebleHashMapKey(int key) {
            this.trace = string.Empty;
            this.key = key;
        }
        public override bool Equals(object obj) {
            this.trace += "->Equals;";
            TestSimpleTracebleHashMapKey other = obj as TestSimpleTracebleHashMapKey;
            return other != null && other.key == key;
        }
        public override int GetHashCode() {
            this.trace += "->GetHashCode;";
            return 0;
        }
        public int Key { get { return key; } }
        public string Trace { get { return trace; } }
    }

    #endregion

    #region TestIIEquatableTracebleHashMapKey

    [DebuggerDisplay("TestIIEquatableTracebleHashMapKey(key={key})")]
    class TestIIEquatableTracebleHashMapKey : IEquatable<TestIIEquatableTracebleHashMapKey> {
        readonly int key;
        string trace;

        public TestIIEquatableTracebleHashMapKey(int key) {
            this.trace = string.Empty;
            this.key = key;
        }
        public override bool Equals(object obj) {
            this.trace += "->Object.Equals;";
            TestIIEquatableTracebleHashMapKey other = obj as TestIIEquatableTracebleHashMapKey;
            return other != null && other.key == key;
        }
        public override int GetHashCode() {
            this.trace += "->Object.GetHashCode;";
            return 0;
        }
        public string Trace { get { return trace; } }

        #region IEquatable

        bool IEquatable<TestIIEquatableTracebleHashMapKey>.Equals(TestIIEquatableTracebleHashMapKey other) {
            this.trace += "->IEquatable.Equals;";
            return key == other.key;
        }

        #endregion

    }

    #endregion

    #region TestHashMapKeyEqualityComparer

    class TestHashMapKeyEqualityComparer : IEqualityComparer<TestSimpleTracebleHashMapKey> {
        string trace;
        public TestHashMapKeyEqualityComparer() {
            this.trace = string.Empty;
        }
        #region IEqualityComparer

        bool IEqualityComparer<TestSimpleTracebleHashMapKey>.Equals(TestSimpleTracebleHashMapKey x, TestSimpleTracebleHashMapKey y) {
            this.trace += "->Equals;";
            return x.Key == y.Key;
        }
        int IEqualityComparer<TestSimpleTracebleHashMapKey>.GetHashCode(TestSimpleTracebleHashMapKey obj) {
            this.trace += "->GetHashCode;";
            return 0;
        }

        #endregion

        public string Trace { get { return trace; } }
    }

    #endregion

}
#endif