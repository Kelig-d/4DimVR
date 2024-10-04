using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // Référence à la caméra
    public Transform playerModel; // Référence au modèle du joueur

    void Update()
    {
        // Met à jour la position du modèle en fonction de la position de la caméra
        Vector3 newPosition = cameraTransform.position;
        newPosition.y = playerModel.position.y; // Garde la hauteur du modèle
        playerModel.position = newPosition;

        // Si nécessaire, aligne également la rotation
        playerModel.rotation = cameraTransform.rotation;
    }
}