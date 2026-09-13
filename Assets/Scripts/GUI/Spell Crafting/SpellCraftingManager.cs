using UnityEngine;
using UnityEngine.UI;

public class SpellCraftingManager : MonoBehaviour
{
    private SpellBlock currentSpell;
    [SerializeField] Image customCursor;

    [SerializeField] SpellCraftSlot[] slots;
    [SerializeField] SpellCraftSlot resultSlot;
    [SerializeField] float maxCraftDistance;
    [SerializeField] Recipe[] recipeList;
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
                customCursor.gameObject.SetActive(false);
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
                    nearestSlot.GetComponent<Image>().sprite = currentSpell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
                    nearestSlot.currentSpell = currentSpell;
                }
                currentSpell = null;
            }

        }
    }
    public void OnMouseDownSpell(SpellBlock spell)
    {
        if(currentSpell == null)
        {
            currentSpell = spell;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentSpell.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        }
    }
    public void CraftSpell()
    {
        Debug.Log(player.learntSpells);
        BaseSpell firstSpell = slots[0].currentSpell.spell;
        BaseSpell secondSpell = slots[1].currentSpell.spell;
        Debug.Log("firstspell: " + firstSpell + " secondspell: " + secondSpell);
        if(firstSpell && secondSpell)
        {
            Debug.Log("Made it through spell null check");
            foreach(Recipe recipe in recipeList)
            {
                BaseSpell resultSpell = recipe.CompareSpells(firstSpell, secondSpell);

                if (resultSpell != null && resultSpell.getLevel() < 2)
                {
                    Debug.Log("Found resultSpell");
                    resultSpell.enable();
                    resultSlot.GetComponent<Image>().sprite = resultSpell.spellIcon;
                    foreach(SpellCraftSlot slot in slots)
                    {
                        slot.GetComponent<Image>().sprite = null;
                        slot.currentSpell.spell.disable();
                        slot.currentSpell = null;
                    }
                }
            }
        }
        Debug.Log(player.learntSpells);

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
        customCursor.gameObject.SetActive(false);
    }
}
