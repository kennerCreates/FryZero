using FryZeroGodot.Config;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay;
using Godot;
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

    [Theory]
    [InlineData(5,6, 100, File.E)]
    [InlineData(-256,256, 100, File.B)]
    [InlineData(306,-306, 100, File.H)]
    [InlineData(-5,-6, 100, File.D)]
    [InlineData(256,-256, 100, File.G)]
    [InlineData(-306,-306, 100, File.A)]
    public void XGameCoordinate_Returns_Expected_File_Based_On_Square_Size(float locationX, float locationY, int squareSize, File expected)
    {
        var location = new Vector2(locationX, locationY);
        var actual = location.X.GetFile(squareSize);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(5,6, 100, Rank.Four)]
    [InlineData(-256,256, 100, Rank.Two)]
    [InlineData(306,306, 100, Rank.One)]
    [InlineData(-5,-6, 100, Rank.Five)]
    [InlineData(256,-256, 100, Rank.Seven)]
    [InlineData(-306,-306, 100, Rank.Eight)]
    public void YGameCoordinate_Returns_Expected_Rank_Based_On_Square_Size(float locationX, float locationY, int squareSize, Rank expected)
    {
        var location = new Vector2(locationX, locationY);
        var actual = location.Y.GetRank(squareSize);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(5,6, 100, File.E, Rank.Four)]
    [InlineData(-256,256, 100, File.B, Rank.Two)]
    [InlineData(306,306, 100, File.H, Rank.One)]
    [InlineData(-5,-6, 100, File.D, Rank.Five)]
    [InlineData(256,-256, 100, File.G, Rank.Seven)]
    [InlineData(-306,-306, 100, File.A, Rank.Eight)]
    public void GameCoordinate_Returns_Expected_Square_Based_On_Square_Size(float locationX, float locationY, int squareSize, File file, Rank rank)
    {
        var location = new Vector2(locationX, locationY);
        var actual = location.GetSquare(squareSize);
        var expectedSquare = new Square(file, rank);

        Assert.Equal(expectedSquare, actual);
    }

}
