using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using UnityEngine;
using System.IO;
using Code.Classes;
using Code.Scripts;
using Code.Scripts.DataPersistence.Data;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;

    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SaveManager).Name);
                    _instance = singletonObject.AddComponent<SaveManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    public void saveData(GameData data)
    {
        if (data.isPlayerData)
        {
            // Créer le JSON avec les données sérialisées
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.persistentDataPath + "/playerSave.json", json);

        }
    }

    public void LoadPlayer(Player player)
    {
        string path = Application.persistentDataPath + "/playerSave.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            player.playerName = loadedData.playerName;
            player.rightHanded = loadedData.rightHanded;
            player.xOrigin.transform.position = new Vector3(loadedData.position[0], loadedData.position[1], loadedData.position[2]);
            
            player.health = loadedData.health;
            
        }
    }
   
   
}
