using System;
using System.IO;

namespace QuickSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Test0();
            //TestQuickSortLast();
            //Task1();
            //TestQuickSortFirst();
            //Task2();
            //TestQuickSortMedina();
            Task3();
        }

        public static void Test0()
        {
            //Passed
            //var inputFileName = @"Data\data_examples_03\input__10.txt"; ;
            //var outputFileName = @"Data\data_examples_03\output__10.txt";

            //var inputFileName = @"Data\data_examples_03\input__100.txt"; ;
            //var outputFileName = @"Data\data_examples_03\output__100.txt";

            var inputFileName = @"Data\data_examples_03\input__1000.txt"; ;
            var outputFileName = @"Data\data_examples_03\output__1000.txt";


            int[] array1 = null;
            int[] array2 = null;
            int[] array3 = null;

            var fileInfo = new FileInfo(inputFileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var i = 0;
            var row = textReader.ReadLine();
            array1 = new int[Convert.ToInt32(row)];
            array2 = new int[Convert.ToInt32(row)];
            array3 = new int[Convert.ToInt32(row)];

            while (!textReader.EndOfStream)
            {
                row = textReader.ReadLine();

                array1[i] = Convert.ToInt32(row);
                array2[i] = Convert.ToInt32(row);
                array3[i] = Convert.ToInt32(row);
                //Console.Write(row);

                ++i;
            }

            //Console.WriteLine("----------------------------");

            


            var s1 = new QuickSortLast(array1);
            var s1Result = s1.Sort();


            var s2 = new QuickSortFirst(array2);
            var s2Result = s2.Sort();

            var s3 = new QuickSortMedian(array3);
            var s3Result = s3.Sort();

            fileInfo = new FileInfo(outputFileName);
            fileStream = fileInfo.OpenRead();
            textReader = new StreamReader(fileStream);
            Console.WriteLine("Output:{0} ---> ({1},{2},{3})", textReader.ReadToEnd(), s1Result, s2Result, s3Result);
            //Console.WriteLine("----------------------------");

        }

        public static void TestQuickSortLast()
        {
            int[] array = { 3, 4, 9, 1, 8, 7, 2, 6, 5, 10 };
            //int[] array = {18, 3, 4, 15, 9, 1, 8, 11, 7, 2, 16, 6, 13, 5, 10, 12, 20, 14, 19, 17,};

            var sort = new QuickSortLast(array);
            Console.WriteLine("Количество сравненений:{0}", sort.Sort());

            //for (var ind = 0; ind < array.Length; ++ind)
            //{
            //    Console.Write("{0},", array[ind]);
            //}

            Console.WriteLine();

        }

        public static void Task1()
        {
            var fileName = @"Data\input__10000.txt";
            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            int[] array = null;

            var i = 0;
            var row = textReader.ReadLine();
            array = new int[Convert.ToInt32(row)];

            while (!textReader.EndOfStream)
            {
                row = textReader.ReadLine();

                array[i] = Convert.ToInt32(row);
                Console.Write("{0},", array[i]);
                ++i;
            }

            Console.WriteLine();

            var sort = new QuickSortLast(array);
            Console.WriteLine("Количество сравненений:{0}", sort.Sort());

            //for (i = 0; i < array.Length; ++i)
            //{
            //    Console.Write("{0},", array[i]);
            //}

            Console.WriteLine();
        }

        public static void TestQuickSortFirst()
        {
            //int[] array = { 3, 4, 9, 1, 8, 7, 2, 6, 5, 10 };
            int[] array = {18, 3, 4, 15, 9, 1, 8, 11, 7, 2, 16, 6, 13, 5, 10, 12, 20, 14, 19, 17,};

            var sort = new QuickSortFirst(array);
            Console.WriteLine("Количество сравненений:{0}", sort.Sort());

            //for (var ind = 0; ind < array.Length; ++ind)
            //{
            //    Console.Write("{0},", array[ind]);
            //}

            Console.WriteLine();

        }

        public static void Task2()
        {
            var fileName = @"Data\input__10000.txt";
            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            int[] array = null;

            var i = 0;
            var row = textReader.ReadLine();
            array = new int[Convert.ToInt32(row)];

            while (!textReader.EndOfStream)
            {
                row = textReader.ReadLine();

                array[i] = Convert.ToInt32(row);
                Console.Write("{0},", array[i]);
                ++i;
            }

            Console.WriteLine();

            var sort = new QuickSortFirst(array);
            Console.WriteLine("Количество сравненений:{0}", sort.Sort());

            //for (i = 0; i < array.Length; ++i)
            //{
            //    Console.Write("{0},", array[i]);
            //}

            Console.WriteLine();
        }

        public static void TestQuickSortMedina()
        {
            int[] array = { 3, 4, 9, 1, 8, 7, 2, 6, 5, 10 };
            //int[] array = { 18, 3, 4, 15, 9, 1, 8, 11, 7, 2, 16, 6, 13, 5, 10, 12, 20, 14, 19, 17, };

            var sort = new QuickSortMedian(array);
            Console.WriteLine("Количество сравненений:{0}", sort.Sort());

            //for (var ind = 0; ind < array.Length; ++ind)
            //{
            //    Console.Write("{0},", array[ind]);
            //}

            Console.WriteLine();

        }

        public static void Task3()
        {
            var fileName = @"Data\input__10000.txt";
            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            int[] array = null;

            var i = 0;
            var row = textReader.ReadLine();
            array = new int[Convert.ToInt32(row)];

            while (!textReader.EndOfStream)
            {
                row = textReader.ReadLine();

                array[i] = Convert.ToInt32(row);
                Console.Write("{0},", array[i]);
                ++i;
            }

            Console.WriteLine();

            var sort = new QuickSortMedian(array);
            Console.WriteLine("Количество сравненений:{0}",sort.Sort());

            //for (i = 0; i < array.Length; ++i)
            //{
            //    Console.Write("{0},", array[i]);
            //}

            Console.WriteLine();
        }
    }

    //***********************************
    // В качестве опорного элемента выбиратеся последний элементы массива 
    //**********************************
    public class QuickSortLast
    {
        private int[] _array;
        private int _comparisonCounter = 0;

        public QuickSortLast(int[] array)
        {
            _array = array;
        }

        public int Sort()
        {
            //Partition1(0, _array.Length - 1, 0);
            Partition(0, _array.Length - 1, 0);
            
            return _comparisonCounter;

            //Console.WriteLine("--------------------");
            //Console.WriteLine("Сравнений:{0}", _comparisonCounter);

            //Console.WriteLine("--------------------");

        }

        private void Partition(int p, int r, int level)
        {
            //Console.Write(new String('-', level) + "> ");
            //Console.Write("p:{0} r:{1} -  ", p, r);

            _comparisonCounter += (r - p);
            int value = 0;
            var x = _array[r];
            //Console.Write("x:{0} - ", x);
            var i = p - 1;
            for(var j = p; j <= (r -1); ++j)
            {
                //++_comparisonCounter;
                if (_array[j] <= x)
                {
                    i += 1;
                     value = _array[i];
                    _array[i] = _array[j];
                    _array[j] = value;
                }
            }

            value = _array[i + 1];
            _array[i + 1] = _array[r];
            _array[r] = value;

            var sortElement = i + 1;

            //Console.WriteLine();

            if (p < sortElement - 1)
                Partition(p, sortElement -1, level + 1);
            if (sortElement + 1 < r)
                Partition(sortElement + 1, r, level + 1);
        }
    }

    //***********************************
    // В качестве опорного элемента выбиратеся 1 элементы массива и 
    // без перестановки первого и последнего элементов
    //**********************************
    //public class QuickSortFirst
    //{
    //    private int[] _array;
    //    private int _comparisonCounter = 0;

    //    public QuickSortFirst(int[] array)
    //    {
    //        _array = array;
    //    }

    //    public void Sort()
    //    {
    //        //Partition1(0, _array.Length - 1, 0);
    //        Partition(0, _array.Length - 1, 0);

    //        Console.WriteLine("--------------------");
    //        Console.WriteLine("Сравнений:{0}", _comparisonCounter);

    //        Console.WriteLine("--------------------");

    //    }

    //    private void Partition(int p, int r, int level)
    //    {
    //        Console.Write(new String('-', level) + "> ");
    //        Console.Write("p:{0} r:{1} -  ", p, r);

    //        //_comparisonCounter += (r - p);
    //        int value = 0;
    //        var x = _array[p];
    //        Console.Write("x:{0} - ", x);
    //        var i = p;
    //        for (var j = p + 1; j <= r; ++j)
    //        {
    //            ++_comparisonCounter;
    //            if (_array[j] <= x)
    //            {
    //                i += 1;
    //                value = _array[i];
    //                _array[i] = _array[j];
    //                _array[j] = value;
    //            }
    //        }

    //        value = _array[i];
    //        _array[i] = _array[p];
    //        _array[p] = value;

    //        var sortElement = i;

    //        //for (var ind = 0; ind < _array.Length; ++ind)
    //        //{
    //        //    Console.Write("{0},", _array[ind]);
    //        //}

    //        Console.WriteLine();

    //        if (p < sortElement - 1)
    //            Partition(p, sortElement - 1, level + 1);
    //        if (sortElement + 1 < r)
    //            Partition(sortElement + 1, r, level + 1);
    //    }
    //}

    //***********************************
    // В качестве опорного элемента выбиратеся 1 элементы массива и 
    // с перестановкой первого и последнего элементов
    //**********************************
    public class QuickSortFirst
    {
        private int[] _array;
        private int _comparisonCounter = 0;

        public QuickSortFirst(int[] array)
        {
            _array = array;
        }

        public int Sort()
        {
            //Partition1(0, _array.Length - 1, 0);
            Partition(0, _array.Length - 1, 0);

             return _comparisonCounter;

            //Console.WriteLine("--------------------");
            //Console.WriteLine("Сравнений:{0}", _comparisonCounter);

            //Console.WriteLine("--------------------");

        }

        private void Partition(int p, int r, int level)
        {
            int value = 0;
            value = _array[r];
            _array[r] = _array[p];
            _array[p] = value;

            //Console.Write(new String('-', level) + "> ");
            //Console.Write("p:{0} r:{1} -  ", p, r);

            _comparisonCounter += (r - p);
            var x = _array[r];
            //Console.Write("x:{0} - ", x);
            var i = p - 1;
            for (var j = p; j <= (r - 1); ++j)
            {
                //++_comparisonCounter;
                if (_array[j] <= x)
                {
                    i += 1;
                    value = _array[i];
                    _array[i] = _array[j];
                    _array[j] = value;
                }
            }

            value = _array[i + 1];
            _array[i + 1] = _array[r];
            _array[r] = value;

            var sortElement = i + 1;

            //Console.WriteLine();

            if (p < sortElement - 1)
                Partition(p, sortElement - 1, level + 1);
            if (sortElement + 1 < r)
                Partition(sortElement + 1, r, level + 1);
        }
    }

    //***********************************
    // В качестве опорного элемента выбиратеся медиана между первым, последним и средним 
    //   элементами массивва
    //**********************************
    public class QuickSortMedian
    {
        private int[] _array;
        private int _comparisonCounter = 0;

        public QuickSortMedian(int[] array)
        {
            _array = array;
        }

        public int Sort()
        {
            //Partition1(0, _array.Length - 1, 0);
            Partition(0, _array.Length - 1, 0);

            return _comparisonCounter;

            //Console.WriteLine("--------------------");
            //Console.WriteLine("Сравнений:{0}", _comparisonCounter);

            //Console.WriteLine("--------------------");

        }

        private void Partition(int p, int r, int level)
        {
            //Console.Write(new String('-', level) + "> ");
            //Console.Write("p:{0} r:{1} -  ", p, r);

            int x;
            int i;
            

            if (p + 1 < r)
            {
                var medianArray = new[] {_array[p], _array[Math.Abs(((p + r)/2))], _array[r]};

                Console.Write("(x1:{0},x2:{1},x3:{2}) ", medianArray[0], medianArray[1], medianArray[2]);
                
                Array.Sort(medianArray);

                x = medianArray[1];
                if (x == _array[p])
                    i = p;
                else if (x == _array[Math.Abs(((p + r)/2))])
                    i = Math.Abs(((p + r)/2));
                else
                    i = r;

                Console.Write("--> x:{0} - ", x);
            }
            else
            {
                x = _array[p];
                i = p;
                Console.Write("(x1:{0}, x2:{1}) --> x:{2} - ", _array[p], _array[r], x);
            }


            var value = 0;
            value = _array[i];
            _array[i] = _array[r];
            _array[r] = value;
            
            _comparisonCounter += (r - p);
            

            i = p - 1;
            for (var j = p; j <= (r - 1); ++j)
            {
                //++_comparisonCounter;
                if (_array[j] <= x)
                {
                    i += 1;
                    value = _array[i];
                    _array[i] = _array[j];
                    _array[j] = value;
                }
            }

            value = _array[i + 1];
            _array[i + 1] = _array[r];
            _array[r] = value;

            var sortElement = i + 1;

            //for (var ind = p; ind < r; ++ind)
            //{
            //    Console.Write("{0},", _array[ind]);
            //}
            Console.WriteLine();

            if (p < sortElement - 1)
                Partition(p, sortElement - 1, level + 1);
            if (sortElement + 1 < r)
                Partition(sortElement + 1, r, level + 1);
        }
    }

}
