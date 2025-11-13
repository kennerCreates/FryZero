using FryZeroGodot.gameplay;
using FryZeroGodot.gameplay.Pieces;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay.Pieces;

public class PieceTests
{
    [Fact]
    public void GetHalfBoardTest()
    {
        var options = new PieceOptions
        {
            SquareSize = 120
        };
        const int expected = 420;
        var actual = BoardLocations.CenterBoardLocation(options);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetLocationFromFileTest()
    {
        var options = new PieceOptions
        {
            PieceFile = File.C,
            SquareSize = 30
        };
        
        const int expected = -45;
        var actual = BoardLocations.GetLocationFromFile(options);
        
        Assert.Equal(expected, actual);
    }
}