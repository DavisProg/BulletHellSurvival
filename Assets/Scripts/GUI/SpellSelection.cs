using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public List<BaseSpell> availableSpells;
    Player playerScript;
    BaseSpell[] chosenSpellList;
    [SerializeField] float maxSpellRefundPercentage;
    [SerializeField] Button[] buttons;
    private bool btn1Enabled = true;
    private bool btn2Enabled;
    private bool btn3Enabled;
    void Start()
    {
        chosenSpellList = new BaseSpell[]{
            availableSpells[Random.Range(0, availableSpells.Count)],
            availableSpells[Random.Range(0, availableSpells.Count)],
            availableSpells[Random.Range(0, availableSpells.Count)]
        };
        playerScript = gameObject.GetComponent<Player>();
    }

    public void showSpellSelectionMenu()
    {
        initMenu();
        for(int i = 0; i < buttons.Length; i++)
        {
            Image spellIcon = buttons[i].gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>();
            TMP_Text titleText = buttons[i].gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
            TMP_Text descriptionText = buttons[i].gameObject.transform.GetChild(4).GetComponent<TMP_Text>();
            if (playerScript.learntSpells.Count >= 8)
            {
                spellSelectionMenu.transform.Find($"Canvas/Choice{1}/Title").GetComponent<TMP_Text>().text = "Refund";
                spellSelectionMenu.transform.Find($"Canvas/Choice{2}/Title").GetComponent<TMP_Text>().text = "Refund";
                spellSelectionMenu.transform.Find($"Canvas/Choice{3}/Title").GetComponent<TMP_Text>().text = "Refund";
                foreach(Button button in buttons)
                {
                    Image refundIcon = button.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>();
                    TMP_Text refundTitleText = button.gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
                    TMP_Text refundDescriptionText = button.gameObject.transform.GetChild(4).GetComponent<TMP_Text>();

                    refundTitleText.text = "Refund";
                    refundDescriptionText.text = "Regain " + maxSpellRefundPercentage + "% XP"; 

                    button.onClick.AddListener(() =>
                {
                    Refund(maxSpellRefundPercentage);
                });
                }
                break;
            }
            int randomSpell = Random.Range(0, availableSpells.Count);
            BaseSpell chosenSpell = availableSpells[randomSpell];
            
            if (chosenSpell.getLevel() == 2)
            {
                availableSpells.Remove(chosenSpell);
                i--;
                continue;
            }
            if (availableSpells.Count < 2){}
            else if (i > 1 && (chosenSpell == chosenSpellList[0] || chosenSpell == chosenSpellList[1]))
            {
                i--;
                continue;
            }

            Debug.Log(chosenSpellList.Length + " " + availableSpells.Count);
            // Empty buttons when not enough spells
            if (availableSpells.Count < 2)
            {
                if (availableSpells.Count == 2 && i == 2)
                {
                    titleText.text = "";
                }
                else if (availableSpells.Count == 1 && i == 1)
                {
                    titleText.text = "";
                }
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
                chosenSpellList[i] = chosenSpell;

                titleText.text = availableSpells[randomSpell].spellName;
                descriptionText.text = availableSpells[randomSpell].spellDescription;
                if (availableSpells[randomSpell].spellIcon)
                {
                    spellIcon.sprite = availableSpells[randomSpell].spellIcon;
                }
            }
            
        }
        if (btn1Enabled && chosenSpellList[0])
        {
            buttons[0].onClick.AddListener(() =>
            {
                Select(chosenSpellList[0]);
            });
        }
        if (btn2Enabled && chosenSpellList[1])
        {
            buttons[1].onClick.AddListener(() =>
            {
                Select(chosenSpellList[1]);
            });
        }
        if (btn3Enabled && chosenSpellList[2])
        {
            buttons[2].onClick.AddListener(() =>
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
    void Refund(float percentage)
    {
        int totalXp;
        if(playerScript.levelRequirements.Length <= playerScript.level)
        {
            totalXp = playerScript.levelRequirements[playerScript.levelRequirements.Length - 1];
        }
        else
        {
            totalXp = playerScript.levelRequirements[playerScript.level - 1];
        }
        playerScript.xp += (int)(totalXp * (percentage / 100));
        Time.timeScale = 1;
        spellSelectionMenu.SetActive(false);
    }
    void initMenu()
    {
        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();
        buttons[2].onClick.RemoveAllListeners();
        btn1Enabled = true;
        btn2Enabled = true;
        btn3Enabled = true;
        Time.timeScale = 0;
        spellSelectionMenu.SetActive(true);
    }
}
