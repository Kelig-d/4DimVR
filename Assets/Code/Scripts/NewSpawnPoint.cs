using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPoint;
    public bool Menu = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision détectée avec : " + other.gameObject.name);

        Transform parentTransform = other.gameObject.transform.parent;

        if (parentTransform != null)
        {
            Player player = parentTransform.GetComponent<Player>();

            if (player != null)
            {
                Debug.Log("try change Spawn");
                player.ChangeSpawnPoint(spawnPoint, Menu);
            }
            else
            {
                Debug.Log("no change Spawn");
            }
        }
        else
        {
            Debug.Log("Pas de parent, impossible de trouver Player.");
        }
    }


}
