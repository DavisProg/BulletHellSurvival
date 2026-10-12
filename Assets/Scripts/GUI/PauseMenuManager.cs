using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] string menuScreen;
    [SerializeField] private GameObject pauseMenu;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf == false && Time.timeScale != 0)
            {
                showMainScreen();
            }
            else if (pauseMenu.activeSelf == true)
            {
                hideMainMenu();
                resumeTime();
            }
        }
    }
    public void exitGame() {
        SceneManager.LoadScene(menuScreen);
        resumeTime();
    }
    public void showMainScreen()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void hideMainMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void resumeTime()
    {
        Time.timeScale = 1;
    }
}
