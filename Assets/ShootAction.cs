using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootAction : MonoBehaviour
{
    public int gunDamage = 1;             // Dommages infligés par le tir
    public float weaponRange = 200f;      // Portée de l'arme
    public float hitForce = 100f;         // Force de l'impact
    private Camera fpsCam;                // Caméra associée à l'arme
    public float fireRate = 0.25f;        // Temps entre chaque tir
    private float nextFire;               // Temps du prochain tir possible
    public LayerMask layerMask;           // Couches affectées par le tir

    // Identifiant pour le bouton de tir sur la manette
    private InputFeatureUsage<bool> shootButton = CommonUsages.primaryButton; 

    // Référence au joueur pour le suivi
    public Transform player;
    public float followSpeed = 3f;

    void Start()
    {
        // Récupération de la caméra principale
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

            // Si le bouton A est pressé
            if (device.TryGetFeatureValue(shootButton, out isShooting) && isShooting && Time.time > nextFire)
            {
                // Calcul du temps pour le prochain tir
                nextFire = Time.time + fireRate;

                // Tir (lancer de rayon)
                Shoot();
            }
        }
    }

    // Méthode pour tirer et gérer le Raycast
    void Shoot()
    {
        // Point de départ du rayon à la position de la caméra
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

            // Si la cible a un composant ReceiveAction, infliger des dégâts
            ReceiveAction target = hit.collider.GetComponent<ReceiveAction>();
            if (target != null)
            {
                target.GetDamage(gunDamage);
            }
        }
    }

    // Méthode pour suivre le joueur
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