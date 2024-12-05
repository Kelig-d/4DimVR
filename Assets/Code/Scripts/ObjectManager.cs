using System.Collections.Generic;
using Code.Classes;
using UnityEngine;

/// <summary>
/// Singleton class responsible for managing registered GameObjects.
/// Allows for registering, retrieving, and managing objects that should persist across scenes.
/// </summary>
public class ObjectManager : Singleton<ObjectManager>
{
    // Dictionary to hold registered GameObjects with their associated names
    private readonly Dictionary<string, GameObject> _registeredObjects = new Dictionary<string, GameObject>();

    /// <summary>
    /// Registers a GameObject with a specific name. If the object is already registered, it destroys the duplicate.
    /// The object is marked to persist across scene loads.
    /// </summary>
    /// <param name="objectName">The unique name associated with the GameObject.</param>
    /// <param name="obj">The GameObject to register.</param>
    public void RegisterObject(string objectName, GameObject obj)
    {
        // Check if the object is already registered
        if (!_registeredObjects.ContainsKey(objectName))
        {
            // Register the object and mark it to not be destroyed on scene load
            _registeredObjects.Add(objectName, obj);
            DontDestroyOnLoad(obj); // Ensures the object persists across scene loads
        }
        else
        {
            // If the object is already registered, destroy the duplicate
            Destroy(obj);
        }
    }

    /// <summary>
    /// Retrieves a registered GameObject by its name.
    /// </summary>
    /// <param name="objectName">The name of the GameObject to retrieve.</param>
    /// <returns>The registered GameObject, or null if not found.</returns>
    public GameObject GetObject(string objectName)
    {
        // Try to get the object from the dictionary
        _registeredObjects.TryGetValue(objectName, out GameObject obj);
        return obj; // Returns null if the object was not found
    }
}