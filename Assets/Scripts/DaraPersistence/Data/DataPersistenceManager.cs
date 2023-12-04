using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] [SerializeField]
    private string fileName;

    private GameData _gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the Scene");
        }
        else
        {
            Debug.Log("Data Persistence Manager instance created");
        }

        Instance = this;
    }

    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        // Delete the existing saved file
        _dataHandler.Delete();

        // Create a new GameData instance
        _gameData = new GameData();
        PlayerLivesManager.Instance.SetLives(3);
    }

    public void LoadGame()
    {
        Debug.Log("LoadGame method called");
        // Load any saved data from a file using the data handler
        _gameData = _dataHandler.Load();

        // If no data is loaded, create a new GameData instance
        if (_gameData == null)
        {
            Debug.Log("No saved data found. Creating new Game Data.");
            _gameData = new GameData();
        }

        // Push the loaded or new data to all other scripts that need it
        foreach (var dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
        
        Debug.Log("Loaded strawberries collected = " + _gameData.totalPoints);
        
        //Debug.Log("Loading scene index: " + sceneIndexToLoad);
        //SceneManager.LoadScene(_gameData.activeScene);
        Debug.Log("Scene loaded " + _gameData.activeScene);
    }

    public int getScene()
    {
        int sceneIndexToLoad = (_gameData != null) ? _gameData.activeScene : 1;
        return sceneIndexToLoad;
    }
    
    public int getPoints()
    {
        return _gameData.totalPoints;
    }

    public void setPos(int x, int y, int z)
    {
        _gameData.playerPos.x = x;
        _gameData.playerPos.y = y;
        _gameData.playerPos.z = z;
    }
    

    public void SaveGame()
    {
        foreach (var dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }
        
        Debug.Log("Saved strawberries collected = " + _gameData.strawberryCollected);
        
        // Save the data to a file using the data handler
        _dataHandler.Save(_gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
