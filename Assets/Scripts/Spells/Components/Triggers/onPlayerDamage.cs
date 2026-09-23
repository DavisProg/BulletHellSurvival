using System;
using UnityEngine;

public class onPlayerDamage : ITrigger
{
    public event Action Triggered;
    private Player player;
    public onPlayerDamage(Player player)
    {
        this.player = player;
    }
    public void Enable()
    {
        player.PlayerDamaged += onPlayerDamageTrigger;
    }
    public void Disable()
    {
        player.PlayerDamaged -= onPlayerDamageTrigger;
    }
    private void onPlayerDamageTrigger()
    {
        Debug.Log("onPlayerDamage initialised");
        Triggered?.Invoke();
    }
}
