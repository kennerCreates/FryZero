using FryZeroGodot.Config.Enums;
using Godot;
using GodotCreatePieceButton = FryZeroGodot.GodotInterface.UI.Buttons.GodotCreatePieceButton;

namespace FryZeroGodot.Statics.UI.Display;

public static class PieceButtonLocations
{

    public static GodotCreatePieceButton CreateNewPieceButton(PieceColor color, PieceType type, int squareSize)
    {
        var button = new GodotCreatePieceButton();
        button.Type = type;
        button.Color = color;
        button.Position = GetNewPieceButtonLocation(color, type, squareSize);
        return button;
    }

    private static Vector2 GetNewPieceButtonLocation(PieceColor color, PieceType type, int squareSize) =>
        new(GetXLocationCoordinate(squareSize, GetIsLeftPiece(type)),
            GetYLocationCoordinate(squareSize, GetPieceRow(color, type)));
    private static int GetXLocationCoordinate(int squareSize, bool isLeftPiece) =>
        isLeftPiece ? squareSize * -6 : squareSize * -5;

    private static bool GetIsLeftPiece(PieceType type) =>
        type switch
        {
            PieceType.Pawn => false,
            PieceType.Knight => true,
            PieceType.Bishop => true,
            PieceType.Rook => true,
            PieceType.Queen => false,
            PieceType.King => false,
            _ => true
        };

    private static int GetYLocationCoordinate(int squareSize, int pieceRow) =>
        pieceRow switch
        {
            1 => squareSize,
            2 => squareSize * 2,
            3 => squareSize * 3,
            -1 => squareSize * -1,
            -2 => squareSize * -2,
            -3 => squareSize * -3,
            _ => squareSize
        };

    private static int GetPieceRow(PieceColor color, PieceType type) =>
        GetPieceRowColor(color) * GetPieceRowType(type);

    private static int GetPieceRowColor(PieceColor color) =>
        color switch
        {
            PieceColor.White => 1,
            PieceColor.Black => -1,
            _ => 0
        };

    private static int GetPieceRowType(PieceType type) =>
        type switch
        {
            PieceType.Pawn => 1,
            PieceType.Knight => 2,
            PieceType.Bishop => 1,
            PieceType.Rook => 3,
            PieceType.Queen => 2,
            PieceType.King => 3,
            _ => 0
        };
}
