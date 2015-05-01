using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using key = System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<string, decimal>>>;
using vertex = System.Collections.Generic.KeyValuePair<string, decimal>;

namespace Dijkstra.Tests
{
    public class WhenIExecute_Dijkstra3_on_graph_with_more_than_one_shortest_route
    {
        private readonly Graph<string> _graph;

        public WhenIExecute_Dijkstra3_on_graph_with_more_than_one_shortest_route()
        {
            _graph = new Graph<string>();
        }

        /*
         1 2 1
         1 6 8
         1 8 7
         
         2 8 8
         2 9 4
         
         3 9 6
         3 10 2
         
         4 10 2
         
         5 6 3
         5 7 1
         5 10 5
         
         6 5 2
         7 10 1
         
         8 4 3
         8 9 12
         8 10 5
         
         9 1 5
         9 3 5
         9 5 11
         9 6 10
         9 8 12          
         */
        private void generateGraph1()
        {
            _graph._graph.Add(new key("1", new List<vertex>
            {
                new vertex("2", 1),
                new vertex("6", 8),
                new vertex("8", 7),
            }));

            _graph._graph.Add(new key("2", new List<vertex>
            {
                new vertex("8", 8),
                new vertex("9", 4)
            }));

            _graph._graph.Add(new key("3", new List<vertex>
            {
                new vertex("9", 6),
                new vertex("10", 2)
            }));

            _graph._graph.Add(new key("4", new List<vertex>
            {
                new vertex("10", 2)
            }));

            _graph._graph.Add(new key("5", new List<vertex>
            {
                new vertex("6", 3),
                new vertex("7", 1),
                new vertex("10",5)
            }));

            _graph._graph.Add(new key("6", new List<vertex>
            {
                new vertex("5", 2)
            }));

            _graph._graph.Add(new key("7", new List<vertex>
            {
                new vertex("10", 1)
            }));

            _graph._graph.Add(new key("8", new List<vertex>
            {
                new vertex("4", 3),
                new vertex("9", 12),
                new vertex("10",5)
            }));

            _graph._graph.Add(new key("9", new List<vertex>
            {
                new vertex("1", 5),
                new vertex("3", 5),
                new vertex("5", 11),
                new vertex("6", 10),
                new vertex("8", 12)
            }));

            _graph._graph.Add(new key("10", null));
        }

        /*
            7 10
            1 2 1
            1 3 3
            1 7 14
            1 7 14
         
            2 4 7
            
            3 4 5
            
            4 5 3
            4 6 2
            
            5 7 3
            
            6 7 4
         */
        private void generateGraph2()
        {
            _graph._graph.Add(new key("1", new List<vertex>
            {
                new vertex("2", 1),
                new vertex("3", 3),
                new vertex("7", 14),
                new vertex("7", 14)
            }));

            _graph._graph.Add(new key("2", new List<vertex>
            {
                new vertex("4", 7)
            }));

            _graph._graph.Add(new key("3", new List<vertex>
            {
                new vertex("4", 5)
            }));

            _graph._graph.Add(new key("4", new List<vertex>
            {
                new vertex("5", 3),
                new vertex("6", 2)
            }));

            _graph._graph.Add(new key("5", new List<vertex>
            {
                new vertex("7", 3)
            }));

            _graph._graph.Add(new key("6", new List<vertex>
            {
                new vertex("7", 4)
            }));

            _graph._graph.Add(new key("7", null));
        }

        [Fact]
        public void Should_find_all_shortes_routes_12()
        {
            //Stub
            generateGraph1();

            IDictionary<string, decimal> _a;
            IDictionary<string, IList<string>> _b;

            //
            _graph.Dijkstra3("1", "10", out _a, out _b);

            _a["10"].Should().Be(12);

            var result = getShortestPath("1", "10", _b);


            result.Length.Should().Be(4);

            result.Contains("10 4 8 1");
            result.Contains("10 3 9 2 1");
            result.Contains("10 8 1");
            result.Contains("10 7 5 6 1");
        }

        [Fact]
        public void Should_find_all_shortes_routes_14()
        {
            //Stub
            generateGraph2();

            IDictionary<string, decimal> _a;
            IDictionary<string,  IList<string>> _b;

            //
            _graph.Dijkstra3("1", "7", out _a, out _b);

            _a["7"].Should().Be(14);

            var result = getShortestPath("1", "7", _b);

            result.Length.Should().Be(5);

            result.Contains("7 1");
            result.Contains("7 6 4 3 1");
            result.Contains("7 6 4 2 1");
            result.Contains("7 5 4 3 1");
            result.Contains("7 5 4 2 1");
        }

        private string[] getShortestPath(string s, string e, IDictionary<string, IList<string>> b)
        {
            var pathes = new List<string>();

            if (!b.ContainsKey(e))
            {
                return new[] {e};
            }

            foreach (var v in b[e])
            {
                var pp = getShortestPath(s, v, b);
                pathes.AddRange(pp.Select(p => e + " " + p));
            }

            return pathes.ToArray();
        }
    }
}
