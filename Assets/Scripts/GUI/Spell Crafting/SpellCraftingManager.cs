using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCraftingManager : MonoBehaviour
{
    private SpellBlock currentSpell;
    [SerializeField] CustomCursor cursor;
    [SerializeField] GameObject spellBlockParent;

    [SerializeField] SpellCraftSlot[] slots = new SpellCraftSlot[2];
    [SerializeField] SpellCraftSlot resultSlot;
    [SerializeField] SpellCraftingScreen spellCraftingScreen;
    [SerializeField] float maxCraftDistance;
    [SerializeField] Recipe[] recipeList;
    [SerializeField] TMP_Text msg;
    Player player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update(){
        if (Input.GetMouseButtonUp(0))
        {
            if(currentSpell != null)
            {
                SpellCraftSlot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach(SpellCraftSlot slot in slots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if(dist < shortestDistance && dist < maxCraftDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                        Debug.Log("Slot: " + slot + " Distance: " + dist);
                    }        
                }
                if (nearestSlot)
                {
                    nearestSlot.gameObject.SetActive(true);
                    cursor.currentObject.transform.position = nearestSlot.transform.position;
                    nearestSlot.currentSpell = currentSpell;
                }
                else
                {
                    cursor.currentObject.transform.parent = spellBlockParent.transform;
                    cursor.currentObject.transform.SetSiblingIndex(0);
                }
                cursor.currentObject = null;
                currentSpell = null;
            }

        }
    }
    public void OnMouseDownSpell(SpellBlock spell)
    {
        if(currentSpell == null)
        {
            if(spell.spell == null)
            {
                return;
            }
            currentSpell = spell;
            cursor.gameObject.SetActive(true);
            currentSpell.transform.SetParent(gameObject.transform);
            cursor.currentObject = currentSpell.gameObject;
        }
    }
    public void CraftSpell()
    {
        Debug.Log(player.learntSpells);
        if(slots[0].currentSpell == null || slots[1].currentSpell == null)
        {
            displayCraftError("Both slots must be filled");
            return;
        }
        BaseSpell firstSpell = slots[0].currentSpell.spell;
        BaseSpell secondSpell = slots[1].currentSpell.spell;
        if(firstSpell.getLevel() < 2 || secondSpell.getLevel() < 2)
        {
            displayCraftError("Both spells must be 2nd level");
            return;
        }
        Debug.Log("firstspell: " + firstSpell + " secondspell: " + secondSpell);
        Debug.Log("Made it through spell null check");
        bool foundRecipe = false;
        foreach(Recipe recipe in recipeList)
        {
            BaseSpell resultSpell = recipe.CompareSpells(firstSpell, secondSpell);

            if (resultSpell != null )
            {
                if(resultSpell.getLevel() >= 2)
                {
                    displayCraftError("Resulting spell already max level!");
                    return;
                }
                Debug.Log("Found resultSpell");
                foundRecipe = true;
                foreach(SpellCraftSlot slot in slots)
                {
                    slot.GetComponent<Image>().sprite = null;
                    slot.currentSpell.spell.disable();
                    Destroy(slot.currentSpell.gameObject);
                    slot.currentSpell = null;
                    displayCraftError("");
                }
                resultSpell.enable();
                spellCraftingScreen.CreateSpellBlock(resultSpell, false, resultSlot.transform.position);
                break;
            }
        }
        if (!foundRecipe)
        {
            displayCraftError("Oops! No recipe like this one...");
        }
        Debug.Log(player.learntSpells);

    }
    public void displayCraftError(string text)
    {
        msg.GetComponent<TextMeshProUGUI>().text = text;
    }
    public void Clear()
    {
        foreach(SpellCraftSlot slot in slots)
        {
            slot.currentSpell = null;
            slot.GetComponent<Image>().sprite = null;
        }
        resultSlot.currentSpell = null;
        resultSlot.GetComponent<Image>().sprite = null;
        currentSpell = null;
    }
}
