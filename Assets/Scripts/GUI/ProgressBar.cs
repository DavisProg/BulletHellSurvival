using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float max;
    public float current;
    public Image mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("xpBar"))
        {
            current = GameObject.Find("Player").GetComponent<Player>().xp;
            max = GameObject.Find("Player").GetComponent<Player>().nextLevelUp;
        }
        else if (gameObject.CompareTag("healthBar"))
        {
            current = GameObject.Find("Player").GetComponent<Player>().health;
            max = GameObject.Find("Player").GetComponent<Player>().maxHealth;
        }

        getCurrentFill();
    }
    void getCurrentFill()
    {
        float fillAmount = current / max;
        //Debug.Log(fillamount);
        mask.fillAmount = fillAmount;
        //mask.transform.localScale = new Vector3(1, );
    }
}
