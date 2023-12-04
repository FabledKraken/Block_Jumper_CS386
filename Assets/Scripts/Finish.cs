using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour, IDataPersistence
{
    private bool levelCompleted = false;
    public Finish Instance;
    private int totalPoints = 0;

    [SerializeField] private AudioSource endSoundEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            endSoundEffect.Play();
            Invoke("CompleteLevel", .75f);
        }
    }

    private void CompleteLevel()
    {
        ItemCollector itemCollector = FindObjectOfType<ItemCollector>();
        if (itemCollector != null)
        {
            totalPoints += itemCollector.GetTotalPoints();
        }
        
        // Save the updated data
        SaveGame();

        // Load the next scene or handle game completion logic
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int getPoints()
    {
        return totalPoints;
    }

    private void SaveGame()
    {
        // Assuming you have a reference to your DataPersistenceManager script on an object in the scene
        DataPersistenceManager dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        

        if (dataPersistenceManager != null)
        {
            dataPersistenceManager.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        // Load any specific data for the Finish script if needed
    }

    public void SaveData(ref GameData data)
    {
        // Save any specific data for the Finish script if needed
    }
}