using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotInterface.Models;

namespace FryZero.Tests.GodotNodes.Game.Pieces;

public class GodotPieceTests
{
    [Fact]
    public void GetUpdatedPinJoint()
    {
        int squareSize = 25;
        var position = new Vector(35, 67);
        var pinJoint = new PinJoint
        {
            Softness = 35, 
            Position = position
        };
        var expected = new PinJoint
        {
            Softness = 25/100f,
            Position = position
        };

        var actual = pinJoint.WithUpdatedSoftness(squareSize);
        
        Assert.Equivalent(expected, actual);
    }
}
