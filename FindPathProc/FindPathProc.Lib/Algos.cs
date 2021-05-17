using System;
using System.Collections.Generic;
using System.Linq;

namespace FindPathProc.Lib
{
    public class Algos
    {
        private double[,] _smMatrix;
        private int _vertsCount;

        public Algos(double[,] smMatrix)
        {
            _smMatrix = smMatrix;
            _vertsCount = _smMatrix.GetLength(0);
        }

        public double Deikstra(int startId, int finishId, ref List<int> path)
        {
            // pathToVerts - Найближчий шлях з початкової воршини до усіх інших
            // Спочатку шлях до всіх вершин, крім початкової - inf
            double[] pathToVerts = new double[_vertsCount];
            List<int>[] shortestPath = new List<int>[_vertsCount];
            bool[] usedVerts = new bool[_vertsCount];
            for (int i = 0; i < _vertsCount; i++)
            {
                if (i == startId)
                {
                    pathToVerts[i] = 0;
                    shortestPath[i] = new List<int> {i};
                }
                else
                {
                    pathToVerts[i] = double.MaxValue;
                }
            }

            int currNode = startId;
            while (usedVerts.Count(a => a == false) != 0)
            {
                for (int i = 0; i < _vertsCount; i++)
                {
                    if (!usedVerts[i] && _smMatrix[currNode, i] != 0)
                    {
                        if (pathToVerts[i] > (pathToVerts[currNode] + _smMatrix[currNode, i]))
                        {
                            pathToVerts[i] = (pathToVerts[currNode] + _smMatrix[currNode, i]);
                            shortestPath[i] = new List<int>(shortestPath[currNode]) {i};
                        }
                    }

                    usedVerts[currNode] = true;
                }
                currNode = FindNode(pathToVerts, usedVerts);
            }

            path = shortestPath[finishId];
            return pathToVerts[finishId];
        }

        private static int FindNode(double[] pathToVerts, bool[] usedVerts)
        {
            double minPath = Double.MaxValue;
            int minPathId = -1;
            for (int i = 0; i < pathToVerts.Length; i++)
            {
                if (pathToVerts[i] < minPath && !usedVerts[i])
                {
                    minPath = pathToVerts[i];
                    minPathId = i;
                }
            }
            return minPathId;
        }

        public void BellmanFord()
        {
        }

        public void AStar()
        {
        }
    }
}