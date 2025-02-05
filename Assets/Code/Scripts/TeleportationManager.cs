using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Scripts
{
    public class TeleportationManager : MonoBehaviour
    {
        public void ChangeDimension(string dimensionName,  Vector3? position = null)
        {
            StartCoroutine(LoadNewDimension(dimensionName, position));
            
        }
        
    
        private IEnumerator LoadNewDimension(string dimensionName, Vector3? position = null)
        {
            string oldscene = SceneManager.GetActiveScene().name;
            if (oldscene != dimensionName)
            {
                position = position == null ? gameObject.GetComponentInChildren<XROrigin>().gameObject.transform.position : position;
                Debug.Log("Loading new dimension...");
                string currentWorldKey = SceneManager.GetActiveScene().name;

                // Charger la nouvelle scène de manière asynchrone
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dimensionName, LoadSceneMode.Additive);
                while (asyncLoad != null && !asyncLoad.isDone)
                {
                    yield return null; // Attendre la prochaine frame
                }

                // Désactiver l'ancienne scène
                SceneManager.UnloadSceneAsync(currentWorldKey);
                GameObject player = gameObject.GetComponentInChildren<XROrigin>().gameObject;
                player.transform.position =(Vector3)position;
                FixInteractors();
                Debug.Log("Loaded new dimension !");
            }
        }
        
        public void FixInteractors()
        {
            // Trouver le XRInteractionManager dans la scène
            XRInteractionManager xrManager = FindObjectOfType<XRInteractionManager>();

            if (xrManager != null)
            {
                // Réassigner le manager à tous les interactors (Ray Interactor, Direct Interactor, etc.)
                foreach (var interactor in FindObjectsOfType<XRBaseInteractor>())
                {
                    interactor.interactionManager = xrManager;
                }

                // Réassigner le manager à tous les objets grabbables
                foreach (var interactable in FindObjectsOfType<XRBaseInteractable>())
                {
                    interactable.interactionManager = xrManager;
                }
            }
        }

    }
}