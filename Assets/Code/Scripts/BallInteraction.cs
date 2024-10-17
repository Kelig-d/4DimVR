using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallInteraction : MonoBehaviour
{
    public Rigidbody bounceBall; // L'objet auquel appliquer la force
    public float upwardForce = 500f; // La force à appliquer vers le haut

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        // Ajouter un listener à l'événement activated pour lancer l'interaction
        grabbable.activated.AddListener(Interaction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Méthode appelée lors de l'activation de l'objet
    void Interaction(ActivateEventArgs args)
    {
        if (bounceBall != null)
        {
            // Applique une force vers le haut à l'objet cible
            bounceBall.AddForce(Vector3.up * upwardForce);
        }
        else
        {
            Debug.LogWarning("Aucun objet cible n'a été assigné !");
        }
    }
}