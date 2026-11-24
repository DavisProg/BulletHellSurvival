using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SpellCooldownDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject spellCooldownObjPrefab;
    [SerializeField] Player player;
    List<GameObject> spellCooldownObjList = new List<GameObject>();
    void Start()
    {
        SpellChangeController.instance.spellChanged += UpdateSpellCooldownDisplay;
    }
    public void UpdateSpellCooldownDisplay()
    {
        foreach(GameObject spell in spellCooldownObjList)
        {
            Destroy(spell);
        }
        foreach(BaseSpell spell in player.learntSpells)
        {
            GameObject spellCooldownObj = Instantiate(spellCooldownObjPrefab, gameObject.transform);
            spellCooldownObj.GetComponent<SpellCooldownBlock>().Init(spell);
            spellCooldownObj.transform.GetChild(0).GetComponent<Image>().sprite = spell.spellIcon;

            spellCooldownObjList.Add(spellCooldownObj);
        }
        
        
    }

}
