using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using v = System.Collections.Generic.KeyValuePair<string, decimal>;

namespace Dijkstra
{
    public class WhenIExecute_Dijkstra1
    {
        private readonly Graph<string> _graph;

        public WhenIExecute_Dijkstra1()
        {
            _graph = new Graph<string>();
        }

        private void generateGraph1()
        {
            _graph._graph.Add("s", new List<v>
            {
                new v("a", 1),
                new v("b", 2)
            });
            _graph._graph.Add("a", new List<v>
            {
                new v("c", 7),
                new v("d", 5)
            });
            _graph._graph.Add("b", new List<v>
            {
                new v("d", 3)
            });
            _graph._graph.Add("c", new List<v>
            {
                new v("e", 1)
            });
            _graph._graph.Add("d", new List<v>
            {
                new v("e", 2)
            });
            _graph._graph.Add("e", null);
        }

        private void generateGraph2()
        {
            _graph._graph.Add("1", new List<v>
            {
                new v("2", 2),
                new v("3", 1),
                new v("4", 4)
            });
            _graph._graph.Add("2", new List<v>
            {
                new v("4", 7),
                new v("5", 2.5M)
            });
            _graph._graph.Add("3", new List<v>
            {
                new v("4", 5),
                new v("5", 10),
                new v("6", 4)
            });
            _graph._graph.Add("4", new List<v>
            {
                new v("6", 5)
            });
            _graph._graph.Add("5", new List<v>
            {
                new v("6", 4)
            });
            _graph._graph.Add("6", null);
        }

        [Fact]
        public void Should_find_minimum_path_for_the_graph_7()
        {
            generateGraph1();
            IDictionary<string, decimal> _a;
            IDictionary<string, string> _b;

            _graph.Dijkstra1("s", "e", out _a, out _b);

            _a["e"].Should().Be(7);
        }

        [Fact]
        public void Should_retrun_correct_path_SBDE()
        {
            generateGraph1();
            IDictionary<string, decimal> _a;
            IDictionary<string, string> _b;

            _graph.Dijkstra1("s", "e", out _a, out _b);

            var d = _b["e"];
            d.Should().Be("d");

            var b = _b[d];
            b.Should().Be("b");

            var s = _b[b];
            s.Should().Be("s");
        }

        [Fact]
        public void Should_find_minimum_path_for_the_graph_5()
        {
            generateGraph2();
            IDictionary<string, decimal> _a;
            IDictionary<string, string> _b;

            _graph.Dijkstra1("1", "6", out _a, out _b);

            _a["6"].Should().Be(5);
        }

        [Fact]
        public void Should_retrun_correct_path_136()
        {
            generateGraph2();
            IDictionary<string, decimal> _a;
            IDictionary<string, string> _b;

            _graph.Dijkstra1("1", "6", out _a, out _b);

            var v3 = _b["6"];
            v3.Should().Be("3");

            var v1 = _b[v3];
            v1.Should().Be("1");
        }

    }
}
