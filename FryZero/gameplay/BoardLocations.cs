using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.gameplay;

public static class BoardLocations
{
    private const int SquareSize = 160;
    
    public static Vector2 GetPieceLocation(File file, Rank rank) => 
        new(GetFileLocation(file), GetRankLocation(rank));

    private static int GetRankLocation(Rank rank) => 
        ((int)rank * SquareSize - GetHalfBoardSize()) * -1;

    private static int GetFileLocation(File file) => 
        (int)file * SquareSize - GetHalfBoardSize();
        
    private static int GetHalfBoardSize() => 
        SquareSize * 3 + SquareSize / 2;
}