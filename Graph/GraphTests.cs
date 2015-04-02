using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph
{
    public class GraphTests
    {
        private readonly Graph _graph;

        public GraphTests()
        {
            _graph = new Graph();
        }

        private void createGraph1()
        {
            _graph._graph.Add("s", new[] { "a", "b" });
            _graph._graph.Add("a", new[] { "s", "c", "d" });
            _graph._graph.Add("b", new[] { "s", "d" });
            _graph._graph.Add("c", new[] { "a", "d", "e" });
            _graph._graph.Add("d", new[] { "a", "b", "c", "e" });
            _graph._graph.Add("e", new[] { "c", "d" });
        }
        private void createGraph2()
        {
            _graph._graph.Add("1", new[] { "2"});
            _graph._graph.Add("2", new[] { "3", "5"});
            _graph._graph.Add("3", new[] { "2", "4", "5" });
            _graph._graph.Add("4", new[] { "3", "5", "9" });
            _graph._graph.Add("5", new[] { "2", "3", "4", "6", "7" });
            _graph._graph.Add("6", new[] { "5", "7", "8" });
            _graph._graph.Add("7", new[] { "5", "6", "9" });
            _graph._graph.Add("8", new[] { "6", "9" });
            _graph._graph.Add("9", new[] { "4", "7", "8" });
        }

        private void createGraph2_with_Connected_Components_3()
        {
            _graph._graph.Add("1", new[] { "2", "3" });
            _graph._graph.Add("2", new[] { "1", "3" });
            _graph._graph.Add("3", new[] { "1", "2" });

            _graph._graph.Add("4", new[] { "5" });
            _graph._graph.Add("5", new[] { "4" });

            _graph._graph.Add("6", new[] { "7", "8" });
            _graph._graph.Add("7", new[] { "6", "9" });
            _graph._graph.Add("8", new[] { "6", "9" });
            _graph._graph.Add("9", new[] { "7", "8" });
        }
        private void createGraph2_with_Connected_Components_4()
        {
            _graph._graph.Add("1", new[] { "2", "5" });
            _graph._graph.Add("2", new[] { "1", "3" });
            _graph._graph.Add("3", new[] { "2", "4" });
            _graph._graph.Add("4", new[] { "3", "5" });
            _graph._graph.Add("5", new[] { "1", "4" });

            _graph._graph.Add("6", new[] { "7", "8" });
            _graph._graph.Add("7", new[] { "6", "8" });
            _graph._graph.Add("8", new[] { "6", "7" });

            _graph._graph.Add("9", new[] { "10" });
            _graph._graph.Add("10", new[] { "9" });

            _graph._graph.Add("11", new[] { "12", "14" });
            _graph._graph.Add("12", new[] { "11", "13" });
            _graph._graph.Add("13", new[] { "12", "14" });
            _graph._graph.Add("14", new[] { "11", "13" });

        }

        private void createGraph1_OrientedGraph()
        {
            _graph._graph.Add("Белье", new[] { "Носки", "Штаны", "Рубашка" });
            _graph._graph.Add("Носки", new[] { "Ботинки"});
            _graph._graph.Add("Штаны", new[] { "Ботинки" });           
            _graph._graph.Add("Ботинки", new List<string>() );
            _graph._graph.Add("Рубашка", new[] { "Куртка" });
            _graph._graph.Add("Куртка", new List<string>());
        }
        private void createGraph2_OrientedGraph_with_Connected_Components_4()
        {
            _graph._graph.Add("a", new[] { "b" });
            _graph._graph.Add("b", new[] { "c", "e", "f" });
            _graph._graph.Add("c", new[] { "d", "g" });
            _graph._graph.Add("d", new[] { "c", "h" });
            _graph._graph.Add("e", new[] { "a", "f" });
            _graph._graph.Add("f", new[] { "g" });
            _graph._graph.Add("g", new[] { "f", "h" });
            _graph._graph.Add("h", new List<string>());
        }

        [Theory]
        [InlineData("s", 1)]
        [InlineData("a", 2)]
        [InlineData("b", 3)]
        [InlineData("c", 4)]
        [InlineData("d", 5)]
        [InlineData("e", 6)]
        public void Should_Add_correct_number_to_Vertex_when_make_BFS(string vertex, int value)
        {
            //Stub
            createGraph1();

            //Test
            _graph.BreadthFirstSearch();

            _graph._bfs.ContainsKey(vertex).Should().BeTrue();
            _graph._bfs[vertex].Should().Be(value);

        }

        [Theory]
        [InlineData("s", 0)]
        [InlineData("a", 1)]
        [InlineData("b", 1)]
        [InlineData("c", 2)]
        [InlineData("d", 2)]
        [InlineData("e", 3)]
        public void Should_calculate_correct_path_between_Vertexes_when_make_BFS(string vertex, int value)
        {
            //Stub
            createGraph1();

            //Test
            _graph.BreadthFirstSearch();

             _graph._d.ContainsKey(vertex).Should().BeTrue();
            _graph._d[vertex].Should().Be(value);
        }

        [Theory]
        //[InlineData("s", 0)]
        [InlineData("a", 1, "as")]
        [InlineData("b", 1, "bs")]
        [InlineData("c", 2, "cas")]
        [InlineData("d", 2, "das")]
        //[InlineData("d", 2, "dbs")] -- Failed
        [InlineData("e", 3, "ecas")]
        //[InlineData("e", 3, "edbs")] -- Failed
        //[InlineData("e", 3, "edas")] -- Failed
        public void Should_create_correct_path_between_Vertexes_when_make_BFS(string vertex, int value, string path)
        {
            //Stub
            createGraph1();

            //Test
            _graph.BreadthFirstSearch();

            _graph._p.ContainsKey(vertex).Should().BeTrue();
            var p = vertex;
            
            var v = vertex;
            while (_graph._p.ContainsKey(v))
            {
                v = _graph._p[v];
                p += v;
            }
            _graph._d[vertex].Should().Be(value);
            p.Should().Be(path);

        }

        [Fact]
        public void Should_return_corect_number_connected_components_3()
        {
            //Stub
            createGraph2_with_Connected_Components_3();

            //Test
            var result = _graph.FindConnectedComponents();

            result.Should().Be(3);
        }

        [Fact]
        public void Should_return_corect_number_connected_components_4()
        {
            //Stub
            createGraph2_with_Connected_Components_4();

            //Test
            var result = _graph.FindConnectedComponents();

            result.Should().Be(4);
        }


        [Theory]
        [InlineData("s", 1)]
        [InlineData("a", 2)]
        [InlineData("c", 3)]
        [InlineData("d", 4)]
        [InlineData("b", 5)]
        [InlineData("e", 6)]
        public void Should_Add_correct_number_to_Vertex_when_make_DFSR(string vertex, int value)
        {
            //Stub
            createGraph1();

            //Test
            _graph.DepthFirstSearch1();

            _graph._dfs.ContainsKey(vertex).Should().BeTrue();
            _graph._dfs[vertex].Should().Be(value);

        }


        [Theory]
        [InlineData("s", 1)]
        [InlineData("a", 2)]
        [InlineData("c", 3)]
        [InlineData("d", 4)]
        [InlineData("b", 5)]
        [InlineData("e", 6)]
        public void Should_Add_correct_number_to_Vertex_when_make_DFS_1(string vertex, int value)
        {
            //Stub
            createGraph1();

            //Test
            _graph.DepthFirstSearch2();

            _graph._dfs.ContainsKey(vertex).Should().BeTrue();
            _graph._dfs[vertex].Should().Be(value, 
                String.Format("Для вершины:{0} ожидалось значение:{1}, а найдено:{2}", 
                            vertex, 
                            value, 
                            _graph._dfs[vertex]));

        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("3", 3)]
        [InlineData("4", 4)]
        [InlineData("5", 5)]
        [InlineData("6", 6)]
        [InlineData("7", 7)]
        [InlineData("8", 9)]
        [InlineData("9", 8)]
        public void Should_Add_correct_number_to_Vertex_when_make_DFS_2(string vertex, int value)
        {
            //Stub
            createGraph2();

            //Test
            _graph.DepthFirstSearch2();

            _graph._dfs.ContainsKey(vertex).Should().BeTrue();
            _graph._dfs[vertex].Should().Be(value,
                String.Format("Для вершины:{0} ожидалось значение:{1}, а найдено:{2}",
                            vertex,
                            value,
                            _graph._dfs[vertex]));

        }

        
        [Theory]
        [InlineData("Ботинки", 6)]
        [InlineData("Носки", 5)]
        [InlineData("Штаны", 4)]
        [InlineData("Рубашка", 2)]
        [InlineData("Куртка", 3)]
        [InlineData("Белье", 1)] 
        public void Should_Add_correct_sortNumber_when_make_TopologicalSort(string vertex, int value)
        {
            //Stub
            createGraph1_OrientedGraph();

            //Test
            _graph.TopologicalSort();

            _graph._ts.ContainsKey(vertex).Should().BeTrue();
            _graph._ts[vertex].Should().Be(value);
        }


        [Theory]
        [InlineData("h")]
        [InlineData("aeb")]
        [InlineData("cd")]
        [InlineData("fg")]
        public void Should_find_strong_components_for_Topological_graph(string strongComponent)
        {
            //Stub
            createGraph2_OrientedGraph_with_Connected_Components_4();

            //Test
            var result = _graph.StrongConnectedComponents();

            var isStrongComponentFound = false;
            foreach (var componentList in result)
            {
                var component = String.Concat(componentList);

                isStrongComponentFound = component == strongComponent;
                if (isStrongComponentFound) break;
            }

            isStrongComponentFound.Should().BeTrue();

        }
    }
}
