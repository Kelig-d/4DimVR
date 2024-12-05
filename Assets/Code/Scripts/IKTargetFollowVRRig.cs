using System;
using UnityEngine;

[Serializable]
public class VRMap
{
    /// <summary>
    /// The VR target transform to map from (e.g., the position and rotation of the VR controller or headset).
    /// </summary>
    public Transform vrTarget;

    /// <summary>
    /// The rig target transform to map to (e.g., the rig's character's position and rotation).
    /// </summary>
    public Transform rigTarget;

    /// <summary>
    /// The position offset applied to the VR target's position when mapping to the rig target.
    /// </summary>
    public Vector3 trackingPositionOffset;

    /// <summary>
    /// The rotation offset applied to the VR target's rotation when mapping to the rig target.
    /// </summary>
    public Vector3 trackingRotationOffset;

    /// <summary>
    /// Maps the VR target's position and rotation to the rig target using the specified offsets.
    /// </summary>
    public void Map()
    {
        // Map position from VR target with the position offset
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);

        // Map rotation from VR target with the rotation offset
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

/// <summary>
/// This class manages the IK (Inverse Kinematics) system for the VR rig, mapping the VR target positions (e.g., for the head and hands) to a rig (e.g., for the character in the game).
/// </summary>
public class IKTargetFollowVRRig : MonoBehaviour
{
    /// <summary>
    /// The mapping for the VR head, including position and rotation offsets.
    /// </summary>
    public VRMap head;

    /// <summary>
    /// The mapping for the VR left hand, including position and rotation offsets.
    /// </summary>
    public VRMap leftHand;

    /// <summary>
    /// The mapping for the VR right hand, including position and rotation offsets.
    /// </summary>
    public VRMap rightHand;

    /// <summary>
    /// The transform used as a constraint for the head's position, usually representing the body of the rig.
    /// </summary>
    public Transform headConstraint;

    /// <summary>
    /// The offset to apply to the body’s position relative to the head's position.
    /// </summary>
    public Vector3 headBodyOffset;

    /// <summary>
    /// The smoothness factor for turning (helps to smooth out body rotation towards the head's direction).
    /// </summary>
    public int turnSmoothness;

    /// <summary>
    /// Updates the rig's position and rotation to follow the VR targets (head, hands) with the specified offsets.
    /// </summary>
    private void Update()
    {
        // Set the rig's position based on the head constraint with an offset for the body position
        transform.position = headConstraint.position + headBodyOffset;

        // Smoothly rotate the rig's forward direction towards the head's forward direction on the plane (ignoring Y-axis)
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        // Map the head, left hand, and right hand positions and rotations to the rig targets
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
