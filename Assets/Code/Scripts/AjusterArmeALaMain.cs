using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableOffset : XRGrabInteractable
{
    public Vector3 positionOffset;  // Offset de position par rapport à la main
    public Vector3 rotationOffset;  // Offset de rotation en degrés (X, Y, Z)

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Ajuster l'offset de position
        transform.localPosition += positionOffset;

        // Ajuster l'offset de rotation
        transform.localEulerAngles += rotationOffset;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Réinitialiser la position et la rotation
        transform.localPosition -= positionOffset;
        transform.localEulerAngles -= rotationOffset;
    }
}