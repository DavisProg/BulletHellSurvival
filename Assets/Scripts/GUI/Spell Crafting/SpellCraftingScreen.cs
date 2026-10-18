using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCraftingScreen : MonoBehaviour
{
[SerializeField] private GameObject spellCraftingScreen;
SpellCraftingManager craftingManager;
[SerializeField] private SpellBlock[] spellBlocks;
[SerializeField] private Sprite defaultSlot;
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

                foreach(SpellBlock spell in spellBlocks)
                {
                    if(player.learntSpells != null)
                    {
                        if (spell.index >= player.learntSpells.Count)
                        {
                            spell.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "";
                            continue;
                        }
                        if (player.learntSpells[spell.index].spellIcon != null)
                        {
                            spell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = player.learntSpells[spell.index].spellIcon;
                            spell.spell = player.learntSpells[spell.index];
                            spell.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "Lvl." + spell.spell.getLevel();
                        }
                    }
                }
            }
        }
        else if(spellCraftingScreen.activeSelf == true)
        {
            spellCraftingScreen.SetActive(false);
            craftingManager.Clear();
            foreach(SpellBlock spell in spellBlocks)
            {
                spell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = defaultSlot;
            }
            Time.timeScale = 1;
        }
    }
}
