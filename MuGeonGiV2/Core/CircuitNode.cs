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
        public static ICircuitNode FindPreviousEndPoint(this ICircuitNode self)
        {
            var previous = self.Previous;
            if (previous == null)
            {
                return null;
            }
            return previous.IsEndPoint ? previous : previous.FindPreviousEndPoint();
        }

        public static ICircuitNode FindNextEndPoint(this ICircuitNode self)
        {
            var next = self.Next;
            if (next == null)
            {
                return null;
            }
            return next.IsEndPoint ? next : next.FindNextEndPoint();
        }

        public static ICircuitNode FindEndPoint(this ICircuitNode self, ICircuitNode from)
        {
            if (self.IsEndPoint)
            {
                return self;
            }

            var oppositeSide = from == self.Previous ? self.Next : self.Previous;
            return self?.FindEndPoint(oppositeSide);
        }
    }
    public interface ICircuitNode
    {
        //               Previous -------> Next
        // Mic => OutputJack => Cable => InputJack => Speaker
        ICircuitNode Next { get; }
        ICircuitNode Previous { get; }
        bool IsEndPoint { get; }
    }
}
