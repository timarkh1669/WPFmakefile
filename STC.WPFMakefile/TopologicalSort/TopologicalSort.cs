using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.WPFMakefile.TopologicalSort
{
    class TopologicalSort<T> where T : IGraphVertex<T>
    {
        Dictionary<T, bool> markedVertices;//used in Dfs sort
        public TopologicalSort()
        {
            markedVertices = new Dictionary<T, bool>();
        }

        public List<T> DfsSort(List<T> graph, T targetVertice)
        {
            List<T> res = new List<T>();
            markedVertices.Add(targetVertice, true);
            foreach (var v in targetVertice.OutEdges)
            {
                if (!markedVertices.ContainsKey(v))
                {
                    res = res.Concat(DfsSort(graph, v)).ToList();
                    markedVertices[v] = false;
                }
                else if (markedVertices[v])
                    throw new ApplicationException("Task dependencies contain a cycle.");
            }

            res.Add(targetVertice);
            return res;
        }

        public List<T> SortKahn(List<T> graph)
        {
            List<T> res = new List<T>();
            var inboudEdgesTCount = ComputeInboundEdgesCnt(graph);
            var vertices = inboudEdgesTCount.Where(x => x.Value == 0).Select(x => x.Key).ToList();
            while (inboudEdgesTCount.Count > 0)
            {
                //1) FindSource()
                if (vertices.Count == 0)
                    throw new ApplicationException("Task dependencies contain a cycle.");
                var v = vertices[vertices.Count - 1];
                vertices.RemoveAt(vertices.Count - 1);

                //2) Add next ordered value
                res.Add(v);

                //3) Decrement edges count
                foreach (var x in v.OutEdges)
                {
                    inboudEdgesTCount[x]--;
                    if (inboudEdgesTCount[x] == 0)
                        vertices.Add(x);
                }

                //4) DeleteSource() with edges
                inboudEdgesTCount.Remove(v);
            }

            res.Reverse();
            return res;
        }
        
        private Dictionary<T, int> ComputeInboundEdgesCnt(List<T> graph)
        {
            Dictionary<T, int> res = new Dictionary<T, int>();
            foreach (var x in graph)
                res.Add(x, 0);

            foreach (var x in graph)
                foreach (var y in x.OutEdges)
                    res[y]++;

            return res;
        }
    }
}
