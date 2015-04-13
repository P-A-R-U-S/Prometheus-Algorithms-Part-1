using FluentAssertions;
using Xunit;

namespace Dijkstra
{
    public class WhenIUseQueue
    {
        [Fact]
        public void Should_Return_Min_Value_for_LowToHigh_Queue()
        {
            var queue = new Queue<string>(QueueType.LowToHigh);
            queue.Enqueue("A", 5);
            queue.Enqueue("B", 3);
            queue.Enqueue("C", 4);
            queue.Enqueue("D", 2);
            queue.Enqueue("E", 7);
            queue.Enqueue("F", 1);
            queue.Enqueue("G", 6);


            //Test
            var r1 = queue.Dequeue();
            r1.Should().Be("F");

            var r2 = queue.Dequeue();
            r2.Should().Be("D");

            var r3 = queue.Dequeue();
            r3.Should().Be("B");

            var r4 = queue.Dequeue();
            r4.Should().Be("C");

            var r5 = queue.Dequeue();
            r5.Should().Be("A");

            var r6 = queue.Dequeue();
            r6.Should().Be("G");

            var r7 = queue.Dequeue();
            r7.Should().Be("E");
        }

        [Fact]
        public void Should_Return_Correct_Value_For_DecreasedKey()
        {
            var queue = new Queue<string>(QueueType.LowToHigh);
            queue.Enqueue("A", 1);
            queue.Enqueue("B", 2);
            queue.Enqueue("C", 3);
            queue.Enqueue("D", 4);
            queue.Enqueue("E", 5);
            queue.Enqueue("F", 6);
            queue.Enqueue("G", 7);


            queue.DecreaseKey("A", 5);
            queue.DecreaseKey("B", 3);
            queue.DecreaseKey("C", 4);
            queue.DecreaseKey("D", 2);
            queue.DecreaseKey("E", 7);
            queue.DecreaseKey("F", 1);
            queue.DecreaseKey("G", 6);

            //Test
            var r1 = queue.Dequeue();
            r1.Should().Be("F");

            var r2 = queue.Dequeue();
            r2.Should().Be("D");

            var r3 = queue.Dequeue();
            r3.Should().Be("B");

            var r4 = queue.Dequeue();
            r4.Should().Be("C");

            var r5 = queue.Dequeue();
            r5.Should().Be("A");

            var r6 = queue.Dequeue();
            r6.Should().Be("G");

            var r7 = queue.Dequeue();
            r7.Should().Be("E");
        }
    }
}
