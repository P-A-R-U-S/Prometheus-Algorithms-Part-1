using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace BinaryTree.Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            #region Тесты практического задания № 1
            //10             
            //Test1("10a");
            //Test1("10b");
            //Test1("10c");
            //Test1("10d");
            //Test1("10e");
            

            //100
            /*
            Test1("100a");
            Test1("100b");
            Test1("100c");
            Test1("100d");
            Test1("100e");
             */
            #endregion

            #region Практическое задание № 1
            //Task1("1000a");
            #endregion

            #region Тесты практического задания № 2
            //10             
            //Test2("10a", 9);
            //Test2("10b", 7);
            //Test2("10c", 9); 
            //Test2("10d", 5);
            //Test2("10e", 5);


            //100
            //Test2("100a", 51);
            //Test2("100b", 78);
            //Test2("100c", 103);
            //Test2("100d", 50);
            //Test2("100e", 50);
            #endregion

            #region Практическое задание № 2
            Task2("1000a", 1059);
            Task2("1000a", 1546);
            Task2("1000a", 1940);
            
            #endregion

        }

        //
        private static void Test1(string test)
        {
 
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1 (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\data_examples_07\input_" + test + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()).ReadToEnd();
            var inputArray = inputReader.Split(' ').Select(v => Convert.ToInt32(v)).ToArray();
            
            Console.WriteLine("INPUT");
            inputArray.ToList().ForEach(v => Console.Write("{0} ", v));
            Console.WriteLine();

            var node = default(BinaryTree.BinaryTreeNode);
            var tree = new List<BinaryTree.BinaryTreeNode>();

            var isAddRToLeft = true;
            var isAddToRight = false;

            foreach (var v in inputArray)
            {
                if (v != 0)
                {
                    node = new BinaryTree.BinaryTreeNode { Key = v, Parent = node};

                    if (tree.Count > 0)
                    {
                        if (isAddRToLeft)
                        {
                            node.Parent.Left = node;
                        }                        
                        else if(isAddToRight)
                        {
                            isAddToRight = false;
                            isAddRToLeft = true;
                            node.Parent.Right = node;
                        }
                    }
                    
                    tree.Add(node);               
                }
                else
                {
                    if (node.Left == null && isAddRToLeft)
                    {
                        isAddRToLeft = false;
                        isAddToRight = true;
                    }
                    else if (!isAddRToLeft && isAddToRight)
                    {
                        while (node.Parent != null && node.Parent.Right != null)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
            }
           
            Console.WriteLine("------------------------");
            var root = tree[0];
            dysplayNode(root);

            var inOrderWalk = new List<int>();
            //var inOrderWalk = new[] {10, 6, 4, 7, 8, 1, 5, 2, 9, 3};
            InOrderTreeWalk(inOrderWalk, root);
            foreach (var v in inOrderWalk) Console.Write("{0},", v);
            Console.WriteLine();
            inOrderWalk.Sort();
            foreach (var v in inOrderWalk) Console.Write("{0},", v);
            Console.WriteLine();
            InOrderTreeWalkChange(inOrderWalk, root);
            dysplayNode(root);
            var lastNodes = new List<BinaryTree.BinaryTreeNode>();
            getLastNodes(lastNodes, root);

            Console.WriteLine(new string('-', 100));
            Console.WriteLine("Oтвет 1: Значение ключа-root:{0}", root.Key);
            Console.WriteLine("Oтвет 2: Первый три листка дерева: {0} {1} {2}",
                lastNodes[0].Key,
                lastNodes[1].Key,
                lastNodes[2].Key);


            Console.WriteLine("Ответ 3: Последние три лиска дерева: {0} {1} {2}",
                lastNodes[lastNodes.Count - 3].Key,
                lastNodes[lastNodes.Count - 2].Key,
                lastNodes[lastNodes.Count - 1].Key);
            Console.Read();
            Console.Read();
        }
        private static void Task1(string task)
        {
            var index = 0;

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1 " + task);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\input_" + task + ".txt";
            
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()).ReadToEnd();
            
            var inputArray = inputReader.Split(' ').Select(v => Convert.ToInt32(v)).ToArray();
            
            Console.WriteLine("INPUT");
            inputArray.ToList().ForEach(v => Console.Write("{0} ", v));
            Console.WriteLine();

            
            var node = default(BinaryTree.BinaryTreeNode);
            var tree = new List<BinaryTree.BinaryTreeNode>();


            var isAddRToLeft = true;
            var isAddToRight = false;

            foreach (var v in inputArray)
            {
                if (v != 0)
                {
                    node = new BinaryTree.BinaryTreeNode { Key = v, Parent = node };

                    if (tree.Count > 0)
                    {
                        if (isAddRToLeft)
                        {
                            node.Parent.Left = node;
                        }
                        else if (isAddToRight)
                        {
                            isAddToRight = false;
                            isAddRToLeft = true;
                            node.Parent.Right = node;
                        }
                    }

                    tree.Add(node);

                    //Console.WriteLine(node.ToString());                 
                }
                else
                {
                    if (node.Left == null && isAddRToLeft)
                    {
                        isAddRToLeft = false;
                        isAddToRight = true;
                    }
                    else if (!isAddRToLeft && isAddToRight)
                    {
                        while (node.Parent != null && node.Parent.Right != null)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
            }

            Console.WriteLine("------------------------");
            var root = tree[0];
            dysplayNode(root);

            var inOrderWalk = new List<int>();
            InOrderTreeWalk(inOrderWalk, root);
            foreach (var v in inOrderWalk) Console.Write("{0},", v);
            Console.WriteLine();
            inOrderWalk.Sort();
            foreach (var v in inOrderWalk) Console.Write("{0},", v);
            Console.WriteLine();
            InOrderTreeWalkChange(inOrderWalk, root);
            dysplayNode(root);
            var lastNodes = new List<BinaryTree.BinaryTreeNode>();
            getLastNodes(lastNodes, root);

            Console.WriteLine(new string('-', 100));
            Console.WriteLine("Oтвет 1: Значение ключа-root:{0}", root.Key);
            Console.WriteLine("Oтвет 2: Первый три листка дерева: {0} {1} {2}",
                lastNodes[0].Key,
                lastNodes[1].Key,
                lastNodes[2].Key);


            Console.WriteLine("Ответ 3: Последние три лиска дерева: {0} {1} {2}",
                lastNodes[lastNodes.Count - 3].Key,
                lastNodes[lastNodes.Count - 2].Key,
                lastNodes[lastNodes.Count - 1].Key);
            Console.Read();
            Console.Read();
        }

        //
        private static void Test2(string test, int s)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\data_examples_07\input_" + test + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()).ReadToEnd();
            var inputArray = inputReader.Split(' ').Select(v => Convert.ToInt32(v)).ToArray();

            var outputFileName = @"Data\data_examples_07\output_" + test + "_" + s + ".txt";
            var outputReader = new StreamReader((new FileInfo(outputFileName)).OpenRead());
            var outputArray = new List<string>();
            while (!outputReader.EndOfStream)
            {
                var row = outputReader.ReadLine();
                outputArray.Add(row);
            }

            Console.Write("INPUT  -->");
            inputArray.ToList().ForEach(v => Console.Write("{0} ", v));
            Console.WriteLine();

            Console.Write("OUTPUT -->");
            outputArray.ForEach(v => Console.WriteLine("{0},", v));
            Console.WriteLine();

            Console.WriteLine("Поиск монотонных путей для числа: {0}", s);

            var node = default(BinaryTree.BinaryTreeNode);
            var tree = new List<BinaryTree.BinaryTreeNode>();

            var isAddRToLeft = true;
            var isAddToRight = false;

            foreach (var v in inputArray)
            {
                if (v != 0)
                {
                    node = new BinaryTree.BinaryTreeNode { Key = v, Parent = node };

                    if (tree.Count > 0)
                    {
                        if (isAddRToLeft)
                        {
                            node.Parent.Left = node;
                        }
                        else if (isAddToRight)
                        {
                            isAddToRight = false;
                            isAddRToLeft = true;
                            node.Parent.Right = node;
                        }
                    }

                    tree.Add(node);
                }
                else
                {
                    if (node.Left == null && isAddRToLeft)
                    {
                        isAddRToLeft = false;
                        isAddToRight = true;
                    }
                    else if (!isAddRToLeft && isAddToRight)
                    {
                        while (node.Parent != null && node.Parent.Right != null)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
            }

            Console.WriteLine("------------------------");
            var root = tree[0];
            //dysplayNode(root);

            var inOrderWalk = new List<int>();
            //var inOrderWalk = new[] {10, 6, 4, 7, 8, 1, 5, 2, 9, 3};
            InOrderTreeWalk(inOrderWalk, root);
            //foreach (var v in inOrderWalk) Console.Write("{0},", v);
            //Console.WriteLine();
            inOrderWalk.Sort();
            //foreach (var v in inOrderWalk) Console.Write("{0},", v);
            //Console.WriteLine();
            InOrderTreeWalkChange(inOrderWalk, root);
            //dysplayNode(root);
           
            //
            //var list = findMonotonePath(root, s);
            //if (list.Count > 0)
            //{
            //    list.ForEach(x => Console.Write("{0} -", x.Key));
            //}
            getAllMonotonePath(root, s);

            Console.Read();
            Console.Read();
        }
        private static void Task2(string test, int s)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\input_" + test + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()).ReadToEnd();
            var inputArray = inputReader.Split(' ').Select(v => Convert.ToInt32(v)).ToArray();

            Console.WriteLine("Поиск монотонных путей для числа: {0}", s);

            var node = default(BinaryTree.BinaryTreeNode);
            var tree = new List<BinaryTree.BinaryTreeNode>();

            var isAddRToLeft = true;
            var isAddToRight = false;

            foreach (var v in inputArray)
            {
                if (v != 0)
                {
                    node = new BinaryTree.BinaryTreeNode { Key = v, Parent = node };

                    if (tree.Count > 0)
                    {
                        if (isAddRToLeft)
                        {
                            node.Parent.Left = node;
                        }
                        else if (isAddToRight)
                        {
                            isAddToRight = false;
                            isAddRToLeft = true;
                            node.Parent.Right = node;
                        }
                    }

                    tree.Add(node);
                }
                else
                {
                    if (node.Left == null && isAddRToLeft)
                    {
                        isAddRToLeft = false;
                        isAddToRight = true;
                    }
                    else if (!isAddRToLeft && isAddToRight)
                    {
                        while (node.Parent != null && node.Parent.Right != null)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
            }

            Console.WriteLine("------------------------");
            var root = tree[0];
            //dysplayNode(root);

            var inOrderWalk = new List<int>();
            InOrderTreeWalk(inOrderWalk, root);
            inOrderWalk.Sort();
            InOrderTreeWalkChange(inOrderWalk, root);
            getAllMonotonePath(root, s);
            Console.WriteLine();
            Console.WriteLine();

        }

        private static List<BinaryTree.BinaryTreeNode> findMonotonePath(BinaryTree.BinaryTreeNode x, int s)
        {            
            var list = new List<BinaryTree.BinaryTreeNode>();

            if (x == null) return list;

            if (s <= 0) return list;

            if (x.Key == s)
            {
                list.Add(x);
                return list;
            }

            if (x.Key < s)
            {               
                //left 
                if (x.Left != null)
                {
                    var leftList = findMonotonePath(x.Left, s - x.Key);
                    if (leftList.Count > 0)
                    {
                        list.Add(x);
                        list.AddRange(leftList);
                        return list;
                    }

                }

                //right
                if (x.Right != null)
                {
                    var rightList = findMonotonePath(x.Right, s - x.Key);
                    if (rightList.Count > 0)
                    {
                        list.Add(x);
                        list.AddRange(rightList);
                        return list;
                    }
                }
            }

            return list;
        }
        private static void getAllMonotonePath(BinaryTree.BinaryTreeNode x, int s)
        {
            var y = x;
            var nodeList = findMonotonePath(y, s);
            if (nodeList.Any())
            {
                nodeList.ForEach(node => Console.Write("{0} -", node.Key));
                Console.WriteLine();
            }

            while (y.Left != null)
            {
                y = y.Left;
                var nodeListLeft = findMonotonePath(y, s);
                if (nodeListLeft.Any())
                {
                    nodeListLeft.ForEach(node => Console.Write("{0} -", node.Key));
                    Console.WriteLine();
                }
            }

            if (y.Right != null) getAllMonotonePath(y.Right, s);

            while (y.Key != x.Key)
            {
                y = y.Parent;
                if (y.Right != null)
                {
                    getAllMonotonePath(y.Right, s);
                }
            }
        }

        #region Private methods
        private static void InOrderTreeWalk(IList<int> array, BinaryTree.BinaryTreeNode x)
        {
            if (x.Left != null) InOrderTreeWalk(array,x.Left);

            array.Add(x.Key);

            if (x.Right != null) InOrderTreeWalk(array,x.Right);
        }
        private static void InOrderTreeWalkChange(IList<int> array, BinaryTree.BinaryTreeNode x)
        {
            if (x.Left != null) InOrderTreeWalkChange(array, x.Left);

            x.Key = array[0];
            array.RemoveAt(0);

            if (x.Right != null) InOrderTreeWalkChange(array, x.Right);
        }
        private static void dysplayNode(BinaryTree.BinaryTreeNode x)
        {
            var y = x;
            Console.WriteLine(y.ToString());

            while (y.Left != null)
            {
                y = y.Left;
                Console.WriteLine(y.ToString());
            }

            if(y.Right != null) dysplayNode(y.Right);

            while (y.Key != x.Key)
            {
                y = y.Parent;
                if (y.Right != null)
                {
                    dysplayNode(y.Right);
                }
            }
        }
        private static void getLastNodes(IList<BinaryTree.BinaryTreeNode> array, BinaryTree.BinaryTreeNode x)
        {
            if (x.Left != null) getLastNodes(array, x.Left);

            if (x.Right != null) getLastNodes(array, x.Right);

            if (x.Left == null && x.Right == null) array.Add(x);
        }
        #endregion
    }

    public class BinaryTree
    {
        public List<BinaryTreeNode> _tree;

        public BinaryTree()
        {
            _tree = new List<BinaryTreeNode>();
        }

        public BinaryTreeNode Search(BinaryTreeNode x, int? k)
        {
            if (x == null || k == x.Key) return x;

            return (k < x.Key) ? Search(x.Left, k) : Search(x.Right, k);
        }

        public BinaryTreeNode HerativeSearch(int? k)
        {
            var x = root;
            while (x != null && k != x.Key)
            {
                x = (k <= x.Key) ? x.Left : x.Right;

            }
            return x;
        }

        public BinaryTreeNode root { get; set; }

        private BinaryTreeNode min(BinaryTreeNode x)
        {
            while (x.Left != null)
            {
                x = x.Left;
            }

            return x;
        }

        private BinaryTreeNode max(BinaryTreeNode x)
        {
            while (x.Right != null)
            {
                x = x.Left;
            }

            return x;
        }

        private void InOrderTreeWalk(BinaryTreeNode x)
        {
            if(x.Left != null)  InOrderTreeWalk( x.Left);

            Console.Write("{0}," , x.Key);

            if (x.Right != null) InOrderTreeWalk(x.Right);
        }

        //TreeSuccessor --> Next
        public BinaryTreeNode Next(BinaryTreeNode x)
        {
            if (x.Left != null)
                return max(x.Left);

            var y = x.Parent;

            while (y != null && x == y.Left)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }

        //TreeSuccessor --> Previous
        public BinaryTreeNode Previous(BinaryTreeNode x)
        {
            if (x.Right != null)
                return min(x);

            var y = x.Parent;

            while (y != null && x == y.Right)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }

        public void Insert(BinaryTreeNode z)
        {
            var y = default(BinaryTreeNode);
            var x = root;
            while (x != null)
            {
                y = x;
                x = (z.Key <= x.Key)
                    ? x.Left
                    : x.Right;
            }

            z.Parent = y;

            if (y == null)
                root = z;
            else if ( z.Key <= y.Key)
                y.Left = z;
            else
                y.Right = z;

            _tree.Add(z);
       }

        private BinaryTreeNode delete1(BinaryTreeNode z)
        {
            if (z.Parent != null)
            {
                if (z.Parent.Left == z)
                    z.Parent.Left = null;

                if (z.Parent.Right == z)
                    z.Parent.Right = null;
            }

            return z;
        }

        private BinaryTreeNode delete2(BinaryTreeNode z)
        {
            BinaryTreeNode x;

            if (z.Left != null)
                x = z.Left;
            else
                x = z.Right;

            if (z.Parent != null)
            {
                if (z.Parent.Left == z)
                    z.Parent.Left = x;
                else
                    z.Parent.Right = x;

                x.Parent = z.Parent;
            }
            else
            {
                root = x;
            }
           
            return z;
        }

        private BinaryTreeNode delete3(BinaryTreeNode z)
        {
            var y = Next(z);
            var x = y.Right;

            if (x != null)
            {
                x.Parent = y.Parent;
                x.Parent.Left = x;
            }

            z.Key = y.Key;

            return y;
        }


        public class BinaryTreeNode
        {
            public int Key { get; set; }

            public BinaryTreeNode Left { get; set; }

            public BinaryTreeNode Right { get; set; }

            public BinaryTreeNode Parent { get; set; }

            public override string ToString()
            {
                return String.Format("Node-->  Key:{0} Parent:({1} -->L:{2}, R:{3} ) Left:{4} Right:{5}", 
                    Key, 
                    Parent != null ? Convert.ToString(Parent.Key) : "null",
                        Parent != null && Parent.Left != null ? Convert.ToString(Parent.Left.Key) : "null",
                        Parent != null && Parent.Right != null ? Convert.ToString(Parent.Right.Key) : "null",
                    Left != null ? Convert.ToString(Left.Key) : "null",
                    Right != null ? Convert.ToString(Right.Key) : "null");   
            }
        }
    }
}
