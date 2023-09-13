using UnityEngine;

public class Heaven : PeopleBehaviour
{
    private void Start()
    {
        RandomNumber();
        spriteRenderer.color = Color.white;
        nodeCount = 0;
    }

    private void OnDisable()
    {
        if (!dead)
        {
            hell.Enable();
            node = null;
            hell.canChange = true;
        }
    }
    private void OnEnable()
    {
        RandomNumber();
        SetNewTarget(node);
        node = null;
        nodeCount = 0;
        hell.canChange = true;
    }
    public override void Enable()
    {
        base.Enable();
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        this.node = node;
        if (node != null && nodeCount > 1 && !dead)
        {
            nodeCount = 0;
            Debug.Log("changeToHell");
            enabled = false;
            node.StartParticles(node);
        }
        else if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (newTarget.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            movement.SetDirection(direction, false);
            nodeCount++;
        }
    }
}
