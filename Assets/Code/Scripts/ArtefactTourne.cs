using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the rotation of an artifact and manages its interactions with various cones.
/// Activates teleportation when certain conditions are met.
/// </summary>
public class ArtefactTourne : MonoBehaviour
{
    /// <summary>
    /// The artifact object that rotates.
    /// </summary>
    public GameObject Artefact;

    /// <summary>
    /// Indicates whether the artifact should rotate along the Y-axis.
    /// </summary>
    private bool rotatY = false;

    /// <summary>
    /// Current rotation angle of the artifact along the Y-axis.
    /// </summary>
    private double rotateY = 0;

    // Cone and activation objects for the first cone
    public GameObject Cone0;
    public GameObject Activator0;
    public bool up0;
    public Color C0;
    private bool Activer0;

    // Cone and activation objects for the second cone
    public GameObject Cone1;
    public Color C1;
    public GameObject Activator1;
    public bool up1;
    private bool Activer1;

    // Cone and activation objects for the third cone
    public GameObject Cone2;
    public Color C2;
    public GameObject Activator2;
    public bool up2;
    private bool Activer2;

    // Cone and activation objects for the fourth cone
    public GameObject Cone3;
    public Color C3;
    public GameObject Activator3;
    public bool up3;
    private bool Activer3;

    /// <summary>
    /// Flag to ensure the teleportation process is initiated only once.
    /// </summary>
    private bool setTP = true;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Called once per frame to handle updates.
    /// </summary>
    void Update()
    {
        FixedUpdate();
    }

    /// <summary>
    /// Coroutine to handle teleportation by loading a new scene and unloading the current one.
    /// </summary>
    /// <returns>An enumerator to manage the asynchronous teleportation process.</returns>
    private IEnumerator SetTp()
    {
        if (setTP)
        {
            print("top");
            setTP = false;

            // Load the new scene asynchronously
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BerceauUpgrade", LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null; // Wait for the next frame
            }

            // Unload the current scene
            SceneManager.UnloadSceneAsync("Garage0");
        }
    }

    /// <summary>
    /// Checks if a cone should be activated based on its rotation and updates its color accordingly.
    /// </summary>
    /// <param name="test">The cone object to update.</param>
    /// <param name="colortot">The color to apply if activated.</param>
    /// <param name="up">Direction flag for activation check.</param>
    /// <param name="ItemVerif">The activation object to check for rotation.</param>
    /// <returns>True if the cone is activated; otherwise, false.</returns>
    bool CheckUpdateColor(GameObject test, Color colortot, bool up, GameObject ItemVerif)
    {
        if (up)
        {
            if (ItemVerif.transform.eulerAngles.x > 270 && ItemVerif.transform.eulerAngles.x < 315)
            {
                test.GetComponent<Renderer>().material.color = colortot;
                return true;
            }
            else
            {
                test.GetComponent<Renderer>().material.color = Color.white;
                return false;
            }
        }
        else
        {
            if (ItemVerif.transform.eulerAngles.x > 30 && ItemVerif.transform.eulerAngles.x < 60)
            {
                test.GetComponent<Renderer>().material.color = colortot;
                return true;
            }
            else
            {
                test.GetComponent<Renderer>().material.color = Color.white;
                return false;
            }
        }
    }

    /// <summary>
    /// Called at fixed time intervals to handle rotation and cone activation logic.
    /// </summary>
    void FixedUpdate()
    {
        // Update rotation angle
        rotateY += Time.deltaTime * 15;

        if (rotateY > 360.0f)
        {
            rotateY = 0.0f;
            rotatY = false;
        }

        // Apply rotation to the artifact
        Artefact.transform.localRotation = Quaternion.Euler(0, (float)rotateY, 0);

        // Update activation states for cones
        Activer0 = CheckUpdateColor(Cone0, C0, up0, Activator0);
        Activer1 = CheckUpdateColor(Cone1, C1, up1, Activator1);
        Activer2 = CheckUpdateColor(Cone2, C2, up2, Activator2);
        Activer3 = CheckUpdateColor(Cone3, C3, up3, Activator3);

        // Check if all activation conditions are met
        if (Activer0 && Activer1 && Activer2 && Activer3)
        {
            // Uncomment the line below to enable teleportation
            // StartCoroutine(SetTp());
        }
    }
}
