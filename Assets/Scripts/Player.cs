using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public int nextLevelUp = 3;
    public int maxHealth = 10;
    public int health = 9;
    private float iFramesTime = 0.5f;

    private bool canDamage = true;
    public List<BaseSpell> learntSpells = new List<BaseSpell>();
    [SerializeField] private GameOverScreenManager gameOverScreen;

    public int xp = 0;
    public int level = 1;
    public int[] levelRequirements;
    private Rigidbody2D rb;
    public Vector2 input;
    public event Action PlayerDamaged;
    private bool facingRight = true;
    [SerializeField] Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void increaseXP(int incXP)
    {
        xp = xp + incXP;
        levelUp();
        Debug.Log(xp);
    }
    public void levelUp()
    {
        if (xp >= nextLevelUp)
        {
            Debug.Log("Level Up");
            xp = xp - nextLevelUp;
            level++;
            if(level <= levelRequirements.Length)
            {
                nextLevelUp = levelRequirements[level - 1];
            }
            GetComponent<SpellSelection>().showSpellSelectionMenu();
        }
    }
    public void takeDamage(int damage)
    {
        Debug.Log("Hello");
        if (canDamage)
        {
            PlayerDamaged?.Invoke();
            Debug.Log("Hi");
            health -= damage;
            canDamage = false;
            //GetComponent<Pulse>().quickCast();
            camera.GetComponent<CameraShake>().startCameraShake();
            StartCoroutine(IFrames(iFramesTime));
            if (health <= 0)
            {
                die();
            }
        }
    }
    private void die()
    {
        gameOverScreen.initGameOverScreen();
    }

    public IEnumerator IFrames(float time)
    {
        yield return new WaitForSeconds(time);
        canDamage = true;
    }
    void Start()
    {
        nextLevelUp = levelRequirements[level - 1];
        GetComponent<PlayerDamageKnockback>().enable();
        //GetComponent<Dash>().enable();
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
