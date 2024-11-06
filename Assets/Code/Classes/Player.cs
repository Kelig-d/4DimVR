using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [Save]
    public string playerName = "Player";

    [Save]
    public float health { get; private set; } = 100f;    
    public float speed = 2f;
    public float damage = 1f;
    [Save]
    public bool rightHanded = true;
    private bool dead = false;
    public XROrigin xOrigin;
    [Save]
    private Vector3 position;
    public GameObject healthBar;
    private HealthBar healthBarScript;

    private void Awake()
    {
        // Charger les données sauvegardées dès le démarrage de l'application
        SaveManager.Instance.LoadPlayer(this);
    }
    
    private void Start()
    {
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateHealth(health);
        if (xOrigin)
        {
            xOrigin.transform.position = position;
        }
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
            xOrigin.transform.position = new Vector3(10, 15, 10);
        }

        if (dead)
        {
            Revive();

        }
        position = xOrigin.transform.position;
    }

    public void TakeDamage(float tookDamage)
    {
        health -= tookDamage;
        healthBarScript.UpdateHealth(health);
    }

    private void Revive()
    {
        xOrigin.transform.position = new Vector3(2, 5, 2);
        health = 100;
        dead = false;
        healthBarScript.UpdateHealth(health);
        position = xOrigin.transform.position;
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.SavePlayer(this);
    }
}
