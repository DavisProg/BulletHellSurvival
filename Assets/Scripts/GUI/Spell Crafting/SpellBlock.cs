using TMPro;
using UnityEngine;

public class SpellBlock : MonoBehaviour
{
    public BaseSpell spell;
    public int index;
    [SerializeField] TMP_Text levelText;

    public void DisplayLevelNumber()
    {
        if (levelText == null)
        {
        Debug.LogError(
            $"Level Text is missing on SpellBlock: {gameObject.name}",
            this
        );
        return;
    }
        if (spell)
        {
            levelText.gameObject.transform.parent.gameObject.SetActive(true);
            int level = spell.getLevel();
            levelText.text = "Lvl." + level;
            return;
        }
        levelText.gameObject.transform.parent.gameObject.SetActive(false);
    }

}
