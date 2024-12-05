using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Handles the functionality of a compass-like artifact to switch between dimensions.
/// </summary>
public class CompassDimensionChange : MonoBehaviour
{
    /// <summary>
    /// The index of the current dimension.
    /// </summary>
    private int currentDimensionIndex = 0;

    /// <summary>
    /// An array containing the names of all available dimensions.
    /// </summary>
    private string[] dimensionNames = { "Berceau", "ZimmaBlue", "Mi7", "ChronoPhagos" };

    /// <summary>
    /// Tracks the instance IDs of objects to prevent duplicates when switching dimensions.
    /// </summary>
    private HashSet<int> existingObjectIDs = new HashSet<int>();

    /// <summary>
    /// Initializes the component and subscribes to the activation event to trigger dimension switching.
    /// </summary>
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        // Add a listener to the activation event to trigger dimension switching
        grabbable.activated.AddListener(ChangeDimension);
    }

    /// <summary>
    /// Triggered when the compass is activated, initiating a dimension change.
    /// </summary>
    /// <param name="args">Event arguments for the activation.</param>
    public void ChangeDimension(ActivateEventArgs args)
    {
        StartCoroutine(LoadNewDimension());
    }

    /// <summary>
    /// Handles the asynchronous loading of a new dimension and unloading of the current one.
    /// </summary>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    private IEnumerator LoadNewDimension()
    {
        string currentWorldKey = dimensionNames[currentDimensionIndex];
        string newWorldKey = dimensionNames[(currentDimensionIndex + 1) % dimensionNames.Length];

        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newWorldKey, LoadSceneMode.Additive);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Wait until the next frame
        }

        // Update the current dimension index
        currentDimensionIndex = (currentDimensionIndex + 1) % dimensionNames.Length;

        // Unload the old scene
        SceneManager.UnloadSceneAsync(currentWorldKey);
    }
}
