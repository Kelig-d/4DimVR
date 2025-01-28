using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPoint;
    public bool Menu = false;
    private void OnTriggerEnter(Collider other)
    {

        Player var = other.gameObject.GetComponent<Player>();
        
        if(var != null)
        {
            var.ChangeSpawnPoint(spawnPoint, Menu);
            Console.WriteLine("change Spawn");
        }


    }
}
