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
    private Vector2 input;

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
        if (canDamage)
        {
            health -= damage;
            canDamage = false;
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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<MagicOrb>().enable();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
