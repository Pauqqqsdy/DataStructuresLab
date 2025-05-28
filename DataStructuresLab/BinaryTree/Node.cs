using System;
using System.Collections;

namespace DataStructuresLab.BinaryTree
{
    class Node<T> 
    {
        public T? Data { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        public int Height { get; set; }

        public Node()
        {
            Data = default(T);
            Left = null;
            Right = null;
        }
        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}