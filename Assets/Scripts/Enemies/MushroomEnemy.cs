using UnityEngine;

public class MushroomEnemy : Enemy
{
    public LayerMask playerLayer;
    Collider2D[] arr = new Collider2D[1];
    bool chase = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (chase)
        {
            getTarget();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
            animator.SetBool("isWalking", false);
        }
    }
    private void FixedUpdate()
    {
        if(chase == true)
        {
            int entitiesCount = Physics2D.OverlapCircleNonAlloc(transform.position, 7, arr, playerLayer);
            if (entitiesCount != 1)
            {
                move();
            }
            else
            {
                chase = false;
            }
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isWalking", true);
        }
    }
    void startChase()
    {
        chase = true;
    }
}
