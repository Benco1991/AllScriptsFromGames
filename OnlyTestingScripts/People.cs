using UnityEngine;

public class People : MonoBehaviour
{
    public Heaven heaven { get; private set; }
    public Hell hell { get; private set; }

    public Movement movement { get; private set; }
    public GameManager gameManager { get; private set; }
    public int point { get; private set; }


    private void Awake()
    {
        heaven = GetComponent<Heaven>();
        hell = GetComponent<Hell>();
        movement = GetComponent<Movement>();
        gameManager = GetComponent<GameManager>();
    }

    void Start()
    {
        Choice();
    }

    private void Choice()
    {
        if (Random.value < 0.3f) { heaven.enabled = true; } else { hell.enabled = true; }
    }
}
