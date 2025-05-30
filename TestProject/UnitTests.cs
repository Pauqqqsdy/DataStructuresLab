using DataStructuresLab;
using System;
using CarLibrary;
using DataStructuresLab.ModelHashSet;
using DataStructuresLab.Model;
using DataStructuresLab.BinaryTree;

namespace TestProject
{
    [TestClass]
    public class LinkedListTests
    {
        #region ����-����� ����������� ������
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

        #region ����-����� ���-�������

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
                string testData = "����";

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
                var transport = new Transport(5, "����", 2010, "������", 25000, 120);
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
                string expectedValue = "����";

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
                table.Add(1, "��");

                // Assert
                Assert.AreEqual(1, table.Count);
            }

            [TestMethod]
            public void ContainsExistingKeyShouldReturnTrue()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "��");

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
                table.Add(1, "��");

                // Act
                var value = table.Get(1);

                // Assert
                Assert.AreEqual("��", value);
            }

            [TestMethod]
            public void RemoveExistingKeyShouldReturnTrueAndDecreaseCount()
            {
                // Arrange
                var table = new HashTable<int, string>();
                table.Add(1, "��");

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
                table.Add(1, "��");
                table.Add(2, "���");

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
                table.Add(1, "��");
                table.Add(2, "���");
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
                table.Add(1, "��");
                table.Add(2, "���");

                // Act
                int count = table.Count();

                // Assert
                Assert.AreEqual(2, count);
            }
            #endregion
        }

        #region ����-����� ��� ��������
        [TestClass]
        public class TreeTests
        {
            [TestMethod]
            public void CopyingShouldCopyAllElements()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);

                var array = new int[3];

                // Act
                tree.CopyTo(array, 0);

                // Assert
                CollectionAssert.AreEquivalent(new[] { 5, 10, 15 }, array);
            }

            [TestMethod]
            public void ContainsShouldReturnTrueForExistingElement()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);

                // Assert
                Assert.IsTrue(tree.Contains(5));
            }

            [TestMethod]
            public void DefaultNodeConstructor()
            {
                // Arrange
                var node = new Node<int>();

                // Assert
                Assert.AreEqual(default(int), node.Data);
                Assert.IsNull(node.Left);
                Assert.IsNull(node.Right);
                Assert.AreEqual(1, node.Height);
            }

            [TestMethod]
            public void ConstructorWithLengthShouldCreateTreeWithSpecifiedSize()
            {
                // Arrange
                int expectedCount = 5;
                Func<int> generator = () => new Random().Next(1, 100);

                // Act
                var tree = new Tree<int>(expectedCount, generator);

                // Assert
                Assert.AreEqual(expectedCount, tree.Count);
            }

            [TestMethod]
            public void ContainsShouldReturnFalseForNonExistingElement()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);

                // Assert
                Assert.IsFalse(tree.Contains(7));
            }

            [TestMethod]
            public void RemoveShouldDecreaseCountAndRemoveElement()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);

                // Act
                bool removed = tree.Remove(5, Comparer<int>.Default.Compare);

                // Assert
                Assert.IsTrue(removed);
                Assert.AreEqual(2, tree.Count);
                Assert.IsFalse(tree.Contains(5));
            }

            [TestMethod]
            public void CountShouldBeCorrectAfterInsertions()
            {
                // Arrange
                var tree = new Tree<int>();

                // Act
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);

                // Assert
                Assert.AreEqual(3, tree.Count);
            }

            [TestMethod]
            public void ClearShouldRemoveAllElements()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);

                // Act
                tree.Clear();

                // Assert
                Assert.AreEqual(0, tree.Count);
                Assert.IsFalse(tree.Contains(10));
                Assert.IsFalse(tree.Contains(5));
            }

            [TestMethod]
            public void DeepCopyShouldHaveSameValuesButDifferentReference()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);

                // Act
                var copiedTree = tree.DeepCopy();

                // Assert
                Assert.AreEqual(tree.Count, copiedTree.Count);
                CollectionAssert.AreEquivalent(tree.ToArray(), copiedTree.ToArray());
                Assert.AreNotSame(tree, copiedTree);
            }

            [TestMethod]
            public void AddingItemsIncreasesCount()
            {
                // Arrange
                var tree = new Tree<int>();

                // Act
                tree.Add(10);
                tree.Add(5);
                tree.Add(15);

                // Assert
                Assert.AreEqual(3, tree.Count);
            }

            [TestMethod]
            public void InOrderTraversalReturnsSortedElements()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Add(10);
                tree.Add(5);
                tree.Add(15);

                // Act
                var result = new List<int>(tree);

                // Assert
                CollectionAssert.AreEqual(new[] { 5, 10, 15 }, result);
            }

            [TestMethod]
            public void CopyToCopiesAllElementsToArray()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Add(10);
                tree.Add(5);
                tree.Add(15);
                var array = new int[3];

                // Act
                tree.CopyTo(array, 0);

                // Assert
                CollectionAssert.AreEquivalent(new[] { 5, 10, 15 }, array);
            }

            [TestMethod]
            public void ContainsReturnsTrueForExistingElement()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Add(10);
                tree.Add(5);

                // Act & Assert
                Assert.IsTrue(tree.Contains(5));
            }

            [TestMethod]
            public void ContainsReturnsFalseForNonExistingElement()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Add(10);
                tree.Add(5);

                // Act & Assert
                Assert.IsFalse(tree.Contains(7));
            }

            [TestMethod]
            public void ClearRemovesAllElements()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Add(10);
                tree.Add(5);

                // Act
                tree.Clear();

                // Assert
                Assert.AreEqual(0, tree.Count);
                Assert.IsFalse(tree.Contains(10));
            }

            [TestMethod]
            public void ConvertToBSTProducesSortedInOrderTraversal()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(20, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(30, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);

                // Act
                var bst = tree.ConvertToBST(Comparer<int>.Default.Compare);

                // Assert
                var result = new List<int>(bst);
                CollectionAssert.AreEqual(new[] { 5, 10, 15, 20, 30 }, result);
            }

            [TestMethod]
            public void CopyConstructorShouldCreateDeepCopyOfTree()
            {
                // Arrange
                var originalTree = new Tree<int>();
                originalTree.Insert(10, Comparer<int>.Default.Compare);
                originalTree.Insert(5, Comparer<int>.Default.Compare);
                originalTree.Insert(15, Comparer<int>.Default.Compare);

                // Act
                var copiedTree = new Tree<int>(originalTree);

                // Assert
                Assert.AreEqual(originalTree.Count, copiedTree.Count);
                Assert.IsTrue(copiedTree.Contains(10));
                Assert.IsTrue(copiedTree.Contains(5));
                Assert.IsTrue(copiedTree.Contains(15));
                Assert.AreNotSame(originalTree.Root, copiedTree.Root);
            }

            [TestMethod]
            public void RotateRightShouldBalanceTreeCorrectly()
            {
                // Arrange:
                var node = new Node<int>(30)
                {
                    Left = new Node<int>(20)
                    {
                        Left = new Node<int>(10)
                    },
                    Right = null,
                    Height = 3
                };

                // Act
                var tree = new Tree<int>();
                Node<int> rotated = tree.RotateRight(node);

                // Assert
                Assert.AreEqual(20, rotated.Data);
                Assert.AreEqual(10, rotated.Left.Data);
                Assert.AreEqual(30, rotated.Right.Data);
            }

            private string ConsoleOutput(Action action)
            {
                var originalOutput = Console.Out;
                using (var writer = new StringWriter())
                {
                    Console.SetOut(writer);
                    action();
                    Console.SetOut(originalOutput);
                    return writer.ToString();
                }
            }

            [TestMethod]
            public void ShowBSTShouldConvertAndPrintBalancedBST()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);
                tree.Insert(3, Comparer<int>.Default.Compare);
                tree.Insert(7, Comparer<int>.Default.Compare);

                // Act
                var output = ConsoleOutput(() => tree.ShowBST(Comparer<int>.Default.Compare));

                // Assert
                StringAssert.Contains(output, "�������");
            }

            [TestMethod]
            public void PrintAsBSTShouldReturnBalancedBSTByLevels()
            {
                // Arrange
                var tree = new Tree<int>();
                tree.Insert(10, Comparer<int>.Default.Compare);
                tree.Insert(5, Comparer<int>.Default.Compare);
                tree.Insert(15, Comparer<int>.Default.Compare);
                tree.Insert(3, Comparer<int>.Default.Compare);
                tree.Insert(7, Comparer<int>.Default.Compare);

                // Act
                string result = tree.PrintAsBST(Comparer<int>.Default.Compare);

                // Assert
                StringAssert.Contains(result, "������� 1:");
                StringAssert.Contains(result, "10");
                StringAssert.Contains(result, "5");
                StringAssert.Contains(result, "15");
            }
        }
        #endregion
    }
}