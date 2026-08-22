using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class onKeyboardButtonPress : ITrigger
{
    public event Action Triggered;
    private event Action keyPressed;
    private BaseSpell spell;
    
    
    public onKeyboardButtonPress(BaseSpell spell)
    {
        /*
        this.spell = spell;
        if (spell.onKeyDown != null)
        {
            keyPressed = spell.onKeyDown;
        }
        */
    }

    public void Enable(){
        keyPressed += activateAbility;
        Debug.Log("OnKeyboardBttonPress Enabled");
        Debug.Log(keyPressed);
    }
    public void Disable(){
        keyPressed -= activateAbility;
    }
    private void activateAbility()
    {
        Debug.Log("Key pressed");
        Triggered?.Invoke();
    }
}
