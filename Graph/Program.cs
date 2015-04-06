using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            #region  Тесты практического задания № 8

            /*
            Test1("08_1");
            Console.Read();
            Console.Read();

            
            Test1("08_2");
            Console.Read();
            Console.Read();

            Test1("08_3");
            Console.Read();
            Console.Read();

            Test1("08_4");
            Console.Read();
            Console.Read();
            
             */

            #endregion

            #region Практическое задание № 8
            Task1("08");
            Console.Read();
            #endregion
        }

        private static void Test1(string test)
        {

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1 (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\test_08\test_" + test + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead());

            var outputFileName = @"Data\test_08\test_" + test + ".output.txt";
            var outputReader = new StreamReader((new FileInfo(outputFileName)).OpenRead());

            var graph = new Graph<string>();

            while (!inputReader.EndOfStream)
            {
                var row = inputReader.ReadLine();
                var vs = row.Split(' ');

                if (vs.Length > 1 && vs[0] == vs[1])
                {
                    continue;
                }

                if (!graph._graph.ContainsKey(vs[0]))
                {
                    graph._graph.Add(vs[0], new List<string>());
                }

                if(vs.Length > 1)
                    graph._graph[vs[0]].Add(vs[1]);
            }
            //Validate Graph
            var length = 0;

            length = graph._graph.Count;
            for (var i=0; i < length; ++i)
            {
                var v = graph._graph.Keys.ElementAt(i);
                foreach (var u in graph._graph[v])
                {
                    if (!graph._graph.Keys.Contains(u))
                    {
                        graph._graph.Add(u, new List<string>());
                    }
                }
            }

            var outPutResults = new List<string>();

            Console.WriteLine("OUTPUT:");
            while (!outputReader.EndOfStream)
            {
                var row = outputReader.ReadLine();
                var vs = row.Split(' ');

                outPutResults.Add(row);
                Console.WriteLine(row);
            }

            var result = graph.StrongConnectedComponents();
            Console.WriteLine(new string('.', 50));

            Console.WriteLine("RESULT:");
            foreach (var r in result)
            {
                var sc = String.Join(" ", (r as List<string>));
                Console.WriteLine("({0}) ", sc);
            }
            Console.WriteLine(new string('.', 50));

            foreach (var outPutResult in outPutResults)
            {
                var ors = outPutResult.Split(' ');

                foreach (var or in ors)
                {
                    Console.Write("{0} -->", or);

                    var count = Convert.ToInt32(or);
                    var isResultFound = false;

                    var i = result.Count-1;
                    while (!isResultFound && i >= 0)
                    {
                        var r = result[i];

                        if ((r as List<string>).Count == count)
                        {
                            isResultFound = true;

                            var sc = String.Join(" ", (r as List<string>));
                            Console.Write("({0}) ", sc);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Passed");
                            Console.ResetColor();

                            result.RemoveAt(i);
                        }

                        --i;
                    }

                    if (!isResultFound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed");
                        Console.ResetColor();
                        Console.Read();
                    }
                }
            }
        }

        private static void Task1(string task)
        {

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1 : " + task);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\input_" + task + ".txt";
            //var inputFileName = @"Data\test_08\test_08_1.txt";
            var inputReader = File.OpenText(inputFileName);

            var graph = new Graph<int>();

            var i = 0;
            while (!inputReader.EndOfStream)
            {
                var row = inputReader.ReadLine();
                var vs = row.Split(' ');

                if (vs.Length > 1 && vs[0] == vs[1])
                {
                    continue;
                }

                if (!graph._graph.ContainsKey(Convert.ToInt32(vs[0])))
                {
                    graph._graph.Add(Convert.ToInt32(vs[0]), new List<int>());
                }

                if (vs.Length > 1)
                    graph._graph[Convert.ToInt32(vs[0])].Add(Convert.ToInt32(vs[1]));

                //Console.WriteLine("{0}:{1}", i, row);
                ++i;
               
            }
            Console.WriteLine("Read: Done");

            var length = 0;

            length = graph._graph.Count;
            for (i = 0; i < length; ++i)
            {
                var v = graph._graph.Keys.ElementAt(i);
                foreach (var u in graph._graph[v])
                {
                    if (!graph._graph.Keys.Contains(u))
                    {
                        graph._graph.Add(u, new List<int>());
                    }
                }
            }

            Console.WriteLine("Validation: Done");

            var result = graph.StrongConnectedComponents();
            Console.WriteLine(new string('.', 50));

            Console.WriteLine("Result: To file --> Start.");

            var componentSize = new List<int>();
            var resultCount = result.Count();
            for (i = 0; i < resultCount; ++i)
            {
                componentSize.Add(result[i].Count);
            }
            componentSize.Sort();

            File.Delete("result.txt");
            var file = new StreamWriter( File.Create("result.txt", 100));
            
           
            var componentCount = componentSize.Count;
            for (i = componentCount-1; i >= 0; --i)
            {
                //Console.Write("{0},", componentSize[i]);
                file.WriteLine(componentSize[i]);
            }

            file.Flush();
            file.Dispose();
            file.Close();

            Console.WriteLine("Result: To file --> End.");

        }

    }

    public class Graph<T>
    {
        internal IDictionary<T, IList<T>> _graph;
        internal IDictionary<T, int> _bfs;
        internal IDictionary<T, int> _dfs;
        internal IDictionary<T, int> _ts;
        internal IDictionary<T, int> _f;
        internal IList<T> _dfs_t;
        internal IDictionary<T, int> _d;
        internal IDictionary<T, T> _p;
        private int _label;
        private int _t;


        public Graph()
        {
            _graph = new Dictionary<T, IList<T>>();
            _bfs = new Dictionary<T, int>();
            _d = new Dictionary<T, int>();
            _p = new Dictionary<T, T>();
            _f = new Dictionary<T, int>();

             _dfs = new Dictionary<T, int>();

             _ts = new Dictionary<T, int>();
            _dfs_t = new List<T>();
        }

        public void BreadthFirstSearch()
        {
            _bfs.Clear();
            _d.Clear();
            _p.Clear();

            var v = _graph.Keys.First();
            BFS(v);
        }
        private void BFS(T s)
        {
            var bfsQueue = new Queue<T>();

            var k = 1;

            _bfs[s] = k;
            _d[s] = 0;

            bfsQueue.Enqueue(s);

            while (bfsQueue.Count > 0)
            {
                var v = bfsQueue.Dequeue();
                foreach (var u in _graph[v])
                {
                    if (!_bfs.ContainsKey(u))
                    {
                        k += 1;

                        _bfs[u] = k;
                        _d[u] = _d[v] + 1;
                        _p[u] = v;

                        bfsQueue.Enqueue(u);
                    }
                }

            }

        }
        public int FindConnectedComponents()
        {
            var N = 0;

            _bfs.Clear();
           

            foreach (var v in _graph.Keys)
            {
                if (!_bfs.ContainsKey(v))
                {
                    _d.Clear();
                    _p.Clear();

                    BFS(v);
                    N += 1;
                }
            }

            return N;
        }


        public void DepthFirstSearch1()
        {
            _dfs.Clear();

            var v = _graph.Keys.First();
            var k = DFSR(v, 1);            
        }
        public void DepthFirstSearch2()
        {
            _dfs.Clear();

            var v = _graph.Keys.First();
            DFS(v);
        }
        private int DFSR(T s, int k)
        {
            _dfs[s] = k;
            foreach (var u in _graph[s])
            {
                if (!_dfs.ContainsKey(u))
                {
                    k = DFSR(u, k + 1);
                }
            }

            return k;
        }
        private void DFS(T s)
        {
            var k = 1;
            var dfsStack = new Stack<T>();
            
            _dfs[s] = k;
            dfsStack.Push(s);
            while (dfsStack.Count > 0)
            {
                var v = dfsStack.Peek();
                var isNotFoundExist = false;

                foreach (var u in _graph[v])
                {
                    if (!_dfs.ContainsKey(u))
                    {
                        k += 1;
                        _dfs[u] = k;
                        dfsStack.Push(u);
                        isNotFoundExist = true;
                        break;

                    }
                }

                if(!isNotFoundExist)
                    dfsStack.Pop();
            }

        }

        
        public void TopologicalSort()
        {
            _ts.Clear();

            _label = _graph.Count;
            _dfs_t.Clear();

            foreach (var v in _graph.Keys)
            {
                if (!_dfs_t.Contains(v))
                {
                    DFSR_Topological(v);                   
                }

            }
        }
        private void DFSR_Topological(T s)
        {
            _dfs_t.Add(s);
            foreach (var u in _graph[s])
            {
                if (!_dfs_t.Contains(u))
                {
                    DFSR_Topological(u);
                }
            }

            _ts[s] = _label;
            _label -= 1;
        }

        
        // Версия StrongConnectedComponend с рекурсией.
        /*
        public IList<IList<T>> StrongConnectedComponents()
        {
            var strongComponents2 = new List<IList<T>>();
            
            //1
            DFSLoop1(_graph, null);

            //2 - транспонировать графф
            var traponedGraph = new Dictionary<T, IList<T>>();

            foreach (var v in _graph.Keys)
            {
                traponedGraph.Add(v, new List<T>());
                foreach(var u in _graph.Keys)
                {
                    if (_graph[u].Contains(v))
                    {
                        traponedGraph[v].Add(u);
                    }
                }
            }

            DFSLoop2(traponedGraph, strongComponents2);

            return strongComponents2;
        }
        internal void DFSLoop1(IDictionary<T, IList<T>> graph, IList<IList<T>> strongComponents)
        {
            _dfs_t.Clear();
            _t = 0;
            foreach (var v in graph.Keys)
            {
                if (!_dfs_t.Contains(v))
                {
                    if (strongComponents != null)
                        strongComponents.Add(new List<T> { v } );

                    DFSR_Topological_StrongComponents(graph, v, strongComponents);
                }
            }
        }
        internal void DFSLoop2(IDictionary<T, IList<T>> graph, IList<IList<T>> strongComponents)
        {
            _dfs_t.Clear();
            _t = 0;

            var _f_sorted = _f.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var f in _f_sorted)
            {
                T v = f.Key;
                if (!_dfs_t.Contains(v))
                {
                    if (strongComponents != null)
                        strongComponents.Add(new List<T> { v });

                    DFSR_Topological_StrongComponents(graph, v, strongComponents);
                }
            }
        }
        internal void DFSR_Topological_StrongComponents(IDictionary<T, IList<T>> graph, T s, IList<IList<T>> strongComponents)
        {
            _dfs_t.Add(s);

            foreach (var u in graph[s])
            {
                if (!_dfs_t.Contains(u))
                {
                    if (strongComponents != null)
                        strongComponents.Last().Add(u);

                    DFSR_Topological_StrongComponents(graph, u, strongComponents);
                }
            }
        
            _t += 1;
            _f[s] = _t;
        }
         */


        // Версия StrongConnectedComponend без рекурсией.
        public IList<IList<T>> StrongConnectedComponents()
        {

            IList<T> keys;
            IList<T> values;
            int keysCount;
            int valuesCount;
            var dfsStack = new Stack<T>();

            #region DFS Loop 1
            Console.WriteLine("Step 1: DFS Loop 1 ");
            _dfs_t.Clear();
            _t = 0;

            keys = _graph.Keys.ToList();
            keysCount = keys.Count;
            for (var i = 0; i < keysCount; ++i)
            {
                var s = keys[i];
                if (!_dfs_t.Contains(s))
                {
                    #region DFS

                    dfsStack.Push(s);
                    _dfs_t.Add(s);
                    while (dfsStack.Count > 0)
                    {
                        var v = dfsStack.Peek();
                        var isNotFoundExist = false;

                        values = _graph[v];
                        valuesCount = values.Count;
                        for (var j = 0; j < valuesCount; ++j)
                        {
                            var u = values[j];
                            if (!_dfs_t.Contains(u))
                            {
                                _dfs_t.Add(u);
                                dfsStack.Push(u);
                                isNotFoundExist = true;
                                break;
                            }
                        }

                        if (!isNotFoundExist)
                        {
                            var vp = dfsStack.Pop();
                            _t += 1;
                            _f[vp] = _t;
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Траспорнирование графф
            Console.WriteLine("Step 2: Transponent graph.");
            var traponedGraph = new Dictionary<T, IList<T>>();
            for (var i = 0; i < keysCount; ++i)
            {
                var v = keys[i];
                traponedGraph.Add(v, new List<T>());
                for (var j = 0; j < keysCount; ++j)
                {
                    var u = keys[j];
                    if (_graph[u].Contains(v))
                    {
                        traponedGraph[v].Add(u);
                    }
                }
            }
            #endregion

            #region DFS Loop 2
            Console.WriteLine("Step 3: DFS Loop 2 ");
            var strongComponents = new List<IList<T>>();

            _dfs_t.Clear();
            _t = 0;

            keys = _f.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            keysCount = keys.Count;
            for (var i = 0; i < keysCount; ++i)
            {
                var s = keys[i];
                if (!_dfs_t.Contains(s))
                {
                    strongComponents.Add(new List<T> { s });

                    dfsStack.Push(s);
                    _dfs_t.Add(s);
                    while (dfsStack.Count > 0)
                    {
                        var v = dfsStack.Peek();
                        var isNotFoundExist = false;

                        values = traponedGraph[v];
                        valuesCount = values.Count;
                        for (var j = 0; j < valuesCount; ++j)
                        {
                            var u = values[j];
                            if (!_dfs_t.Contains(u))
                            {
                               strongComponents.Last().Add(u);

                                _dfs_t.Add(u);
                                dfsStack.Push(u);
                                isNotFoundExist = true;
                                break;

                            }
                        }

                        if (!isNotFoundExist)
                        {
                            dfsStack.Pop();
                        }
                    }
                }
            }
            
            #endregion

            return strongComponents;
        }

        //private int stackLevel = 0;
        //internal void DFSR_Topological_StrongComponents(IDictionary<T, IList<T>> graph, T s, IList<IList<T>> strongComponents)
        //{
        //    //++ stackLevel;

        //    //1
        //    var dfsStack = new Stack<T>();

        //    dfsStack.Push(s);
        //    _dfs_t.Add(s);
        //    while (dfsStack.Count > 0)
        //    {
        //        var v = dfsStack.Peek();
        //        var isNotFoundExist = false;

        //        foreach (var u in graph[v])
        //        {
        //            //Console.Write(new string('-', stackLevel) + ">");
        //            //Console.Write("{0} --> {1}", v, u);

        //            if (!_dfs_t.Contains(u))
        //            {
        //                //Console.Write(" :NOT FOUND");

        //                if (strongComponents != null)
        //                    strongComponents.Last().Add(u);

        //                _dfs_t.Add(u);

        //                dfsStack.Push(u);
        //                isNotFoundExist = true;

        //                //++stackLevel;

        //                break;

        //            }
        //            //Console.WriteLine();
        //        }

        //        if (!isNotFoundExist)
        //        {
        //            var vp = dfsStack.Pop();

        //            //--stackLevel;

        //            _t += 1;
        //            _f[vp] = _t;
        //            //Console.Write(":f[{0}] = {1}", vp, _t);
        //        }

        //        //Console.WriteLine();
        //    }


        //    //2
        //    //_dfs_t.Add(s);
        //    //foreach (var u in graph[s])
        //    //{
        //    //    Console.Write(new string('-', stackLevel) + ">");

        //    //    Console.Write("{0} --> {1}", s, u);
        //    //    if (!_dfs_t.Contains(u))
        //    //    {
        //    //        Console.WriteLine(" :NOT FOUND");

        //    //        if (strongComponents != null)
        //    //            strongComponents.Last().Add(u);

        //    //        DFSR_Topological_StrongComponents(graph, u, strongComponents);
        //    //    }
        //    //    Console.WriteLine();
        //    //}

        //    //_t += 1;
        //    //_f[s] = _t;
        //    //Console.Write(new string('.', stackLevel) + ":");
        //    //Console.WriteLine("f[{0}] = {1}", s, _t);

        //    //--stackLevel;
        //}

    }
         
}
