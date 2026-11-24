using System;
using UnityEngine;

public class SpellChangeController : MonoBehaviour
{
    public event Action spellChanged;
    public static SpellChangeController instance;
    public void InvokeChange()
    {
        spellChanged?.Invoke();
    }
    void Awake()
    {
        instance = this;
    }
}
