using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SpellSelection : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public BaseSpell[] availableSpells;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void showSpellSelectionMenu()
    {
        spellSelectionMenu.SetActive(true);
        BaseSpell[] chosenSpellList = {
            availableSpells[Random.Range(0, availableSpells.Length)],
            availableSpells[Random.Range(0, availableSpells.Length)],
            availableSpells[Random.Range(0, availableSpells.Length)]
        }
        ;
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
    }
}
