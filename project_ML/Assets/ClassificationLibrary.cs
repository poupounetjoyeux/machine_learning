using System;
using System.Runtime.InteropServices;

namespace Assets
{
    public class ClassificationLibrary
    {
        [DllImport("ClassificationDLL")]
        public static extern IntPtr create_model(int inputsDimension);

        [DllImport("ClassificationDLL")]
        public static extern int predict(IntPtr model, double[] inputk, int dimensions);

        [DllImport("ClassificationDLL")]
        public static extern void release_model(IntPtr model);

        [DllImport("ClassificationDLL")]
        public static extern void train_model(IntPtr model, double[] inputs, int inputsDimension, int nbInputs,
            int[] expectedSigns, double learnStep, int nbIterations);
    }
}

