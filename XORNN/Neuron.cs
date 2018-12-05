using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORNN
{
    public class Neuron
    {
        public double[] Inputs = new double[2];
        public double[] Weight = new double[2];

        public double Error;

        private double m_Bias;

        private Random m_Rand = new Random();

        public double Output { get { return Sigmoid.SigmoidOutput(Weight[0] * Inputs[0] + Weight[1] * Inputs[1] + m_Bias); } }

        public void SetRandomWeights()
        {
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = m_Rand.NextDouble();
            }
           
            m_Bias += Error;
        }

        public void AdjustTheWeights()
        {
            for (int i = 0; i < Weight.Length; i++)
            {
                Weight[i] = Error * Inputs[i];
            }
            m_Bias += Error;
        }
    }
}
