using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private static bool playerExists = false;
    public GameObject playerPrefab;
    
    void Start()
    {
        // Vérifie si un Player existe déjà avant d’en instancier un nouveau
        if (FindObjectOfType<Player>() == null)
        {
            Instantiate(playerPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
        }
    }
}
