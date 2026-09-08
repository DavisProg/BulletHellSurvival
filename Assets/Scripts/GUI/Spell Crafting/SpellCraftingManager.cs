using UnityEngine;
using UnityEngine.UI;

public class SpellCraftingManager : MonoBehaviour
{
    private SpellBlock currentSpell;
    [SerializeField] Image customCursor;

    [SerializeField] SpellCraftSlot[] slots;
    [SerializeField] float maxCraftDistance;
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
                    nearestSlot.GetComponent<Image>().sprite = currentSpell.GetComponent<Image>().sprite;
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
            customCursor.sprite = currentSpell.GetComponent<Image>().sprite;
        }
    }
}
