using System.Collections.Generic;
using System.Windows;
using FindPathProc.Lib;
using Microsoft.Msagl.Drawing;

namespace FindPathProc.WPFApp
{
    public partial class PrintGraph
    {
        private readonly CustomGraph _customGraph;
        private readonly int _startId;
        private readonly int _destId;

        private readonly (string name, string timeWorking, string distance, List<int> path)[] _results =
            new (string, string, string, List<int>)[3];

        public PrintGraph(CustomGraph customGraph, int startId, int destId)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            _customGraph = customGraph;
            _startId = startId;
            _destId = destId;
        }

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

        private void WriteToFile_Click(object sender, RoutedEventArgs e)
        {
            FileManager fileManager =
                new FileManager(_customGraph, _startId, _destId, _results, @"../../../../Results/");
            fileManager.Write();
            MessageBox.Show("Successful!", "Program message");
        }
    }
}