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
        public static ICircuitNode FindEndPoint(this ICircuitNode self, ICircuitNode from)
        {
            if (self.IsEndPoint)
            {
                return self;
            }

            var oppositeSide = from == self.Previous ? self.Next : self.Previous;
            return oppositeSide?.FindEndPoint(self);
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
