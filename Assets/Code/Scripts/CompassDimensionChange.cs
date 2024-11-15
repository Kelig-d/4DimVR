using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class CompassDimensionChange : MonoBehaviour
{
    private int currentDimensionIndex = 0;
    private string[] dimensionNames = { "Berceau", "ZimmaBlue", "Mi7", "ChronoPhagos" };
    
    private HashSet<int> existingObjectIDs = new HashSet<int>(); // Pour suivre les IDs d'instance des objets

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        // Ajouter un listener à l'événement activated pour lancer l'interaction
        grabbable.activated.AddListener(ChangeDimension);
    }
    // Update is called once per frame
    public void ChangeDimension(ActivateEventArgs args)
    {
        StartCoroutine(LoadNewDimension());
    }
    
    private IEnumerator LoadNewDimension()
    {
        string currentWorldKey = dimensionNames[currentDimensionIndex];
        string newWorldKey = dimensionNames[(currentDimensionIndex + 1) % dimensionNames.Length];

        // Charger la nouvelle scène de manière asynchrone
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newWorldKey, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null; // Attendre la prochaine frame
        }

        
        // Passer à la dimension suivante
        currentDimensionIndex = (currentDimensionIndex + 1) % dimensionNames.Length;

        // Désactiver l'ancienne scène
        SceneManager.UnloadSceneAsync(currentWorldKey);
    }
}

