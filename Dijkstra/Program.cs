#define LOG1

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dijkstra
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            //==============
            //10 x 10
            //==============
            //Test1("5_10", "5");
            //Test1("6_10", "6");
            //Test1("7_10", "7");
            //Test1("8_10", "8");

            //==============
            //100 x 100
            //==============
            //Test1("1_100", "1"); //Отсутствуют вершины - 94
            //Test1("2_100", "2"); //Отсутствуют вершины - 57, 61
            //Test1("3_100", "3"); //Отсутствуют вершины - 

            //==============
            //1000 x 1000
            //==============
            //Test1("4_1000", "4"); //Отсутствуют вершины - 

            Task1("USA-FLA");

            Console.Read();
            Console.Read();
        }

        private static void Test1(string input, string output)
        {

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1 (тест): " + input);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\test_09\input_" + input + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead());

            var outputFileName = @"Data\test_09\output_" + output + ".txt";
            var outputReader = new StreamReader((new FileInfo(outputFileName)).OpenRead());

            var graph = new Graph<string>();
            var vertexCount = 0;
            var edgeCount = 0;

            #region INPUT

            var keys = new List<int>();
            var row = inputReader.ReadLine();
            var vs = row.Split(' ');
            vertexCount = Convert.ToInt32(vs[0]);
            edgeCount = Convert.ToInt32(vs[0]);

            graph._graph = new Dictionary<string, IList<KeyValuePair<string, decimal>>>(vertexCount);

            while (!inputReader.EndOfStream)
            {
                row = inputReader.ReadLine();
                vs = row.Split(' ');

                var key = vs[1];
                var cost = Convert.ToDecimal(vs[2]);

                if(!keys.Contains(Convert.ToInt32(key))) keys.Add(Convert.ToInt32(key));

                if (!graph._graph.ContainsKey(vs[0]))
                {
                    graph._graph.Add(new KeyValuePair<string, IList<KeyValuePair<string, decimal>>>(vs[0], new List<KeyValuePair<string, decimal>>()));
                }

                graph._graph[vs[0]].Add(new KeyValuePair<string, decimal>(key, cost));
            }

            //Validate Graph
            var length = graph._graph.Count;
            for (var i = 0; i < length; ++i)
            {
                var v = graph._graph.Keys.ElementAt(i);
                foreach (var u in graph._graph[v])
                {
                    if (!graph._graph.ContainsKey(u.Key))
                    {
                        graph._graph.Add(u.Key, null);
                    }
                }
            }

            #endregion

            #region OUTPUT

            var outPutResults = new int?[vertexCount][];

            Console.WriteLine("OUTPUT:");

            {
                var i = 0;
                while (!outputReader.EndOfStream)
                {
                    row = outputReader.ReadLine();
                    vs = row.Split(' ');

                    var vsCount = vs.Length;
                    outPutResults[i] = new int?[vertexCount];

                    for (var j = 0; j < vsCount; ++j)
                    {
                        int shortPath;
                        if (int.TryParse(vs[j], out shortPath))
                        {
                            outPutResults[i][j] = shortPath;
                        }
                    }

                    ++i;

                    Console.WriteLine(row);
                }
            }
        

            Console.WriteLine(new string('.', 50));
            #endregion

            var resultFile = File.AppendText(input + "-result.txt");

            for (var i=0; i< outPutResults.Length; ++i)
            {
                var outPutResult = outPutResults[i];
                for (var j = 0; j < outPutResult.Length; ++j)
                {
                    var result = outPutResult[j];

                    if (result == null) continue;


                    var s = Convert.ToString(i + 1); //keys[i]);
                    var e = Convert.ToString(j + 1); //keys[j]);

                    if (!graph._graph.ContainsKey(s))
                    {
                        Console.Write("----> Начальная вершина: {0}", s);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("- НЕ СУЩЕСТВУЕТ.");
                        Console.ResetColor();

                        resultFile.Write("----> Начальная вершина: {0} - НЕ СУЩЕСТВУЕТ.", s);
                        continue;
                    }

                    if (!graph._graph.ContainsKey(e))
                    {
                        Console.Write("----> Конечная вершина: {0}", e);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("- НЕ СУЩЕСТВУЕТ.");
                        Console.ResetColor();

                        resultFile.Write("----> Конечная вершина: {0} - НЕ СУЩЕСТВУЕТ.", e);

                        continue;
                    }

                    IDictionary<string, decimal> a;
                    IDictionary<string, string> b;

                    graph.Dijkstra2(s, e, out a, out b);

                    Console.Write("{0}:{1} --> (Output:{2}, Result:{3}) -- ", s, e, result, a[e]);
                    if (a[e] == result)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Passed");
                        Console.ResetColor();
                        continue;
                    }

                    resultFile.Write("{0}:{1} --> (Output:{2}, Result:{3}) -- Failed", s, e, result, a[e]);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed");
                    Console.ResetColor();
                }
            }
        }

        private static void Task1(string input)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание #1: " + input);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("ATTENTION: Выполнение задания занимает значительное время. Ориентировочно 48 часов.");
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\" + input + ".txt";
            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead());

            var graph = new Graph<string>();
            var vertexCount = 0;
            var edgeCount = 0;

            #region INPUT

            var row = inputReader.ReadLine();
            var vs = row.Split(' ');
            vertexCount = Convert.ToInt32(vs[0]);
            edgeCount = Convert.ToInt32(vs[0]);

            graph._graph = new Dictionary<string, IList<KeyValuePair<string, decimal>>>(vertexCount);

            while (!inputReader.EndOfStream)
            {
                row = inputReader.ReadLine();
                vs = row.Split(' ');

                var key = vs[1];
                var cost = Convert.ToDecimal(vs[2]);

                if (!graph._graph.ContainsKey(vs[0]))
                {
                    graph._graph.Add(new KeyValuePair<string, IList<KeyValuePair<string, decimal>>>(vs[0], new List<KeyValuePair<string, decimal>>()));
                }

                graph._graph[vs[0]].Add(new KeyValuePair<string, decimal>(key, cost));
            }

            //Validate Graph
            var length = graph._graph.Count;
            for (var i = 0; i < length; ++i)
            {
                var v = graph._graph.Keys.ElementAt(i);
                foreach (var u in graph._graph[v])
                {
                    if (!graph._graph.ContainsKey(u.Key))
                    {
                        graph._graph.Add(u.Key, null);
                    }
                }
            }

            #endregion
            Console.WriteLine("Файл прочитан:{0}", DateTime.Now);

            var resultFile = File.AppendText(input + "-result.txt");

            var s = "100562";
            var e = "1070345";
            IDictionary<string, decimal> a;
            IDictionary<string, IList<string>> b;

            graph.Dijkstra3(s, e, out a, out b);

            Console.WriteLine("Путь расчитан:{0}", DateTime.Now);

            Console.WriteLine(new string('.', 50));
            var question1 =
                String.Format("Вопрос 1: Наиболее короткое расcтояние между вершинами: {0} --> {1} равно: {2}", s, e,
                    a[e]);
            Console.WriteLine(question1);

            resultFile.Write(question1);
            resultFile.Write(Environment.NewLine);
            
            Console.WriteLine(new string('.', 50));

            var pathCount = getShortestPath(s, e, b);
            var question2 =
                String.Format("Вопрос 2: Количество уникальных коротких путей между вершинами: {0} --> {1} равно: {2}", s, e,
                    pathCount.Length);
            Console.WriteLine(question2);

            resultFile.Write(question2);
            resultFile.Write(Environment.NewLine);
            resultFile.Flush();
            resultFile.Dispose();
            resultFile.Close();

            Console.WriteLine();
            Console.WriteLine();

        }
        private static string[] getShortestPath(string s, string e, IDictionary<string, IList<string>> b)
        {
            var pathes = new List<string>();

            if (!b.ContainsKey(e))
            {
                return new[] { e };
            }

            foreach (var v in b[e])
            {
                var pp = getShortestPath(s, v, b);
                pathes.AddRange(pp.Select(p => e + " " + p));
            }

            return pathes.ToArray();
        }
    }

    public class Graph<T> where T: class
    {
        internal IDictionary<T, IList<KeyValuePair<T, decimal>>> _graph;

        public Graph()
        {
            _graph = new Dictionary<T, IList<KeyValuePair<T, decimal>>>();
        }

        //Алгоритм с использование циклов. Время выполенения Q(n^2)
        public void Dijkstra1(T s, T e, out IDictionary<T, decimal> a, out IDictionary<T, T> b)
        {
            a = _graph.Keys.ToDictionary(g => g, g => decimal.MaxValue);
            b = new Dictionary<T, T>();
            var _x = new List<T>();

            a[s] = 0;
            _x.Add(s);
            var v = s;
            while (_x.Count < a.Count)
            {
                foreach (var u in _graph[v])
                {
                    if (a[u.Key] > a[v] +  u.Value)
                    {
                        a[u.Key] = a[v] + u.Value;
                        b[u.Key] = v;
                    }
                }

                var minValue = decimal.MaxValue;
                foreach (var u in a)
                {
                    if (u.Value < minValue &&  !_x.Contains(u.Key))
                    {
                        minValue = u.Value;
                        _x.Add(u.Key);
                        v = u.Key;
                    }
                }

                if (v == e)
                     break;
            }
        }

        //Алгоритм с использование приоритезированной очереди. Время выполенения Q(n*log n)
        public void Dijkstra2(T s, T e, out IDictionary<T, decimal> a, out IDictionary<T, T> b)
        {            
            var queue = new Queue<T>(QueueType.LowToHigh);
            a = _graph.Keys.ToDictionary(g => g, g => decimal.MaxValue);
            b = new Dictionary<T, T>();

            a[s] = 0;

            a.ToList().ForEach(v => queue.Enqueue(v.Key, v.Value));

            while (queue.Size > 0)
            {
#if LOG               
                Console.WriteLine("-------");  
#endif
                var v = queue.Dequeue();
#if LOG
                Console.WriteLine("var v = queue.Dequeue(): -- >v = {0}", v);
#endif                
                if(_graph[v] == null) continue;

                var vValue = _graph[v];
                var vCount = _graph[v].Count;

                for (var i = 0; i < vCount; ++i)
                {
                    
                    var u = vValue[i];
#if LOG
                    Console.WriteLine("          var u = vValue[i];  --> u = {0}", u);

                    Console.WriteLine("          a[v]:{0} < decimal.MaxValue && a[u.Key]:({1}) > a[v]:({0}) + u.Value:({2});", a[v], a[u.Key], u.Value);
#endif 
                    if (a[v] < decimal.MaxValue && a[u.Key] >  a[v] + u.Value)
                    {
                        a[u.Key] = a[v] + u.Value;

                        if (b.ContainsKey(u.Key))
                        {
                            var err = 1;
                        }

                        b[u.Key] = v;
                        queue.DecreaseKey(u.Key, a[u.Key]);
#if LOG                        
                        Console.WriteLine("          a[u.Key]:{0} = a[v]:({1}) + u.Value:({2})", u.Key, a[v], u.Value);
                        Console.WriteLine("          b[u.Key]:({0}) = v;:({1})", u.Key, v);
                        Console.WriteLine("          queue.DecreaseKey(u.Key:({0}), a[u.Key];({1}))", u.Key, a[u.Key]);
                        Console.WriteLine();
#endif 

                    }
                }
            }
        }

        //Алгоритм с использование приоритезированной очереди и нахождением нескольких . 
        //Время выполенения Q(n*log n)
        public void Dijkstra3(T s, T e, out IDictionary<T, decimal> a, out IDictionary<T, IList<T>> b)
        {
            var queue = new Queue<T>(QueueType.LowToHigh);
            a = _graph.Keys.ToDictionary(g => g, g => decimal.MaxValue);
            b = new Dictionary<T, IList<T>>();

            a[s] = 0;

            a.ToList().ForEach(v => queue.Enqueue(v.Key, v.Value));

            while (queue.Size > 0)
            {
#if LOG               
                Console.WriteLine("-------");  
#endif
                var v = queue.Dequeue();
#if LOG
                Console.WriteLine("var v = queue.Dequeue(): -- >v = {0}", v);
#endif
                if (_graph[v] == null) continue;

                var vValue = _graph[v];
                var vCount = _graph[v].Count;

                for (var i = 0; i < vCount; ++i)
                {
                    var u = vValue[i];
#if LOG
                    Console.WriteLine("          var u = vValue[i];  --> u = {0}", u);

                    Console.WriteLine("          if(a[v]:{0} < decimal.MaxValue && a[u.Key]:({1}) > a[v]:({0}) + u.Value:({2}))", a[v], a[u.Key], u.Value);
#endif
                    if (a[v] < decimal.MaxValue && a[u.Key] > a[v] + u.Value)
                    {
                        a[u.Key] = a[v] + u.Value;
                        b[u.Key] = new List<T> {v};
                        queue.DecreaseKey(u.Key, a[u.Key]);
#if LOG                        
                        Console.WriteLine("          a[u.Key]:{0} = a[v]:({1}) + u.Value:({2})", u.Key, a[v], u.Value);
                        Console.WriteLine("          b[u.Key]:({0}) = new List<T> {{v:{1}}}; ", u.Key, v);
                        Console.WriteLine("          queue.DecreaseKey(u.Key:({0}), a[u.Key];({1}))", u.Key, a[u.Key]);
                        Console.WriteLine();
#endif
                    }
                    else if (a[v] < decimal.MaxValue && a[u.Key] == a[v] + u.Value)
                    {
                        if (!b[u.Key].Contains(v)) b[u.Key].Add(v);

#if LOG
                        Console.WriteLine("          else if (a[v]:{0} < decimal.MaxValue && a[u.Key]:({1}) == a[v]:({0}) + u.Value:({2}))", a[v], a[u.Key], u.Value);
                        Console.WriteLine("          b[u.Key::({0})].Add(v::({1});", u.Key, v);
                        Console.WriteLine();
#endif
                    }
                }
            }
        }
    }

    public enum QueueType
    {
        HighToLow = 1,
        LowToHigh = 2
    }

    //Приоритезированная очередь на основе Пирамид
    public class Queue<T> where T: class 
    {
        public class QueueItem
        {
            public T Key { get; set; }
            public decimal Value { get; set; }
        }

        private readonly QueueType _queueType;
        private readonly IList<QueueItem> _array;
        private readonly int _d;

        public Queue(QueueType queueType)
        {
            _queueType = queueType;
            _array = new List<QueueItem>();
            _d = 2;
        }

        //Высота Пирамиды  H = log d (n), где N количество элементов пирамиды

        // Index родительского элемента узла
        private int Parent(int index)
        {
            return Math.Abs((index - 1) / _d);
        }

        //Индекс левого элемента узла
        private int Left(int index)
        {
            //d(x−1)+j+1 
            //_d * (index - 1) + 1 + 1; 
            //index * _d;
            return _d * index + 1;
        }

        //Индекс правого элемента узла
        private int Right(int index)
        {
            //d(x−1)+j+1 
            //_d * (index - 1) + _d + 1;  
            //index * _d + (_d - 1); 
            return _d * index + _d;
        }

        //Размерность пирамиды
        public int Size
        {
            get { return _array.Count; }
        }

        #region Heapify
        private void MaxHeapify(int index, int heapSize)
        {
            var l = Left(index);
            var r = Right(index);

            var largest = (l <= (heapSize - 1) && _array[l].Value > _array[index].Value)
                ? l
                : index;

            if (r <= (heapSize - 1) && _array[r].Value > _array[largest].Value)
                largest = r;

            if (largest != index)
            {
                var value = _array[index];
                _array[index] = _array[largest];
                _array[largest] = value;
                MaxHeapify(largest, heapSize);
            }
        }

        private void MinHeapify(int index, int heapSize)
        {
            var l = Left(index);
            var r = Right(index);

            var lowest = (l <= (heapSize - 1) && _array[l].Value < _array[index].Value)
                ? l
                : index;

            if (r <= (heapSize - 1) && _array[r].Value < _array[lowest].Value)
                lowest = r;

            if (lowest != index)
            {
                var value = _array[index];
                _array[index] = _array[lowest];
                _array[lowest] = value;
                MinHeapify(lowest, heapSize);
            }
        }
        #endregion

        #region Build
        private void BuildMaxHeap()
        {
            var heapSize = Size;
            for (var i = Math.Abs(Size / 2) - 1; i >= 0; --i)
                MaxHeapify(i, heapSize);
        }

        private void BuildMinHeap()
        {
            var heapSize = Size;
            for (var i = Math.Abs(Size / 2) - 1; i >= 0; --i)
                MinHeapify(i, heapSize);
        }
        #endregion

        #region Max
        private QueueItem MaxHighToLow
        {
            get
            {
                if (_array.Count == 0) return default(QueueItem);
                return _array[0];
            }
        }

        private QueueItem MaxLowToHigh
        {
            get
            {
                if (_array.Count == 0) return default(QueueItem);

                throw new NotImplementedException("Not implemented....");
            }
        }
        #endregion

        #region Min
        private QueueItem MinHighToLow
        {
            get
            {
                if (_array.Count == 0) return default(QueueItem);

                throw new NotImplementedException("Not implemented....");
            }
        }

        private QueueItem MinLowToHigh
        {
            get
            {
                if (_array.Count == 0) return default(QueueItem);

                return _array[0];
            }

        }
        #endregion

        #region HeapIncreaseKey / HeapDecreseKey
        private void HeapIncreaseKey(int index, decimal value)
        {
            _array[index].Value = value;
            while (index > 0 && _array[Parent(index)].Value < _array[index].Value)
            {
                var parentValue = _array[Parent(index)];
                _array[Parent(index)] = _array[index];
                _array[index] = parentValue;

                index = Parent(index);
            }
        }
        private void HeapDecreseKey(int index, decimal value)
        {
            _array[index].Value = value;
            while (index > 0 && _array[Parent(index)].Value > _array[index].Value)
            {
                var parentValue = _array[Parent(index)];
                _array[Parent(index)] = _array[index];
                _array[index] = parentValue;

                index = Parent(index);
            }
        }
        #endregion

        public void Enqueue(T key, decimal value)
        {
            _array.Add(new QueueItem { Key = key, Value = value });

            if (_queueType == QueueType.HighToLow)
                HeapIncreaseKey(Size - 1, value);

            if (_queueType == QueueType.LowToHigh)
                HeapDecreseKey(Size - 1, value);
        }

        public T Dequeue()
        {
            T ret = null;
            if (_queueType == QueueType.HighToLow)
            {
                ret = MinHighToLow.Key;
                _array.RemoveAt(0);
                BuildMaxHeap();
            }
            if (_queueType == QueueType.LowToHigh)
            {
                ret = MinLowToHigh.Key;
                _array.RemoveAt(0);
                BuildMinHeap();
            }

            return ret;
        }

        public void DecreaseKey(T key, decimal value)
        {
            var qi = default(QueueItem);
            var i = 0;
            while (qi == null && i < Size)
            {
                if (EqualityComparer<T>.Default.Equals(_array[i].Key, key))
                {
                    HeapDecreseKey(i, value);
                }
                ++i;
            }
        }
    }
}
