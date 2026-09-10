using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField] private BaseSpell firstSpell;
    [SerializeField] private BaseSpell secondSpell;
    [SerializeField] private BaseSpell resultSpell;

    public BaseSpell CompareSpells(BaseSpell spell1, BaseSpell spell2){
        Debug.Log("Expected spells: " + firstSpell + " " + secondSpell);
        if(spell1 == spell2)
        {
            return null;
        }
        if(spell1 == firstSpell || spell1 == secondSpell)
        {
            if(spell2 == secondSpell || spell2 == firstSpell)
            {
                Debug.Log("Comparison success");
                return resultSpell;
            }
        }
        return null;
    }
}
