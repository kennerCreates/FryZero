using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.UI.GameTheme;

namespace FryZero.Tests.UI.GameTheme;

public class ThemeExtensionsTests
{
    [Fact]
    public void Test1()
    {
        var gameThemeData = new GameThemeData
        {
            ResourceLocalToScene = false,
            ResourcePath = null,
            ResourceName = null,
            ResourceSceneUniqueId = null,
            LightColor = default,
            LightHighlightColor = default,
            LightShadowColor = default,
            LightAccentColor = default,
            DarkColor = default,
            DarkHighlightColor = default,
            DarkShadowColor = default,
            DarkAccentColor = default,
            BackgroundTexture = null,
            BackgroundColor = ThemeColor.Light,
            BackgroundPatternColor = ThemeColor.Light,
            BackgroundPatternScale = 0,
            SquareSize = 0,
            LightSquareTexture = null,
            LightSquareColor = ThemeColor.Light,
            DarkSquareTexture = null,
            DarkSquareColor = ThemeColor.Light,
            PieceMovementDelay = 0,
            PieceStyle = PieceStyle.Tiny,
            PieceAtlasTexture = null,
            PieceSize = 0
        };
    }
}
