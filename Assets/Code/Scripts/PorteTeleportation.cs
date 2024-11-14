using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteTeleportation : MonoBehaviour
{
    public GameObject autrePorte;         // La porte de destination
    public Material porteMaterial;        // Le matériau de la porte
    public Camera cameraApercu;           // La caméra d'aperçu
    public RenderTexture renderTexture;   // La RenderTexture utilisée pour afficher l'aperçu
    public float distanceMax = 2.0f;      // La distance minimale pour activer la téléportation

    private void Start()
    {
        // Assurez-vous que la caméra d'aperçu est correctement initialisée
        if (cameraApercu != null && porteMaterial != null && renderTexture != null)
        {
            cameraApercu.targetTexture = renderTexture;   // Assigner la RenderTexture à la caméra
            porteMaterial.mainTexture = renderTexture;     // Appliquer la texture à la porte
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si le joueur entre dans la porte, tenter la téléportation
        if (other.CompareTag("Player"))
        {
            Teleporter(other.gameObject);
        }
    }

    private void Teleporter(GameObject player)
    {
        if (autrePorte != null)
        {
            // Vérifier si le joueur est à une distance raisonnable de la porte
            if (Vector3.Distance(player.transform.position, transform.position) <= distanceMax)
            {
                // Téléportation du joueur à l'autre porte
                player.transform.position = autrePorte.transform.position;

                // S'assurer que le joueur regarde dans la direction de la porte d'arrivée
                player.transform.rotation = autrePorte.transform.rotation;
            }
        }
    }
}
