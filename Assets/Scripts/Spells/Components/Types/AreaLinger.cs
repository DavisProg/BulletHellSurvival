using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AreaLinger : MonoBehaviour
{
    IEffect[] effects;
    Collider2D col;
    float interval;
    bool stationary;
    Rigidbody2D rb;
    Vector2 direction;
    float speed;
    Dictionary<Collider2D, float> timers = new Dictionary<Collider2D, float>();
    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    public void Init(float size, IEffect[] effects, float duration, float interval, bool stationary = true, float speed = 0f, Transform caster = default, Vector2 target = default){
        this.effects = effects;
        this.interval = interval;
        this.stationary = stationary;
        this.speed = speed;
        rb = GetComponent<Rigidbody2D>();
        if (!stationary)
        {
            Vector2 playerLocation = caster.position;
            direction = (target - playerLocation).normalized;
        }
        float scale = size * 2f;
        gameObject.transform.localScale = new Vector3(scale, scale, 1f);

        col.enabled = true;
        StartCoroutine(Stay(duration));
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy")){
		    if (collision.TryGetComponent(out Enemy enemy)){
                timers[collision] = interval;
                foreach (var eff in effects)
                {
                    eff.Apply(collision.gameObject, gameObject);
                }
            }
        }   
    }
    void OnTriggerExit2D(Collider2D collision){
        if (collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            timers.Remove(collision);
        }
    }
    void Update()
    {
        List<Collider2D> enemies = new List<Collider2D>(timers.Keys);
        foreach(Collider2D enemy in enemies)
        {
            if (!enemy)
            {
                timers.Remove(enemy);
                continue;
            }
            timers[enemy] -= Time.deltaTime;
            if(timers[enemy] <= 0)
            {
                foreach(var eff in effects)
                {
                    eff.Apply(enemy.gameObject, gameObject);
                }
                timers[enemy] = interval;
            }
        }
    }
    void FixedUpdate()
    {
        if (!stationary)
        {
            rb.linearVelocity = direction * speed;
        }
    }
    IEnumerator Stay(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
