using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementsAleatoireMi7 : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnnemiMi7 ennemiScript;
    
    //private Animator animator;
    private float range; // radius of sphere
    private int haveToWait;
    private int timeToWait;
    private float checkNextMove;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        checkNextMove = Time.time;
        ennemiScript = GetComponent<EnnemiMi7>(); // Assure-toi que Ennemi est bien sur le même GameObject
        
        if (ennemiScript == null)
        {
            Debug.LogError("Le script Ennemi est manquant sur ce GameObject !");
        }
    }

    void Update()
    {   
        if(ennemiScript != null)
        {///// C'est ici que ca merde PUTAIN
            if(ennemiScript.IsAttacking == false && ennemiScript.IsTakingDamages == false && ennemiScript.IsDead == false)
            {
                if(Time.time > checkNextMove)
                {
                    range = UnityEngine.Random.Range(5f, 10f);
                    checkNextMove = Time.time + (range/agent.speed);

                    if (agent.remainingDistance <= agent.stoppingDistance) // Done with path
                    {
                        //Debug.LogWarning("Oui");
                        Vector3 point;
                        if (RandomPoint(transform.position, range, out point)) // Pass in our centre point and radius of area
                        {
                            Debug.DrawRay(point, Vector3.up, Color.blue, 5.0f); // So you can see with gizmos
                            agent.SetDestination(point);
                            //Debug.LogWarning("Point valide trouvé et affecté comme destination.");


                            // Déterminer si l'agent doit attendre
                            haveToWait = Random.Range(1, 4);
                            if (haveToWait == 1)
                            {
                                ennemiScript.presenceOndeLumineuse = false;
                                // Déterminer le temps d'attente en secondes
                                timeToWait = Random.Range(1, 7);
                                //Debug.LogWarning("L'agent attend pendant " + timeToWait + " secondes.");

                                // Démarrer une coroutine pour attendre
                                StartCoroutine(AttendreAvantProchaineDestination(timeToWait));
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator AttendreAvantProchaineDestination(int secondes)
    {
        // Désactiver le NavMeshAgent pendant l'attente
        agent.isStopped = true;

        // Attendre le nombre de secondes spécifié
        yield return new WaitForSecondsRealtime(secondes);

        // Réactiver le NavMeshAgent après l'attente
        agent.isStopped = false;
        ennemiScript.presenceOndeLumineuse = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        int attempts = 0;
        int maxAttempts = 10; // Limite de tentatives pour éviter une boucle infinie
        result = Vector3.zero; // Initialisation de la variable de sortie
        
        while (attempts < maxAttempts)
        {
            // Génère un point aléatoire dans une sphère de rayon "range" autour du centre
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            
            // Essaie de trouver un point navigable sur le NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                // Vérifie que le point est bien dans la distance maximale
                if (Vector3.Distance(center, hit.position) <= range)
                {
                    result = hit.position;
                    return true; // Point valide trouvé et respectant la distance maximale
                }
            }
            
            attempts++;
        }

        // Aucun point valide trouvé après le maximum de tentatives
        //Debug.LogWarning("Aucun point valide trouvé dans la limite de distance.");
        return false;
    }
}