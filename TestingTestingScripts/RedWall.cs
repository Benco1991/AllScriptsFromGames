using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWall : ChangingWalls
{
    public void StartAgain()
    {
        Changing(true, 0, redWall, enumWall = EnumWall.Red);
    }
    protected override void Changing(bool enable, int color, RedWall redWall, EnumWall enumWall)
    {

        base.Changing(enable, color, redWall, enumWall);
        StartCoroutine(TimeToChangeWall(1f, redWall));
        //Debug.Log("po toto to funguje1");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && enumWall == EnumWall.Red)
        {
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager != null) { Debug.Log("Restart"); manager.WaitToRestart(); }
            /*GameManager manager = collision.gameObject.GetComponent<GameManager>();
            if (manager != null) { Debug.Log("Restart"); manager.Restart(); }*/
        }
    }
}
