using UnityEngine;

public class NormalEnemy : Enemy
{
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
        move();
    }
}
