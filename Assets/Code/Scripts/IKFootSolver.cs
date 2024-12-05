using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Solves the inverse kinematics (IK) for a foot, ensuring the foot stays grounded and moves according to the character's movement.
/// </summary>
public class IKFootSolver : MonoBehaviour
{
    /// <summary>
    /// Indicates whether the foot is moving forward.
    /// </summary>
    public bool isMovingForward;

    /// <summary>
    /// The layer mask to detect terrain for raycasting.
    /// </summary>
    [SerializeField] LayerMask terrainLayer = default;

    /// <summary>
    /// The body transform of the character to help calculate foot placement.
    /// </summary>
    [SerializeField] Transform body = default;

    /// <summary>
    /// A reference to the other foot IK solver, used to check if the other foot is moving.
    /// </summary>
    [SerializeField] IKFootSolver otherFoot = default;

    /// <summary>
    /// The speed at which the foot moves when solving IK.
    /// </summary>
    [SerializeField] float speed = 4;

    /// <summary>
    /// The minimum distance between the current foot position and the target position before moving the foot.
    /// </summary>
    [SerializeField] float stepDistance = .2f;

    /// <summary>
    /// The length of the step when the foot moves forward.
    /// </summary>
    [SerializeField] float stepLength = .2f;

    /// <summary>
    /// The length of the step when the foot moves sideways.
    /// </summary>
    [SerializeField] float sideStepLength = .1f;

    /// <summary>
    /// The height of the foot during the step to simulate lifting the foot off the ground.
    /// </summary>
    [SerializeField] float stepHeight = .3f;

    /// <summary>
    /// An offset to apply to the foot's position relative to its calculated position.
    /// </summary>
    [SerializeField] Vector3 footOffset = default;

    /// <summary>
    /// The rotational offset applied to the foot.
    /// </summary>
    public Vector3 footRotOffset;

    /// <summary>
    /// The vertical offset for the foot's position.
    /// </summary>
    public float footYPosOffset = 0.1f;

    /// <summary>
    /// The starting point of the raycast relative to the body position.
    /// </summary>
    public float rayStartYOffset = 0;

    /// <summary>
    /// The length of the raycast used to detect the terrain below the foot.
    /// </summary>
    public float rayLength = 1.5f;

    /// <summary>
    /// The spacing between the two feet for calculating foot positions.
    /// </summary>
    float footSpacing;

    /// <summary>
    /// The previous and current positions and normals of the foot for interpolation.
    /// </summary>
    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;

    /// <summary>
    /// The interpolation factor to smoothly move the foot.
    /// </summary>
    float lerp;

    /// <summary>
    /// Initializes the foot solver by setting up initial values for position, normal, and interpolation.
    /// </summary>
    private void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    /// <summary>
    /// Updates the foot's position and rotation based on the raycast hit information.
    /// </summary>
    void Update()
    {
        // Apply Y offset to the foot's position
        transform.position = currentPosition + Vector3.up * footYPosOffset;
        
        // Apply foot rotation offset
        transform.localRotation = Quaternion.Euler(footRotOffset);

        // Create a raycast starting from the body to check the ground below the foot
        Ray ray = new Ray(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);

        // Debug the raycast path in the scene view
        Debug.DrawRay(body.position + (body.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);
            
        // Perform raycast to detect the terrain below
        if (Physics.Raycast(ray, out RaycastHit info, rayLength, terrainLayer.value))
        {
            // If the distance to the hit point is large enough and the other foot is not moving
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0; // Start a new step

                // Calculate direction to move the foot based on the hit point and foot's position
                Vector3 direction = Vector3.ProjectOnPlane(info.point - currentPosition, Vector3.up).normalized;

                // Calculate angle between the body and the foot's direction
                float angle = Vector3.Angle(body.forward, body.InverseTransformDirection(direction));

                // Determine if the foot is moving forward or sideways
                isMovingForward = angle < 50 || angle > 130;

                if (isMovingForward)
                {
                    // Move the foot forward
                    newPosition = info.point + direction * stepLength + footOffset;
                    newNormal = info.normal;
                }
                else
                {
                    // Move the foot sideways
                    newPosition = info.point + direction * sideStepLength + footOffset;
                    newNormal = info.normal;
                }
            }
        }

        // Interpolate foot movement based on lerp value
        if (lerp < 1)
        {
            // Smoothly interpolate the foot's position and apply step height
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed; // Increment lerp value based on speed
        }
        else
        {
            // Update old position and normal once the step is complete
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    /// <summary>
    /// Draws gizmos in the scene view to visualize the foot's position.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.1f);
    }

    /// <summary>
    /// Checks whether the foot is still moving or not.
    /// </summary>
    /// <returns>True if the foot is still moving, false if it is stationary.</returns>
    public bool IsMoving()
    {
        return lerp < 1;
    }
}
