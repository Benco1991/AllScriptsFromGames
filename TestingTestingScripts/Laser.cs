using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private float lerpDuration = 2.5f;
    private float lerpTime = 0.0f;

    BoxCollider2D col;
    GameManager manager;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        manager = FindObjectOfType<GameManager>();
    }

    public void StartLaserMovement()
    {
        StartCoroutine(LerpPosition());
    }
    IEnumerator LerpPosition()
    {
        startPos = new Vector3(-8.184f, -15f); endPos = new Vector3(-8.184f, 16.9f);
        lerpTime = 0.0f;

        while (lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;

            float t = lerpTime / lerpDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.position = endPos;
        lerpTime = 0.0f;
        startPos = new Vector3(-8.184f, 16.9f); endPos = new Vector3(-8.184f, -15f);

        while (lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;

            float t = lerpTime / lerpDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        transform.position = endPos;
        lerpTime = 0.0f;
        yield return StartCoroutine(LerpPosition());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Vulnerable"))
        {
            manager.WaitToRestart();
            Debug.Log("Laser");
        }
    }

}
