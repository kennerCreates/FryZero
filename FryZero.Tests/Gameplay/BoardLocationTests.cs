using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Pieces;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay;
using Godot;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay;

public class BoardLocationTests
{
    
    [Fact]
    public void GetHalfBoardTest()
    {
        var options = new PieceAttributes
        {
            SquareSize = 30
        };
        const int expected = 105;
        var actual = BoardLocations.CenterBoardLocation(options);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetLocationFromFileTest()
    {
        var options = new PieceAttributes
        {
            SquareSize = 30
        };
        
        const int expected = -15;
        
        var actual = BoardLocations.GetLocationFromFile(options, new Square(File.D, Rank.Three));
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetLocationFromRankTest()
    {
        var options = new PieceAttributes
        {
            SquareSize = 60
        };
        const int expected = -210;
        
        var actual = BoardLocations.GetLocationFromRank(options, new Square(File.D, Rank.One));
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetLocationFromSquareTest()
    {
        var options = new PieceAttributes
        {
            SquareSize = 40
        };
        
        Vector2 expected = new(140,-20);
        
        var actual = BoardLocations.GetLocationFromSquare(options, new Square(File.H, Rank.Four));
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetFileFromLocationTest()
    {
        var options = new PieceAttributes()
        {
            SquareSize = 40
        };
        
        var expected = File.C;
        
        var actual = BoardLocations.GetFileFromLocation(options, -60);
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetRankFromLocationTest()
    {
        var options = new PieceAttributes()
        {
            SquareSize = 40
        };
        
        var expected = Rank.Two;
        
        var actual = BoardLocations.GetRankFromLocation(options, -100);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetDecimalVectorTest()
    {
        var options = new PieceAttributes()
        {
            SquareSize = 40
        };
        var location = new Vector2(-140, 40);
        var expected = new Vector2(-.5f, 0);
        
        var actual = BoardLocations.GetDecimalVector(options, location);
        
        Assert.Equal(expected, actual);
    }
    
}