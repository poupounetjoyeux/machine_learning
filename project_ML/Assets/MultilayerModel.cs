using System;

namespace Assets
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    struct MultiLayerModel
    {
        public double[][][] w;

        public double[][][] x;

        public double[][][] sigmas;
    }
}
