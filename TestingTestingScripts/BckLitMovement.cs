using System.Collections;
using UnityEngine;

public class BckLitMovement : MonoBehaviour
{
    public bool changeLVL;
    float moveSpeed = 10;
    private Vector3 targetPosition;
    private Vector3 targetDirection;
    private Vector3 startPosition;
    public GameObject bck;

    private void Start()
    {
        transform.position = Vector3.zero;
        bck.transform.position = this.transform.position;
    }
    /*public void StartMovement()
    {
        StartCoroutine(MoveToTargetPosition());
    }*/
    private IEnumerator MoveToTargetPosition()
    {
        if (!changeLVL) { targetPosition = new Vector3(70, 0, 0); startPosition = new Vector3(0, 0, 0); targetDirection = Vector3.right; } 
        else { targetPosition = new Vector3(0, 0, 0); startPosition = new Vector3(70, 0, 0); targetDirection = Vector3.left; }

        while (transform.position != targetPosition)
        {
            transform.Translate( targetDirection * moveSpeed);
            yield return null;
        }
        if (transform.position != targetPosition)
            transform.position = targetPosition;
        changeLVL = !changeLVL;
    }
}
