using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

  public class Ennemi : MonoBehaviour
{
  private NavMeshAgent ennemi;

  [SerializeField]
  private Transform player;
  [SerializeField]
  private Transform placeEnnemi;

  //Distance entre le joueur et l'ennemi
  private float Distance;
  //Distance entre le joueur et l'ennemi
  private float DistanceDuSpawn;
  // Cible de l'ennemi
  public Transform Target;
  //Distance de poursuite
  public float distancePoursuite = 10;
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
       if (!isDead)
        {
          Distance = Vector3.Distance(player.position, transform.position);
          DistanceDuSpawn = Vector3.Distance(placeEnnemi.position, transform.position);
          //Debug.LogWarning("Distance : "+Distance);
          
          // ennemi dans le perimetre
          if (Distance < distancePoursuite){
            //animations.Play("ghost_run");
            ennemi.destination = player.position;
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            if(Distance < attackRange){
              attack();
            }
          }else if(Distance > distancePoursuite && DistanceDuSpawn > 2){
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
        if (Time.time > attackTime) {
            //Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
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
}
