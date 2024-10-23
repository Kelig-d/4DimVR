using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootAction : MonoBehaviour
{
    public int gunDamage = 1;             // Dommages inflig�s par le tir
    public float weaponRange = 200f;      // Port�e de l'arme
    public float hitForce = 100f;         // Force de l'impact
    private Camera fpsCam;                // Cam�ra associ�e � l'arme
    public float fireRate = 0.25f;        // Temps entre chaque tir
    private float nextFire;               // Temps du prochain tir possible
    public LayerMask layerMask;           // Couches affect�es par le tir

    // Identifiant pour le bouton de tir sur la manette
    private InputFeatureUsage<bool> shootButton = CommonUsages.primaryButton; 

    // R�f�rence au joueur pour le suivi
    public Transform player;
    public float followSpeed = 3f;

    void Start()
    {
        // R�cup�ration de la cam�ra principale
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        FollowPlayer(); // Appel pour suivre le joueur

        // Liste des appareils VR (manettes)
        List<InputDevice> devices = new List<InputDevice>();
        
        // Filtrer pour obtenir uniquement les manettes
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller, devices);

        // Parcourir les manettes disponibles
        foreach (var device in devices)
        {
            bool isShooting;

            // Si le bouton A est press�
            if (device.TryGetFeatureValue(shootButton, out isShooting) && isShooting && Time.time > nextFire)
            {
                // Calcul du temps pour le prochain tir
                nextFire = Time.time + fireRate;

                // Tir (lancer de rayon)
                Shoot();
            }
        }
    }

    // M�thode pour tirer et g�rer le Raycast
    void Shoot()
    {
        // Point de d�part du rayon � la position de la cam�ra
        Vector3 rayOrigin = fpsCam.transform.position;

        // Contient les informations du hit
        RaycastHit hit;

        // Si le raycast touche une cible
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
        {
            // Si la cible a un Rigidbody, appliquer une force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            // Si la cible a un composant ReceiveAction, infliger des d�g�ts
            ReceiveAction target = hit.collider.GetComponent<ReceiveAction>();
            if (target != null)
            {
                target.GetDamage(gunDamage);
            }
        }
    }

    // M�thode pour suivre le joueur
    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
            transform.LookAt(player.position);
        }
    }
}