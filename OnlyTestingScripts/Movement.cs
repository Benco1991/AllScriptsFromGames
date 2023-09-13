using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2[] beginningVector = new Vector2[] { 
    Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };
    public float speed = 4f;
    public Vector2 initialDirection { get; private set; }
    public LayerMask wallLayer;

    public Rigidbody2D rb { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        BeginningDirection();
        direction = initialDirection;
        nextDirection = Vector2.zero;
    }
    private void BeginningDirection()
    {
        int i = Random.Range(0, beginningVector.Length);
        initialDirection = beginningVector[i];
    }
    public void ChangeSpeed(float value, bool hero)
    {
        speed = value;
        StartCoroutine(CountSpeed(hero)) ;
    }
    IEnumerator CountSpeed(bool h)
    {
        if(h)
        {
            while (speed > 6)
            {
                yield return new WaitForSeconds(0.5f);
                speed /= 1.1f;
            }
            speed = 6;
        }
        else
        {
            while (speed < 4)
            {
                yield return new WaitForSeconds(0.5f);
                speed *= 1.1f;
            }
            speed = 4f;
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rb.MovePosition(position + translation);
    }

    void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection, false);
        }
    }
    public void WallHitDirection(Vector2 direction)
    { 
        this.direction = direction * -1;
    }
    public void SetDirection(Vector2 direction, bool changeDir)
    {
        if(changeDir)
        {
            this.direction = direction * -1;
        }
        else
        {
            if (!Occupied(direction))
            {
                this.direction = direction;
                nextDirection = Vector2.zero;
            }
            else
            {
                nextDirection = direction;
            }
        }
    }
    public bool Occupied(Vector2 direction)
    {
        //(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, wallLayer)  -  TOP
        //Vector2.one * 0.9 alebo 0.95 a distance je bud 1 alebo 1.5
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, wallLayer);
        return hit.collider != null;
    }

}
