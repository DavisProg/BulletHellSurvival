using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    Rigidbody2D rb;
    Enemy enemy;
    [SerializeField] AnimationCurve curve;
    public void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void takeKnockback(float intensity, float delay, Vector2 direction)
    {
        enemy.disableMovement();
        StopAllCoroutines();
        StartCoroutine(Knockback(intensity, delay, direction));
    }
    IEnumerator Knockback(float intensity, float delay, Vector2 direction)
    {
        float elapsedTime = 0;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / delay);
            rb.linearVelocity = direction * intensity * strength;
            yield return null;
        }
        StartCoroutine(Freeze(0.6f));
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
