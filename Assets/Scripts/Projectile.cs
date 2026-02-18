using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage;
    float speed;
    float damageOverTime;
    float damageOverTimeInterval;
    float damagePerInterval;
    int pierce;
    bool slow;
    Vector2 direction;
    Rigidbody2D rb;
    Renderer m_Renderer;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            if (collision.TryGetComponent(out Enemy component)){
		        component.takeDamage(damage);
                if (damageOverTime > 0)
                {
                    Debug.Log("Hello");
                    component.StartCoroutine(component.takeDamageOverTime(damageOverTime, damageOverTimeInterval, damagePerInterval));
                }
                pierce -= 1;
                if (pierce <= 0){
                    Destroy(gameObject);
                }   
	        }
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<Renderer>();
        
    }
    public void Init(Vector2 dir, float damage, float speed, int pierce, float damageOverTime, float damageOverTimeInterval, float damagePerInterval, bool slow)
    {
        direction = dir.normalized;
        this.damage = damage;
        this.speed = speed;
        this.pierce = pierce;
        this.damageOverTime = damageOverTime;
        this.damageOverTimeInterval = damageOverTimeInterval;
        this.damagePerInterval = damagePerInterval;
        this.slow = slow;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
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
