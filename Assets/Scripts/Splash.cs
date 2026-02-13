using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Splash : MonoBehaviour
{
    float damage;
    float stayTime;
    float damageOverTime;
    float damageOverTimeInterval;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }
    public void Init(float damage, float stayTime, float size)
    {
        this.damage = damage;
        this.stayTime = stayTime;

        float scale = size * 2f;
        gameObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            if (collision.TryGetComponent(out Enemy component)){
		        component.takeDamage(damage);
                StartCoroutine(Linger());
	        }
        }
    }
    IEnumerator Linger()
    {
        yield return new WaitForSeconds(stayTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
