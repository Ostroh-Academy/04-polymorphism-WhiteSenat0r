namespace PatternsLab;

internal interface IVectorSystem
{
    void PrintCoordinates();
    bool IsLinearlyIndependent();
    double GetDeterminant();
}

internal class Vector2DSystem(double[] a, double[] b) : IVectorSystem
{
    public void PrintCoordinates()
    {
        Console.WriteLine($"Vector A: ({string.Join(',', a)})");
        Console.WriteLine($"Vector B: ({string.Join(',', b)})");
    }

    public bool IsLinearlyIndependent() => !GetDeterminant().Equals(0);

    public double GetDeterminant() => a[0] * b[1] - a[1] * b[0];
}

internal class Vector3DSystem(double[] a, double[] b, double[] c) : IVectorSystem
{
    public void PrintCoordinates()
    {
        Console.WriteLine($"Vector A: ({string.Join(',', a)})");
        Console.WriteLine($"Vector B: ({string.Join(',', b)})");
        Console.WriteLine($"Vector C: ({string.Join(',', c)})");
    }

    public bool IsLinearlyIndependent() => !GetDeterminant().Equals(0);
    
    public double GetDeterminant() =>
        a[0] * (b[1] * c[2] - b[2] * c[1]) -
        a[1] * (b[0] * c[2] - b[2] * c[0]) +
        a[2] * (b[0] * c[1] - b[1] * c[0]);
}

internal abstract class VectorSystemFactory
{
    public abstract IVectorSystem InstantiateVectorSystem(params double[][] vectors);
}

internal class Vector2DSystemFactory : VectorSystemFactory
{
    public override IVectorSystem InstantiateVectorSystem(params double[][] vectors) => 
        new Vector2DSystem(vectors[0], vectors[1]);
}

internal class Vector3DSystemFactory : VectorSystemFactory
{
    public override IVectorSystem InstantiateVectorSystem(params double[][] vectors) => 
        new Vector3DSystem(vectors[0], vectors[1], vectors[2]);
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Choose a vector system to instantiate (2 - 2D system, 3 - 3D system): ");
        var option = Console.ReadKey();
        Console.WriteLine('\n');

        IVectorSystem system = option.Key switch
        {
            ConsoleKey.D2 => new Vector2DSystemFactory().InstantiateVectorSystem(
                [1, 2], [3, 4]),
            ConsoleKey.D3 => new Vector3DSystemFactory().InstantiateVectorSystem(
                [1, 2, 3], [3, 4, 5], [6, 7, 8]),
            _ => throw new KeyNotFoundException("Option was not found")
        };

        system.PrintCoordinates();
        
        var determinant = system.GetDeterminant();
        Console.WriteLine($"System's determinant: {determinant}");

        var isIndependent = system.IsLinearlyIndependent();
        Console.WriteLine($"System is linearly independent: {isIndependent}");
    }
}