using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb;
    Enemy enemy;
    public void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void takeKnockback(float strength, float delay, Vector2 direction)
    {
        enemy.disableMovement();
        StopAllCoroutines();
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset(delay));
    }
    private IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Freeze(0.4f));
    }
    private IEnumerator Freeze(float duration)
    {
        rb.linearVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        enemy.enableMovement();
    }
    void Update()
    {
        
    }
}
