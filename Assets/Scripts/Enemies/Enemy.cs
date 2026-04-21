using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.5f;
    public float health = 7;
    public bool facingRight = true;

    public GameObject xp;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public Transform target;
    public Animator animator;

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
    public void move()
    {
        if (target)
        {
            rb.linearVelocity = moveDirection * speed;
            tryFlip();
        }
    }
    public void getTarget()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized; 
            moveDirection = direction;
        }
    }
    public void tryFlip()
    {
        if (target.transform.position.x > transform.position.x && facingRight)
        {
            facingRight = !facingRight;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (target.transform.position.x < transform.position.x && !facingRight)
        {
            facingRight = !facingRight;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<Player>().takeDamage(1);
        }
    }
}
