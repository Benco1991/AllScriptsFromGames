using UnityEngine;

public class Targets : MonoBehaviour
{
    public float speed = 10;
    public Vector3 direction = Vector3.up;

    void Update()
    {
        transform.RotateAround(transform.position, direction, speed * Time.deltaTime);
    }

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PeopleBehaviour>() != null)
        {
            PeopleBehaviour people = collision.gameObject.GetComponent<PeopleBehaviour>();

            if (people.heaven.enabled)
            {
                people.heaven.dead = true;
                Destroy(people.gameObject, 0.2f);
                gameManager.KarmaMeter(1);
            }
            else
            {
                gameManager.badKarmaSound.Play();
                people.hell.dead = true;
                Destroy(people.gameObject, 0.2f);
                gameManager.KarmaMeter(-1);
            }
        }
    }
}
