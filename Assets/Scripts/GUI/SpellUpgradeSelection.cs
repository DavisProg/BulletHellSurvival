using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        TMP_Text firstButtonTitleText = btn1.gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text firstButtonDescriptionText = btn1.gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
        TMP_Text secondButtonTitleText = btn2.gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text secondButtonDescriptionText = btn2.gameObject.transform.GetChild(3).GetComponent<TMP_Text>();

        firstButtonTitleText.text = spell.firstPathName;
        secondButtonTitleText.text = spell.secondPathName;
        firstButtonDescriptionText.text = spell.firstPathDescription;
        secondButtonDescriptionText.text = spell.secondPathDescription;

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
