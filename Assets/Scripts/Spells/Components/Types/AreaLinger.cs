using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AreaLinger : MonoBehaviour
{
    IEffect[] effects;
    Collider2D col;
    float interval;
    bool stationary;
    float duration;
    Rigidbody2D rb;
    Vector2 direction;
    float speed;
    Animator animator;
    Dictionary<Collider2D, float> timers = new Dictionary<Collider2D, float>();
    [SerializeField] AudioClip[] soundFX;
    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        if(soundFX.Length > 0)
        {
            SoundFXManager.instance.playSoundEffect(soundFX, transform, 1f);
        }
    }

    public void Init(float size, IEffect[] effects, float duration, float interval, bool stationary = true, float speed = 0f, Transform caster = default, Vector2 target = default){
        this.effects = effects;
        this.interval = interval;
        this.stationary = stationary;
        this.speed = speed;
        this.duration = duration;
        rb = GetComponent<Rigidbody2D>();
        if (!stationary)
        {
            Vector2 playerLocation = caster.position;
            direction = (target - playerLocation).normalized;
        }
        float scale = size * 2f;
        gameObject.transform.localScale = new Vector3(scale, scale, 1f);
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
        if (col.enabled == false)
        {
            float animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if(animTime >= 1.0f)
            {
                Debug.Log("Area linger animation finished");
                col.enabled = true;
                StartCoroutine(Stay(duration));
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
        col.enabled = false;
        animator.SetBool("isLeavingScene", true);
        yield return null;
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
