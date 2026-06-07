using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.5f;
    public float health = 7;

    public GameObject xp;
    bool canMove = true;
    Rigidbody2D rb;
    Vector2 moveDirection;
    Transform target;

    void die()
    {
        Instantiate(xp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }
    public void takeKnockback(float strength, float delay, Vector2 direction)
    {
        canMove = false;
        StopCoroutine(Reset(delay));
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset(delay));
    }
    private IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
           Vector3 direction = (target.position - transform.position).normalized; 
           moveDirection = direction;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<Player>().takeDamage(1);
        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            if (canMove)
            {
                rb.linearVelocity = moveDirection * speed;
            }
        }
    }
}
