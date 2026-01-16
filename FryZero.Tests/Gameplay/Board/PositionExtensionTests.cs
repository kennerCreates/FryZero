using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.Statics.Gameplay.Board;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay.Board;

public class PositionExtensionTests
{
    [Fact]
    public void SetupPositionWithEmptyBoardTest()
    {
        var position = new ChessPosition();
        var expected = PositionFactory.GetEmptyPosition();
        var actual = position.SetupPositionWithEmptyBoard();
        Assert.Equal(expected.Squares.Count, actual.Squares.Count);
        Assert.Equivalent(expected.Squares, actual.Squares);
    }

}
