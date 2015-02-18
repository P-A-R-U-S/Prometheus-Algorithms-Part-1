using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class QuickSort
    {
        private int[] _array;
        private int _comparisonCounter = 0;

        public QuickSort(int[] array)
        {
            _array = array;
        }

        public void Sort()
        {
            
        }

        private int Partition(int p, int r)
        {
            ++ _comparisonCounter;

            int value = 0;
            var x = _array[r];
            var i = p - 1;
            for(var j = p; j <= (r -1); ++j)
            {
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

            return i + 1;
        }
    }
}
