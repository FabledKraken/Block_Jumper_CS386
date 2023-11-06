using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Starts the first level
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        // Will load the option scene and change the settings
    }

    // This calls the Load from DataPersistenceManager and loads all data that has ben saved
    // and will start a new game if no saved data was found
    public void Load()
    {
        DataPersistenceManager.Instance.LoadGame();
    }

    // Quits the game when the quit button is pressed, called from title screen
    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
