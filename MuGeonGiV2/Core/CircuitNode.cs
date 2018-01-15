using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public static class CircuitNodeExtension
    {
        public static List<ICircuitNode> FindPreviousEndPoints(this ICircuitNode self)
        {
            return self.Previouses
                .Select(previous =>
                    previous.IsEndPoint
                        ? new List<ICircuitNode>() { previous }
                        : previous.FindPreviousEndPoints())
                .SelectMany(i => i)
                .ToList();
        }

        public static List<ICircuitNode> FindNextEndPoints(this ICircuitNode self)
        {
            Console.WriteLine(self.GetType());
            return self.Nexts
                .Select(next =>
                    next.IsEndPoint
                        ? new List<ICircuitNode>() { next }
                        : next.FindNextEndPoints())
                .SelectMany(i => i)
                .ToList();
        }
    }
    public interface ICircuitNode
    {
        //               Previouses -------> Nexts
        // Mic => OutputJack => Cable => InputJack => Speaker
        List<ICircuitNode> Nexts { get; }
        List<ICircuitNode> Previouses { get; }
        bool IsEndPoint { get; }
    }
}
