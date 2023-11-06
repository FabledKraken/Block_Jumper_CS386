using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

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
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from a file using the data handler
        _gameData = _dataHandler.Load();
        
        // If no data can be loaded, load a new game 
        if (_gameData == null)
        {
            Debug.Log("Creating new Game Data");
            NewGame();
        }

        // Push the loaded data to all other scripts that need it
        foreach (var dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
        
        Debug.Log("Loaded strawberries collected = " + _gameData.strawberryCollected);
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
