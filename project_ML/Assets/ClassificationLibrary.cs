using System.Runtime.InteropServices;

namespace Assets
{
    public class ClassificationLibrary
    {
        [DllImport("ClassificationDLL")]
        public static extern double[] create_model(int inputsDimension);

        [DllImport("ClassificationDLL")]
        public static extern void train_model(double[] model, double[] inputs, int inputsDimension, int nbInputs,
            int[] expectedSigns, double learnStep, int nbIterations);
    }
}

