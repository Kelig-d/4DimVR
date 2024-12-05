using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manages teleportation artifacts, their interactions, and dimension switching.
/// </summary>
public class ArtefactdeTp : MonoBehaviour
{
    /// <summary>
    /// Core object linked to the teleportation artifact.
    /// </summary>
    public GameObject Coeur;

    /// <summary>
    /// Circle object displayed when the artifact is grabbed.
    /// </summary>
    public GameObject Cercle;

    // GameObjects representing fragments for different dimensions
    public GameObject CaillouARTMi7;
    public GameObject CaillouCERMi7;
    public GameObject GrabMi7;

    public GameObject CaillouARTZima;
    public GameObject CaillouCERZima;
    public GameObject GrabZima;

    public GameObject CaillouARTCp;
    public GameObject CaillouCERCp;
    public GameObject GrabCp;

    public GameObject CaillouARTBerceau;
    public GameObject CaillouCERBearcea;
    public GameObject GrabBerceau;

    /// <summary>
    /// XRGrabInteractable component used to manage interactions.
    /// </summary>
    private XRGrabInteractable grabInteractable;

    // Flags indicating which fragments have been activated
    private bool FragBerceau;
    private bool FragZima;
    private bool FragChronos;
    private bool FragMi;

    /// <summary>
    /// Initializes the artifact and subscribes to interaction events.
    /// </summary>
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        Cercle.SetActive(false);

        // Initialize flags and disable fragment objects
        FragBerceau = true;
        FragZima = true;
        FragMi = true;
        FragChronos = true;
		
        CaillouARTMi7.SetActive(false);
        CaillouCERMi7.SetActive(false);
        GrabMi7.SetActive(false);

        CaillouARTZima.SetActive(false);
        CaillouCERZima.SetActive(false);
        GrabZima.SetActive(false);

        CaillouARTCp.SetActive(false);
        CaillouCERCp.SetActive(false);
        GrabCp.SetActive(false);

        CaillouARTBerceau.SetActive(false);
        CaillouCERBearcea.SetActive(false);
        GrabBerceau.SetActive(false);

        if (grabInteractable != null)
        {
            // Subscribe to grab and release events
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component missing on this object!");
        }
    }

    /// <summary>
    /// Called when the artifact is grabbed. Activates relevant fragments.
    /// </summary>
    /// <param name="args">Event arguments for the grab interaction.</param>
    private void OnGrab(SelectEnterEventArgs args)
    {
        Cercle.SetActive(true);

        if (FragBerceau)
        {
            CaillouARTMi7.SetActive(true);
            CaillouCERMi7.SetActive(true);
            GrabMi7.SetActive(true);
        }

        if (FragChronos)
        {
            CaillouARTZima.SetActive(true);
            CaillouCERZima.SetActive(true);
            GrabZima.SetActive(true);
        }

        if (FragMi)
        {
            CaillouARTCp.SetActive(true);
            CaillouCERCp.SetActive(true);
            GrabCp.SetActive(true);
        }

        if (FragZima)
        {
            CaillouARTBerceau.SetActive(true);
            CaillouCERBearcea.SetActive(true);
            GrabBerceau.SetActive(true);
        }
    }

    /// <summary>
    /// Called when the artifact is released. Deactivates all fragments.
    /// </summary>
    /// <param name="args">Event arguments for the release interaction.</param>
    private void OnRelease(SelectExitEventArgs args)
    {
        Cercle.SetActive(false);

        CaillouARTMi7.SetActive(false);
        CaillouCERMi7.SetActive(false);
        GrabMi7.SetActive(false);

        CaillouARTZima.SetActive(false);
        CaillouCERZima.SetActive(false);
        GrabZima.SetActive(false);

        CaillouARTCp.SetActive(false);
        CaillouCERCp.SetActive(false);
        GrabCp.SetActive(false);

        CaillouARTBerceau.SetActive(false);
        CaillouCERBearcea.SetActive(false);
        GrabBerceau.SetActive(false);
    }

    /// <summary>
    /// Adds a dimension fragment to the artifact.
    /// </summary>
    /// <param name="dimensionName">The name of the dimension fragment to add.</param>
    public void AddDimension(string dimensionName)
    {
        switch (dimensionName)
        {
            case "Berceau":
                FragBerceau = true;
                break;

            case "ZimmaBlue":
                FragZima = true;
                break;

            case "Mi7":
                FragMi = true;
                break;

            case "ChronoPhagos":
                FragChronos = true;
                break;
        }
    }

    /// <summary>
    /// Changes the current dimension by loading the corresponding scene.
    /// </summary>
    /// <param name="dimensionName">The name of the new dimension to load.</param>
    public void ChangeDimension(string dimensionName)
    {
        StartCoroutine(LoadNewDimension(dimensionName));
    }

    /// <summary>
    /// Asynchronously loads a new dimension scene and unloads the current one.
    /// </summary>
    /// <param name="dimensionName">The name of the new dimension scene to load.</param>
    /// <returns>A coroutine handling the asynchronous scene loading.</returns>
    private IEnumerator LoadNewDimension(string dimensionName)
    {
        Debug.Log("Loading new dimension...");
        string currentWorldKey = SceneManager.GetActiveScene().name;

        // Asynchronously load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dimensionName, LoadSceneMode.Additive);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null; // Wait for the next frame
        }

        // Unload the current scene
        SceneManager.UnloadSceneAsync(currentWorldKey);
        Debug.Log("Loaded new dimension!");
    }
}
