using UnityEngine;

public class SpellCraftingScreen : MonoBehaviour
{
[SerializeField] private GameObject spellCraftingScreen;
SpellCraftingManager craftingManager;

    void Awake()
    {
        craftingManager = spellCraftingScreen.GetComponent<SpellCraftingManager>();
        
    }
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
            craftingManager.Clear();
            Time.timeScale = 1;
        }
    }
}
