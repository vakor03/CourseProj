using System.Collections.Generic;
using System.IO;
using System.Windows;
using FindPathProc.Lib;
using Microsoft.Msagl.Drawing;

namespace FindPathProc.WPFApp
{
    /// <summary>
    /// Interaction logic for PrintGraph.xaml
    /// </summary>
    public partial class PrintGraph
    {
        /// <summary>
        /// Initial graph
        /// </summary>
        private readonly CustomGraph _customGraph;

        /// <summary>
        /// Id of start vertex
        /// </summary>
        private readonly int _startId;

        /// <summary>
        /// Id of destination vertex
        /// </summary>
        private readonly int _destId;

        /// <summary>
        /// results of path finding algorithms
        /// </summary>
        private readonly (string name, string timeWorking, string distance, List<int> path)[] _results =
            new (string, string, string, List<int>)[3];

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customGraph">Initial CustomGraph</param>
        /// <param name="startId">Id of start vertex in path</param>
        /// <param name="destId">Id of destination vertex in path</param>
        public PrintGraph(CustomGraph customGraph, int startId, int destId)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            _customGraph = customGraph;
            _startId = startId;
            _destId = destId;
        }

        /// <summary>
        /// Prints graph, algorithms results
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PrintMainGraph(_customGraph);
            Algo algo = new Algo(_customGraph, _startId, _destId);

            BellmanDist.Text = "None";
            DijkstraDist.Text = "None";
            AStarDist.Text = "None";
            _results[0].name = "Bellman-Ford";
            _results[1].name = "Dijkstra";
            _results[2].name = "A*";
            if (_customGraph.PathExist(_startId, _destId))
            {
                if (!_customGraph.HasNegativeEdges)
                {
                    DijkstraGraph(algo);
                    AStarGraph(algo);
                }
                else
                {
                    MessageBox.Show("Graph contains negative edges. Dijkstra and A* algorithms are unavailable!",
                        "Program message");
                }

                BellmanFordGraph(algo);
            }
            else
            {
                MessageBox.Show("Path doesn't exist!", "Program message");
            }
        }

        /// <summary>
        /// Prints users graph
        /// </summary>
        /// <param name="customGraph">initial graph</param>
        private void PrintMainGraph(CustomGraph customGraph)
        {
            Graph graph = new Graph();
            MainGraphControl.Graph = new Graph();
            for (int i = 0; i < customGraph.VertCount; i++)
            {
                for (int j = 0; j < customGraph.VertCount; j++)
                {
                    if (customGraph.MatrixWeight[i, j] != 0)
                    {
                        graph.AddEdge(i + "", customGraph.MatrixWeight[i, j] + "", j + "");
                    }
                }

                graph.AddNode(i + "");
            }

            graph.Attr.SimpleStretch = true;
            graph.Attr.LayerDirection = LayerDirection.TB;


            MainGraphControl.Graph = graph;

            StartLabel.Text = "Start Id: " + _startId;
            FinishLabel.Text = "Destination Id: " + _destId;
        }

        /// <summary>
        /// Prints results of Bellman-Ford algorithm 
        /// </summary>
        /// <param name="algo">path finding algorithms with initial graph</param>
        private void BellmanFordGraph(Algo algo)
        {
            Graph graph = new Graph();


            bool negativeLoops = false;

            double distance = algo.BellmanFord(out List<int> path, ref negativeLoops, out long iterations);


            if (!negativeLoops)
            {
                BellmanTime.Text = iterations + " iterations";
                BellmanDist.Text = distance + "";
                for (int i = 0; i < path.Count - 1; i++)
                {
                    graph.AddEdge(path[i] + "", _customGraph.MatrixWeight[path[i], path[i + 1]] + "", path[i + 1] + "");
                }

                graph.Attr.LayerDirection = LayerDirection.LR;
                BellmanGraphControl.Graph = graph;
            }
            else
            {
                MessageBox.Show("Graph contains negative loops. Bellman-Ford algorithm is unavailable!",
                    "Program message");
                BellmanDist.Text = "NegLoop";
            }

            _results[0] = ("Bellman-Ford", BellmanTime.Text, BellmanDist.Text, path);
        }

        /// <summary>
        /// Prints results of Dijkstra algorithm 
        /// </summary>
        /// <param name="algo">path finding algorithms with initial graph</param>
        private void DijkstraGraph(Algo algo)
        {
            Graph graph = new Graph();


            double distance = algo.Dijkstra(out List<int> path, out long iterations);

            DijkstraTime.Text = iterations + " iterations";
            DijkstraDist.Text = distance + "";
            for (int i = 0; i < path.Count - 1; i++)
            {
                graph.AddEdge(path[i] + "", _customGraph.MatrixWeight[path[i], path[i + 1]] + "", path[i + 1] + "");
            }

            graph.Attr.LayerDirection = LayerDirection.LR;
            DijkstraGraphControl.Graph = graph;
            _results[1] = ("Dijkstra", DijkstraTime.Text, DijkstraDist.Text, path);
        }

        /// <summary>
        /// Prints results of A* algorithm 
        /// </summary>
        /// <param name="algo">path finding algorithms with initial graph</param>
        private void AStarGraph(Algo algo)
        {
            Graph graph = new Graph();


            double distance = algo.AStar(out List<int> path, out long iterations);


            AStarTime.Text = iterations + " iterations";
            AStarDist.Text = distance + "";
            for (int i = 0; i < path.Count - 1; i++)
            {
                graph.AddEdge(path[i] + "", _customGraph.MatrixWeight[path[i], path[i + 1]] + "", path[i + 1] + "");
            }

            graph.Attr.LayerDirection = LayerDirection.LR;
            AStarGraphControl.Graph = graph;
            _results[2] = ("A*", AStarTime.Text, AStarDist.Text, path);
        }

        /// <summary>
        /// Prints results to file 
        /// </summary>
        private void WriteToFile_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(PathToDir.Text))
            {
                MessageBox.Show("Directory doesn't exist. results will be added to dir \"Results\".",
                    "Program message");
            }
            else
            {
                if (PathToDir.Text[PathToDir.Text.Length - 1] != '\\' ||
                    PathToDir.Text[PathToDir.Text.Length - 1] != '/')
                {
                    PathToDir.Text += '\\';
                }
            }

            FileManager fileManager =
                new FileManager(_customGraph, _startId, _destId, _results, PathToDir.Text);
            fileManager.Write();
            MessageBox.Show("Successful!", "Program message");
        }
    }
}