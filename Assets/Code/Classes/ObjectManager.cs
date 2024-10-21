using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    public GameObject[] Objects;
    
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);

        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Objects = GameObject.FindGameObjectsWithTag("DimensionObject");
        }
    }

    public void persistObjects()
    {
        foreach (GameObject o in Objects)
        {
            DontDestroyOnLoad(o);
        }
    }
}