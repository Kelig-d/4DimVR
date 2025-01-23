using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public string playerName = "Player";

    public float health { get; private set; } = 100f;    
    public float speed = 2f;
    public float damage = 1f;
    public bool rightHanded = true;
    private bool dead = false;
    public XROrigin xOrigin;
    public GameObject healthBar;
    private HealthBar healthBarScript;

    public Vector3 SpawnPoint = new Vector3(0,0,0);


    public void ChangeSpawnPoint(GameObject newSpawnPoint)
    {
        this.SpawnPoint = newSpawnPoint.transform.position;
    }

    private void Start()
    {
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }
        if (xOrigin.transform.position.y < -10)
        {
            xOrigin.transform.position = SpawnPoint;
        }

        if (dead)
        {
            Revive();

        }
    }

    public void TakeDamage(float tookDamage)
    {
        health -= tookDamage;
        healthBarScript.UpdateHealth(health);
    }

    private void Revive()
    {
        xOrigin.transform.position = SpawnPoint;
        health = 100;
        dead = false;
        healthBarScript.UpdateHealth(health);
    }
    
}
