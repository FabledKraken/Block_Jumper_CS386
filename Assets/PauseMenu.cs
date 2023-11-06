using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Used in the Paused screen for "RESTART"
    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
    }

    // Used in the Paused screen for "QUIT"
    public void QuitGame()
    {
        DataPersistenceManager.Instance.SaveGame();
        isPaused = false;
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
