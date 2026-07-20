using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.5f;
    public float health = 7;
    public event Action<Enemy, BaseSpell> onDeath;
    private bool isRegisteredForDeath = false;

    public GameObject xp;
    bool canMove = true;
    Rigidbody2D rb;
    Vector2 moveDirection;
    Transform target;

    void die(BaseSpell source)
    {
        Instantiate(xp, transform.position, Quaternion.identity);
        Debug.Log("Died from " + source);
        onDeath?.Invoke(this, source);
        Destroy(gameObject);
    }
    public void takeDamage(float damage, BaseSpell source)
    {
        health -= damage;
        Debug.Log("Took damage from " + source);
        if (!isRegisteredForDeath)
        {
            EnemyDeathController.Instance.RegisterEnemyDeathEvent(this);
            isRegisteredForDeath = true;
        }
        if (health <= 0)
        {
            die(source);
        }
    }
    public void disableMovement()
    {
        canMove = false;
    }
    public void enableMovement()
    {
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
