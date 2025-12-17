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

    [Fact]
    public void GetUpdatedRectangleShape()
    {
        int squareSize = 36;
        var shape = new RectangleShape();
        var expected = new RectangleShape
        {
            Size = new Vector(36,36)
        };

        var actual = shape.WithUpdatedShapex(squareSize);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void GetUpdatedArea()
    {
        var shape = new RectangleShape
        {
            Size = new Vector(12,12)
        };
        var area = new Area();

        var expected = new Area
        {
            Shape = new RectangleShape
            {
                Size = new Vector(12,12)
            },
        };

        var actual = area.WithUpdatedArea(shape);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void GetUpdatedPhsyics()
    {
        var shape = new RectangleShape
        {
            Size = new Vector(30,30)
        };
        var physics = new Physics();

        var expected = new Physics
        {
            Shape = new RectangleShape
            {
                Size = new Vector(30,30)
            },
        };

        var actual = physics.WithUpdatedPhysics(shape);

        Assert.Equivalent(expected, actual);
    }
}
