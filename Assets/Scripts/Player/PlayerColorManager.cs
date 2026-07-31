using System;
using UnityEngine;

public class PlayerColorManager: MonoBehaviour
{
    [SerializeField]
    private Color[] _colors = new Color[]
    {
        Color.white, Color.red, Color.blue, Color.magenta, Color.green, Color.yellow, Color.cyan, Color.white,
    };
    public Color GetColor(PlayerColor color) { return _colors[(int)color]; }
}


[Flags]
public enum PlayerColor : uint
{
    None = 0,
    Red = 1 << 0,//1
    Blue = 1 << 1,//2
    Green = 1 << 2,//4

    //混色
    Magenta = Red | Blue,//3
    Yellow = Red | Green,//5
    Cyan = Blue | Green,//6
    White = Red | Blue | Green,//7

    Black = 1 << 30,
}

