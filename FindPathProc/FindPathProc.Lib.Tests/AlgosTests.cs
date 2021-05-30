using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindPathProc.Lib.Tests
{
    [TestClass] public class AlgosTests
    {
        [TestMethod]
        public void Dijkstra()
        {
            int[,] matrix =
            {
                {0, -1, 9, 0, 0, 14,0},
                {-1, 0, 10, 15, 0, 0,0},
                {9, 10, 0, 11, 0, 2,0},
                {0, 15, 11, 0, 6, 0,0},
                {0, 0, 0, 6, 0, 9,0},
                {14, 0, 2, 0, 9, 0,0},
                {0,0,0,0,0,0,0}
            };
            Algo algo = new Algo(new CustomGraph(matrix, null));
            List<int> path1 = new List<int>();
            List<int> path2 = new List<int>();
            //algos.Dijkstra(0, 4, ref path1);
            
            
 
        }

        [TestMethod]
        public void Hallo()
        {
            Assert.AreEqual(1,1);
        }
    }
}