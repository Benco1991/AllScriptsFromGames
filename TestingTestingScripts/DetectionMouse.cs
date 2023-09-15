using UnityEngine;

public class DetectionMouse : MonoBehaviour
{
    private CircleCollider2D _col;
    public CircleCollider2D Col { get { return _col; } set { _col = value; } }
    private void Awake()
    {
        _col = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Vulnerable"))
        {
            collision.gameObject.layer = LayerMask.NameToLayer("Player");
            Debug.Log("ChangedToPlayer");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.layer = LayerMask.NameToLayer("Vulnerable");
            Debug.Log("ChangedToVulnerable");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
