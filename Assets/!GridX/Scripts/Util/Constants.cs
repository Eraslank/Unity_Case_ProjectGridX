using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESide
{
    NONE = -1,

    Up,
    Right,
    Down,
    Left,

    COUNT
}

public static class ESideExtensions
{
    public static Vector2Int GetV2Int(this ESide i) => i switch
    {
        ESide.Up => Vector2Int.up,
        ESide.Right => Vector2Int.right,
        ESide.Down => Vector2Int.down,
        ESide.Left => Vector2Int.left,

        _ => default,
    };
    public static ESide FromInt(int i) => (ESide)i;
    public static int Int(this ESide i) => (int)i;
}

public static class GameUtil
{
    public static bool InEditor //True if in editor
    {
        get { return !Application.isPlaying; }
    }
}