using System;
using System.IO;
using System.Text;

namespace Inversions
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            //Test();
            //Run1();
            Run2();
          
            Console.Read();
        }

        public static void Test()
        {
            //var fileName = @"Data\data_examples_02\test_10_5.txt";
            var fileName = @"Data\data_examples_02\test_100_50.txt";
            //var fileName = @"Data\data_examples_02\test_5_10.txt";
            //var fileName = @"Data\data_examples_02\test_5_5.txt";
            //var fileName = @"Data\data_examples_02\test_50_100.txt";


            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var isMatrixCreated = false;
            var isMatrixFilled = false;
            int[][] matrix = null;
            Inversions inversion = null;
            int users = 0, movies = 0;
            int userIndex = 0;
            int userCompareIndex = 0;


            while (!textReader.EndOfStream)
            {
                var row = textReader.ReadLine();

                if (string.IsNullOrEmpty(row)) Console.WriteLine();

                if (row.StartsWith("#"))
                {
                    Console.WriteLine(row);

                    if (row.StartsWith("# Для користувача №"))
                    {
                        var indexStart = 19;
                        var indexEnd = 19;
                        while (Char.IsDigit(row[indexEnd]))
                        {
                            ++indexEnd;
                        }
                        userCompareIndex = Convert.ToInt32(row.Substring(indexStart, indexEnd - indexStart));
                    }
                    continue;
                }

                if (string.IsNullOrEmpty(row) && !isMatrixCreated)
                {
                    Console.WriteLine("------------------");
                    continue;
                }

                if (string.IsNullOrEmpty(row) && isMatrixCreated && !isMatrixFilled)
                {
                    Console.WriteLine("------------------");
                    isMatrixFilled = true;

                    inversion = new Inversions(matrix);

                    continue;
                }


                if (!string.IsNullOrEmpty(row) && isMatrixCreated && isMatrixFilled)
                {
                    var splitRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var user2Index = Convert.ToInt32(splitRow[0]);
                    var user2Result = Convert.ToInt32(splitRow[1]);

                    var result = inversion.Inversion(userCompareIndex, user2Index);

                    Console.Write("Расcчитаем инверсии для пользователя {0:###} c пользователем {1:###} Ответ:{2:###}", userCompareIndex, user2Index, user2Result);
                    Console.Write(" Result:{0:###} - ", result);
                    Console.ForegroundColor = result == user2Result ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(result == user2Result ? "Passed" : "Failed");
                    Console.ResetColor();
                }

                if (!isMatrixCreated)
                {
                    var splitRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(row + " ");

                    users = Convert.ToInt32(splitRow[0]);
                    movies = Convert.ToInt32(splitRow[1]);

                    matrix = new int[users][];

                    for (var i = 0; i < users; ++i)
                        matrix[i] = new int[movies];

                    isMatrixCreated = true;

                    continue;
                }

                if (!isMatrixFilled)
                {
                    var splitRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < movies; ++i)
                    {
                        matrix[userIndex][i] = Convert.ToInt32(splitRow[i + 1]);
                        Console.Write(matrix[userIndex][i] + " ");
                    }
                    Console.WriteLine();
                    ++userIndex;
                }

            }

        }

        public static void Run1()
        {
            var fileName = @"Data\input_1000_5.txt";

            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var isMatrixCreated = false;
            var isMatrixFilled = false;
            int[][] matrix = null;
            Inversions inversion = null;
            int users = 0, movies = 0;
            int userIndex = 0;
 


            while (!textReader.EndOfStream)
            {
                var row = textReader.ReadLine();

                if (!isMatrixCreated)
                {
                    var splitRow = row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(row + " ");

                    users = Convert.ToInt32(splitRow[0]);
                    movies = Convert.ToInt32(splitRow[1]);

                    matrix = new int[users][];

                    for (var i = 0; i < users; ++i)
                        matrix[i] = new int[movies];

                    isMatrixCreated = true;

                    continue;
                }

                if (!isMatrixFilled)
                {
                    var splitRow = row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                    Console.Write("{0} - ", userIndex + 1);
                    for (var i = 0; i < movies; ++i)
                    {
                        matrix[userIndex][i] = Convert.ToInt32(splitRow[i + 1]);
                        Console.Write(matrix[userIndex][i] + " ");
                    }
                    Console.WriteLine();
                    ++userIndex;
                }
            }
            inversion = new Inversions(matrix);

            //Пользователь 452 и 100
            var result1 = inversion.Inversion(452, 100);

            Console.WriteLine("Расcчитаем инверсии для пользователя {0:###} c пользователем {1:###} Result:{2:###}", 
                452,
                100, 
                result1);

            //Пользователь 863 та 29.
            var result2 = inversion.Inversion(863, 29);

            Console.WriteLine("Расcчитаем инверсии для пользователя {0:###} c пользователем {1:###} Result:{2}",
                863,
                29,
                result2);
        }

        public static void Run2()
        {
            var fileName = @"Data\input_1000_100.txt";

            var fileInfo = new FileInfo(fileName);
            var fileStream = fileInfo.OpenRead();
            var textReader = new StreamReader(fileStream);

            var isMatrixCreated = false;
            var isMatrixFilled = false;
            int[][] matrix = null;
            Inversions inversion = null;
            int users = 0, movies = 0;
            int userIndex = 0;
           

            while (!textReader.EndOfStream)
            {
                var row = textReader.ReadLine();

                if (!isMatrixCreated)
                {
                    var splitRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(row + " ");

                    users = Convert.ToInt32(splitRow[0]);
                    movies = Convert.ToInt32(splitRow[1]);

                    matrix = new int[users][];

                    for (var i = 0; i < users; ++i)
                        matrix[i] = new int[movies];

                    isMatrixCreated = true;

                    continue;
                }

                if (!isMatrixFilled)
                {
                    var splitRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Console.Write("{0} - ", userIndex + 1);
                    for (var i = 0; i < movies; ++i)
                    {
                        matrix[userIndex][i] = Convert.ToInt32(splitRow[i + 1]);
                        Console.Write(matrix[userIndex][i] + " ");
                    }
                    Console.WriteLine();
                    ++userIndex;
                }
            }
            inversion = new Inversions(matrix);

            //Пользователь 618 та 1
            var result1 = inversion.Inversion(618, 1);

            Console.WriteLine("Расcчитаем инверсии для пользователя {0:###} c пользователем {1:###} Result:{2:###}",
                618,
                1,
                result1);

            //Пользователь 951 та 178.
            var result2 = inversion.Inversion(951, 178);

            Console.WriteLine("Расcчитаем инверсии для пользователя {0:###} c пользователем {1:###} Result:{2}",
                951,
                178,
                result2);
        }

        public class Inversions
        {
            private readonly int[][] _matrix;

            public Inversions(int[][] matrix)
            {
                _matrix = matrix;
            }

            public int Inversion(int user1Index, int user2Index)
            {
                var arrLength = _matrix[0].Length;

                var compare = new int[2][];
                compare[0] = new int[arrLength];
                compare[1] = new int[arrLength];

                for (var i = 0; i < arrLength; ++i)
                {
                    compare[0][i] = _matrix[user1Index - 1][i];
                }
                Array.Sort(compare[0]);
                for (var i = 0; i < arrLength; ++i)
                {
                    var isFound = false;
                    var j = 0;
                    while (!isFound)
                    {
                        if (compare[0][i] == _matrix[user1Index - 1][j])
                        {
                            compare[1][i] = _matrix[user2Index - 1][j];
                            isFound = true;
                        }
                        ++j;
                    }
                    
                }

                var ret = 0;
                for (var i = 0; i < arrLength; ++i)
                {
                    for (var j = i; j < arrLength; ++j)
                    {
                        if (compare[1][i] > compare[1][j])
                        {
                            ++ret;
                        }
                    }
                    
                }


                return ret;
            }
        }

        
    }
}
