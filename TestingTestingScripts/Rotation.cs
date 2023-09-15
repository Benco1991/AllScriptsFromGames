using UnityEngine;

public class Rot : MonoBehaviour
{
    public float rotationSpeed = 30f;

    private void Update()
    {

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}