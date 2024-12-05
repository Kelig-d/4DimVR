using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behavior of an enemy in the game. The enemy detects and reacts to players within a certain range, attacks when close, and takes damage.
/// </summary>
public class Ennemi : MonoBehaviour
{
    /// <summary>
    /// Reference to the red marker object that highlights the enemy’s target.
    /// </summary>
    public GameObject redMarker;
    
    /// <summary>
    /// Duration for how long the red marker stays visible.
    /// </summary>
    private float durationTimeRedMarker;

    /// <summary>
    /// Reference to the enemy's sound emitter (used for emitting sound when the enemy is active).
    /// </summary>
    public GameObject sonEnnemi;

    /// <summary>
    /// The NavMeshAgent component responsible for moving the enemy.
    /// </summary>
    public NavMeshAgent ennemi;

    /// <summary>
    /// Animator that handles the enemy's animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// LayerMask used for detecting the player in the environment.
    /// </summary>
    public LayerMask playerLayer;

    /// <summary>
    /// The current target the enemy is pursuing (usually the player).
    /// </summary>
    public GameObject Target = null;

    /// <summary>
    /// A backup target used in certain situations.
    /// </summary>
    public GameObject _Target = null;

    /// <summary>
    /// Reference to the SpawnerZone which controls enemy spawning.
    /// </summary>
    public SpawnerZone spawnerzone;

    /// <summary>
    /// Range within which the enemy will attack.
    /// </summary>
    public float attackRange;

    /// <summary>
    /// Time interval between consecutive attacks.
    /// </summary>
    public float attackRepeatTime = 1;

    /// <summary>
    /// The amount of damage the enemy does when attacking.
    /// </summary>
    public float TheDammage;

    /// <summary>
    /// The health of the enemy.
    /// </summary>
    public float enemyHealth;

    /// <summary>
    /// Range within which the enemy can detect the player.
    /// </summary>
    public float detectionRange;

    /// <summary>
    /// Interval time for periodic checks during enemy behavior execution.
    /// </summary>
    public float checkInterval;

    private float Distance;
    private float attackTime;
    private float nextCheckTime;
    private bool isDead = false;

    // Animator states and tags for different enemy actions
    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackTag = Animator.StringToHash("Attack");

    private const int Dissolve = 1;
    private const int Attack = 2;
    private const int Surprised = 3;

    /// <summary>
    /// Dictionary to store the enemy’s current status (whether it is attacking, surprised, or dissolving).
    /// </summary>
    private Dictionary<int, bool> EnemyStatus = new Dictionary<int, bool>
    {
        { Dissolve, false },
        { Attack, false },
        { Surprised, false },
    };

    private float dissolveValue = 1f;

    /// <summary>
    /// Array of mesh renderers for the enemy, used for visual effects like dissolving.
    /// </summary>
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;

    /// <summary>
    /// Property to check if the enemy is attacking.
    /// </summary>
    public bool IsAttacking => EnemyStatus[Attack];

    /// <summary>
    /// Property to check if the enemy is taking damage.
    /// </summary>
    public bool IsTakingDamages => EnemyStatus[Surprised];

    /// <summary>
    /// Property to check if the enemy is dead.
    /// </summary>
    public bool IsDead => EnemyStatus[Dissolve];

    void Start()
    {
        ennemi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        redMarker = transform.Find("redMarker").gameObject;
        redMarker.SetActive(false);
        sonEnnemi = transform.Find("SonEnnemi").gameObject;
        sonEnnemi.SetActive(false);
        attackTime = Time.time;
        StartCoroutine(EnnemiBehavior());
    }

    /// <summary>
    /// Updates the enemy's status, checking for attack, death, and damage conditions.
    /// </summary>
    private void UpdateStatus()
    {
        if (isDead && enemyHealth <= 0)
        {
            EnemyStatus[Dissolve] = true;
        }
        else if (!isDead)
        {
            EnemyStatus[Dissolve] = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
        {
            EnemyStatus[Attack] = true;
        }
        else
        {
            EnemyStatus[Attack] = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
        {
            EnemyStatus[Surprised] = true;
        }
        else
        {
            EnemyStatus[Surprised] = false;
        }
    }

    /// <summary>
    /// Coroutine that handles the enemy’s behavior, including moving towards the player and attacking.
    /// </summary>
    IEnumerator EnnemiBehavior()
    {
        while (!isDead)
        {
            if(animator.GetBool("isTakingDamages") == false)
            {
                findPlayer();
                SeePlayer();

                if (Target)
                {
                    sonEnnemi.SetActive(true);

                    if(Distance > attackRange)
                    {
                        ennemi.destination = Target.transform.position;
                        animator.SetBool("isMoving", true);
                        animator.SetBool("isWaiting", false);
                    } else if (Distance < attackRange)
                    {
                        ennemi.destination = ennemi.transform.position;
                        if (Time.time > attackTime)
                        {
                            attack();
                        }
                    }
                }
                else
                {
                    animator.SetBool("isMoving", false);
                    animator.SetBool("isWaiting", true);
                    sonEnnemi.SetActive(false);
                }
            }
            else
            {
                ennemi.destination = ennemi.transform.position;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// Executes an attack on the player if within range.
    /// </summary>
    void attack()
    {
        ennemi.destination = transform.position;
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        Target.transform.parent.GetComponent<Player>().TakeDamage(TheDammage);
        attackTime = Time.time + attackRepeatTime;
        StartCoroutine(ResetAttackAnimation(0.4f));
    }

    /// <summary>
    /// Applies damage to the enemy, and checks for death.
    /// </summary>
    /// <param name="damage">Amount of damage dealt to the enemy.</param>
    void ApplyDammage(float damage)
    {
        if (!isDead)
        {
            enemyHealth -= damage;
            animator.SetBool("isTakingDamages", true);
            StartCoroutine(ResetDamageAnimation(0.2f));
            if (enemyHealth <= 0) Dead();
        }
    }

    /// <summary>
    /// Resets the damage animation after a delay.
    /// </summary>
    /// <param name="delay">Time to wait before resetting the animation.</param>
    private IEnumerator ResetDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isTakingDamages", false);
        animator.SetBool("isMoving", true);
    }

    /// <summary>
    /// Resets the attack animation after a delay.
    /// </summary>
    /// <param name="delay">Time to wait before resetting the animation.</param>
    private IEnumerator ResetAttackAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isAttacking", false);
        if(Target && (Target != null))
        {
            animator.SetBool("isMoving", true); 
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private IEnumerator DelayOfDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject, 1);
    }
    
    /// <summary>
    /// Handles the enemy's death process, including animation and destruction.
    /// </summary>
    private void Dead()
    {
        isDead = true;
        ennemi.destination = ennemi.transform.position;
        animator.SetBool("isDead", true);
        if(spawnerzone != null){
            spawnerzone.nbEnnemies -=1;
        }
        StartCoroutine(DelayOfDeath(2));     
    }

    /// <summary>
    /// Finds the closest player to the enemy within the detection range.
    /// </summary>
    void findPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = detectionRange;
        GameObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            if(player == GameObject.Find("XR Origin (XR Rig)"))
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = player;
                }
            }
        }

        Target = closestPlayer;
        Distance = minDistance;
    }

    /// <summary>
    /// Trigger event to apply damage to the enemy when hit by a weapon.
    /// </summary>
    /// <param name="other">Collider that collided with the enemy.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arme"))
        {
            ApplyDammage(1);
        }
    }

    /// <summary>
    /// Displays the red marker to indicate the target's position.
    /// </summary>
    public void ShowRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(true); // Show the red marker
        }
    }

    /// <summary>
    /// Hides the red marker when the enemy is no longer focused on the player.
    /// </summary>
    public void HideRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(false); // Hide the red marker
        }
    }

    /// <summary>
    /// Detects when the enemy sees the player for the first time and activates the red marker.
    /// </summary>
    public void SeePlayer()
    {
        if(Target == null && _Target != null)
        {
            _Target = null;
        }

        // If the enemy sees the player for the "first" time
        if(Target != null && _Target == null)
        {
            if(!redMarker.activeSelf)
            {
                ShowRedMarker();
                _Target = Target;
                durationTimeRedMarker = Time.time;
            }
        }
        if(redMarker.activeSelf && (Time.time - durationTimeRedMarker > 1))
        {
            HideRedMarker();
        }
    }
}
