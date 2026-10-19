using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCraftingScreen : MonoBehaviour
{
[SerializeField] private GameObject spellCraftingScreen;
SpellCraftingManager craftingManager;
[SerializeField] GameObject spellBlockPrefab;
[SerializeField] GameObject spellBlockParent;
[SerializeField] private Sprite defaultSlot;
List<GameObject> spellBlockList = new List<GameObject>();
[SerializeField] Player player;

    void Awake()
    {
        craftingManager = spellCraftingScreen.GetComponent<SpellCraftingManager>();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed");
            initSpellCraftingScreen();
        }
    }
    public void initSpellCraftingScreen()
    {
        if(spellCraftingScreen.activeSelf == false)
        {
            if (Time.timeScale != 0)
            {
                spellCraftingScreen.SetActive(true);
                Time.timeScale = 0;
                foreach(BaseSpell spell in player.learntSpells)
                {
                    CreateSpellBlock(spell);
                }
            }
        }
        else if(spellCraftingScreen.activeSelf == true)
        {
            spellCraftingScreen.SetActive(false);
            craftingManager.Clear();
            foreach(GameObject spell in spellBlockList)
            {
                if (spell)
                {
                    Destroy(spell);
                }
            }
            Time.timeScale = 1;
        }
    }
    public void CreateSpellBlock(BaseSpell spell, bool addToList = true, Vector3 position = default)
    {
        GameObject spellObj;
        if (addToList)
        {
            spellObj = Instantiate(spellBlockPrefab, spellBlockParent.transform);
        }
        else
        {
            spellObj = Instantiate(spellBlockPrefab, position, Quaternion.identity, spellBlockParent.transform.parent);
        }
        SpellBlock spellBlock = spellObj.GetComponent<SpellBlock>();
        spellBlockList.Add(spellObj);

        spellObj.transform.GetChild(0).GetComponent<Image>().sprite = spell.spellIcon;
        spellBlock.spell = spell;
        spellBlock.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "Lvl." + spell.getLevel();

        EventTrigger trigger = spellObj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;

        entry.callback.AddListener(
            (eventData) => craftingManager.OnMouseDownSpell(spellBlock)
        );

        trigger.triggers.Add(entry);
    }
}
