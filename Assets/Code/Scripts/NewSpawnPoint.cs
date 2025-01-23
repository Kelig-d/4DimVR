using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        Player var  = GameObject.Find("Player SANS BUG").GetComponent<Player>();

        var.ChangeSpawnPoint(spawnPoint);
        Console.WriteLine("change Spawn");

    }
}
