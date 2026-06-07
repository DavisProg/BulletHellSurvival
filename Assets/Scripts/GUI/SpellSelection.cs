using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public List<BaseSpell> availableSpells;
    BaseSpell[] chosenSpellList;
    public Button btn1, btn2, btn3;
    private bool btn1Enabled = true;
    private bool btn2Enabled;
    private bool btn3Enabled;
    GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //I forgor, I think I had an error cause the array started off as empty 
        chosenSpellList = new BaseSpell[]{
            availableSpells[Random.Range(0, availableSpells.Count)],
            availableSpells[Random.Range(0, availableSpells.Count)],
            availableSpells[Random.Range(0, availableSpells.Count)]
        };
        Player = GameObject.Find("Player");
    }

    public void showSpellSelectionMenu()
    {
        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn1Enabled = true;
        btn2Enabled = true;
        btn3Enabled = true;
        Time.timeScale = 0;
        spellSelectionMenu.SetActive(true);
        int maxSpellAmount;
        if (availableSpells.Count >= 3)
        {
            maxSpellAmount = 3;
        }
        else
        {
            maxSpellAmount = availableSpells.Count;
        }
        for(int i = 1; i <= maxSpellAmount; i++)
        {
            int randomSpell = Random.Range(0, availableSpells.Count);
            BaseSpell chosenSpell = availableSpells[randomSpell];

            TMP_Text titleText = spellSelectionMenu.transform
            .Find($"Canvas/Choice{i}/Title")
            .GetComponent<TMP_Text>();
            
            if (chosenSpell.getLevel() == 2)
            {
                availableSpells.Remove(chosenSpell);
                i--;
                continue;
            }
            if (availableSpells.Count < 3){}
            else if (i > 1 && (chosenSpell == chosenSpellList[0] || chosenSpell == chosenSpellList[1]))
            {
                i--;
                continue;
            }

            Debug.Log(chosenSpellList.Length + " " + availableSpells.Count);
            if (availableSpells.Count < 3)
            {
                if (availableSpells.Count == 2 && i == 3)
                {
                    titleText.text = "";
                }
                else if (availableSpells.Count == 1 && i == 2)
                {
                    titleText.text = "";
                }
                chosenSpellList[i - 1] = chosenSpell; 
                if(availableSpells.Count == 1)
                {
                    btn2Enabled = false;
                    btn3Enabled = false;
                }
                else if(availableSpells.Count == 2)
                {
                    btn3Enabled = false;
                }
            }
            else
            {
                chosenSpellList[i - 1] = chosenSpell;

                titleText.text = availableSpells[randomSpell].GetType().Name;
            }
            
        }
        if (btn1Enabled)
        {
            btn1.onClick.AddListener(() =>
            {
                Select(chosenSpellList[0]);
            });
        }
        if (btn2Enabled)
        {
            btn2.onClick.AddListener(() =>
            {
                Select(chosenSpellList[1]);
            });
        }
        if (btn3Enabled)
        {
            btn3.onClick.AddListener(() =>
            {
                Select(chosenSpellList[2]);
            });
        }
        
    }
    void Select(BaseSpell chosenSpell)
    {
        if (chosenSpell.getLevel() == 0)
        {
            Time.timeScale = 1;
        }
        chosenSpell.enable();
        spellSelectionMenu.SetActive(false);
    }
}
