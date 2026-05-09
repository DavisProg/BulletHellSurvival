using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public BaseSpell[] availableSpells;
    BaseSpell[] chosenSpellList;
    public Button btn1, btn2, btn3;
    GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chosenSpellList = new BaseSpell[]{
            availableSpells[Random.Range(0, availableSpells.Length)],
            availableSpells[Random.Range(0, availableSpells.Length)],
            availableSpells[Random.Range(0, availableSpells.Length)]
        };
        Player = GameObject.Find("Player");
    }

    public void showSpellSelectionMenu()
    {
        Time.timeScale = 0;
        spellSelectionMenu.SetActive(true);
        for(int i = 1; i <= 3; i++)
        {
            int randomSpell = Random.Range(0, availableSpells.Length);
            BaseSpell chosenSpell = availableSpells[randomSpell];
            if (i > 1 && chosenSpell == chosenSpellList[0] || chosenSpell == chosenSpellList[1])
            {
                i--;
                continue;
            }
            chosenSpellList[i - 1] = chosenSpell;
            TMP_Text titleText = spellSelectionMenu.transform
            .Find($"Canvas/Choice{i}/Title")
            .GetComponent<TMP_Text>();

            titleText.text = availableSpells[randomSpell].GetType().Name;
        }
        btn1.onClick.AddListener(() =>
            {
                Select(chosenSpellList[0]);
            });
        btn2.onClick.AddListener(() =>
            {
                Select(chosenSpellList[1]);
            });
        btn3.onClick.AddListener(() =>
            {
                Select(chosenSpellList[2]);
            });
    }
    void Select(BaseSpell chosenSpell)
    {
        chosenSpell.enable();
        spellSelectionMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
