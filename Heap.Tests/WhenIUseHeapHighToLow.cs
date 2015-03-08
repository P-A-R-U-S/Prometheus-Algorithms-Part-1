using FluentAssertions;
using System.Collections.Generic;
using HeapMedian;
using Xunit;
using Heap = HeapMedian.Heap;

namespace Tests.HeapHighToLow
{

    public class WhenIUseHeapHighToLow
    {
        private Heap _heapHighToLow;

        [Fact]
        public void Should_Return_Correct_Parent_Index()
        {
            //(0 -->(1, 2)
            //(1 --> (3, 4)
            //(2 --> (5, 6)
            //(3 --> (7, 8))
            //(4 --> (9, ))
            
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            //index 0
            _heapHighToLow.Parent(1).Should().Be(0);
            _heapHighToLow.Parent(2).Should().Be(0);

            //index 1
            _heapHighToLow.Parent(3).Should().Be(1);
            _heapHighToLow.Parent(4).Should().Be(1);

            //index 2
            _heapHighToLow.Parent(5).Should().Be(2);
            _heapHighToLow.Parent(6).Should().Be(2);

            //index 3
            _heapHighToLow.Parent(7).Should().Be(3);
            _heapHighToLow.Parent(8).Should().Be(3);

            //index 4
            _heapHighToLow.Parent(9).Should().Be(4);
            _heapHighToLow.Parent(10).Should().Be(4);
        }

        [Fact]
        public void Should_add_High_element_to_the_Top_1()
        {
            var A_correct = new List<int> {16, 14, 10, 8, 7, 9, 3, 2, 4, 1};

            var A = new List<int> {16, 4, 10, 14, 7, 9, 3, 2, 8, 1};
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.MaxHeapify(1, _heapHighToLow.Size);

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);
            
        }

        [Fact]
        public void Should_add_High_element_to_the_Top_2()
        {
            var A_correct = new List<int> { 16, 14, 10, 8, 7, 9, 3, 4, 2, 1 };

            var A = new List<int> { 16, 2, 10, 14, 7, 9, 3, 4, 8, 1 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.MaxHeapify(1, _heapHighToLow.Size);

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);           
        }

        [Fact]
        public void Should_Build_Max_Heap_1()
        {
            var A_correct = new List<int> { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 };

            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]); 
        }

        [Fact]
        public void Should_Build_Max_Heap_2()
        {
            var A_correct = new List<int> { 12, 10, 5, 8, 7, 4, 3, 1, 2, 6 };

            var A = new List<int> { 2, 8, 4, 10, 6, 5, 3, 1, 12, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);

        }

        [Fact]
        public void Should_Sort_From_Low_to_High()
        {
            var A_correct = new List<int> { 1, 2, 3, 4, 7, 8, 9, 10, 14, 16 };

            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Sort();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct[i]);
        }

        [Fact]
        public void Should_Return_Max()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            _heapHighToLow.Max.Should().Be(16);
        }

        [Fact]
        public void Should_Return_Min()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            _heapHighToLow.Min.Should().Be(1);
        }

        [Fact]
        public void Should_Return_ExtractMax()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            _heapHighToLow.ExtractMax().Should().Be(16);
        }

        [Fact]
        public void Should_Return_ExtractMin()
        {
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            _heapHighToLow.ExtractMin().Should().Be(1);
        }

        [Fact]
        public void Should_Increase_Key()
        {
            var A_correct1 = new List<int> { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 };
            var A_correct2 = new List<int> { 16, 15, 10, 14, 7, 9, 3, 2, 8, 1 };

            //
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct1[i]);

            _heapHighToLow.HeapIncreaseKey(8, 15);
            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct2[i]);
        }

        [Fact]
        public void Should_Insert_Key_In_Correct_Position()
        {
            var A_correct1 = new List<int> { 16, 14, 10, 8, 7, 9, 3, 2, 4, 1 };
            var A_correct2 = new List<int> { 16, 15, 10, 8, 14, 9, 3, 2, 4, 1, 7 };

            //
            var A = new List<int> { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7 };
            _heapHighToLow = new Heap(A, HeapType.HighToLow);
            _heapHighToLow.Build();

            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct1[i]);

            _heapHighToLow.Insert(15);
            for (var i = 0; i < A.Count; ++i)
                A[i].Should().Be(A_correct2[i]);
        }
    }
}
