using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ZimmaBlueArtefact : MonoBehaviour
{
    /// <summary>
    /// The projectile LineRenderer that will be used to visually represent the projectile.
    /// </summary>
    public LineRenderer projectile;

    /// <summary>
    /// The point from where the projectile will be spawned.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// The maximum distance the projectile can travel.
    /// </summary>
    public float fireDistance = 10;

    /// <summary>
    /// The amount of time the projectile's visual representation remains visible.
    /// </summary>
    public float projectileShow = 0.5f;

    /// <summary>
    /// The layer mask to filter which objects the raycast should interact with.
    /// </summary>
    public LayerMask layerMask;

    // Start is called before the first frame update
    /// <summary>
    /// Initializes the interaction by adding the Shoot method as a listener for the activation event.
    /// </summary>
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Shoot);
    }

    void Update()
    {
        // No update logic implemented here.
    }

    /// <summary>
    /// Fires a projectile when the artefact is activated.
    /// The projectile is visualized as a LineRenderer, and a raycast is cast from the spawn point to detect the target.
    /// </summary>
    /// <param name="arg">The event arguments passed when the artefact is activated.</param>
    public void Shoot(ActivateEventArgs arg)
    {
        // Instantiate the projectile's visual representation (LineRenderer)
        LineRenderer spawProjectile = Instantiate(projectile);
        RaycastHit hit;
        
        // Perform a raycast to detect if the projectile hits any object
        bool hasHit = Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit, layerMask);
        Vector3 endPoint = Vector3.zero;

        // Check if the raycast hit something
        if (hasHit)
        {
            // Check if the hit object has the EffetZimmaBlue component (special target)
            EffetZimmaBlue target = hit.transform.GetComponent<EffetZimmaBlue>();
            ZimmaBlueJustColor target2 = hit.transform.GetComponent<ZimmaBlueJustColor>();

            if (target != null)
            {
                // Change color of the special target
                target.ChangeColor(spawProjectile.startColor);
                endPoint = hit.point; // Set the end point to the hit point
                Debug.Log("Cible atteinte speciale");
            }
            else if (target2 != null)
            {
                // Change color of the regular target
                target2.ChangeColor(spawProjectile.startColor);
                endPoint = hit.point; // Set the end point to the hit point
                Debug.Log("Cible atteinte");
            }
            else
            {
                // If no valid target is hit, set the endpoint to the maximum fire distance
                endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
                Debug.Log("Cible non atteinte");
            }
        }
        else
        {
            // If no hit is detected, set the endpoint to the maximum fire distance
            endPoint = spawnPoint.position + spawnPoint.forward * fireDistance;
            Debug.Log("Cible non atteinte");
        }

        // Set the projectile's start and end points
        spawProjectile.positionCount = 2;
        spawProjectile.SetPosition(0, spawnPoint.position);
        spawProjectile.SetPosition(1, endPoint);

        // Destroy the projectile after the specified time
        Destroy(spawProjectile.gameObject, projectileShow);
    }
}
