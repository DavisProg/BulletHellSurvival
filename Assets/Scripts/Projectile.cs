using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    int pierce;
    float speed;
    IEffect[] effects;
    Vector2 direction;
    Rigidbody2D rb;
    Renderer m_Renderer;
    Collider2D col;
    string enemyTag;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag)){
            foreach(var eff in effects)
            {
               eff.Apply(collision.gameObject);
            }
            pierce -= 1;
            if (pierce <= 0){
                Destroy(gameObject);
            }   
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<Renderer>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    public void Init(Vector2 target, Transform caster, float speed, int pierce, float size, IEffect[] effects, string enemyTag)
    {
        this.speed = speed;
        this.pierce = pierce;
        this.effects = effects;
        this.enemyTag = enemyTag;

        Vector2 playerLocation = caster.position;
        direction = (target - playerLocation).normalized;

        gameObject.transform.localScale = new Vector3(size, size, 1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        col.enabled = true;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
