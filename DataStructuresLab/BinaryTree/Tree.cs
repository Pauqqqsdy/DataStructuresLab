using DataStructuresLab.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresLab.BinaryTree
{
    public class Tree<T> : IEnumerable<T>, ICollection<T>
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public Node<T>? Root { get; set; }

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Количество элементов в дереве (только для чтения)
        /// </summary>
        public int Length => Count;

        /// <summary>
        /// Показывает доступность дерева к изменениям
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Пустое дерево
        /// </summary>
        public Tree()
        {
            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Дерево с заданным размером, заполняемое случайными элементами
        /// </summary>
        /// <param name="length">Количество элементов для добавления</param>
        /// <param name="generator">Функция для генерации элементов случайным образом</param>
        public Tree(int length, Func<T> generator)
        {
            if (generator == null) throw new ArgumentNullException();

            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                Insert(generator(), Comparer<T>.Default.Compare);
            }
        }

        /// <summary>
        /// Выполняет глубокую копию другого дерева
        /// </summary>
        /// <param name="other">Дерево, которое требуется скопировать</param>
        public Tree(Tree<T> other) : this()
        {
            if (other == null) throw new ArgumentNullException();

            foreach (var item in other)
            {
                Insert(item, Comparer<T>.Default.Compare);
            }
        }

        /// <summary>
        /// Создание идеально сбалансированного дерева
        /// </summary>
        /// <param name="size">Размер дерева</param>
        /// <param name="getData">Функция для получения данных конкретного узла</param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод, возвращающий строковое представление бинарного дерева по каждому уровню
        /// </summary>
        /// <returns>Строка с представлением дерева</returns>
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

        /// <summary>
        /// Находит максимальный элемент в дереве
        /// </summary>
        /// <param name="comparer">Функция сравнения двух элементов</param>
        /// <returns>Максимальный элемент</returns>
        public T FindMax(Func<T, T, int> comparer)
        {
            if (Root == null) throw new InvalidOperationException("Дерево пусто");

            return TraverseTree(Root, comparer);
        }

        /// <summary>
        /// Рекурсивный обход дерева для сравнения его элементов
        /// </summary>
        /// <param name="node">Узел, который нужно сравнить</param>
        /// <param name="comparer">Функция сравнения двух элементов</param>
        /// <returns></returns>
        public T TraverseTree(Node<T>? node, Func<T, T, int> comparer)
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

        /// <summary>
        /// Метод для построения сбалансированного дерева поиска
        /// </summary>
        /// <param name="sortedElements">Отсортированный список элементов</param>
        /// <param name="start">Начальный индекс</param>
        /// <param name="end">Конечный индекс</param>
        /// <returns></returns>
        public Node<T>? BuildBST(IList<T> sortedElements, int start, int end)
        {
            if (start > end) return null;

            int mid = (start + end) / 2;
            var node = new Node<T>(sortedElements[mid]);

            node.Left = BuildBST(sortedElements, start, mid - 1);
            node.Right = BuildBST(sortedElements, mid + 1, end);

            return node;
        }

        /// <summary>
        /// Рекурсивное добавление элементов в список 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elements"></param>
        public void TraversalAdd(Node<T> node, List<T> elements)
        {
            if (node == null) return;

            TraversalAdd(node.Left, elements);
            elements.Add(node.Data);
            TraversalAdd(node.Right, elements);
        }

        /// <summary>
        /// Метод, позволяющий изменить существующее дерево в сбалансированное дерево поиска
        /// </summary>
        /// <param name="comparer">Функция сравнения двух элементов</param>
        /// <returns>Сбалансированное дерево поиска</returns>
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

        /// <summary>
        /// Глубокое копирование дерева
        /// </summary>
        /// <returns>Новое дерево с сохранёнными данными</returns>
        public Tree<T> DeepCopy()
        {
            var newTree = new Tree<T>();
            newTree.Root = DeepCopyNode(Root);
            newTree.Count = Count;
            return newTree;
        }

        /// <summary>
        /// Рекурсивное создание копии узла и его потомков
        /// </summary>
        /// <param name="node">Узел для копирования</param>
        /// <returns>Новая копия узла</returns>
        public Node<T>? DeepCopyNode(Node<T>? node)
        {
            if (node == null) return null;

            var newNode = new Node<T>(node.Data);
            newNode.Left = DeepCopyNode(node.Left);
            newNode.Right = DeepCopyNode(node.Right);

            return newNode;
        }

        /// <summary>
        /// Вывод дерева поиска
        /// </summary>
        /// <param name="comparer">Функция сравнения элементов</param>
        /// <returns>Представление дерева поиска по уровням</returns>
        public string PrintAsBST(Func<T, T, int> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();

            if (Root == null)
                return "Дерево пусто";

            var bstTree = ConvertToBST(comparer);
            return bstTree.PrintByLevels();
        }

        /// <summary>
        /// Выводит представление дерева в виде бинарного дерева поиска
        /// </summary>
        /// <param name="comparer">Функция сравнения элементов</param>
        public void ShowBST(Func<T, T, int> comparer)
        {
            Console.WriteLine("Дерево поиска:");
            Console.WriteLine(PrintAsBST(comparer));
        }

        /// <summary>
        /// Удялет элемент из дерева
        /// </summary>
        /// <param name="key">Элемент для удаления</param>
        /// <param name="comparer">Функция сравнения элементов</param>
        /// <returns>В зависимости от того, удалён ли элемент, выводит true или false соответственно</returns>
        public bool Remove(T key, Func<T, T, int> comparer)
        {
            if (Root == null) return false;

            bool found = false;
            Root = RemoveNode(Root, key, comparer, ref found);

            if (found)
                Count--;

            return found;
        }

        /// <summary>
        /// Возвращает высоту узла в дереве
        /// </summary>
        /// <param name="node">Узел, который проверяем</param>
        /// <returns>Высота узла</returns>
        public int GetHeight(Node<T>? node)
        {
            return node?.Height ?? 0;
        }

        /// <summary>
        /// Вычисление баланс фактора
        /// </summary>
        /// <param name="node">Узел, который проверяем</param>
        /// <returns>Разность между левым и правым поддеревьями</returns>
        public int GetBalance(Node<T>? node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        /// <summary>
        /// Правый поворот вокруг определённого узла
        /// </summary>
        /// <param name="root">Узел для поворота</param>
        /// <returns>Новый корень поддерева</returns>
        public Node<T>? RotateRight(Node<T>? root)
        {
            Node<T> newRoot = root.Left;

            Node<T> rightSubtree = newRoot.Right;

            newRoot.Right = root;
            root.Left = rightSubtree;

            root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

            newRoot.Height = 1 + Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right));

            return newRoot;
        }

        /// <summary>
        /// Левый поворот вокруг определённого узла
        /// </summary>
        /// <param name="root">Узел для поворота</param>
        /// <returns>Новый корень поддерева</returns>
        public Node<T>? RotateLeft(Node<T>? root)
        {
            Node<T> newRoot = root.Right;

            Node<T> leftSubtree = newRoot.Left;

            newRoot.Left = root;
            root.Right = leftSubtree;

            root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

            newRoot.Height = 1 + Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right));

            return newRoot;
        }

        /// <summary>
        /// Удаляет рекурсивно узел с заданным ключом и балансирует дерево
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <param name="key">Ключ</param>
        /// <param name="comparer">Функция сравнения элементов</param>
        /// <param name="found">Флаг поиска узла (false - не найден, true - найден)</param>
        /// <returns>Новый корень поддерева после удаления и балансировки</returns>
        public Node<T>? RemoveNode(Node<T>? node, T key, Func<T, T, int> comparer, ref bool found)
        {
            if (node == null) return null;

            if (node.Data == null) throw new InvalidOperationException();
                
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

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

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

        /// <summary>
        /// Поиск минимального элемента в поддереве
        /// </summary>
        /// <param name="node">Корень поддерева</param>
        /// <returns>Минимальный элемент</returns>
        public T FindMin(Node<T> node)
        {
            while (node.Left != null)
                node = node.Left;

            return node.Data;
        }

        /// <summary>
        /// Полностью очищает дерево
        /// </summary>
        public void Clear()
        {
            Root = ClearNode(Root);
            Count = 0;
        }

        /// <summary>
        /// Очищает все узлы дерева с помощью рекурсии
        /// </summary>
        /// <param name="root">Корень поддерева</param>
        /// <returns>Возвращает null</returns>
        public Node<T>? ClearNode(Node<T>? root)
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

        /// <summary>
        /// Добавляет элемент в дерево
        /// </summary>
        /// <param name="key">Элемент для добавления</param>
        /// <param name="comparer">Функция сравнения элементов</param>
        public void Insert(T key, Func<T, T, int> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();

            Root = InsertNode(Root, key, comparer);
            Count++;
        }

        /// <summary>
        /// Вставляет рекурсивно новый узел и балансирует в AVL дерево
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public Node<T> InsertNode(Node<T>? node, T key, Func<T, T, int> comparer)
        {
            if (node == null) return new Node<T>(key);

            int compareResult = comparer(key, node.Data);
            if (compareResult < 0)
                node.Left = InsertNode(node.Left, key, comparer);
            else if (compareResult > 0)
                node.Right = InsertNode(node.Right, key, comparer);
            else
                return node;

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

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

        /// <summary>
        /// Поиск узла с заданным ключом
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <param name="key">Ключ поиска</param>
        /// <param name="comparer">Функция сравнения элементов</param>
        /// <returns>Найденный узел</returns>
        public Node<T>? Find(Node<T>? node, T key, Func<T, T, int> comparer)
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

        /// <summary>
        /// Выполняет обход дерева слева-направо
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <returns>Последовательность элементов</returns>
        public IEnumerable<T> InOrder(Node<T>? node)
        {
            if (node == null) yield break;

            foreach (var left in InOrder(node.Left))
                yield return left;

            yield return node.Data;

            foreach (var right in InOrder(node.Right))
                yield return right;
        }

        /// <summary>
        /// Добавление элемента в дерево
        /// </summary>
        /// <param name="item">Элемент, который нужно добавить</param>
        public void Add(T item)
        {
            Insert(item, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Проверка на наличие элемента в дереве
        /// </summary>
        /// <param name="item">Элемент, который нужно найти</param>
        /// <returns>Возвращает true, если элемент был найден, иначе - false</returns>
        public bool Contains(T item)
        {
            return Find(Root, item, Comparer<T>.Default.Compare) != null;
        }

        /// <summary>
        /// Копирует все элементы дерева в массив
        /// </summary>
        /// <param name="array">Массив, в который копируются элементы</param>
        /// <param name="arrayIndex">Индекс массива</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException();

            if (arrayIndex < 0 || arrayIndex + Count > array.Length) throw new ArgumentOutOfRangeException();

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

         bool ICollection<T>.Contains(T item)
        {
            return Find(Root, item, Comparer<T>.Default.Compare) != null;
        }

        public bool Remove(T item)
        {
            return Remove(item, Comparer<T>.Default.Compare);
        }
    }
}