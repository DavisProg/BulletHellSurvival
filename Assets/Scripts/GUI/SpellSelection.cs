using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public List<BaseSpell> availableSpells;
    private List<BaseSpell> spellsTempList = new List<BaseSpell>();
    Player playerScript;
    [SerializeField] float maxSpellRefundPercentage;
    [SerializeField] Button[] buttons;
    void Start()
    {
        /*
        chosenSpellList = new BaseSpell[]{
            spellsTempList[Random.Range(0, spellsTempList.Count)],
            spellsTempList[Random.Range(0, spellsTempList.Count)],
            spellsTempList[Random.Range(0, spellsTempList.Count)]
        };
        */
        playerScript = gameObject.GetComponent<Player>();
    }

    public void showSpellSelectionMenu()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        spellsTempList.Clear();
        foreach(BaseSpell spell in availableSpells)
        {
            if (spell.getLevel() < 2)
            {
                spellsTempList.Add(spell);
            }
        }

        initMenu();
        for (int i = 0; i < buttons.Length; i++)
        {
            Image spellIcon = buttons[i].gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>();
            TMP_Text titleText = buttons[i].gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
            TMP_Text descriptionText = buttons[i].gameObject.transform.GetChild(4).GetComponent<TMP_Text>();
            spellIcon.sprite = null;
            titleText.text = "";
            descriptionText.text = "";

            if (playerScript.learntSpells.Count >= 8 || spellsTempList.Count == 0)
            {
                SetRefundButton(buttons[i], titleText, descriptionText);
                continue;
            }

            int randomSpell = Random.Range(0, spellsTempList.Count);
            BaseSpell chosenSpell = spellsTempList[randomSpell];
            spellsTempList.Remove(chosenSpell);

            titleText.text = chosenSpell.spellName;
            descriptionText.text = chosenSpell.spellDescription;
            spellIcon.sprite = chosenSpell.spellIcon;
            buttons[i].onClick.AddListener(() =>
            {
                Select(chosenSpell);
            });
        }
    }

    void SetRefundButton(Button button, TMP_Text titleText, TMP_Text descriptionText)
    {
        titleText.text = "Refund";
        descriptionText.text = "Regain " + maxSpellRefundPercentage + "% XP";
        button.onClick.AddListener(() => Refund(maxSpellRefundPercentage));
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
        Time.timeScale = 0;
        spellSelectionMenu.SetActive(true);
    }
}
