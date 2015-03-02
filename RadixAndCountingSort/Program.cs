using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RadixAndCountingSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test0();
            Task1();
        }

        public static  void Test0()
        {
            var list = new string[10];

            var inputFileName = @"Data\input_10.txt";

            var fileInfo = new FileInfo(inputFileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var i = 0;
            while (!textReader.EndOfStream)
            {
                var row = textReader.ReadLine();

                list[i] = row;

                ++i;
            }

            var passwordFinder = new PasswordFinder(list);
            passwordFinder.GetPassword();

        }

        public static void Task1()
        {
            var list = new string[1000];

            var inputFileName = @"Data\anagrams.txt";

            var fileInfo = new FileInfo(inputFileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var i = 0;
            while (!textReader.EndOfStream)
            {
                var row = textReader.ReadLine();

                list[i] = row;

                ++i;
            }

            var passwordFinder = new PasswordFinder(list);
            passwordFinder.GetPassword();
        }
    }

    public class PasswordFinder
    {
        private string[] _array;
        private IDictionary<char, int> _charFrequiestyCounting = new Dictionary<char, int>();

        public PasswordFinder(string[] array)
        {
            _array = array;

            for (char c = 'a'; c <= 'z'; c++)
            {
                //use System.Convert.ToChar() f.e. here
                _charFrequiestyCounting.Add(c, 0);
            }

        }

        public void GetPassword()
        {

            var length = _array.Length;

            var d = 0;
            for (var i = 0; i < length; i++)
            {
                if (d < _array[i].Length) d = _array[i].Length;
            }

            for (var i = d - 1; i >= 0; i--)
            {
                var array = new char[length];
                Console.WriteLine("Step:" + i);
                for (var j = 0; j < length; ++j)
                {
                    array[j] = _array[j][i];
                    Console.Write(array[j] + ",");
                }
                Console.WriteLine();

                //var cs1 = new CountingSort(array);
                //cs1.Sort();

                array = CountingSort(array);

                for (var j = 0; j < length; ++j)
                {
                    Console.Write(array[j] + ",");
                }

                var arrayReplace = new string[length];
                for (var j = length - 1; j >= 0; j--)
                {
                    var c1 = array[j];
                    _charFrequiestyCounting[c1] = ++_charFrequiestyCounting[c1];

                    for (var j2 = length - 1; j2 >= 0; j2--)
                    {
                        if (_array[j2] == null) continue;
                        if (_array[j2][i] == c1)
                        {
                            arrayReplace[j] = _array[j2];
                            _array[j2] = null;
                            break;
                        }
                    }
                }
                Console.WriteLine();
                Console.Write("Array:");
                _array = arrayReplace;
                for (var j = 0; j < length; ++j)
                {
                    Console.Write(_array[j] + ",");
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            var maxChar = default(char);
            var maxCharRepetition = 0;
            foreach (var key in _charFrequiestyCounting.Keys)
            {
                if (maxCharRepetition < _charFrequiestyCounting[key])
                {
                    maxCharRepetition = _charFrequiestyCounting[key];
                    maxChar = key;
                }
            }

            foreach (var key in _charFrequiestyCounting.Keys)
            {
                if (maxCharRepetition == _charFrequiestyCounting[key])
                {
                    Console.WriteLine("{0}{1}{2}", _array[0], maxChar, _array[length - 1]);
                }
            }

        }


        public char[] CountingSort(char[] a)
        {
            var length = a.Length;

            var b = new char[length];
            var c = new Dictionary<char, int>();
           
            //Spep 1
            for (var j = 'a'; j <= 'z'; j++)
                if(a.Contains(j) && !c.ContainsKey(j))
                    c.Add(j, 0);
            
            //Step 2
            for (var i = 0; i < length; i++)
                c[a[i]] = c[a[i]] + 1;
           
            //Step 3
            var keys = c.Keys.ToArray();
            var cj_1 = 0;
            foreach (var j in keys)
            {
                c[j] = c[j] + cj_1;

                cj_1 = c[j];

            }

            //Step 4
            for (var i = length-1; i >= 0; i--)
            {
                var ai = a[i];
                var cai = c[a[i]];
                var bcai = b[c[a[i]] -1];

                b[c[a[i]]-1] = a[i];
                c[a[i]] = c[a[i]] - 1;
            }

            return b;
        }
    }


    public class CountingSort
    {
        private char[] _array;
        public CountingSort(char[] array)
        {
            _array = array;
        }

        public void Sort()
        {
            Array.Sort(_array);
        }
    }
}
