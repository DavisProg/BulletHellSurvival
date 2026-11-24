using UnityEngine.UI;
using UnityEngine;
using Unity.Collections.Tests.CoreCLR.TestJobs;

public class SpellCooldownBlock : MonoBehaviour
{   
    private BaseSpell spell;
    private Image mask;
    bool enabled = false;

    void Start()
    {
        mask = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }
    public void Init(BaseSpell spell)
    {
        this.spell = spell;
        enabled = true;
        
    }
    void Update()
    {
        if (enabled)
        {
            getCurrentFill(spell.trigger.cooldown, spell.trigger.elapsedTime);
        }
    }
    void getCurrentFill(float max, float current)
    {
        float fillAmount = current / max;
        mask.fillAmount = fillAmount;
    }
}
