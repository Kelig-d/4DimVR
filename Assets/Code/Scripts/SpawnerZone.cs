using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZone : MonoBehaviour
{
    /// <summary>
    /// The enemy prefab to be spawned in the zone.
    /// </summary>
    public GameObject Ghost;

    /// <summary>
    /// The spawn positions within the zone.
    /// </summary>
    public Vector3 spawner1;
    public Vector3 spawner2;
    public Vector3 spawner3;
    public Vector3 spawner4;
    public Vector3 spawner5;
    public Vector3 spawner6;
    public Vector3 spawner7;
    public Vector3 spawner8;
    public Vector3 spawner9;
    public Vector3 spawner10;

    /// <summary>
    /// List holding all configured spawn positions.
    /// </summary>
    List<Vector3> tableauDeVecteur;

    /// <summary>
    /// The current number of enemies in the zone.
    /// </summary>
    public int nbEnnemies = 0;

    /// <summary>
    /// The maximum number of enemies allowed in the zone.
    /// </summary>
    public int nbMaxEnnemies;

    /// <summary>
    /// The initial delay before starting enemy spawn.
    /// </summary>
    public float timeToWaitAtTheStart;

    /// <summary>
    /// The delay between each enemy spawn.
    /// </summary>
    public float timeToWaitRespawn;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the list of spawn points
        tableauDeVecteur = new List<Vector3> { spawner1, spawner2, spawner3, spawner4, spawner5, spawner6, spawner7, spawner8, spawner9, spawner10 };

        // Remove unconfigured spawners (those set to Vector3.zero)
        for (int i = tableauDeVecteur.Count - 1; i >= 0; i--)
        {
            Vector3 spawner = tableauDeVecteur[i];
            if (spawner == Vector3.zero)
            {
                tableauDeVecteur.RemoveAt(i);
            }
        }

        // Start the continuous enemy spawn coroutine with an initial delay
        StartCoroutine(SpawnerEnnemisContinu());
    }

    /// <summary>
    /// Coroutine that continuously spawns enemies at random spawn points.
    /// </summary>
    /// <returns>Enumerator for coroutine control.</returns>
    IEnumerator SpawnerEnnemisContinu()
    {
        // Wait for the initial delay before spawning enemies
        yield return new WaitForSeconds(timeToWaitAtTheStart);

        // Continuously spawn enemies until the maximum limit is reached
        while (true)
        {
            // Check if the current number of enemies is below the maximum limit
            if (nbEnnemies < nbMaxEnnemies)
            {
                // Spawn a new enemy
                SpawnerUnEnnemi();

                // Wait for the respawn delay before spawning the next enemy
                yield return new WaitForSeconds(timeToWaitRespawn);
            }
            else
            {
                // If the maximum number of enemies is reached, check again after 1 second
                yield return new WaitForSeconds(1f);
            }
        }
    }

    /// <summary>
    /// Spawns a single enemy at a random spawn point.
    /// </summary>
    void SpawnerUnEnnemi()
    {
        // Choose a random spawn point
        int choixAleatoireDuSpawner = Random.Range(0, tableauDeVecteur.Count);

        // Instantiate the enemy at the chosen spawn point
        GameObject instantiated = Instantiate(Ghost);
        instantiated.transform.position = tableauDeVecteur[choixAleatoireDuSpawner];

        // Pass the reference of the spawn zone to the enemy for managing the number of enemies
        Ennemi ennemiScript = instantiated.GetComponent<Ennemi>();
        ennemiScript.spawnerzone = this;

        // Increment the current enemy count
        nbEnnemies++;
    }
}
