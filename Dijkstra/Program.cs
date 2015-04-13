using System;
using System.Collections.Generic;
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
            var _a = _graph.Keys.ToDictionary(g => g, g => decimal.MaxValue);
            var _b = new Dictionary<T, T>();
            var _x = new List<T>();

            _a[s] = 0;
            _x.Add(s);
            var v = s;
            while (_x.Count < _a.Count)
            {
                foreach (var u in _graph[v])
                {
                    if (_a[u.Key] > _a[v] +  u.Value)
                    {
                        _a[u.Key] = _a[v] + u.Value;
                        _b[u.Key] = v;
                    }
                }

                var minValue = decimal.MaxValue;
                foreach (var u in _a)
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

            a = _a;
            b = _b;
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
                var v = queue.Dequeue();
  
                var vValue = _graph[v];
                var vCount = _graph[v].Count;

                for (var i = 0; i < vCount; ++i)
                {
                    var u = vValue[i];
                    if (a[v] < decimal.MaxValue && a[u.Key] > a[v] + u.Value)
                    {
                        a[u.Key] = a[v] + u.Value;
                        b[u.Key] = v;

                        queue.DecreaseKey(u.Key, a[u.Key]);
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
                if (_array[i].Key == key)
                {
                    qi = _array[i];
                    qi.Value = value;

                    HeapDecreseKey(i, value);
                }
                ++i;
            }
        }
    }
}
