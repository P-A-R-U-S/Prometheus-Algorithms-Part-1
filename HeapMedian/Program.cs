using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HeapMedian
{
    class Program
    {
        static IList<int> _htl;
        static Heap _highToLow;

        static IList<int> _lth;
        static Heap _lowToHigh;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            //Раскоментрируйте чтобы запустить тесты
            /*
            Test("01_10");
            Test("02_10");
            Test("03_10");
            Test("04_10");

            Test("05_100");
            Test("06_100");
            Test("07_100");
            Test("08_100");

            Test("09_1000");
            Test("10_1000");
            Test("11_1000");
            Test("12_1000");
             */

        }

        private static void Test(string test)
        {
            _htl = new List<int>();
            _highToLow = new Heap(_htl, HeapType.HighToLow);

            _lth = new List<int>();
            _lowToHigh = new Heap(_lth, HeapType.LowToHigh);

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Test: " + test);
            Console.WriteLine("--------------------------------------------");


            var inputFileName = @"Data\data_examples_05\input_" + test  + ".txt";
            var extOutputFileName = @"Data\data_examples_05\ext_output_" + test + ".txt";
            var heapsFileName = @"Data\data_examples_05\heaps_" + test + ".txt";
            var outputFileName = @"Data\data_examples_05\output_" + test + ".txt";

            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead());
            var extOutputReader = new StreamReader((new FileInfo(extOutputFileName)).OpenRead());
            var heapsReader = new StreamReader((new FileInfo(heapsFileName)).OpenRead());
            var outputReader = new StreamReader((new FileInfo(outputFileName)).OpenRead());


            var i = 0;
            var row = inputReader.ReadLine();
            Console.WriteLine("Количество элементов:" + row);

            while (!inputReader.EndOfStream)
            {
                row = inputReader.ReadLine();
                var value = Convert.ToInt32(row);

                var median = Step(value);
                var originMedian = outputReader.ReadLine();


                Console.Write("Median: " + originMedian + " --> " + median + ":");
                if (originMedian == median)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Passed");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed.");
                    Console.WriteLine("To continie click Enter.");
                    Console.Read();
                }

                Console.ResetColor();

                ++i;
            }
        }

        private static string Step(int value)
        {
            var extractMax = _highToLow.Max;
            var extractMin = _lowToHigh.Min;

            if (extractMax == null && extractMin == null)
            {
                _highToLow.Insert(value);
                return Convert.ToString(value);
            }

            if (value < extractMax)
                _highToLow.Insert(value);
            else
                _lowToHigh.Insert(value);

            if (_highToLow.Size > _lowToHigh.Size + 1)
            {
                var maxHighToLow = _highToLow.ExtractMax();
                _lowToHigh.Insert(maxHighToLow.Value);
            }

            if (_lowToHigh.Size > _highToLow.Size + 1)
            {
                var minLowToHigh = _lowToHigh.ExtractMin();
                _highToLow.Insert(minLowToHigh.Value);
            }

            //GetMedian
            var median = string.Empty;
            if (_highToLow.Size == _lowToHigh.Size)
            {
                median = String.Format("{0} {1}", _highToLow.Max, _lowToHigh.Min);
            }

            if (_highToLow.Size > _lowToHigh.Size)
            {
                median = String.Format("{0}", _highToLow.Max);
            }

            if (_lowToHigh.Size > _highToLow.Size)
            {
                median = String.Format("{0}", _lowToHigh.Min);
            }

            return median;
        }
    }

    

    public enum HeapType
    {
        HighToLow = 1,
        LowToHigh = 2
    }

    public class Heap
    {
        private readonly HeapType _heapType;
        private readonly IList<int> _array;
        private readonly int _d;

        public Heap(IList<int> a, HeapType heapType)
        {
            _heapType = heapType;
            _array = a;
            _d = 2;
        }

        public Heap(HeapType heapType)
        {
            _heapType = heapType;
            _array = new List<int>();
            _d = 2;
        }

        //Высота Пирамиды  H = log d (n), где N количество элементов пирамиды

        // Index родительского элемента узла
        internal int Parent(int index)
        {
            return Math.Abs( (index - 1) / _d );
        }

        //Индекс левого элемента узла
        private int Left(int index)
        {
            //d(x−1)+j+1 
            //_d * (index - 1) + 1 + 1; 
            //index * _d;
            return _d * index  + 1;
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
        public int Size {
            get { return _array.Count; }
        }

        #region Heapify
        internal void MaxHeapify(int index, int heapSize)
        {
            var l = Left(index);
            var r = Right(index);

            var largest = (l <= (heapSize - 1) && _array[l] > _array[index])
                ? l
                : index;

            if (r <= (heapSize - 1) && _array[r] > _array[largest])
                largest = r;

            if (largest != index)
            {
                var value = _array[index];
                _array[index] = _array[largest];
                _array[largest] = value;
                MaxHeapify(largest, heapSize);
            }
        }

        internal void MinHeapify(int index, int heapSize)
        {
            var l = Left(index);
            var r = Right(index);

            var lowest = (l <= (heapSize - 1) && _array[l] < _array[index])
                ? l
                : index;

            if (r <= (heapSize - 1) && _array[r] < _array[lowest])
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

        public void Build()
        {
            if (_heapType == HeapType.HighToLow)
                BuildMaxHeap();

            if(_heapType == HeapType.LowToHigh)
                BuildMinHeap();
        }
        #endregion 

        #region Sort

        private void SortHighToLow()
        {
            BuildMaxHeap();
            var heapSize = Size;
            for (var i = heapSize - 1; i >= 0; --i)
            {
                var value = _array[0];
                _array[0] = _array[i];
                _array[i] = value;

                heapSize -= 1;
                MaxHeapify(0, heapSize);

                //Console.Write("Step:{0}  ", i);
                //var isSplited = false;
                //for (var j = 0; j < Size; ++j)
                //{
                //    Console.Write("{0:##} ", _array[j]);
                //    if (j < heapSize || isSplited)
                //        Console.Write("|");
                //    else
                //    {
                //        isSplited = true;
                //        Console.Write("|--> ");
                //    }
                //}
                //Console.WriteLine();
            }
        }

        private void SortLowToHigh()
        {
            BuildMinHeap();
            var heapSize = Size;
            for (var i = heapSize - 1; i >= 0; --i)
            {
                var value = _array[0];
                _array[0] = _array[i];
                _array[i] = value;
                heapSize -= 1;
                MinHeapify(0, heapSize);
            }
        }

        public void Sort()
        {
            if (_heapType == HeapType.HighToLow) SortHighToLow();

            if (_heapType == HeapType.LowToHigh) SortLowToHigh();

        }
        #endregion

        #region Max

        private int? MaxHighToLow
        {
            get
            {
                if (_array.Count == 0) return null;
                return _array[0];
            }
        }

        private int? MaxLowToHigh
        {
            get
            {
                if (_array.Count == 0) return null;

                throw new NotImplementedException("Not implemented....");
            }
        }

        public int? Max 
        {
            get
            {
                if (_heapType == HeapType.HighToLow) return MaxHighToLow;
                if (_heapType == HeapType.LowToHigh) return MaxLowToHigh;

                throw new Exception("HeapType not defined.");
            }
        }

        #endregion

        #region Min
        private int? MinHighToLow
        {
            get
            {
                if (_array.Count == 0) return null;

                throw new NotImplementedException("Not implemented....");
            }
        }

        private int? MinLowToHigh
        {
            get
            {
                if (_array.Count == 0) return null;

                return _array[0];
            }
           
        }

        public int? Min
        {
            get
            {
                if (_heapType == HeapType.HighToLow) return MinHighToLow;
                if (_heapType == HeapType.LowToHigh) return MinLowToHigh;

                throw new Exception("HeapType not defined.");
            }
        }
        #endregion

        #region ExtractMax

        private int? ExtractMaxHighToLow()
        {
            if (_array.Count == 0) return null;

            var max = _array[0];
            _array[0] = _array[Size - 1];
            _array.RemoveAt(Size-1);
            
            MaxHeapify(0,Size);

            return max;
        }

        private int? ExtractMaxLowToHigh()
        {
            if (_array.Count == 0) return null;

            throw  new NotImplementedException("Not implemented...");
        }

        public int? ExtractMax()
        {
            if (_heapType == HeapType.HighToLow) return ExtractMaxHighToLow();
            if (_heapType == HeapType.LowToHigh) return ExtractMaxLowToHigh();

            throw new Exception("HeapType not defined.");

        }

        #endregion

        #region ExtractMin

        private int? ExtractMinHighToLow()
        {
            if (_array.Count == 0) return null;

            throw new NotImplementedException("Not implemented...");

        }

        private int? ExtractMinLowToHigh()
        {
            if (_array.Count == 0) return null;

            var max = _array[0];
            _array[0] = _array[Size - 1];
            _array.RemoveAt(Size - 1);

            MinHeapify(0, Size);

            return max;
        }

        public int? ExtractMin()
        {
            if (_heapType == HeapType.HighToLow) return ExtractMinHighToLow();
            if (_heapType == HeapType.LowToHigh) return ExtractMinLowToHigh();

            throw new Exception("HeapType not defined.");

        }

        #endregion

        #region Insert
        internal void HeapIncreaseKey(int index, int value)
        {
            _array[index] = value;
            while (index > 0 && _array[Parent(index)] < _array[index])
            {
                var parentValue = _array[Parent(index)];
                _array[Parent(index)] = _array[index];
                _array[index] = parentValue;

                index = Parent(index);
            }
        }
        internal void HeapDecreseKey(int index, int value)
        {
            _array[index] = value;
            while (index > 0 && _array[Parent(index)] > _array[index])
            {
                var parentValue = _array[Parent(index)];
                _array[Parent(index)] = _array[index];
                _array[index] = parentValue;

                index = Parent(index);
            }
        }

        public void Insert(int value)
        {
            _array.Add(default(int));
            //heapSize(A) = heapSize(A) + 1

            if(_heapType == HeapType.HighToLow)
                HeapIncreaseKey(Size - 1, value);

            if(_heapType == HeapType.LowToHigh)
                HeapDecreseKey(Size - 1, value);
        }

        #endregion
    }
}
