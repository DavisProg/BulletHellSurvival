using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DebugScreen : MonoBehaviour
{
    private GameObject debugScreen;
    private GameObject debugMenuScreen;
    private GameObject spellSelectionDebugScreen;
    private TMP_Dropdown spellSelectionDropdown;
    private List<BaseSpell> spellList;
    private List<TMP_Dropdown.OptionData> spellOptions = new List<TMP_Dropdown.OptionData>();

    void Start()
    {
        debugScreen = transform.Find("Debug").gameObject;
        debugMenuScreen = transform.Find("Debug/DebugMenu").gameObject;
        spellSelectionDebugScreen = transform.Find("Debug/SpellSelectionDebugScreen").gameObject;
        spellSelectionDropdown = transform.Find("Debug/SpellSelectionDebugScreen/SpellSelectionDropdown").GetComponent<TMP_Dropdown>();
        spellList = GameObject.Find("Player").GetComponent<SpellSelection>().availableSpells;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("Debug button pressed");
            initDebugScreen();
        }
    }
    private void initDebugScreen()
    {
        if(debugScreen.activeSelf == false)
        {
            debugScreen.SetActive(true);
            debugMenuScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (debugScreen.activeSelf == true)
        {
            debugScreen.SetActive(false);
            debugMenuScreen.SetActive(false);
            spellSelectionDebugScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void initSpellSelectionDebugScreen()
    {
        Debug.Log("init called");
        debugMenuScreen.SetActive(false);
        spellSelectionDebugScreen.SetActive(true);

        spellOptions.Clear();
        spellSelectionDropdown.ClearOptions();
        foreach (BaseSpell spell in spellList)
        {
            TMP_Dropdown.OptionData spellOption = new TMP_Dropdown.OptionData(spell.ToString(), null, new Color(1f, 0f, 0f));
            spellOptions.Add(spellOption);
            
        }
        spellSelectionDropdown.AddOptions(spellOptions);
    }
    public void selectDebugSpell()
    {
        spellList[spellSelectionDropdown.value].enable();
    }

  
}
