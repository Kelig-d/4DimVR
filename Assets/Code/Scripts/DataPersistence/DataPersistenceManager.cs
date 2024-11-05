using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }
    private GameData gameData;

    private List<IDataPersistence> _dataPersistencesObjects;
    private void Awake()
    {
        if (instance != null) Debug.LogError("Found more than one Data Persistence Manager in scene.");
        instance = this;
    }

    private void Start()
    {
        this._dataPersistencesObjects = findAllDataPersistenceObjects();
        loadGame();
    }

    public void newGame()
    {
        gameData = new GameData();
    }

    public void loadGame()
    {
        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing new game.");
            newGame();
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void saveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in _dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
