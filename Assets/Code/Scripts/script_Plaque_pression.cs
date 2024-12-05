using UnityEngine;

public class PlaqueTrigger : MonoBehaviour
{
    /// <summary>
    /// The first sphere affected by the plaque trigger.
    /// </summary>
    public GameObject sphere1;

    /// <summary>
    /// The second sphere affected by the plaque trigger.
    /// </summary>
    public GameObject sphere2;

    private Renderer rendererSphere1;
    private Renderer rendererSphere2;

    /// <summary>
    /// The player object that will interact with the plaque trigger.
    /// </summary>
    public GameObject joueur;

    private bool isYellow1; ///< Tracks if the first sphere is yellow.
    private bool isYellow2; ///< Tracks if the second sphere is yellow.

    private bool isActivated = false; ///< Indicates if the plaque is activated.

    private Color Yellow; ///< Color for the yellow state of the spheres.
    private Color Black;  ///< Color for the black state of the spheres.

    void Start()
    {
        // Get the renderers of the spheres to change their material colors
        rendererSphere1 = sphere1.GetComponent<Renderer>();
        rendererSphere2 = sphere2.GetComponent<Renderer>();

        // Initialize color values
        Yellow = Color.yellow; // Yellow color
        Black = Color.black; // Black color
    }

    /// <summary>
    /// Called when an object enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered and the plaque is not already activated
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Mark the plaque as activated
            isYellow1 = rendererSphere1.material.color == Yellow; // Check if the first sphere is yellow
            isYellow2 = rendererSphere2.material.color == Yellow; // Check if the second sphere is yellow
            ChangeSpheresColor(); // Change the color of the spheres
        }
    }

    /// <summary>
    /// Called when the player exits the trigger zone.
    /// </summary>
    /// <param name="other">The collider that exited the trigger zone.</param>
    private void OnTriggerExit(Collider other)
    {
        // When the player exits the plaque, deactivate the plaque
        if (other.CompareTag("Player"))
        {
            isActivated = false;
        }
    }

    /// <summary>
    /// Alternates the color of the spheres between yellow and black.
    /// </summary>
    void ChangeSpheresColor()
    {
        // Toggle the color of the first sphere
        isYellow1 = !isYellow1;
        rendererSphere1.material.color = isYellow1 ? Yellow : Black;

        // Toggle the color of the second sphere
        isYellow2 = !isYellow2;
        rendererSphere2.material.color = isYellow2 ? Yellow : Black;

        // Check if all spheres are yellow and trigger an event in the GameManager
        GameManager.Instance.CheckAllSpheres();
    }
}
