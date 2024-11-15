using UnityEngine;

public class PortailCameraSync : MonoBehaviour
{
    public Transform porteA; // Position de Porte A
    public Transform porteB; // Position de Porte B
    public Camera cameraPorteA; // Caméra de Porte A
    public Camera cameraPorteB; // Caméra de Porte B
    public Transform playerCamera; // La caméra principale du joueur

    [Header("Field of View Settings")]
    public float minFOV = 30f;   // Champ de vision minimum (quand le joueur est proche)
    public float maxFOV = 90f;   // Champ de vision maximum (quand le joueur est éloigné)
    public float maxDistance = 10f; // Distance maximum pour un FOV max

    private void Update()
    {
        UpdatePortalCamera(cameraPorteA, porteB, porteA);
        UpdatePortalCamera(cameraPorteB, porteA, porteB);
    }

    private void UpdatePortalCamera(Camera portalCamera, Transform targetPortal, Transform sourcePortal)
    {
        // Calcule la position relative du joueur par rapport au portail source
        Vector3 relativePos = sourcePortal.InverseTransformPoint(playerCamera.position);
        relativePos = new Vector3(-relativePos.x, relativePos.y, -relativePos.z);

        // Calcule la distance entre le joueur et le portail source
        float distanceToSourcePortal = Vector3.Distance(playerCamera.position, sourcePortal.position);

        // Place la caméra du portail cible à la même distance de celui-ci
        Vector3 directionFromTargetPortal = targetPortal.TransformDirection(relativePos.normalized);
        portalCamera.transform.position = targetPortal.position + directionFromTargetPortal * distanceToSourcePortal;

        // Calcule l'orientation pour correspondre à la vue du joueur
        Quaternion relativeRot = Quaternion.Inverse(sourcePortal.rotation) * playerCamera.rotation;
        portalCamera.transform.rotation = targetPortal.rotation * relativeRot;

        // Ajuste dynamiquement le champ de vision de la caméra
        AdjustFieldOfView(portalCamera, distanceToSourcePortal);
    }

    private void AdjustFieldOfView(Camera portalCamera, float distanceToPortal)
    {
        // Calcule le FOV en fonction de la distance avec une interpolation linéaire
        float t = Mathf.Clamp01(distanceToPortal / maxDistance);
        portalCamera.fieldOfView = Mathf.Lerp(minFOV, maxFOV, t);
    }
}