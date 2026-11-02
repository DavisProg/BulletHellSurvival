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
    // Funkcija, kas izpildās spēles sākumā. Veido savienojumu ar spēlētāja klasi.
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    /*Funkcija, kas palaižas katru kadru. Pārbauda, ja spēlētājs velk spēju ar pelīti, 
    vai atlaišanas brīdi spēja ir pietiekami tuvu spēju kombinēšanas blokam*/
    void Update(){
        //Pārbauda pelīšu kreiso klikšķa atlaišanu
        if (Input.GetMouseButtonUp(0))
        {
            if(currentSpell != null)
            {
                //Pārbada tuvāko spēju kombinēšanas lauciņu un vai tā ir tuvāka par maksimālo attālumu
                SpellCraftSlot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach(SpellCraftSlot slot in slots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if(dist < shortestDistance && dist < maxCraftDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }        
                }
                /* Ja veiksmīgi atrada tuvāko spēju kombinēšana lauciņu
                Tad ieliek spēju kombinēšanas lauciņas pozīcijā un atzīme to tās klasē*/
                
                if (nearestSlot)
                {
                    nearestSlot.gameObject.SetActive(true);
                    cursor.currentObject.transform.position = nearestSlot.transform.position;
                    nearestSlot.currentSpell = currentSpell;
                }
                //Ja neatrada tad noliek spēju sarakstā, kā pirmo elementu
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
    //Funkcija, kas turot ar kreiso pelītes taustiņu uz spējas attēlojumu izņem no spēju saraksta un liek tai sekot pelītes pozīcijai
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
    //Funkcija, kas aktivizējas uzklikšķinot kombinēšanas pogai
    public void CraftSpell()
    {
        //Pārbauda, ka abas spēju kombinēšanas lauciņi ir aizpildīti
        if(slots[0].currentSpell == null || slots[1].currentSpell == null)
        {
            displayCraftError("Both slots must be filled");
            return;
        }
        BaseSpell firstSpell = slots[0].currentSpell.spell;
        BaseSpell secondSpell = slots[1].currentSpell.spell;
        //Pārbauda, ka abas spēju kombinēšanas laucīņu saturošās spējas ir sasniegušas otro līmeni
        if(firstSpell.getLevel() < 2 || secondSpell.getLevel() < 2)
        {
            displayCraftError("Both spells must be 2nd level");
            return;
        }
        bool foundRecipe = false;
        //Pārbauda katru iespējamo kombināciju
        foreach(Recipe recipe in recipeList)
        {
            BaseSpell resultSpell = recipe.CompareSpells(firstSpell, secondSpell);

            if (resultSpell != null )
            {
                //Pārbauda vai rezultāta spēja ir jau sasniedzis otro līmeni
                if(resultSpell.getLevel() >= 2)
                {
                    displayCraftError("Resulting spell already max level!");
                    return;
                }
                foundRecipe = true;
                //Iztukšo katru spēju kombinēšanas lauciņu un izslēdz abas izmantotās spējas
                foreach(SpellCraftSlot slot in slots)
                {
                    slot.GetComponent<Image>().sprite = null;
                    slot.currentSpell.spell.disable();
                    Destroy(slot.currentSpell.gameObject);
                    slot.currentSpell = null;
                    displayCraftError("");
                }
                //Aktivizē rezultāta spēju un izveido rezultāta spējas attēlojumu
                resultSpell.enable();
                spellCraftingScreen.CreateSpellBlock(resultSpell, false, resultSlot.transform.position);
                break;
            }
        }
        //Ja nav atrasta kombinācija, tad paziņo spēlētājam
        if (!foundRecipe)
        {
            displayCraftError("Oops! No recipe like this one...");
        }
    }
    //Funkcija, kas attēlo kļūdas paziņojumu
    public void displayCraftError(string text)
    {
        msg.GetComponent<TextMeshProUGUI>().text = text;
    }
    //Iztukšo spēju kombinēšanas lauciņus
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
