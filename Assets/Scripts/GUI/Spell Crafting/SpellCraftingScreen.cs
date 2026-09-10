using UnityEngine;

public class SpellCraftingScreen : MonoBehaviour
{
[SerializeField] private GameObject spellCraftingScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed");
            initSpellCraftingScreen();
        }
    }
    public void initSpellCraftingScreen()
    {
        if(spellCraftingScreen.activeSelf == false)
        {
            spellCraftingScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if(spellCraftingScreen.activeSelf == true)
        {
            spellCraftingScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
