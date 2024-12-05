using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RadialSelection : MonoBehaviour
{
    /// <summary>
    /// The input action reference for the spawn button (trigger to open radial menu).
    /// </summary>
    public InputActionReference spawnButton;

    /// <summary>
    /// The number of parts in the radial menu.
    /// </summary>
    [Range(2, 10)]
    public int numberOfRadialPart;

    /// <summary>
    /// The prefab for the individual radial parts.
    /// </summary>
    public GameObject radialPartPrefab;

    /// <summary>
    /// The canvas that holds the radial parts.
    /// </summary>
    public Transform radialPartCanvas;

    /// <summary>
    /// The hand transform (used to position the radial menu near the player's hand).
    /// </summary>
    public Transform handTransform;

    /// <summary>
    /// The angle between each radial part.
    /// </summary>
    public float angleBetweenPart = 10;

    /// <summary>
    /// Unity event triggered when a radial part is selected.
    /// </summary>
    public UnityEvent<int> OnPartSelected;

    // Private fields
    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    private List<string> TextMenue;
    private bool select;

    /// <summary>
    /// Initializes the radial selection by subscribing to the button press event.
    /// </summary>
    void Start()
    {
        spawnButton.action.started += ButtonWasPressed;
        // spawnButton.action.canceled += ButtonWasReleased;  // Optional, if releasing button is needed
    }

    /// <summary>
    /// Handles button press event to trigger radial menu and scene transition.
    /// </summary>
    /// <param name="context">The context for the input action.</param>
    public void ButtonWasPressed(InputAction.CallbackContext context)
    {
        StartCoroutine(LoadNewDimension());
        select = true;
    }

    /// <summary>
    /// Coroutine to load a new scene asynchronously.
    /// Unloads the current scene after the new scene has been loaded.
    /// </summary>
    /// <returns>IEnumerator for coroutine execution.</returns>
    private IEnumerator LoadNewDimension()
    {
        string oldscene = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);

        // Wait until the scene is fully loaded
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the old scene after loading the new one
        SceneManager.UnloadSceneAsync(oldscene);
    }

    // The following methods (ButtonWasReleased, HideAndTriggerSelected, GetSelectedRadialPart, SpawnRadialPart)
    // are commented out for now but can be used to manage the radial menu interaction.

    /*
    public void ButtonWasReleased(InputAction.CallbackContext context)
    {
        HideAndTriggerSelected();
        select = false;
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
        {
            angle += 360;
        }

        currentSelectedRadialPart = (int)(angle * numberOfRadialPart / 360);

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.green;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
        }
    }

    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = -i * 360 / numberOfRadialPart - angleBetweenPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedRadialPart.GetComponent<Image>().fillAmount = (1 / (float)numberOfRadialPart) - (angleBetweenPart / 360);

            spawnedParts.Add(spawnedRadialPart);
        }

        TextMesh test = FindAnyObjectByType<TextMesh>();
        test.bool();
    }
    */
}
