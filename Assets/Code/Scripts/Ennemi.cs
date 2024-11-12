
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Classes;
using UnityEngine;
using UnityEngine.AI;

  public class Ennemi : MonoBehaviour
{
  private NavMeshAgent ennemi;

  [SerializeField]
  private Transform placeEnnemi;
  
  public LayerMask playerLayer;

  //Distance entre le joueur et l'ennemi
  private float Distance;
  //Distance entre le joueur et l'ennemi
  private float DistanceDuSpawn;
  // Cible de l'ennemi
  public GameObject Target = null;
  //Distance de poursuite
  // Portée des attaques
  public float attackRange = 2.2f;
  // Cooldown des attaques
  public float attackRepeatTime = 1;
  private float attackTime;
  // Montant des dégâts infligés
  public float TheDammage;
  // Animations de l'ennemi
  private Animator animator;
  // Vie de l'ennemi
  public float enemyHealth;
  private bool isDead = false;
  public float detectionRange = 10f;
  public float checkInterval = 0.5f; // Intervalle entre les vérifications

  private float nextCheckTime;
  //Animations
  //private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
  //private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");

    // Start is called before the first frame update
    void Start()
    {
        ennemi = gameObject.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        attackTime = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        findPlayer();
        if (!isDead)
        {
          DistanceDuSpawn = Vector3.Distance(placeEnnemi.position, transform.position);
          //Debug.LogWarning("Distance : "+Distance);
          
          // ennemi dans le perimetre
          if (Target)
          {
              ennemi.destination = Target.transform.position;
              animator.SetBool("isMoving", true);
              animator.SetBool("isAttacking", false);
              if (Distance < attackRange)
              {
                  attack();
              }
          }
          else if(Distance > detectionRange && DistanceDuSpawn > 2){
            // ennemi loin, pas dans le perimetre
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            ennemi.destination = placeEnnemi.position;
          }else{
            //retour au spawn
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            ennemi.destination = ennemi.destination;
          }
        }
    }

    //attaque
    void attack()
    {
        // empeche l'ennemi de traverser le joueur
        ennemi.destination = transform.position;
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
 
        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            Target.transform.parent.GetComponent<Player>().TakeDamage(TheDammage);
            Debug.LogWarning("L'ennemi a envoyé " + TheDammage + " points de dégâts");
            Debug.Log("L'ennemi a envoyé " + TheDammage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    public void ApplyDammage(float TheDammage)
    {
        if (!isDead)
        {
            enemyHealth = enemyHealth - TheDammage;
            animator.SetBool("isTakingDamages", true);
            print(gameObject.name + "a subit " + TheDammage + " points de dégâts.");
 
            if(enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        Debug.LogWarning("Mort de l'ennemie");
        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(transform.gameObject, 5);
    }
    private void findPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float[] playerDistance = new float[players.Length];
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            int index = 0;
            foreach (GameObject p in players)
            {
                // Calculer la direction vers le joueur
                Vector3 directionToPlayer = p.transform.position - transform.position;

                // Envoyer un raycast pour vérifier la visibilité
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange,
                        playerLayer))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        // Le joueur est visible, maintenant calculons la distance
                        playerDistance[index] = directionToPlayer.magnitude;
                    }
                }
                else
                {
                    playerDistance[index] = -1;
                }

                index++;
            }

            if (playerDistance.Max() == -1)
            {
                Target = null;
            }
            else
            {
                int minDistanceIndex = Array.IndexOf(playerDistance, playerDistance.Min());
                Target = players[minDistanceIndex];
                Distance = playerDistance[minDistanceIndex];

            }
           
        }
    }
    
}

  