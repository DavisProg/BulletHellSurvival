using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SpellUpgrade : MonoBehaviour
{
    public GameObject spellUpgradeMenu;
    public Button btn1, btn2;

    public void showUpgradeSelectionMenu(BaseSpell spell)
    {
        Debug.Log(spell);
        Time.timeScale = 0;
        spellUpgradeMenu.SetActive(true);
        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();

        btn1.onClick.AddListener(() =>
            {
                spell.firstPathEnable();
                Select();
            });
        btn2.onClick.AddListener(() =>
            {
                spell.secondPathEnable();
                Select();
            });
    }
    private void Select()
    {
        Time.timeScale = 1;
        spellUpgradeMenu.SetActive(false);
    }
}
