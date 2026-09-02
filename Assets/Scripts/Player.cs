using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float nextLevelUp = 3;
    public float maxHealth = 10;
    public float health = 9;
    private float iFramesTime = 0.5f;

    private bool canDamage = true;

    public int xp = 0;
    private Rigidbody2D rb;
    public Vector2 input;
    public event Action PlayerDamaged;
    private bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void increaseXP(int incXP)
    {
        xp = xp + incXP;
        levelUp();
        Debug.Log(xp);
    }
    public void levelUp()
    {
        if (xp == nextLevelUp)
        {
            Debug.Log("Level Up");
            xp = 0;
            GetComponent<SpellSelection>().showSpellSelectionMenu();
        }
        else
        {
            Debug.Log("Cunt");
        }
    }
    public void takeDamage(float damage)
    {
        Debug.Log("Hello");
        if (canDamage)
        {
            PlayerDamaged?.Invoke();
            Debug.Log("Hi");
            health -= damage;
            canDamage = false;
            //GetComponent<Pulse>().quickCast();
            StartCoroutine(IFrames(iFramesTime));
        }
    }

    public IEnumerator IFrames(float time)
    {
        yield return new WaitForSeconds(time);
        canDamage = true;
    }
    void Start()
    {
        GetComponent<Pulse>().enable();
        GetComponent<Dash>().enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x == -1 && facingRight)
        {
            facingRight = !facingRight;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (input.x == 1 && !facingRight)
        {
            facingRight = !facingRight;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        input.Normalize();

        if (Input.GetKeyDown(KeyCode.P))
        {
            increaseXP(3);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
