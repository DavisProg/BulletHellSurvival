using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    int pierce;
    float speed;
    IEffect[] effects;
    Vector2 direction;
    Rigidbody2D rb;
    Collider2D col;
    float maxLifeTime = 5;
    [SerializeField] private float immunityTime = 0.01f;
    private bool canCollide = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy")){
            if (collision.TryGetComponent(out Enemy component)){
                if (canCollide)
                {
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
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    public void Init(Vector2 target, Transform caster, float speed, int pierce, float size, float maxLifeTime, IEffect[] effects)
    {
        this.speed = speed;
        this.pierce = pierce;
        this.effects = effects;
        this.maxLifeTime = maxLifeTime;

        Vector2 playerLocation = caster.position;
        direction = (target - playerLocation).normalized;

        gameObject.transform.localScale = new Vector3(size, size, 1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        col.enabled = true;
        StartCoroutine(lifeTime());
        StartCoroutine(immunityTimer());
    }
    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }
    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(maxLifeTime);
        Destroy(gameObject);
    }
    IEnumerator immunityTimer()
    {
        yield return new WaitForSeconds(immunityTime);
        canCollide = true;
    }
}
