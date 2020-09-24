using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores data about the game and player points between scene loads. 
 */

public static class GameData
{
    private static uint p1Points, p2Points;
    private static string nextScene;
    private static GameObject canvas;

    public static uint P1Points {
        get { return p1Points; }
        set { p1Points = value; }
    }

    public static uint P2Points {
        get { return p2Points; }
        set { p2Points = value; }
    }

    public static string NextScene {
        get { return nextScene; }
        set { nextScene = value; }
    }

    public static GameObject Canvas {
        get { return canvas; }
        set { canvas = value; }
    }
}
