using DataStructuresLab;
using DataStructuresLab.Model;
using System;
using CarLibrary;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
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
            Assert.IsFalse(list.Contains("No"));
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

        private DuplexLinkedList<Transport> transportList;
        private TransportsListActions actions;


    }
}