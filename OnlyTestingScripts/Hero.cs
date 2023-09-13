using UnityEngine;

public class Hero : MonoBehaviour
{
    public Collider2D col { get; private set; }
    public Movement movement { get; private set; }
    public GameManager manager { get; private set; }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up, false);
            manager.click.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down, false);
            manager.click.Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left, false);
            manager.click.Play();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right, false);
            manager.click.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            movement.WallHitDirection(movement.direction);
        }
        if (collision.gameObject.GetComponent<PeopleBehaviour>() != null)
        {
            PeopleBehaviour people = collision.gameObject.GetComponent<PeopleBehaviour>();
            if (people != null && people.hell.enabled && people.hell.CanChangeTarget())
            {
                manager.RotCourutine();
                people.movement.SetDirection(people.movement.direction, true);
                people.hell.enabled = false;
            }
        }

    }
}
