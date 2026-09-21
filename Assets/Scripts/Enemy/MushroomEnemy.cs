using UnityEngine;

public class MushroomEnemy : Enemy
{
    [SerializeField] float detectionRange;
    [SerializeField] GameObject enemyProjectile;
    private bool inGround = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        getDirection();
    }
    void FixedUpdate()
    {
        if (!inGround)
        {
            move();
            checkForPlayer();
        } 
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inGround)
            {
                inGround = false;
            }
        }
    }
    void checkForPlayer()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Debug.Log("Distance: " + distance);
        if (distance <= detectionRange)
        {
            inGround = true;
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
}
