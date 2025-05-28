using DataStructuresLab;
using DataStructuresLab.Model;
using System;
using CarLibrary;
using DataStructuresLab.ModelHashSet;

namespace TestProject
{
    [TestClass]
    public class LinkedListTests
    {
        #region Юнит-тесты двусвязного списка
        [TestMethod]
        public void TestConstructorWithData()
        {
            // Arrange
            string expectedData = "BMW";

            // Act
            Item<string> item = new Item<string>(expectedData);

            // Assert
            Assert.AreEqual(expectedData, item.Data);
        }

        [TestMethod]
        public void TestDataSetAndGet()
        {
            // Arrange
            Item<int> item = new Item<int>();
            int expected = 42;

            // Act
            item.Data = expected;

            // Assert
            Assert.AreEqual(expected, item.Data);
        }

        [TestMethod]
        public void TestDefaultConstructor()
        {
            // Act
            Item<string> item = new Item<string>();

            // Assert
            Assert.IsNull(item.Data);
        }

        [TestMethod]
        public void TestPreviousLink()
        {
            // Arrange
            Item<int> item1 = new Item<int>(1);
            Item<int> item2 = new Item<int>(2);

            // Act
            item2.Previous = item1;

            // Assert
            Assert.AreEqual(item1, item2.Previous);
            Assert.IsNull(item1.Previous);
        }

        [TestMethod]
        public void TestNextLink()
        {
            // Arrange
            Item<int> item1 = new Item<int>(1);
            Item<int> item2 = new Item<int>(2);

            // Act
            item1.Next = item2;

            // Assert
            Assert.AreEqual(item2, item1.Next);
            Assert.IsNull(item2.Next);
        }

        [TestMethod]
        public void TestToStringItem()
        {
            // Arrange
            Item<string> item = new Item<string>();

            // Act
            string result = item.ToString();

            // Assert
            Assert.AreEqual("null", result);
        }

        [TestMethod]
        public void TestToStringWithData()
        {
            // Arrange
            Item<int> item = new Item<int>(42);

            // Act
            string result = item.ToString();

            // Assert
            Assert.AreEqual("42", result);
        }

        [TestMethod]
        public void TestConstructorCreatesEmptyList()
        {
            // Act
            var list = new DuplexLinkedList<int>();

            // Assert
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod]
        public void TestAddSetsCorrectLinks()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();

            // Act
            list.Add(1);
            list.Add(2);

            // Assert
            Assert.AreEqual(1, list.Head.Data);
            Assert.AreEqual(2, list.Tail.Data);
            Assert.AreEqual(list.Head.Next, list.Tail);
            Assert.AreEqual(list.Tail.Previous, list.Head);
        }

        [TestMethod]
        public void TestAddRangeAddElements()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            var items = new[] { 1, 2, 3 };

            // Act
            list.AddRange(items);

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list.Head.Data);
            Assert.AreEqual(3, list.Tail.Data);
        }

        [TestMethod]
        public void TestClearList()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            list.AddRange(new[] { 1, 2, 3 });

            // Act
            list.Clear();

            // Assert
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod]
        public void TestContainsReturnsTrueForExistingItem()
        {
            // Arrange
            var list = new DuplexLinkedList<string>();
            list.Add("Lada");

            // Assert
            Assert.IsTrue(list.Contains("Lada"));
            Assert.IsFalse(list.Contains("Capa"));
        }

        [TestMethod]
        public void TestRemoveExistingItem()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            list.AddRange(new[] { 1, 2, 3 });

            // Act
            bool result = list.Remove(2);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.Contains(2));
        }

        [TestMethod]
        public void TestConstructorWithElement()
        {
            // Arrange
            int expected = 42;

            // Act
            var list = new DuplexLinkedList<int>(expected);

            // Assert
            Assert.AreEqual(expected, list.Head.Data);
            Assert.AreEqual(expected, list.Tail.Data);
            Assert.AreEqual(1, list.Count);
            Assert.IsFalse(list.IsEmpty);
        }

        [TestMethod]
        public void TestReverseList()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            list.AddRange(new[] { 1, 2, 3 });

            // Act
            list.Reverse();

            // Assert
            Assert.AreEqual(3, list.Head.Data);
            Assert.AreEqual(1, list.Tail.Data);
            Assert.AreEqual(2, list.Head.Next.Data);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            list.AddRange(new[] { 1, 2, 3 });
            var array = new int[3];

            // Act
            list.CopyTo(array, 0);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, array);
        }

        [TestMethod]
        public void TestRemoveFromToEnd()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            list.AddRange(new[] { 1, 2, 3, 4, 5 });

            // Act
            int removed = list.RemoveFromToEnd(3);

            // Assert
            Assert.AreEqual(3, removed);
            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.Contains(4));
            Assert.IsFalse(list.Contains(5));
        }

        [TestMethod]
        public void TestAddAtOddPositionsToEmptyList()
        {
            // Arrange
            var list = new DuplexLinkedList<int>();
            int counter = 1;

            // Act
            list.AddAtOddPositions(() => counter++ * 10, 2);

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(20, list.Head.Data);
            Assert.AreEqual(20, list.Tail.Data);
        }
        #endregion

        #region Юнит-тесты хеш-таблицы

        [TestClass]
        public class HashTableTests
        {
            [TestMethod]
            public void TestDefaultConstructorHT()
            {
                // Act
                var item = new ItemTable<int>();

                // Assert
                Assert.AreEqual(default(int), item.Data);
                Assert.IsNull(item.Next);
            }

            [TestMethod]
            public void TestConstructorWithDataHT()
            {
                // Arrange
                string testData = "Тест";

                // Act
                var item = new ItemTable<string>(testData);

                // Assert
                Assert.AreEqual(testData, item.Data);
                Assert.IsNull(item.Next);
            }

            [TestMethod]
            public void TestToStringWithDataHT()
            {
                // Arrange
                var item = new ItemTable<double>(2.27);

                // Act
                var result = item.ToString();

                // Assert
                Assert.AreEqual("2,27", result);
            }

            [TestMethod]
            public void TestToStringWithNullData()
            {
                // Arrange
                var item = new ItemTable<string>();

                // Act
                var result = item.ToString();

                // Assert
                Assert.AreEqual("null", result);
            }

            [TestMethod]
            public void TestDataPropertySetter()
            {
                // Arrange
                var item = new ItemTable<int>(10);
                int newValue = 20;

                // Act
                item.Data = newValue;

                // Assert
                Assert.AreEqual(newValue, item.Data);
            }

            [TestMethod]
            public void TestNextPropertySetter()
            {
                // Arrange
                var firstItem = new ItemTable<string>("first");
                var secondItem = new ItemTable<string>("second");

                // Act
                firstItem.Next = secondItem;

                // Assert
                Assert.AreSame(secondItem, firstItem.Next);
            }

            [TestMethod]
            public void TestToStringWithCustomObject()
            {
                // Arrange
                var transport = new Transport(5, "Лада", 2010, "Зелёный", 25000, 120);
                var item = new ItemTable<Transport>(transport);

                // Act
                string result = item.ToString();

                // Assert
                Assert.AreEqual(transport.ToString(), result);
            }

            [TestMethod]
            public void TestChainingItems()
            {
                // Arrange
                var first = new ItemTable<int>(1);
                var second = new ItemTable<int>(2);
                var third = new ItemTable<int>(3);

                // Act
                first.Next = second;
                second.Next = third;

                // Assert
                Assert.AreSame(second, first.Next);
                Assert.AreSame(third, second.Next);
                Assert.IsNull(third.Next);
            }

            [TestMethod]
            public void EntryShouldSetKeyAndValue()
            {
                // Arrange
                int expectedKey = 42;
                string expectedValue = "Тест";

                // Act
                var entry = new Entry<int, string>(expectedKey, expectedValue);

                // Assert
                Assert.AreEqual(expectedKey, entry.Key);
                Assert.AreEqual(expectedValue, entry.Value);
            }
            [TestMethod]
            public void InitializeEmptyTable()
            {
                // Act
                var table = new HashTable<int, string>();

                // Assert
                Assert.AreEqual(0, table.Count);
                Assert.IsFalse(table.Any());
            }

            [TestMethod]
            public void AddShouldIncreaseCount()
            {
                // Arrange
                var table = new HashTable<int, string>();

                // Act
                table.Add(1, "ам");

                // Assert
                Assert.AreEqual(1, table.Count);
            }

            [TestMethod]
            public void ContainsExistingKeyShouldReturnTrue()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");

                // Act
                bool contains = table.Contains(1);

                // Assert
                Assert.IsTrue(contains);
            }

            [TestMethod]
            public void ContainsNonExistingKeyShouldReturnFalse()
            {
                // Arrange
                var table = new HashTable<int, string>();

                // Act
                bool contains = table.Contains(1);

                // Assert
                Assert.IsFalse(contains);
            }

            [TestMethod]
            public void GetExistingKeyShouldReturnValue()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");

                // Act
                var value = table.Get(1);

                // Assert
                Assert.AreEqual("ам", value);
            }

            [TestMethod]
            public void RemoveExistingKeyShouldReturnTrueAndDecreaseCount()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");

                // Act
                bool removed = table.Remove(1);

                // Assert
                Assert.IsTrue(removed);
                Assert.AreEqual(0, table.Count);
            }

            [TestMethod]
            public void RemoveNonExistingKeyShouldReturnFalse()
            {
                // Arrange
                var table = new HashTable<int, string>();

                // Act
                bool removed = table.Remove(1);

                // Assert
                Assert.IsFalse(removed);
            }

            [TestMethod]
            public void Clear_ShouldResetTable()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");
                table.Add(2, "ням");

                // Act
                table.Clear();

                // Assert
                Assert.AreEqual(0, table.Count);
                Assert.IsFalse(table.Contains(1));
                Assert.IsFalse(table.Contains(2));
            }

            [TestMethod]
            public void CopyingShouldCopyAllElements()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");
                table.Add(2, "ням");
                var array = new Entry<int, string>[2];

                // Act
                table.CopyTo(array, 0);

                // Assert
                Assert.AreEqual(2, array.Count(x => x != null));
            }

            [TestMethod]
            public void EnumeratorShouldReturnAllElements()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "ам");
                table.Add(2, "ням");

                // Act
                int count = table.Count();

                // Assert
                Assert.AreEqual(2, count);
            }
            #endregion
        }


    }
}