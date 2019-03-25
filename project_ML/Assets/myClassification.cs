using System.Runtime.InteropServices;

public class myClassification
{
    [DllImport("ClassificationDLL")]
    public static extern int return5();
}

