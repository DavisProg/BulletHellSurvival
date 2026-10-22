using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public int nextLevelUp = 3;
    public int maxHealth = 3;
    public int health = 3;
    [SerializeField] GameObject[] healthSlots;
    [SerializeField] Sprite emptyHealth, fullHealth;
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
    public void increaseXPRequirement(float percentage, int startLevel = 1)
    {
        for(int i = startLevel - 1; i < levelRequirements.Length; i++)
        {
            levelRequirements[i] = (int)(levelRequirements[i] * percentage);
        }
    }
    public void takeDamage(int damage)
    {
        Debug.Log("Hello");
        if (canDamage)
        {
            PlayerDamaged?.Invoke();
            health -= damage;
            displayHealth();
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
        displayHealth();
        nextLevelUp = levelRequirements[level - 1];
        GetComponent<PlayerDamageKnockback>().enable();
        GetComponent<MagicMissile>().enable();
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
    void displayHealth()
    {
        for(int i = 0; i < healthSlots.Count(); i++)
        {
            if(health >= i + 1)
            { 
                healthSlots[i].GetComponent<Image>().sprite = fullHealth;
            }
            else
            {
                healthSlots[i].GetComponent<Image>().sprite = emptyHealth;
            }
            RectTransform rect = healthSlots[i].GetComponent<Image>().rectTransform;
            rect.pivot = new Vector2(0.5f, 0f);
            healthSlots[i].GetComponent<Image>().SetNativeSize();
            
        }
    }
}
