using UnityEngine;
using System.Collections;
public class AreaLinger : MonoBehaviour
{
    IEffect[] effects;
    Collider2D col;
    float interval;
    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    public void Init(float size, IEffect[] effects, float duration, float interval){
        this.effects = effects;
        this.interval = interval;
        
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
                foreach (var eff in effects)
                {
                    eff.Apply(collision.gameObject);
                }
            }
        }   
    }
    IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            yield return null;
        }
        yield return new WaitForSeconds(interval);
        if (collision.gameObject.CompareTag("Enemy")){
		    if (collision.TryGetComponent(out Enemy enemy)){
                foreach (var eff in effects)
                {
                    eff.Apply(collision.gameObject);
                }
            }
        } 
    }
    IEnumerator Stay(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
