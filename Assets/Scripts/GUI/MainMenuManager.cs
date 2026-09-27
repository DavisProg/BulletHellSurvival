using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string gameScreen;
    void Awake()
    {
    }

    public void StartGame()
    {
        GetComponent<SettingsMenuScreen>().SaveSettings();
        SceneManager.LoadScene(gameScreen);
    }
    public void exitGame() {
        Application.Quit();
    }
}
