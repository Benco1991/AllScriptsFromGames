using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static readonly Dictionary<NumberOfLVL, Vector2[]> CollectionsPos = new Dictionary<NumberOfLVL, Vector2[]>()
    {
        { NumberOfLVL.One, new Vector2[] { new Vector2(-24f, 11f), new Vector2(24.5f, 11f), new Vector2(24.5f, -11.3f), new Vector2(-24f, -11f) } },
        { NumberOfLVL.Two, new Vector2[] { new Vector2(-18.8f, 12.2f), new Vector2(-27.9f, 2.5f), new Vector2(-18.8f, -6.4f), new Vector2(-18.7f, 2.6f), 
            new Vector2(-0.1f, 9.7f), new Vector2(0, -9.1f), new Vector2(18.7f, 7f), new Vector2(28.5f, -2.6f), new Vector2(18.7f, -11.7f), new Vector2(18.7f, -2.6f), 
            new Vector2(-27.7f, -14.6f), new Vector2(28.3f, 14.5f) } },
    };
    //0 = red; 1 = green;
    public static readonly Color[] WallColor = new Color[] {
        new Color(1, 0.03911422f, 0, 1), new Color(0.08362639f, 1, 0, 1)
    };

    public static readonly Vector2[] OK = new Vector2[] {
        new Vector2(-14.70207f, 1.287081f), new Vector2(1.177931f, -1.412919f), new Vector2(-6.802069f, -8.412918f), new Vector2(12.56793f, 8.067081f),new Vector2(-10.61207f, -4.022919f), new Vector2(8.967931f, 4.957082f), new Vector2(-2.662069f, -4.932919f), new Vector2(5.127931f, 1.557081f)
    };
    public static readonly Vector2[] Smiley = new Vector2[] {
        new Vector2(-16.65f, -3.95f), new Vector2(9.79f, -4.01f), new Vector2(-12.63f, -7.7f), new Vector2(5.09f, -7.68f),new Vector2(-10.32f, 8.1f), new Vector2(2.96f, 8.25f), new Vector2(-7.15f, -9.5f), new Vector2(-0.7f, -9.46f)
    };
}
