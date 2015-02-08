using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi
{
    class Program
    {
        private static void Main(string[] args)
        {
            var n = 50; //Convert.ToInt64( Console.Read() );

            Console.WriteLine("Fibonachi1 for " + n + " will be:" + Fibonachi1(n));
            Console.WriteLine("Fibonachi2 for " + n + " will be:" + Fibonachi2(n));

            Console.Read();
        }

        private static long Fibonachi1(long n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return Fibonachi1(n - 1) + Fibonachi1(n - 2);
        }

        private static long Fibonachi2(long n)
        {
            if (n == 0) return 0;
            //if (n == 1) return 1;
            var f = new int[n+1];
            f[0] = 0;
            f[1] = 1;
            for (var i = 2; i <= n; ++i)
            {
                f[i] = f[i - 1] + f[i - 2];
            }
            return f[n];
        }
    }
}
