using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string gameScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(gameScreen);
    }
    public void exitGame() {
        Application.Quit();
    }
}
