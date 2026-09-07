using UnityEngine;
using UnityEngine.UI;

public class SpellCraftingManager : MonoBehaviour
{
    private SpellBlock currentSpell;
    [SerializeField] Image customCursor;
    public void OnMouseDownSpell(SpellBlock spell)
    {
        if(spell == null)
        {
            currentSpell = spell;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentSpell.GetComponent<Image>().sprite;
        }
    }
}
