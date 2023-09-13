using System.Collections;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    public Heaven heaven { get; private set; }
    public Hell hell { get; private set; }

    public Movement movement { get; private set; }
    public GameManager gameManager { get; private set; }
    public int point { get; private set; }
    protected SpriteRenderer spriteRenderer;
    protected Node node;
    protected int nodeCount = 0;
    public Transform[] target;
    protected Transform newTarget;
    public bool dead = false;
    public bool canChange = true;

    private void Awake()
    {
        heaven = GetComponent<Heaven>();
        hell = GetComponent<Hell>();
        movement = GetComponent<Movement>();
        gameManager = GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Choice();
    }

    protected void RandomNumber()
    {
        int random = 0;

        if (Random.value < 0.5f)
        {
            random = 0;
            newTarget = target[random];
        }
        else
        {
            random = 1;
            newTarget = target[random];
        }

    }
    public bool CanChangeTarget()
    {
        return canChange;
    }
    private void Choice()
    {
        if (Random.value < 0.3f) { heaven.enabled = true; } else { hell.enabled = true; }
    }
    protected void SetNewTarget(Node node)
    {
        if (node != null && enabled)
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
        }
    }
    public virtual void Enable()
    {
        enabled = true;
        //RandomNumber();
        //SetNewTarget(node);
        nodeCount = 0;
    }
    protected IEnumerator Timer(float duration, Vector2 direction)
    {
        yield return new WaitForSeconds(duration);
        movement.SetDirection(direction, false);
    }
}
