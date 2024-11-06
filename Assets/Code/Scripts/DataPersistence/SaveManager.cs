using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;
using Code.Scripts;

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

    public void SavePlayer(Player player)
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        FieldInfo[] fields = typeof(Player).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (Attribute.IsDefined(field, typeof(SaveAttribute)))
            {
                var fieldValue = field.GetValue(player);

                // Si le champ est une position (Vector3) ou une rotation (Quaternion)
                if (field.FieldType == typeof(Vector3) || field.FieldType == typeof(Quaternion))
                {
                    string serializedValue = JsonUtility.ToJson(fieldValue);
                    saveData.Add(field.Name, serializedValue);
                }
                else
                {
                    saveData.Add(field.Name, fieldValue);
                }
            }
        }

        // Créer le JSON avec les données sérialisées
        string json = JsonUtility.ToJson(new SerializationHelper(saveData), true);
        Debug.Log(json);
        // Sauvegarde dans un fichier
        File.WriteAllText(Application.persistentDataPath + "/playerSave.json", json);
    }

    public void LoadPlayer(Player player)
    {
        string path = Application.persistentDataPath + "/playerSave.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializationHelper loadedData = JsonUtility.FromJson<SerializationHelper>(json);

            foreach (var entry in loadedData.data)
            {
                FieldInfo field = typeof(Player).GetField(entry.key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    // Désérialiser la valeur du champ
                    object fieldValue = JsonUtility.FromJson(entry.value, field.FieldType);
                    field.SetValue(player, fieldValue);
                }
            }
        }
    }
    
    [System.Serializable]
    public class SaveDataEntry
    {
        public string key;
        public string value;

        public SaveDataEntry(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [System.Serializable]
    public class SerializationHelper
    {
        public List<SaveDataEntry> data;

        public SerializationHelper(Dictionary<string, object> saveData)
        {
            data = new List<SaveDataEntry>();

            foreach (var kvp in saveData)
            {
                string serializedValue = JsonUtility.ToJson(kvp.Value);
                data.Add(new SaveDataEntry(kvp.Key, serializedValue));
            }
        }
    }
}
