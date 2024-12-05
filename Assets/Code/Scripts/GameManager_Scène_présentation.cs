using UnityEngine;

/// <summary>
/// Manages the game state, including checking the color of spheres and changing the cube's color when all spheres are yellow.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GameManager.
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// Array holding references to all the spheres in the game.
    /// </summary>
    public GameObject[] spheres;

    /// <summary>
    /// Reference to the cube that will change color when all spheres are yellow.
    /// </summary>
    public GameObject cube;

    /// <summary>
    /// Initializes the GameManager singleton instance and ensures only one instance exists.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Checks if all spheres are yellow. If they are, changes the cube's color to red.
    /// </summary>
    public void CheckAllSpheres()
    {
        foreach (GameObject sphere in spheres)
        {
            // Checks if the sphere's color is not yellow
            if (sphere.GetComponent<Renderer>().material.color != Color.yellow)
            {
                return; // If any sphere is not yellow, stops checking further
            }
        }

        // If all spheres are yellow, change the cube's color to red
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}