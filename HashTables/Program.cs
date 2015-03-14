using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            //Теоретическое задание 2 - 3
            //Test2_3();

            //Test1("06");
            //Test2("06");
            Task("06");

            Console.Read();
        }

        private static  void Test2_3()
        {
            for(var i = 0.000;i <=1; i+=0.001)
            {
               
                var v1 = 1/(1 - i);
                var v2 = (1/i)*Math.Log(1/(1 - i));

                //среднее количество при неудачном поиске / 2 = среднее количество при удачном поиске.
                if (v1/2 >= v2)
                {
                    Console.Write("Step {0}: ==>", i);
                    Console.Write("V1:{0}  ", v1);
                    Console.Write("V2:{0}  ", v2);
                    Console.WriteLine();
                }
                
                //Тут главное, чтобы 1 / (1 - alpha) отличалось от 1 / alpha * ln(1 / (1 - alpha)) в не менее чем два раза
            }
            
        }

        private static void Test1(string test)
        {
            var array = new List<long>();
            var Smin = -1000;
            var Smax = 1000;

            var Scount = new List<long>();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\test_" + test + ".txt";

            var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead());

            while (!inputReader.EndOfStream)
            {
                var row = inputReader.ReadLine();

                array.Add(Convert.ToInt64(row));
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < array.Count; ++i)
            {
                //Console.WriteLine("Step:{0}", i);

                for (var j = 0; j < array.Count; ++j)
                {

                    if (i == j) continue;

                    var aiaj = array[i] + array[j];

                    if (aiaj >= Smin && aiaj <= Smax)
                    {
                        //Console.WriteLine("Step:i:{0} j:{1} --> {2}", i, j, aiaj);
                        if (!Scount.Contains(aiaj))
                            Scount.Add(aiaj);
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine("Количество возможных значение S в интервале [-1000, 1000] (включительно):{0} Время выполнения:{1} msec",
                Scount.Count,
                stopwatch.ElapsedMilliseconds);
        }

        private static void Test2(string test)
        {
            var array = new Hashtable();
            var Smin = -1000;
            var Smax = 1000;

            var Scount = new List<long>();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание (тест): " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\test_" + test + ".txt";

            using (var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()))
            {
                while (!inputReader.EndOfStream)
                {
                    var row = inputReader.ReadLine();
                    var x = Convert.ToInt64(row);

                    if (!array.Contains(x))
                    {
                        array.Add(x, x);
                    }
                }
            }

            Console.WriteLine("Массив:{0}", array.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var keys = array.Keys;
            var i = 0;
            foreach (var key in keys)
            {
                //Console.WriteLine("Step:{0}", i);

                var x = Convert.ToInt64(key);
                
                for (var S = Smin; S <= Smax; ++S)
                {
                    var y = S - x;

                    if (array.ContainsKey(y))
                    {
                        if (!Scount.Contains(y + x))
                        {
                            Scount.Add(y + x);
                            //Console.WriteLine("Added --> Количество:{0}", Scount.Count);
                        }
                    }
                }

                ++i;

                //for (var j = 0; j < array.Count; ++j)
                //{

                //    if (i == j) continue;

                //    var aiaj = array[i] + array[j];

                //    if (aiaj >= Smin && aiaj <= Smax)
                //    {
                //        //Console.WriteLine("Step:i:{0} j:{1} --> {2}", i, j, aiaj);
                //        if (!Scount.Contains(aiaj))
                //            Scount.Add(aiaj);
                //    }
                //}
            }

            stopwatch.Stop();

            Console.WriteLine("Количество возможных значение S в интервале [-1000, 1000] (включительно):{0} Время выполнения:{1} msec", 
                Scount.Count,
                stopwatch.ElapsedMilliseconds);
        }

        private static void Task(string test)
        {
            var array = new Hashtable();
            var Smin = -1000;
            var Smax = 1000;

            var Scount = new List<long>();
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Практическое задание: " + test);
            Console.WriteLine("--------------------------------------------");

            var inputFileName = @"Data\input_" + test + ".txt";

            using (var inputReader = new StreamReader((new FileInfo(inputFileName)).OpenRead()))
            {
                while (!inputReader.EndOfStream)
                {
                    var row = inputReader.ReadLine();
                    var x = Convert.ToInt64(row);

                    if (!array.Contains(x))
                    {
                        array.Add(x, x);
                    }
                }
            }

            Console.WriteLine("Массив:{0}", array.Count);

            var keys = array.Keys;
            var i = 0;
            foreach (var key in keys)
            {
                Console.WriteLine("Step:{0}", i);

                var x = Convert.ToInt64(key);

                for (var S = Smin; S <= Smax; ++S)
                {
                    var y = S - x;

                    if (array.ContainsKey(y))
                    {
                        if (!Scount.Contains(y + x))
                        {
                            Scount.Add(y + x);
                            Console.WriteLine("Added --> Количество:{0}", Scount.Count);
                        }
                    }
                }

                ++i;

                //for (var j = 0; j < array.Count; ++j)
                //{

                //    if (i == j) continue;

                //    var aiaj = array[i] + array[j];

                //    if (aiaj >= Smin && aiaj <= Smax)
                //    {
                //        //Console.WriteLine("Step:i:{0} j:{1} --> {2}", i, j, aiaj);
                //        if (!Scount.Contains(aiaj))
                //            Scount.Add(aiaj);
                //    }
                //}
            }

            Console.WriteLine(
                "Количество возможных значение S в интервале [-1000, 1000] (включительно):{0}" , Scount.Count);
        }
    }

}
