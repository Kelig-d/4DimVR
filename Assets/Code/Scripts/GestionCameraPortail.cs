using UnityEngine;

/// <summary>
/// Synchronizes the camera views between two portals, ensuring that the player's perspective is consistent when transitioning between them.
/// Also adjusts the field of view based on the distance from the portal.
/// </summary>
public class PortailCameraSync : MonoBehaviour
{
    /// <summary>
    /// The position of Portal A.
    /// </summary>
    public Transform porteA;

    /// <summary>
    /// The position of Portal B.
    /// </summary>
    public Transform porteB;

    /// <summary>
    /// The camera associated with Portal A.
    /// </summary>
    public Camera cameraPorteA;

    /// <summary>
    /// The camera associated with Portal B.
    /// </summary>
    public Camera cameraPorteB;

    /// <summary>
    /// The main camera of the player (usually attached to the player’s head).
    /// </summary>
    public Transform playerCamera;

    [Header("Field of View Settings")]
    /// <summary>
    /// The minimum field of view when the player is close to the portal.
    /// </summary>
    public float minFOV = 30f;

    /// <summary>
    /// The maximum field of view when the player is far from the portal.
    /// </summary>
    public float maxFOV = 90f;

    /// <summary>
    /// The maximum distance from the portal for the maximum field of view.
    /// </summary>
    public float maxDistance = 10f;

    /// <summary>
    /// Updates both portal cameras based on the player's position and orientation.
    /// </summary>
    private void Update()
    {
        UpdatePortalCamera(cameraPorteA, porteB, porteA);
        UpdatePortalCamera(cameraPorteB, porteA, porteB);
    }

    /// <summary>
    /// Updates the camera of a given portal to match the player's position and rotation relative to the target portal.
    /// </summary>
    /// <param name="portalCamera">The camera to be updated.</param>
    /// <param name="targetPortal">The portal to which the camera should be aligned.</param>
    /// <param name="sourcePortal">The portal from which the player's position and rotation are measured.</param>
    private void UpdatePortalCamera(Camera portalCamera, Transform targetPortal, Transform sourcePortal)
    {
        // Calculate the player's position relative to the source portal
        Vector3 relativePos = sourcePortal.InverseTransformPoint(playerCamera.position);
        relativePos = new Vector3(-relativePos.x, relativePos.y, -relativePos.z);

        // Calculate the distance from the player to the source portal
        float distanceToSourcePortal = Vector3.Distance(playerCamera.position, sourcePortal.position);

        // Position the portal camera at the same distance from the target portal
        Vector3 directionFromTargetPortal = targetPortal.TransformDirection(relativePos.normalized);
        portalCamera.transform.position = targetPortal.position + directionFromTargetPortal * distanceToSourcePortal;

        // Adjust the camera's rotation to match the player's view
        Quaternion relativeRot = Quaternion.Inverse(sourcePortal.rotation) * playerCamera.rotation;
        portalCamera.transform.rotation = targetPortal.rotation * relativeRot;

        // Adjust the field of view based on the distance to the source portal
        AdjustFieldOfView(portalCamera, distanceToSourcePortal);
    }

    /// <summary>
    /// Adjusts the field of view of the portal camera based on the distance to the portal.
    /// </summary>
    /// <param name="portalCamera">The portal camera whose field of view needs adjustment.</param>
    /// <param name="distanceToPortal">The distance between the player and the portal.</param>
    private void AdjustFieldOfView(Camera portalCamera, float distanceToPortal)
    {
        // Linearly interpolate between the minimum and maximum field of view based on distance
        float t = Mathf.Clamp01(distanceToPortal / maxDistance);
        portalCamera.fieldOfView = Mathf.Lerp(minFOV, maxFOV, t);
    }
}
