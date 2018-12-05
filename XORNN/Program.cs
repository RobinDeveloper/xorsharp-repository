using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORNN
{
    class Program
    {
        static void Main(string[] args)
        {
            Train();
        }

        private static bool CheckResult(double expected, double actual)
        {
            if ((Math.Round(actual) <= 0 && expected >= 1) || (Math.Round(actual) >= 1 && expected <= 0))
                return false;
            else
                return true;
        }

        private static void Train()
        {
            double[,] neuralNetworkInputs = { { 0, 1 }, { 1, 0 }, { 0, 0 }, { 1, 1 }, { 0 , 1 }, { 1 , 1 }, { 0 , 1 }, { 0 , 0 } };
            double[] expectedResults = { 1, 1, 0, 0, 1, 0, 1, 0 };

            Neuron[] hiddenNeurons = new Neuron[3]; // 2 neurons and a output neuron

            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                hiddenNeurons[i] = new Neuron();
            }

            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                hiddenNeurons[i].SetRandomWeights();
            }

            int iteration = 0;

            while (iteration < 2000)
            {
                iteration++;
                for (int i = 0; i < 8; i++) //all examples
                {
                    for (int j = 0; j < 2; j++)
                    {
                        hiddenNeurons[j].Inputs = new double[] { neuralNetworkInputs[i, 0], neuralNetworkInputs[i, 1] };
                    }

                    hiddenNeurons[2].Inputs = new double[] { hiddenNeurons[0].Output, hiddenNeurons[1].Output }; //the output neuron

                    bool trueOrFalse = CheckResult(expectedResults[i],hiddenNeurons[2].Output);

                    bool didGood = false;

                    if (Math.Round(expectedResults[i]) == Math.Round(hiddenNeurons[2].Output))
                        didGood = true;
                    else
                        didGood = false;

                    Console.WriteLine(neuralNetworkInputs[i,0] + " xor " + neuralNetworkInputs[i,1] + " so output is " + hiddenNeurons[2].Output + " which makes it " + trueOrFalse.ToString() + " expected outcome was " + expectedResults[i] + " which made this NN " + (didGood ? "nailed" : "didn't catch"));
                    //now comes the back prop

                    hiddenNeurons[2].Error = Sigmoid.SigmoidDerivative(hiddenNeurons[0].Output) * (expectedResults[i] - hiddenNeurons[2].Output);
                    hiddenNeurons[2].AdjustTheWeights();

                    for (int k = 0; k < 2; k++)
                    {
                        hiddenNeurons[k].Error = Sigmoid.SigmoidDerivative(hiddenNeurons[k].Output) * hiddenNeurons[2].Error * hiddenNeurons[2].Weight[k];
                    }

                    for (int l = 0; l < 2; l++)
                    {
                        hiddenNeurons[l].AdjustTheWeights();
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
