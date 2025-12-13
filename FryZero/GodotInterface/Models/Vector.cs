using System.Security.Cryptography.X509Certificates;

namespace FryZeroGodot.GodotInterface.Models;

public record Vector
{
    public Vector(float x, float y)
    {
        X = x;
        Y = y;
    }
    
    public float X {get; init;}
    public float Y {get; init;}
}
