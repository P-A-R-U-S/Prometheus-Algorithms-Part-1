using System.Linq;
using FluentAssertions;
using HeapMedian;
using System.Collections.Generic;
using Xunit;

namespace Tests.HeapLowToHigh
{
    public class WhenIUseHeapLowToHigh
    {
        private Heap _heapLowToHigh;

        [Fact]
        public void Should_Return_Correct_Parent_Index()
        {
            //(0 -->(1, 2)
            //(1 --> (3, 4)
            //(2 --> (5, 6)
            //(3 --> (7, 8))
            //(4 --> (9, ))
            var A_correct = new List<int> { 1, 2, 3, 4, 7, 9, 10, 14, 8, 16 };

            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            //index 0
            _heapLowToHigh.Parent(1).Should().Be(0);
            _heapLowToHigh.Parent(2).Should().Be(0);

            //index 1
            _heapLowToHigh.Parent(3).Should().Be(1);
            _heapLowToHigh.Parent(4).Should().Be(1);

            //index 2
            _heapLowToHigh.Parent(5).Should().Be(2);
            _heapLowToHigh.Parent(6).Should().Be(2);

            //index 3
            _heapLowToHigh.Parent(7).Should().Be(3);
            _heapLowToHigh.Parent(8).Should().Be(3);
            
            //index 4
            _heapLowToHigh.Parent(9).Should().Be(4);
            _heapLowToHigh.Parent(10).Should().Be(4);
        }

        [Fact]
        public void TestMinHeapify1()
        {
            var A_correct = new List<int> {1, 3, 5, 8, 10, 7, 6, 9, 11, 12 };

            var A = new List<int> { 1, 9, 5, 3, 10, 7, 6, 8, 11, 12 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.MinHeapify(1, _heapLowToHigh.Size);

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);

        }

        [Fact]
        public void TestMinHeapify2()
        {
            var A_correct = new List<int> { 1, 3, 5, 8, 10, 7, 6, 9, 11, 12 };

            var A = new List<int> { 1, 11, 5, 3, 10, 7, 6, 9, 8, 12 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.MinHeapify(1, _heapLowToHigh.Size);

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);
        }

        [Fact]
        public void Should_Build_Max_Heap_1()
        {
            var A_correct = new List<int> { 1, 2, 3, 4, 7, 9, 10, 14, 8, 16 };

            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);
        }

        [Fact]
        public void Should_Build_Max_Heap_2()
        {
            //var A_correct = new List<int> { 12, 10, 5, 8, 7, 4, 3, 1, 2, 6 };

            //var A = new List<int> { 2, 8, 4, 10, 6, 5, 3, 1, 12, 7 };
            //_heapHighToLow = new Heap(A, HeapType.HighToLow);
            //_heapHighToLow.Build();

            //for (var i = 0; i < A.Count; ++i)
            //    A[i].Should().Be(A_correct[i]);

        }

        [Fact]
        public void Should_Sort_From_High_to_Low()
        {
            var A_correct = new List<int> {16, 14, 10, 9, 8, 7, 4, 3, 2, 1};

            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Sort();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);
        }

        [Fact]
        public void Should_Return_Max()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            _heapLowToHigh.Max.Should().Be(16);
        }

        [Fact]
        public void Should_Return_Min()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            _heapLowToHigh.Min.Should().Be(1);
        }

        [Fact]
        public void Should_Return_ExtractMax()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            _heapLowToHigh.ExtractMax().Should().Be(16);
        }

        [Fact]
        public void Should_Return_ExtractMin()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            _heapLowToHigh.ExtractMin().Should().Be(1);
        }

        [Fact]
        public void Should_Descrese_Key()
        {
            var A_correct1 = new List<int> { 1, 5, 3, 6, 7, 9, 10, 14, 8, 16 };
            var A_correct2 = new List<int> { 1, 4, 3, 5, 7, 9, 10, 14, 6, 16 };

            //
            var A = new List<int> { 6, 1, 3, 5, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct1[i]);

            _heapLowToHigh.HeapDecreseKey(8, 4);
            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct2[i]);
        }

        [Fact]
        public void Should_Insert_Key_In_Correct_Position()
        {
            var A_correct1 = new List<int> { 1, 5, 3, 6, 7, 9, 10, 14, 8, 16 };
            var A_correct2 = new List<int> { 1, 2, 3, 6, 5, 9, 10, 14, 8, 16, 7 };

            //
            var A = new List<int> { 6, 1, 3, 5, 16, 9, 10, 14, 8, 7 };
            _heapLowToHigh = new Heap(A, HeapType.LowToHigh);
            _heapLowToHigh.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct1[i]);

            _heapLowToHigh.Insert(2);
            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct2[i]);
        }
    }
}
