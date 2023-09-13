using System.Collections.Generic;
//test
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject nodeParticle;
    public GameObject nodeParticle2D;

    public LayerMask wallLayer;
    public List<Vector2> availableDirections { get; private set; }

    private void Start()
    {
        float number = Random.Range(0, 5);
        InvokeRepeating(nameof(Spawning), number, 1.5f);

        availableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, wallLayer);
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, wallLayer);
        if (hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }
    public void StartParticles(Node node)
    {
        GameObject particle = Instantiate(nodeParticle, node.transform.position, Quaternion.identity) as GameObject;
        Destroy(particle, 3);
    }
    private void Spawning()
    {
        GameObject particle = Instantiate(nodeParticle2D, transform.position, Quaternion.identity) as GameObject;
        Destroy(particle, 3);
    }
}
