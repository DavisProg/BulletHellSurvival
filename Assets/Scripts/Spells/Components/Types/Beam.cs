using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{

    IEffect[] effects;
    Collider2D col;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    public void Init(Vector2 target, Transform caster, float height, float width, float duration, IEffect[] effects)
    {
        this.effects = effects;

        gameObject.transform.localScale = new Vector3(width, height, 1f);

        Vector2 playerLocation = caster.position;
        Vector2 direction = (target - playerLocation).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
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
    IEnumerator Stay(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
