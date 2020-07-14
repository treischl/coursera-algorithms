using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core.GraphSearch
{
    public class Kosaraju : IKosaraju
    {
        public int[] GetSccGroupSizes(DirectedAdjacencyList inputGraph)
        {
            var finishingTimes = new List<DirectedVertex>();
            inputGraph.Vertices.Reverse();
            foreach (var vertex in inputGraph.Vertices.Where(x => !x.Explored))
            {
                finishingTimes.AddRange(GetFinishingTimes(vertex));
            }

            MarkVerticesUnexplored(inputGraph.Vertices);

            finishingTimes.Reverse();
            foreach (var vertex in finishingTimes.Where(x => !x.Explored))
            {
                ComputeStronglyConnectedComponent(vertex, vertex.Label);
            }

            return inputGraph.Vertices
                .GroupBy(x => x.SccLeader)
                .Select(x => x.Count())
                .OrderByDescending(x => x)
                .ToArray();
        }

        private IEnumerable<DirectedVertex> GetFinishingTimes(DirectedVertex rootVertex)
        {
            var popped = new Stack<DirectedVertex>();
            RunDfs(rootVertex, DfsDirection.Reverse, (vertex) => popped.Push(vertex));

            while (popped.Count > 0)
            {
                yield return popped.Pop();
            }
        }

        private void MarkVerticesUnexplored(IEnumerable<DirectedVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                vertex.Explored = false;
            }
        }

        private void ComputeStronglyConnectedComponent(
            DirectedVertex rootVertex,
            int sccLeader)
        {
            RunDfs(rootVertex, DfsDirection.Normal, (vertex) => vertex.SccLeader = sccLeader);
        }

        private enum DfsDirection
        {
            Normal,
            Reverse,
        };

        private void RunDfs(
            DirectedVertex rootVertex,
            DfsDirection direction,
            Action<DirectedVertex> handleVertex = null)
        {
            var dfsStack = new Stack<DirectedVertex>();
            dfsStack.Push(rootVertex);
            rootVertex.Explored = true;
            var searchForNeighbors = GetNeighborFunction(direction);
            while (dfsStack.Count > 0)
            {
                var vertex = dfsStack.Pop();
                handleVertex?.Invoke(vertex);
                searchForNeighbors(vertex, dfsStack);
            }
        }

        private delegate void SearchForNeighbor(
            DirectedVertex current,
            Stack<DirectedVertex> dfsStack);

        private SearchForNeighbor GetNeighborFunction(DfsDirection searchDirection)
        {
            return searchDirection switch
            {
                DfsDirection.Normal => new SearchForNeighbor((vertex, dfsStack) =>
                {
                    foreach (var edge in vertex.EdgesOut.Where(x => !x.Head.Explored))
                    {
                        dfsStack.Push(edge.Head);
                        edge.Head.Explored = true;
                    }
                }),
                DfsDirection.Reverse => new SearchForNeighbor((vertex, dfsStack) =>
                {
                    foreach (var edge in vertex.EdgesIn.Where(x => !x.Tail.Explored))
                    {
                        dfsStack.Push(edge.Tail);
                        edge.Tail.Explored = true;
                    }
                }),
                _ => throw new ArgumentException("Invalid search direction"),
            };
        }
    }
}
