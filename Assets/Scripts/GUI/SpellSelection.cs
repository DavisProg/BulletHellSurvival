using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public List<BaseSpell> availableSpells;
    BaseSpell[] chosenSpellList;
    public Button btn1, btn2, btn3;
    private bool btn1Enabled;
    private bool btn2Enabled;
    private bool btn3Enabled;
    GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Time.timeScale = 0;
        spellSelectionMenu.SetActive(true);
        for(int i = 1; i <= 3; i++)
        {
            int randomSpell = Random.Range(0, availableSpells.Count);
            BaseSpell chosenSpell = availableSpells[randomSpell];
            if (chosenSpell.getLevel() == 2)
            {
                availableSpells.Remove(chosenSpell);
                i--;
                continue;
            }
            if (i > 1 && (chosenSpell == chosenSpellList[0] || chosenSpell == chosenSpellList[1]))
            {
                i--;
                continue;
            }
            TMP_Text titleText = spellSelectionMenu.transform
            .Find($"Canvas/Choice{i}/Title")
            .GetComponent<TMP_Text>();

            if (chosenSpellList.Length == availableSpells.Count)
            {
                chosenSpellList[i - 1] = chosenSpell; 
                titleText.text = "";
                if(i == 2)
                {
                    btn2Enabled = false;
                }
                else if(i == 3)
                {
                    btn3Enabled = false;
                }
            }
            else
            {
                chosenSpellList[i - 1] = chosenSpell;

                titleText.text = availableSpells[randomSpell].GetType().Name;
                btn1Enabled = true;
                if(i == 2)
                {
                    btn2Enabled = true;
                }
                else if(i == 3)
                {
                    btn3Enabled = true;
                }
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
