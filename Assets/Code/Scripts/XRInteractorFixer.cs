using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorFixer : MonoBehaviour
{
    private XRInteractionManager xrManager;

    private void Start()
    {
        FixInteractors();
    }

    public void FixInteractors()
    {
        // Trouver le XRInteractionManager dans la scène
        xrManager = FindObjectOfType<XRInteractionManager>();

        if (xrManager != null)
        {
            // Réassigner le manager à tous les interactors
            foreach (var interactor in FindObjectsOfType<XRBaseInteractor>())
            {
                interactor.interactionManager = xrManager;
            }

            Debug.Log("✅ XR Interaction Manager réassigné !");
        }
        else
        {
            Debug.LogError("❌ Aucun XRInteractionManager trouvé dans la scène !");
        }
    }

    // Réassigner les interacteurs après chaque téléportation
    private void OnEnable()
    {
        LocomotionProvider[] locomotionProviders = FindObjectsOfType<LocomotionProvider>();

        foreach (var provider in locomotionProviders)
        {
            provider.endLocomotion += OnTeleportEnd;
        }
    }

    private void OnDisable()
    {
        LocomotionProvider[] locomotionProviders = FindObjectsOfType<LocomotionProvider>();

        foreach (var provider in locomotionProviders)
        {
            provider.endLocomotion -= OnTeleportEnd;
        }
    }

    private void OnTeleportEnd(LocomotionSystem locomotionSystem)
    {
        Debug.Log("🔄 Téléportation terminée, réassignation des interacteurs...");
        FixInteractors();
    }
}