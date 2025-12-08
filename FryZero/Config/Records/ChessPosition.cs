#nullable enable

using System.Collections.Generic;
using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.Config.Records;

public record ChessPosition
{
    public List<Square> Squares { get; set; } = [];
    public PieceColor ActiveColor { get; set; }
    public bool WhiteCanCastleKingSide { get; set; }
    public bool WhiteCanCastleQueenSide { get; set; }
    public bool BlackCanCastleKingSide { get; set; }
    public bool BlackCanCastleQueenSide { get; set; }
    public Square? EnPassantTarget { get; set; }
    public int HalfMoveClock { get; set; }
    public int FullMoveNumber { get; set; }
}
