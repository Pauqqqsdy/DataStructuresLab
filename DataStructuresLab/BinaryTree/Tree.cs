using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresLab.BinaryTree
{
    class Tree<T> : IEnumerable<T>, ICollection<T>, IReadOnlyCollection<T>
    {
        public Node<T>? Root { get; set; }
        public int Count { get; set; }
        public bool IsReadOnly => false;
        int IReadOnlyCollection<T>.Count => Count;

        public Tree()
        {
            Root = null;
            Count = 0;
        }

        public Tree(int length, Func<T> generator)
        {
            if (generator == null)
                throw new ArgumentNullException();

            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                Insert(generator(), Comparer<T>.Default.Compare);
            }
        }

        public Tree(Tree<T> other) : this()
        {
            if (other == null)
                throw new ArgumentNullException();

            foreach (var item in other)
            {
                Insert(item, Comparer<T>.Default.Compare);
            }
        }

        public Node<T>? CreateIdealTree(int size, Func<T> getData)
        {
            if (size == 0) return null;

            int leftSize = size / 2;
            int rightSize = size - leftSize - 1;

            T data = getData();
            Node<T> node = new Node<T>(data);

            node.Left = CreateIdealTree(leftSize, getData);
            node.Right = CreateIdealTree(rightSize, getData);

            return node;
        }

        public string PrintByLevels()
        {
            if (Root == null) return "Дерево пусто";

            var result = new StringBuilder();
            var currentLevel = new List<Node<T>> { Root };
            int level = 1;

            while (currentLevel.Count > 0)
            {
                result.Append($"\nУровень {level}: ");
                var nextLevel = new List<Node<T>>();

                foreach (var node in currentLevel)
                {
                    result.Append("\n" + node.Data);

                    if (node.Left != null)
                        nextLevel.Add(node.Left);
                    if (node.Right != null)
                        nextLevel.Add(node.Right);
                }

                result.AppendLine();
                currentLevel = nextLevel;
                level++;
            }

            return result.ToString();
        }

        public T FindMax(Func<T, T, int> comparer)
        {
            if (Root == null) throw new InvalidOperationException("Дерево пусто");

            return TraverseTree(Root, comparer);
        }

        private T TraverseTree(Node<T>? node, Func<T, T, int> comparer)
        {
            if (node == null) return default!;

            T current = node.Data;

            T left = TraverseTree(node.Left, comparer);
            T right = TraverseTree(node.Right, comparer);

            if (left != null && comparer(left, current) > 0)
                current = left;

            if (right != null && comparer(right, current) > 0)
                current = right;

            return current;
        }

        private Node<T>? BuildBST(IList<T> sortedElements, int start, int end)
        {
            if (start > end) return null;

            int mid = (start + end) / 2;
            var node = new Node<T>(sortedElements[mid]);

            node.Left = BuildBST(sortedElements, start, mid - 1);
            node.Right = BuildBST(sortedElements, mid + 1, end);

            return node;
        }

        private void TraversalAdd(Node<T> node, List<T> elements)
        {
            if (node == null) return;

            TraversalAdd(node.Left, elements);
            elements.Add(node.Data);
            TraversalAdd(node.Right, elements);
        }

        public Tree<T> ConvertToBST(Func<T, T, int> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();

            var elements = new List<T>();
            TraversalAdd(Root, elements);

            elements.Sort((x, y) => comparer(x, y));

            var bstTree = new Tree<T>();
            bstTree.Root = BuildBST(elements, 0, elements.Count - 1);
            bstTree.Count = elements.Count;

            return bstTree;
        }

        public Tree<T> DeepCopy()
        {
            var newTree = new Tree<T>();
            newTree.Root = DeepCopyNode(Root);
            newTree.Count = Count;
            return newTree;
        }

        private Node<T>? DeepCopyNode(Node<T>? node)
        {
            if (node == null) return null;

            var newNode = new Node<T>(node.Data);
            newNode.Left = DeepCopyNode(node.Left);
            newNode.Right = DeepCopyNode(node.Right);

            return newNode;
        }

        public string PrintAsBST(Func<T, T, int> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();

            if (Root == null)
                return "Дерево пусто";

            var bstTree = ConvertToBST(comparer);
            return bstTree.PrintByLevels();
        }

        public void ShowBST(Func<T, T, int> comparer)
        {
            Console.WriteLine("Дерево поиска:");
            Console.WriteLine(PrintAsBST(comparer));
        }

        public bool Remove(T key, Func<T, T, int> comparer)
        {
            if (Root == null) return false;

            bool found = false;
            Root = RemoveNode(Root, key, comparer, ref found);

            if (found)
                Count--;

            return found;
        }

        private int Height(Node<T>? node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(Node<T>? node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private Node<T>? RotateRight(Node<T>? root)
        {
            Node<T> newRoot = root.Left;

            Node<T> rightSubtree = newRoot.Right;

            newRoot.Right = root;
            root.Left = rightSubtree;

            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));

            newRoot.Height = 1 + Math.Max(Height(newRoot.Left), Height(newRoot.Right));

            return newRoot;
        }

        private Node<T>? RotateLeft(Node<T>? root)
        {
            Node<T> newRoot = root.Right;

            Node<T> leftSubtree = newRoot.Left;

            newRoot.Left = root;
            root.Right = leftSubtree;

            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));

            newRoot.Height = 1 + Math.Max(Height(newRoot.Left), Height(newRoot.Right));

            return newRoot;
        }

        private Node<T>? RemoveNode(Node<T>? node, T key, Func<T, T, int> comparer, ref bool found)
        {
            int compareResult = comparer(key, node.Data);

            if (compareResult < 0)
            {
                node.Left = RemoveNode(node.Left, key, comparer, ref found);
            }
            else if (compareResult > 0)
            {
                node.Right = RemoveNode(node.Right, key, comparer, ref found);
            }
            else
            {
                found = true;

                if (node.Left == null && node.Right == null)
                    return null;

                if (node.Left == null)
                    return node.Right;

                if (node.Right == null)
                    return node.Left;

                node.Data = FindMin(node.Right);
                node.Right = RemoveNode(node.Right, node.Data, comparer, ref found);
            }

            if (node == null)
                return null;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RotateRight(node);

            if (balance < -1 && GetBalance(node.Right) <= 0)
                return RotateLeft(node);

            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private T FindMin(Node<T> node)
        {
            while (node.Left != null)
                node = node.Left;

            return node.Data;
        }

        public void Clear()
        {
            Root = ClearNode(Root);
            Count = 0;
        }

        private Node<T>? ClearNode(Node<T>? root)
        {
            while (root != null)
            {
                if (root.Left != null)
                {
                    var prev = root.Left;
                    while (prev.Right != null && prev.Right != root)
                        prev = prev.Right;

                    if (prev.Right == null)
                    {
                        prev.Right = root;
                        root = root.Left;
                    }
                    else
                    {
                        prev.Right = null;
                        root.Data = default!;
                        root = root.Right;
                    }
                }
                else
                {
                    root.Data = default!;
                    root = root.Right;
                }
            }

            return null;
        }

        public void Insert(T key, Func<T, T, int> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();

            Root = InsertNode(Root, key, comparer);
            Count++;
        }

        private Node<T> InsertNode(Node<T>? node, T key, Func<T, T, int> comparer)
        {
            if (node == null) return new Node<T>(key);

            int compareResult = comparer(key, node.Data);
            if (compareResult < 0)
                node.Left = InsertNode(node.Left, key, comparer);
            else if (compareResult > 0)
                node.Right = InsertNode(node.Right, key, comparer);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (comparer(key, node.Left.Data) < 0)
                    return RotateRight(node);
                else
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            if (balance < -1)
            {
                if (comparer(key, node.Right.Data) > 0)
                    return RotateLeft(node);
                else
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private Node<T>? Find(Node<T>? node, T key, Func<T, T, int> comparer)
        {
            if (node == null) return null;

            int compareResult = comparer(key, node.Data);

            if (compareResult == 0)
                return node;
            else if (compareResult < 0)
                return Find(node.Left, key, comparer);
            else
                return Find(node.Right, key, comparer);
        }

        public IEnumerator<T> GetEnumerator() => InOrder(Root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<T> InOrder(Node<T>? node)
        {
            if (node == null) yield break;

            foreach (var left in InOrder(node.Left))
                yield return left;

            yield return node.Data;

            foreach (var right in InOrder(node.Right))
                yield return right;
        }

        public void Add(T item)
        {
            Insert(item, Comparer<T>.Default.Compare);
        }

        public bool Contains(T item)
        {
            return Find(Root, item, Comparer<T>.Default.Compare) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)  
                throw new ArgumentNullException();
            if (arrayIndex < 0 || arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException();

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item, Comparer<T>.Default.Compare);
        }

        bool ICollection<T>.Contains(T item)
        {
            return Find(Root, item, Comparer<T>.Default.Compare) != null;
        }
    }
}