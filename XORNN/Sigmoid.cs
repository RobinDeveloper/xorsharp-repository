using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORNN
{
    public class Sigmoid
    {
        public static double SigmoidOutput(double e)
        {
            return 1.0 / (1.0 + Math.Exp(-e));
        }

        public static double SigmoidDerivative(double e)
        {
            return e * (1 - e);
        }
    }
}
