using System;
using System.Collections;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class Splash : MonoBehaviour
{
    IEffect[] effects;
    Collider2D col;
    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    public void Init(float size, IEffect[] effects, float duration){
        this.effects = effects;
        
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
    IEnumerator Stay(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
