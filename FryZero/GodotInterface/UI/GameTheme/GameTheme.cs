using System;
using System.Collections.Generic;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Statics.UI.GameTheme;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.GameTheme;
public partial class GameTheme : Node2D
{
    public static GameTheme Instance { get; private set; }

    private GameThemeData _themeData = ResourceLoader.Load<GameThemeData>("res://assets/UI/CurrentColors.tres");

    private ShaderMaterial _themedMaterial = GD.Load<ShaderMaterial>("res://Shaders/HueShiftMaterial.tres");

    private Dictionary<int, AtlasTexture> _cache;

    public override void _Ready()
    {
        Instance = this;
        _themedMaterial.UpdateAllThemeColors(_themeData);
    }
    public Color GetBackgroundColor() => _themeData.BackgroundColor.GetModulateColor();
    public Color GetPatternColor() => _themeData.BackgroundPatternColor.GetModulateColor();
    public Texture2D GetBackgroundTexture() => _themeData.BackgroundTexture;
    public int GetPatternScale() => _themeData.BackgroundPatternScale;
    public Color GetDarkSquareColor() => _themeData.DarkSquareColor.GetModulateColor();
    public Color GetLightSquareColor() => _themeData.LightSquareColor.GetModulateColor();
    public Texture2D GetLightSquareTexture() => _themeData.LightSquareTexture;
    public Texture2D GetDarkSquareTexture() => _themeData.DarkSquareTexture;
    public int GetPieceDelay() => _themeData.PieceMovementDelay;
    public int GetSquareSize() => _themeData.SquareSize;
    public ShaderMaterial GetThemeMaterial() => _themedMaterial;
    public AtlasTexture GetPieceTexture(PieceType type, PieceColor color, InteractState state)
    {
        _cache ??= BuildCache();
        return _cache[MakeKey(type, color, state)];
    }

    private Dictionary<int, AtlasTexture> BuildCache()
    {
        var dict = new Dictionary<int, AtlasTexture>();
        foreach (var type in Enum.GetValues<PieceType>())
        foreach (var color in Enum.GetValues<PieceColor>())
        foreach (var state in Enum.GetValues<InteractState>())
        {
            var atlas = new AtlasTexture
            {
                Atlas = _themeData.PieceStyle.GetPieceTextureFromStyle(),
                Region = ComputeRegion(type, color, state)
            };
            dict[MakeKey(type, color, state)] = atlas;
        }
        return dict;
    }

    private Rect2 ComputeRegion(PieceType type, PieceColor color, InteractState state)
    {
        var column = (int)type;
        var row = color switch
        {
            PieceColor.White => state == InteractState.Normal ? 0 : 1,
            PieceColor.Black => state == InteractState.Normal ? 2 : 3,
            _ => 0
        };
        return new Rect2
            (
            column * _themeData.PieceSize,
            row * _themeData.PieceSize,
            _themeData.PieceSize,
            _themeData.PieceSize
            );
    }

    private static int MakeKey(PieceType type, PieceColor color, InteractState state) =>
        (int)type * 4 + (int)color * 2 + (int)state;

}
