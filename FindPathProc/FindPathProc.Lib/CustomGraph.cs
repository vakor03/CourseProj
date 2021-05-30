namespace FindPathProc.Lib
{
    /// <summary>
    /// Represents implementation of basic graph
    /// </summary>
    public class CustomGraph
    {
        /// <summary>
        /// Weight matrix of initial graph
        /// </summary>
        public readonly int[,] MatrixWeight;

        /// <summary>
        /// Distance matrix of initial graph
        /// </summary>
        public readonly int[,] MatrixDist;

        /// <summary>
        /// Number of vertices in graph
        /// </summary>
        public readonly int VertCount;

        ///<summary>
        /// Existence of negative edges in graph 
        ///</summary>
        public bool HasNegativeEdges
        {
            get
            {
                for (int i = 0; i < VertCount; i++)
                {
                    for (int j = i; j < VertCount; j++)
                    {
                        if (MatrixWeight[i, j] < 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="matrixWeight">weight matrix of graph</param>
        /// <param name="matrixDist">distance matrix of graph</param>
        public CustomGraph(int[,] matrixWeight, int[,] matrixDist)
        {
            MatrixWeight = matrixWeight;
            MatrixDist = matrixDist;
            VertCount = matrixWeight.GetLength(0);
        }
        
        ///<summary>
        /// Checks for path in graph between two vertices
        ///</summary>
        /// <param name="start">id of start vertex</param>
        /// <param name="finish">id of destination vertex</param>
        /// <returns>existence of current path in graph </returns>
        public bool PathExist(int start, int finish)
        {
            bool[] visited = new bool[VertCount];
            return PathExist(start, finish, visited);
        }

        /// <summary>
        /// Auxiliary method for PathExist(int start, int finish)
        /// </summary>
        private bool PathExist(int start, int finish, bool[] visited)
        {
            if (start == finish)
            {
                return true;
            }

            visited[start] = true;
            for (int i = start; i < VertCount; i++)
            {
                for (int j = 0; j < VertCount; j++)
                {
                    if (MatrixWeight[i, j] != 0 && !visited[j])
                    {
                        if (PathExist(j, finish, visited))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}