using System;
using System.Numerics;

namespace KaratsubaMultiplication
{
    class Program
    {
        public static long _ab_plus_bc = 105;
        public static long _ab_plus_bc_count = 0;

        static void Main(string[] args)
        {
            //Console.BackgroundColor = ConsoleColor.White;
            Console.BufferWidth = 9000;
            //Console.
            
            string x1 = "1685287499328328297814655639278583667919355849391453456921116729"; //"";
            //"21625695688898558125310188636840316594920403182768"; 
            //"23456"; //
            string x2 = "7114192848577754587969744626558571536728983167954552999895348492"; //"";
            //"13306827740879180856696800391510469038934180115260"; 
            //"34567";//
            string resultX1X2 = Convert.ToString( BigInteger.Parse(x1)*BigInteger.Parse(x2));

            //"287769407308846640970310151509826255482575362419155842891311909556878670000425352112987881085839680";
            //"810803552";
            Console.WriteLine("{0}*{1}={2}", x1, x2, resultX1X2);
            Console.WriteLine("RESULT:{0}", resultX1X2);
            Console.WriteLine("----------------------------");

            Console.Read();
            var alg1 = new Karatsuba();
            Console.WriteLine("RESULT: {0}", Convert.ToString(alg1.Mult(x1, x2)));
            Console.Read();
            Console.Read();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Count:{0}", _ab_plus_bc_count);
            Console.Read();
            Console.Read();


        }
    }


    public class Karatsuba
    {
        public BigInteger Mult(string x1, string x2)
        {
            var x1s = Convert.ToString(x1);
            var x1n = x1s.Length;

            var x2s = Convert.ToString(x2);
            var x2n = x2s.Length;


            return Multiply(x1s, x2s, 0);
        }

        private BigInteger Multiply(string x1, string x2, int level)
        {
            const int ASCII = 48;

            if ( (x1.Length > 2 && x2.Length > 2) || (x1.Length > 2 && x2.Length == 1) )
            {
                var x1Length = x1.Length;
                int half = x1Length / 2;
                half += (x1Length%2 == 0 ? 0 : 1);
                var s11 = x1.Substring(0, half);
                var s12 = x1.Substring(half, x1Length - half);

                Console.Write(new string('-', level));
                Console.Write(new string('-', level));
                Console.Write(">>");
                Console.Write("x1:{0}, x2:{1} --> s1:{2} * x2:{1} * 10^{4}, s2:{3} * x2:{1}", x1, x2, s11, s12, x1Length - half);
                Console.WriteLine();

                return  Multiply(s11, x2, level + 1) * Pow10(x1Length - half) + Multiply(s12, x2, level + 1);
            }


            if ((x1.Length == 2 && x2.Length > 2) || (x1.Length == 1 && x2.Length > 2))
            {
                var x2Length = x2.Length;
                int half = x2Length / 2;
                half += (x2Length % 2 == 0 ? 0 : 1);
                var s21 = x2.Substring(0, half);
                var s22 = x2.Substring(half, x2Length - half);

                Console.Write(new string('-', level));
                Console.Write(new string('-', level));
                Console.Write(">>");
                Console.Write("x1:{0}, x2:{1} --> x1:{0} * s1:{2} * 10^{4}, x1:{0} * s2:{3}", x1, x2, s21, s22, x2Length - half);
                Console.WriteLine();
                return Multiply(x1, s21, level + 1) * Pow10(x2Length - half) + Multiply(x1, s22, level + 1);               
             }



            BigInteger result = 0;

            if (x1 == "0" || x1 == "00") return 0;

            if (x1.Length == 1 && x2.Length == 2)
            {
                result = Multiply("0" + x1, x2, level + 1); ;
            }

            if (x1.Length == 2 && x2.Length == 1)
            {
                result = Multiply(x1, "0" + x2, level + 1); ;
            }

            if (x1.Length == 2 && x2.Length == 2)
            {
                int a = (x1[0] - ASCII);
                int b = (x1[1] - ASCII);

                int c = (x2[0] - ASCII);
                int d = (x2[1] - ASCII);


                int ac = a * c;
                int bd = b * d;
                int a_plus_b = (a + b);
                int c_plus_d = (c + d);
                int a_plus_b__mult_c_plus_d = 0;

                if (a_plus_b > 10 || c_plus_d > 10)
                {
                    a_plus_b__mult_c_plus_d =
                        (int)Multiply(a_plus_b.ToString("00"), c_plus_d.ToString("00"), level + 1);
                }
                else
                {
                    a_plus_b__mult_c_plus_d = a_plus_b*c_plus_d;
                }

                long ad_plus_bc = Math.Abs((a_plus_b__mult_c_plus_d - ac - bd));

                if (ad_plus_bc == Program._ab_plus_bc) Program._ab_plus_bc_count += 1;

                Console.Write(new string('-', level));
                Console.Write(new string('-', level));
                Console.Write(">>");
                Console.Write("Mult: x1:{0}, x2:{1}", x1, x2);

                result = (ac * 100) + ((10) * ad_plus_bc) + bd;
            }

            Console.WriteLine(": Result:{0}", result);

            return result;
        }

        private static BigInteger Pow10(long pow)
        {
            switch (pow)
            {
                case 0:  return 1;
                case 1:  return 10;
                case 2:  return 100;
                case 3:  return 1000;
                case 4:  return 10000;
                case 5:  return 100000;
                case 6:  return 1000000;
                case 7:  return 10000000;
                case 8:  return 100000000;
                case 9:  return 1000000000;
                case 10: return 10000000000;
                case 11: return 100000000000;
                case 12: return 1000000000000;
                case 13: return 10000000000000;
                case 14: return 100000000000000;
                case 15: return 1000000000000000;
                case 16: return 10000000000000000;
                case 17: return 100000000000000000;
                case 18: return 1000000000000000000;
                case 19: return 10000000000000000000;
                case 20: return BigInteger.Parse("100000000000000000000");
                case 21: return BigInteger.Parse("1000000000000000000000");
                case 22: return BigInteger.Parse("10000000000000000000000");
                case 23: return BigInteger.Parse("100000000000000000000000");
                case 24: return BigInteger.Parse("1000000000000000000000000");
                case 25: return BigInteger.Parse("10000000000000000000000000");
                case 26: return BigInteger.Parse("100000000000000000000000000");
                case 27: return BigInteger.Parse("1000000000000000000000000000");
                case 28: return BigInteger.Parse("10000000000000000000000000000");
                case 29: return BigInteger.Parse("100000000000000000000000000000");
                case 30: return BigInteger.Parse("1000000000000000000000000000000");
                case 31: return BigInteger.Parse("10000000000000000000000000000000");
                case 32: return BigInteger.Parse("100000000000000000000000000000000");
                case 33: return BigInteger.Parse("1000000000000000000000000000000000");
                case 34: return BigInteger.Parse("10000000000000000000000000000000000");
                case 35: return BigInteger.Parse("100000000000000000000000000000000000");
                case 36: return BigInteger.Parse("1000000000000000000000000000000000000");
                default:
                    throw new Exception();
                //// up to 32 can be added 
                ////default: // Vilx's solution is used for default
                ////    BigInteger ret = 1;
                ////    while (pow != 0)
                ////    {
                ////        if ((pow & 1) == 1)
                ////            ret *= 10;
                ////        pow >>= 1;
                ////    }
                ////    return ret;
            }
        }
    }
}
