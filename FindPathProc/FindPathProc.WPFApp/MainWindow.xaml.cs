using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using FindPathProc.Lib;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;

namespace FindPathProc.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int startId = 0;
            int finishId = 4;
            double[,] matrix =
            {
                {0, 7, 9, 0, 0, 14},
                {7, 0, 10, 15, 0, 0},
                {9, 10, 0, 11, 0, 2},
                {0, 15, 11, 0, 6, 0},
                {0, 0, 0, 6, 0, 9},
                {14, 0, 2, 0, 9, 0}
            };
            CustomGraph customGraph = new CustomGraph(matrix, new double[6,6]);
            MainGraph(customGraph);
            Algos algos = new Algos(customGraph);
            BellmanFordGraph(algos, startId, finishId, customGraph);
            DeikstraGraph(algos, startId, finishId, customGraph);
            AStarGraph(algos, startId, finishId, customGraph);
        }

        private void MainGraph(CustomGraph customGraph)
        {
            Graph graph = new Graph();
            for (int i = 0; i < customGraph.vertsCount; i++)
            {
                for (int j = i; j < customGraph.vertsCount; j++)
                {
                    if (customGraph.matrixSm[i, j] != 0)
                    {
                        graph.AddEdge(i + "", customGraph.matrixSm[i, j] + "", j + "");
                    }
                }
            }

            graph.Attr.SimpleStretch = true;
            graph.Attr.LayerDirection = LayerDirection.TB;

            mainGraphControl.Graph = graph;
        }

        private void BellmanFordGraph(Algos algos, int startId, int finishId, CustomGraph customGraph)
        {
            Graph graph = new Graph();
            List<int> path = new List<int>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double distance = algos.BellmanFord(startId, finishId, ref path);
            stopwatch.Stop();
            BellmanTime.Text = stopwatch.ElapsedTicks + " ticks";
            BellmanDist.Text = distance + "";
            for (int i = 0; i < path.Count-1; i++)
            {
                graph.AddEdge(path[i] + "", customGraph.matrixSm[path[i], path[i + 1]] + "", path[i + 1] + "");
            }

            graph.Attr.LayerDirection = LayerDirection.LR;
            BellmanGraphControl.Graph = graph;
        }

        private void DeikstraGraph(Algos algos, int startId, int finishId, CustomGraph customGraph)
        {
            Graph graph = new Graph();
            List<int> path = new List<int>();
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double distance = algos.Deikstra(startId, finishId, ref path);
            stopwatch.Stop();
            
            DeikstraTime.Text = stopwatch.ElapsedTicks + " ticks";
            DeikstraDist.Text = distance + "";
            for (int i = 0; i < path.Count-1; i++)
            {
                graph.AddEdge(path[i] + "", customGraph.matrixSm[path[i], path[i + 1]] + "", path[i + 1] + "");
            }

            graph.Attr.LayerDirection = LayerDirection.LR;
            DeikstraGraphControl.Graph = graph;
        }

        private void AStarGraph(Algos algos, int startId, int finishId, CustomGraph customGraph)
        {
            Graph graph = new Graph();
            List<int> path = new List<int>();
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double distance = algos.AStar(startId, finishId, ref path);
            stopwatch.Stop();
            AStarTime.Text = stopwatch.ElapsedTicks + " ticks";
            AStarDist.Text = distance + "";
            for (int i = 0; i < path.Count-1; i++)
            {
                graph.AddEdge(path[i] + "", customGraph.matrixSm[path[i], path[i + 1]] + "", path[i + 1] + "");
            }

            graph.Attr.LayerDirection = LayerDirection.LR;
            AStarGraphControl.Graph = graph;
        }
    }
}