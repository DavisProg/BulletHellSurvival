 using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 1.5f;
    [SerializeField] public float health = 7;
    [SerializeField] protected float separationDistance = 1.5f;
    [SerializeField] protected float separationStrength = 2f;
    [SerializeField] protected float seperationWeight = 0.5f;
    public event Action<Enemy, BaseSpell> onDeath;
    [SerializeField] protected bool isRegisteredForDeath = false;

    public GameObject xp;
    bool canMove = true;
    protected Rigidbody2D rb;
    protected Vector2 moveDirection;
    protected Transform target;
    protected bool facingRight = true;

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
    public void move()
    {
        if (target)
        {
            if (canMove)
            {
                Vector2 separation = getSeperationForce();
                Vector2 finalDirection = moveDirection + separation * seperationWeight;
                rb.linearVelocity = finalDirection.normalized * speed;
                tryFlip();
            }
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
    public void getDirection()
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
        move();
    }
    private void OnDrawGizmosSelected()
{
    Gizmos.DrawWireSphere(transform.position, separationDistance);
}
    private Vector2 getSeperationForce()
    {
        Vector2 separation = Vector2.zero;

        Collider2D[] overlappingEnemies = new Collider2D[10];
        int overlappingEnemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, separationDistance, overlappingEnemies, 1 << gameObject.layer);

        for(int i = 0; i < overlappingEnemyCount; i++)
        {
            Collider2D col = overlappingEnemies[i];
            if (col.gameObject == gameObject)
            {
                continue;
            }
            Vector2 away = transform.position - col.transform.position;
            if (away.magnitude > 0)
            {
                separation += away.normalized / away.magnitude;
            }
        }
        return separation * separationStrength;
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
}
