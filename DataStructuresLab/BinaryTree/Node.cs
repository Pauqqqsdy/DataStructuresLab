using System;
using System.Collections;

namespace DataStructuresLab.BinaryTree
{
    public class Node<T> 
    {
        /// <summary>
        /// Данные из узла
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Левый узел
        /// </summary>
        public Node<T>? Left { get; set; }

        /// <summary>
        /// Правый узел
        /// </summary>
        public Node<T>? Right { get; set; }

        /// <summary>
        /// Высота дерева
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Узел, в котором значение используется по умолчанию, а дочерние узлы равны null
        /// </summary>
        public Node()
        {
            Data = default(T);
            Left = null;
            Right = null;
            Height = 1;
        }

        /// <summary>
        /// Узел, в который передаются данные
        /// </summary>
        /// <param name="data">Данные, хранящиеся в конкретном узле</param>
        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}