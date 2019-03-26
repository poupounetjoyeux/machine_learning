using System.Runtime.InteropServices;

public class ClassificationLibrary
{
    [DllImport("ClassificationDLL")]
    public static extern double[] create_model(int inputsDimension);
}

