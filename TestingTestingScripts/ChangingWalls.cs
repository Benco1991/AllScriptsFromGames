using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public enum TypeOfWall
{
    Static, Dynamic
}
public class ChangingWalls : MonoBehaviour
{
    protected enum EnumWall
    {
        Red, Green
    }
    protected EnumWall enumWall;
    public TypeOfWall typeOfWall;
    protected BoxCollider2D col;
    protected Light2D light2D;
    //public LayerMask wall;
    public GreenWall greenWall { get; private set; }
    public RedWall redWall { get; private set; }


    protected void Awake()
    {
        //typeOfWall = TypeOfWall.Static;
        col = GetComponent<BoxCollider2D>();
        light2D = GetComponent<Light2D>();

    }
    protected void Start()
    {
        greenWall = GetComponent<GreenWall>();
        redWall = GetComponent<RedWall>();
        DedictionTypeOfWall();
    }
    private void DedictionTypeOfWall()
    { //cant detect collider - i dont know why... //Maybe I was doing it wrong with Collider - NOT Collider2D
        //too late testing again - its not enough time, but ---> experience++
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.01f, 0f, Vector2.zero, 0, wall);
        //if (hit.collider != null)
        //Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, wall);
        //foreach (Collider hit in hitColliders)
        //{
        //if(hit != null)
        //{
        typeOfWall = TypeOfWall.Dynamic;
        redWall.Changing(true, 0, redWall, enumWall = EnumWall.Red);
        //}

        //}

        //Debug.Log("po toto to funguje");
        //potomkova metoda na zmenu green a red

    }
    protected virtual void Changing(bool turnOn, int color, RedWall redWall, EnumWall enumWall)//true, 0 = red; false, 1 = green
    {
        //Debug.Log("Change");
        //toto je base
        Color newColor = StaticData.WallColor[color];
        light2D.color = newColor;
        col.enabled = turnOn;
        this.enumWall = enumWall;
    }
    protected IEnumerator TimeToChangeWall(float time, RedWall redWall)
    {
        yield return new WaitForSeconds(time);//0.15f
        if (redWall != null)
        {
            this.redWall.enabled = false;
            this.greenWall.enabled = true;
            this.greenWall.Changing(false, 1, null, enumWall = EnumWall.Green);

        }
        else
        {
            this.greenWall.enabled = false;
            this.redWall.enabled = true;
            this.redWall.Changing(true, 0, this.redWall, enumWall = EnumWall.Red);
        }
    }

}
