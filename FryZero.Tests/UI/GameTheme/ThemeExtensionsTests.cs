using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.UI.GameTheme;
using FryZeroGodot.Statics.UI.GameTheme;
using Godot;
using NSubstitute;

namespace FryZero.Tests.UI.GameTheme;

public class ThemeExtensionsTests
{
    [Fact]
    public void GetModulateColor_Returns_Expected_Color_Dark()
    {
        var themeColor = ThemeColor.Dark;
        var expected = Colors.Black;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_DarkHighlight()
    {
        var themeColor = ThemeColor.DarkHighlight;
        var expected = Colors.Cyan;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_DarkShadow()
    {
        var themeColor = ThemeColor.DarkShadow;
        var expected = Colors.Blue;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_DarkAccent()
    {
        var themeColor = ThemeColor.DarkAccent;
        var expected = Colors.Green;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_Light()
    {
        var themeColor = ThemeColor.Light;
        var expected = Colors.White;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_LightHighlight()
    {
        var themeColor = ThemeColor.LightHighlight;
        var expected = Colors.Yellow;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_LightShadow()
    {
        var themeColor = ThemeColor.LightShadow;
        var expected = Colors.Magenta;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Fact]
    public void GetModulateColor_Returns_Expected_Color_LightAccent()
    {
        var themeColor = ThemeColor.LightAccent;
        var expected = Colors.Red;
        var actual = themeColor.GetModulateColor();
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }

    [Theory]
    [InlineData(ThemeColor.Light, "light_color")]
    [InlineData(ThemeColor.LightHighlight, "light_highlight_color")]
    [InlineData(ThemeColor.LightShadow, "light_shadow_color")]
    [InlineData(ThemeColor.LightAccent, "light_accent_color")]
    [InlineData(ThemeColor.Dark, "dark_color")]
    [InlineData(ThemeColor.DarkHighlight, "dark_highlight_color")]
    [InlineData(ThemeColor.DarkShadow, "dark_shadow_color")]
    [InlineData(ThemeColor.DarkAccent, "dark_accent_color")]
    public void GetMaterialParameterNameStringTests(ThemeColor themeColor, string expected)
    {
        var actual = themeColor.GetParameterString();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetThemeColor_Returns_Expected_ThemeColor_Light()
    {
        var themeColorTestData = GameThemeDataFactory.ThemeColorTestData();
        var expected = themeColorTestData.LightColor;
        var actual = ThemeColor.Light.GetThemeColor(themeColorTestData);
        Assert.Equal(expected.R, actual.R);
        Assert.Equal(expected.G, actual.G);
        Assert.Equal(expected.B, actual.B);
    }
}
