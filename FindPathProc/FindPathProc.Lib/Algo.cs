using System.Collections.Generic;

namespace FindPathProc.Lib
{
    /// <summary>
    /// Represents implementation of basic path finding algorithms in graph
    /// </summary>
    public class Algo
    {   /// <summary>
        /// Initial graph
        /// </summary>
        private readonly CustomGraph _graph;
        /// <summary>
        /// Id of start vertex
        /// </summary>
        private readonly int _startId;
        /// <summary>
        /// Id of destination vertex
        /// </summary>
        private readonly int _destId;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graph">Initial CustomGraph</param>
        /// <param name="startId">Id of start vertex in path</param>
        /// <param name="destId">Id of destination vertex in path</param>
        public Algo(CustomGraph graph, int startId, int destId)
        {
            _graph = graph;
            _startId = startId;
            _destId = destId;
        }

        /// <summary>
        /// Finds the shortest path using Dijkstra algorithm
        /// </summary>
        /// <param name="path">represents newly path of algorithm from startId to finishId</param>
        /// <param name="iterations">represents number of iterations of algorithm</param>
        /// <returns>length of the shortest path</returns>
        public double Dijkstra(out List<int> path, out long iterations)
        {
            iterations = 0;
            path = new List<int>();
            PriorityQueue<int> frontier = new PriorityQueue<int>();
            frontier.AddElement(_startId, 0);
            int[] cameFrom = new int[_graph.VertCount];
            double[] costSoFar = new double[_graph.VertCount];
            cameFrom[_startId] = -1;
            for (int i = 0; i < _graph.VertCount; i++)
            {
                if (i == _startId)
                {
                    costSoFar[i] = 0;
                }
                else
                {
                    costSoFar[i] = double.MaxValue;
                    cameFrom[i] = _startId;
                }
            }

            while (!frontier.IsEmpty())
            {
                int currentId = frontier.Dequeue();

                if (currentId == _destId)
                {
                    path = new List<int>();
                    while (currentId != -1)
                    {
                        path.Add(currentId);
                        currentId = cameFrom[currentId];
                    }

                    path.Reverse();
                    return costSoFar[_destId];
                }


                for (int j = 0; j < _graph.VertCount; j++)
                {
                    if (_graph.MatrixWeight[currentId, j] != 0)
                    {
                        iterations++;
                        double newCost = costSoFar[currentId] + _graph.MatrixWeight[currentId, j];
                        if (newCost < costSoFar[j])
                        {
                            costSoFar[j] = newCost;
                            frontier.AddElement(j, newCost);
                            cameFrom[j] = currentId;
                        }
                    }
                }
            }
            
            return costSoFar[_destId];
        }

        /// <summary>
        /// Finds the shortest path using Bellman-Ford algorithm
        /// </summary>
        /// <param name="path">represents newly path of algorithm from startId to finishId</param>
        /// <param name="negativeLoop">represents existence of negative loops in graph</param>
        /// <param name="iterations">represents number of iterations of algorithm</param>
        /// <returns>length of the shortest path</returns>
        public double BellmanFord(out List<int> path, ref bool negativeLoop, out long iterations)
        {
            path = new List<int>();
            iterations = 0;
            double[] costSoFar = new double[_graph.VertCount];
            List<int>[] shortestPath = new List<int>[_graph.VertCount];

            for (int i = 0; i < _graph.VertCount; i++)
            {
                if (i == _startId)
                {
                    costSoFar[i] = 0;
                    shortestPath[i] = new List<int> {i};
                }
                else
                {
                    costSoFar[i] = double.MaxValue;
                }
            }

            for (int i = 0; i < _graph.VertCount-1; i++)
            {
                for (int j = 0; j < _graph.VertCount; j++)
                {
                    for (int k = 0; k < _graph.VertCount; k++)
                    {
                        if (_graph.MatrixWeight[j, k] != 0)
                        {
                            iterations++;
                            if (costSoFar[j] != double.MaxValue && costSoFar[k] > costSoFar[j] + _graph.MatrixWeight[j, k])
                            {
                                costSoFar[k] = costSoFar[j] + _graph.MatrixWeight[j, k];
                                shortestPath[k] = new List<int>(shortestPath[j]) {k};
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < _graph.VertCount; i++)
            {
                for (int j = 0; j < _graph.VertCount; j++)
                {
                    if (_graph.MatrixWeight[i, j] != 0)
                    {
                        if (costSoFar[i] != double.MaxValue && costSoFar[j] > costSoFar[i] + _graph.MatrixWeight[i, j])
                        {
                            negativeLoop = true;
                            return -1;
                        }
                    }
                }
            }

            path = shortestPath[_destId];
            return costSoFar[_destId];
        }

        /// <summary>
        /// Finds the shortest path using A* algorithm
        /// </summary>
        /// <param name="path">represents newly path of algorithm from startId to finishId</param>
        /// <param name="iterations">represents number of iterations of algorithm</param>
        /// <returns>length of the shortest path</returns>
        public double AStar(out List<int> path, out long iterations)
        {
            path = new List<int>();
            iterations = 0;
            PriorityQueue<int> frontier = new PriorityQueue<int>();
            frontier.AddElement(_startId, 0);
            int[] cameFrom = new int[_graph.VertCount];
            double[] costSoFar = new double[_graph.VertCount];
            cameFrom[_startId] = -1;
            for (int i = 0; i < _graph.VertCount; i++)
            {
                if (i == _startId)
                {
                    costSoFar[i] = 0;
                }
                else
                {
                    costSoFar[i] = double.MaxValue;
                }
            }

            while (!frontier.IsEmpty())
            {
                int currentId = frontier.Dequeue();

                if (currentId == _destId)
                {
                    path = new List<int>();
                    currentId = _destId;
                    while (currentId != -1)
                    {
                        path.Add(currentId);
                        currentId = cameFrom[currentId];
                    }

                    path.Reverse();
                    return costSoFar[_destId];
                }

                for (int j = 0; j < _graph.VertCount; j++)
                {
                    if (_graph.MatrixWeight[currentId, j] != 0)
                    {
                        iterations++;
                        double newCost = costSoFar[currentId] + _graph.MatrixWeight[currentId, j];
                        if (newCost < costSoFar[j])
                        {
                            costSoFar[j] = newCost;
                            frontier.AddElement(j, newCost + _graph.MatrixDist[j, _destId]);
                            cameFrom[j] = currentId;
                        }
                    }
                }
            }

            return costSoFar[_destId];
        }
    }
}