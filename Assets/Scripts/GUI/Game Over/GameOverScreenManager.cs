using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] string gameScreen;
    [SerializeField] string menuScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(gameScreen);
        Time.timeScale = 1;
    }
    public void exitGame() {
        SceneManager.LoadScene(menuScreen);
        Time.timeScale = 1;
    }
    public void initGameOverScreen()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
