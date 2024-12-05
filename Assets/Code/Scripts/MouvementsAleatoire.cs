using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the random movement behavior of an enemy character using Unity's NavMesh system.
/// The enemy moves randomly within a specified range, waits at destinations, and adjusts its movement behavior 
/// based on the enemy's state (e.g., attacking, taking damage, or dead).
/// </summary>
public class MouvementsAleatoire : MonoBehaviour
{
    private NavMeshAgent agent;  ///< The NavMeshAgent component attached to this GameObject.
    private Ennemi ennemiScript; ///< Reference to the Ennemi script attached to the same GameObject.
    
    private float range; ///< The radius within which the enemy can randomly move.
    private int haveToWait; ///< A random number determining whether the enemy has to wait at the destination.
    private int timeToWait; ///< The time (in seconds) the enemy should wait before moving again.
    private float checkNextMove; ///< Time when the enemy should check for the next move.

    /// <summary>
    /// Initializes the script by fetching necessary components.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Fetch the NavMeshAgent component
        checkNextMove = Time.time; // Set the initial time for the next move check
        ennemiScript = GetComponent<Ennemi>(); // Ensure that the Ennemi script is on the same GameObject
        
        if (ennemiScript == null)
        {
            Debug.LogError("The Ennemi script is missing on this GameObject!");
        }
    }

    /// <summary>
    /// Updates the enemy's movement behavior every frame.
    /// </summary>
    void Update()
    {   
        if(ennemiScript != null)
        {
            // Ensure the enemy is not attacking, taking damage, or dead
            if(ennemiScript.IsAttacking == false && ennemiScript.IsTakingDamages == false && ennemiScript.IsDead == false)
            {
                if(Time.time > checkNextMove)
                {
                    range = UnityEngine.Random.Range(5f, 10f); // Random movement range between 5 and 10 units
                    checkNextMove = Time.time + (range / agent.speed); // Set the next move check time based on speed

                    if (agent.remainingDistance <= agent.stoppingDistance) // If the current path is completed
                    {
                        Vector3 point;
                        if (RandomPoint(transform.position, range, out point)) // Get a valid random point within range
                        {
                            Debug.DrawRay(point, Vector3.up, Color.blue, 5.0f); // Debug: visualize the point with a ray
                            agent.SetDestination(point); // Set the new destination for the agent

                            // Determine if the agent has to wait
                            haveToWait = Random.Range(1, 4); // Randomly choose if the agent should wait (1 to 3)
                            if (haveToWait == 1)
                            {
                                // Randomly determine how long the agent should wait
                                timeToWait = Random.Range(1, 7); // Time to wait is between 1 and 7 seconds

                                // Start a coroutine to wait before the next movement
                                StartCoroutine(AttendreAvantProchaineDestination(timeToWait));
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Coroutine to make the agent wait for a specified time before moving again.
    /// </summary>
    /// <param name="secondes">The number of seconds to wait.</param>
    /// <returns>A coroutine that waits for the specified time.</returns>
    IEnumerator AttendreAvantProchaineDestination(int secondes)
    {
        agent.isStopped = true; // Stop the NavMeshAgent while waiting

        yield return new WaitForSecondsRealtime(secondes); // Wait for the specified time

        agent.isStopped = false; // Resume the NavMeshAgent after waiting
    }

    /// <summary>
    /// Attempts to find a random point on the NavMesh within a specified range of the center.
    /// </summary>
    /// <param name="center">The center point to search from.</param>
    /// <param name="range">The radius within which to find the random point.</param>
    /// <param name="result">The resulting random point on the NavMesh.</param>
    /// <returns>True if a valid point is found; false otherwise.</returns>
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        int attempts = 0;
        int maxAttempts = 10; // Maximum number of attempts to prevent an infinite loop
        result = Vector3.zero; // Initialize the result variable
        
        while (attempts < maxAttempts)
        {
            // Generate a random point within a sphere of "range" around the center
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            
            // Try to find a valid point on the NavMesh within the random point range
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                // Check if the point is within the valid range from the center
                if (Vector3.Distance(center, hit.position) <= range)
                {
                    result = hit.position; // Set the result to the valid point
                    return true; // Return true, indicating a valid point was found
                }
            }
            
            attempts++; // Increment the attempt counter
        }

        // If no valid point was found after the maximum number of attempts
        return false; // Return false, indicating no valid point was found
    }
}
