using FryZeroGodot.Config;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay;

public class BoardLocationTests
{
    [Theory]
    [InlineData(File.D, Rank.Four, 30, -15)]
    [InlineData(File.D, Rank.Five, 30, -15)]
    [InlineData(File.E, Rank.Four, 30, 15)]
    [InlineData(File.E, Rank.Five, 30, 15)]
    [InlineData(File.A, Rank.One, 30, -105)]
    [InlineData(File.A, Rank.Eight, 30, -105)]
    [InlineData(File.H, Rank.One, 30, 105)]
    [InlineData(File.H, Rank.Eight, 30, 105)]
    public void XCoordinate_Returns_Expected_Coordinates_Based_On_Square_Size(File file, Rank rank, int squareSize, float expected)
    {
        var square = new Square(file, rank);

        float actual = square.XCoordinate(squareSize);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(File.D, Rank.Four, 30, 15)]
    [InlineData(File.D, Rank.Five, 30, -15)]
    [InlineData(File.E, Rank.Four, 30, 15)]
    [InlineData(File.E, Rank.Five, 30, -15)]
    [InlineData(File.A, Rank.One, 30, 105)]
    [InlineData(File.A, Rank.Eight, 30, -105)]
    [InlineData(File.H, Rank.One, 30, 105)]
    [InlineData(File.H, Rank.Eight, 30, -105)]
    public void YCoordinate_Returns_Expected_Coordinates_Based_On_Square_Size(File file, Rank rank, int squareSize, float expected)
    {
        var square = new Square(file, rank);

        float actual = square.YCoordinate(squareSize);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(File.D, Rank.Four, 30, -15, 15)]
    [InlineData(File.D, Rank.Five, 30, -15, -15)]
    [InlineData(File.E, Rank.Four, 30, 15, 15)]
    [InlineData(File.E, Rank.Five, 30, 15, -15)]
    [InlineData(File.A, Rank.One, 30, -105, 105)]
    [InlineData(File.A, Rank.Eight, 30, -105, -105)]
    [InlineData(File.H, Rank.One, 30, 105, 105)]
    [InlineData(File.H, Rank.Eight, 30, 105, -105)]
    public void LocationVector_Returns_Expected_Coordinates_Based_On_Square_Size(File file, Rank rank, int squareSize, float expectedX, float expectedY)
    {
        var square = new Square(file, rank);
        var actual = square.LocationVector(squareSize);

        Assert.Equal(expectedX, actual.X);
        Assert.Equal(expectedY, actual.Y);
    }
}
