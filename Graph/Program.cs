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

            //Test1("08_1"); 
            //Test1("08_2");
            //Test1("08_3");
            //Test1("08_4");
            //Console.Read();
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

            var graph = new Graph();

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
                    }
                }
            }
        }
    }

    public class Graph
    {
        internal IDictionary<string, IList<string>> _graph;
        internal IDictionary<string, int> _bfs;
        internal IDictionary<string, int> _dfs;
        internal IDictionary<string, int> _ts;
        internal IDictionary<string, int> _f;
        internal IList<string> _dfs_t;
        internal IDictionary<string, int> _d;
        internal IDictionary<string, string> _p;
        private int _label;
        private int _t;


        public Graph()
        {
            _graph = new Dictionary<string, IList<string>>();
            _bfs = new Dictionary<string, int>();
            _d = new Dictionary<string, int>();
            _p = new Dictionary<string, string>();
            _f = new Dictionary<string, int>();

             _dfs = new Dictionary<string, int>();

             _ts = new Dictionary<string, int>();
            _dfs_t = new List<string>();
        }

        public void BreadthFirstSearch()
        {
            _bfs.Clear();
            _d.Clear();
            _p.Clear();

            var v = _graph.Keys.First();
            BFS(v);
        }
        private void BFS(string s)
        {
            var bfsQueue = new Queue<string>();

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
        private int DFSR(string s, int k)
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
        private void DFS(string s)
        {
            var k = 1;
            var dfsStack = new Stack<string>();
            
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
        private void DFSR_Topological(string s)
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

        
        public IList<IList<string>> StrongConnectedComponents()
        {
            var strongComponents2 = new List<IList<string>>();
            
            //1
            DFSLoop1(_graph, null);

            //2 - транспонировать графф
            var traponedGraph = new Dictionary<string, IList<string>>();

            foreach (var v in _graph.Keys)
            {
                traponedGraph.Add(v, new List<string>());
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
        internal void DFSLoop1(IDictionary<string, IList<string>> graph, IList<IList<string>> strongComponents)
        {
            _dfs_t.Clear();
            _t = 0;
            foreach (var v in graph.Keys)
            {
                if (!_dfs_t.Contains(v))
                {
                    if (strongComponents != null)
                        strongComponents.Add(new List<string> { v } );

                    DFSR_Topological_StrongComponents(graph, v, strongComponents);
                }
            }
        }
        internal void DFSLoop2(IDictionary<string, IList<string>> graph, IList<IList<string>> strongComponents)
        {
            _dfs_t.Clear();
            _t = 0;

            var _f_sorted = _f.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var f in _f_sorted)
            {
                var v = f.Key;
                if (!_dfs_t.Contains(v))
                {
                    if (strongComponents != null)
                        strongComponents.Add(new List<string> { v });

                    DFSR_Topological_StrongComponents(graph, v, strongComponents);
                }
            }
        }
        internal void DFSR_Topological_StrongComponents(IDictionary<string, IList<string>> graph, string s, IList<IList<string>> strongComponents)
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
    }
}
