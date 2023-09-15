using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public GameManager manager;
    Rigidbody2D rb;
    private CircleCollider2D _col;
    public CircleCollider2D Col { get { return _col; } set { _col = value; } }
    public bool mouseDetect;
    public float moveSpeed = 40.0f;
    public GameObject particles;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        if (manager != null)
            Debug.Log("Detect");

    }
    void Update()
    {
        if (mouseDetect)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        }

    }
    private void OnMouseEnter()
    {
        if (manager.ActualLVL == 0) { mouseDetect = true; manager.StartTiming(); }
        else if (manager.ActualLVL == 1)
        {
            mouseDetect = true; 
            DetectionMouse[] detection = FindObjectsOfType<DetectionMouse>();
            for(int i = 0;i < detection.Length;i++ )
                detection[i].Col.enabled = true;
            //{ foreach (DetectionMouse found in detection) found.Col.enabled = true; }
        }
        else if (manager.ActualLVL == 2) { mouseDetect = true; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            manager.WaitToRestart();
            Debug.Log("Platform");
        }
    }

    /*public void SpawnParticles()
    {
        GameObject particle = Instantiate(particles, transform.position, Quaternion.identity);
        particle.transform.parent = gameObject.transform;
        Destroy(particle, 1);
    }*/
}
