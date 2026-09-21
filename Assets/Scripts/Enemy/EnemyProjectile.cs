using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float speed;
    Vector2 direction;
    Rigidbody2D rb;
    Renderer m_Renderer;
    Collider2D col;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (collision.TryGetComponent(out Player component)){
                component.takeDamage(1);
                Destroy(gameObject);
	        }
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<Renderer>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }
    public void Init(Vector2 target, Transform caster, float speed, float size)
    {
        this.speed = speed;
        Vector2 casterPosition = caster.position;
        direction = (target - casterPosition).normalized;

        gameObject.transform.localScale = new Vector3(size, size, 1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        col.enabled = true;
    }
    void FixedUpdate()
    {
        /*
        Debug.Log(
        "Damage: " + damage + 
        "Speed: " + speed + 
        "Direction: " + direction);
        */
        rb.linearVelocity = direction * speed;
        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
