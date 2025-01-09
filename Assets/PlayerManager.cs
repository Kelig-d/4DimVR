using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(existingPlayer);
        if (existingPlayer == null)
        {
            Instantiate(playerPrefab);
        }
    }
}
