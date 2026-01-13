using FryZeroGodot.GodotInterface.UI.GameTheme;
using Godot;
using NSubstitute;

namespace FryZero.Tests.UI.GameTheme;

public static class GameThemeDataFactory
{
    public static IGameThemeData ThemeColorTestData()
    {
        var themeColorTestData = Substitute.For<IGameThemeData>();
        themeColorTestData.LightColor = Colors.AliceBlue;
        themeColorTestData.LightHighlightColor = Colors.BlueViolet;
        themeColorTestData.LightShadowColor = Colors.Crimson;
        themeColorTestData.LightAccentColor = Colors.DarkGoldenrod;
        themeColorTestData.DarkColor = Colors.Coral;
        themeColorTestData.LightHighlightColor = Colors.DarkOrchid;
        themeColorTestData.LightHighlightColor = Colors.BlanchedAlmond;
        themeColorTestData.LightHighlightColor = Colors.DarkSeaGreen;

        return themeColorTestData;
    }

}
