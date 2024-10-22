using System.Collections.Generic;
using Code.Classes;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private readonly Dictionary<string, GameObject> _registeredObjects = new Dictionary<string, GameObject>();

    // Enregistre un objet et l'associe à son nom
    public void RegisterObject(string objectName, GameObject obj)
    {
        if (!_registeredObjects.ContainsKey(objectName))
        {
            _registeredObjects.Add(objectName, obj);
            DontDestroyOnLoad(obj); // Marque cet objet comme persistant
        }
        else
        {
            Destroy(obj); // Si l'objet est déjà enregistré, détruit le doublon
        }
    }

    // Récupère un objet enregistré
    public GameObject GetObject(string objectName)
    {
        _registeredObjects.TryGetValue(objectName, out GameObject obj);
        return obj;
    }
}