using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    int pierce;
    float speed;
    IEffect[] effects;
    Vector2 direction;
    Rigidbody2D rb;
    //Renderer m_Renderer;
    Collider2D col;
    float maxLifeTime = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy")){
            if (collision.TryGetComponent(out Enemy component)){
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
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //m_Renderer = GetComponent<Renderer>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    public void Init(Vector2 target, Transform caster, float speed, int pierce, float size, IEffect[] effects)
    {
        this.speed = speed;
        this.pierce = pierce;
        this.effects = effects;

        Vector2 playerLocation = caster.position;
        direction = (target - playerLocation).normalized;

        gameObject.transform.localScale = new Vector3(size, size, 1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        col.enabled = true;
        StartCoroutine(lifeTime());
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
        /*
        if (!m_Renderer.isVisible)
        {
            Destroy(gameObject);
        }
        */
    }
    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(maxLifeTime);
        Destroy(gameObject);
    }
}
