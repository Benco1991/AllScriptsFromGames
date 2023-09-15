using UnityEngine;


public class GreenWall : ChangingWalls
{
    protected override void Changing(bool enable, int color, RedWall redWall, EnumWall enumWall)
    {
        base.Changing(enable, color, redWall, enumWall);
        StartCoroutine(TimeToChangeWall(1f, redWall));
    }
}
