using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class controls the behavior of a skeleton minion enemy. It includes detection of the player, movement, attacking, and health management.
/// </summary>
public class EnnemiSkeletonMinion : MonoBehaviour
{
    /// <summary>
    /// The red marker object indicating the target’s position for the enemy.
    /// </summary>
    public GameObject redMarker;

    /// <summary>
    /// The duration for how long the red marker stays visible.
    /// </summary>
    private float durationTimeRedMarker;

    /// <summary>
    /// Sound emitter for the enemy.
    /// </summary>
    public GameObject sonEnnemi;

    /// <summary>
    /// NavMeshAgent component to control enemy movement.
    /// </summary>
    public NavMeshAgent ennemi;

    /// <summary>
    /// Animator to control enemy animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// LayerMask for detecting players in the game.
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
    /// Reference to the SpawnerZone for spawning the enemy.
    /// </summary>
    public SpawnerZone spawnerzone;

    /// <summary>
    /// The range within which the enemy will attack.
    /// </summary>
    public float attackRange = 1.0f;

    /// <summary>
    /// The repeat time between consecutive attacks.
    /// </summary>
    public float attackRepeatTime = 1;

    /// <summary>
    /// The amount of damage the enemy inflicts on the player.
    /// </summary>
    public float TheDammage;

    /// <summary>
    /// The health of the enemy.
    /// </summary>
    public float enemyHealth;

    /// <summary>
    /// The detection range for the enemy to see the player.
    /// </summary>
    public float detectionRange = 10f;

    /// <summary>
    /// The time interval between checks during enemy behavior execution.
    /// </summary>
    public float checkInterval = 0.5f;

    /// <summary>
    /// The distance to the current target.
    /// </summary>
    private float Distance;

    /// <summary>
    /// The time when the enemy last attacked.
    /// </summary>
    private float attackTime;

    /// <summary>
    /// The time for the next behavior check.
    /// </summary>
    private float nextCheckTime;

    /// <summary>
    /// Indicates whether the enemy is dead.
    /// </summary>
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
    /// Dictionary holding the status of the enemy.
    /// </summary>
    private Dictionary<int, bool> EnemyStatus = new Dictionary<int, bool>
    {
        { Dissolve, false },
        { Attack, false },
        { Surprised, false },
    };

    private float dissolveValue = 1f;

    /// <summary>
    /// Array of mesh renderers for the enemy, used for the dissolving effect.
    /// </summary>
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;

    /// <summary>
    /// Property indicating whether the enemy is currently attacking.
    /// </summary>
    public bool IsAttacking => EnemyStatus[Attack];

    /// <summary>
    /// Property indicating whether the enemy is currently taking damage.
    /// </summary>
    public bool IsTakingDamages => EnemyStatus[Surprised];

    /// <summary>
    /// Property indicating whether the enemy is dead.
    /// </summary>
    public bool IsDead => EnemyStatus[Dissolve];

    /// <summary>
    /// Initializes the enemy's components and starts the behavior coroutine.
    /// </summary>
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
    /// Updates the status of the enemy, including checking for attack or death conditions.
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
    /// Handles the dissolving animation and disables the enemy once dissolved.
    /// </summary>
    private void HandleDissolve()
    {
        if (EnemyStatus[Dissolve])
        {
            dissolveValue -= Time.deltaTime;
            foreach (var renderer in meshRenderers)
            {
                renderer.material.SetFloat("_Dissolve", dissolveValue);
            }
            if (dissolveValue <= 0)
            {
                ennemi.enabled = false;
            }
        }
    }

    /// <summary>
    /// Coroutine handling the main behavior of the enemy, including moving toward the player, attacking, and waiting.
    /// </summary>
    IEnumerator EnnemiBehavior()
    {
        while (!isDead)
        {
            if (!animator.GetBool("isTakingDamages"))
            {
                findPlayer();
                SeePlayer();

                if (Target)
                {
                    sonEnnemi.SetActive(true);

                    if (Distance > attackRange)
                    {
                        ennemi.destination = Target.transform.position;
                        animator.SetBool("isMoving", true);
                        animator.SetBool("isWaiting", false);
                    }
                    else if (Distance < attackRange)
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
    /// Executes the attack on the player if within range.
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
    /// Applies damage to the enemy and checks for death if health reaches 0.
    /// </summary>
    /// <param name="damage">The amount of damage dealt to the enemy.</param>
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
    private IEnumerator ResetDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isTakingDamages", false);
        animator.SetBool("isMoving", true);
    }

    /// <summary>
    /// Resets the attack animation after a delay.
    /// </summary>
    private IEnumerator ResetAttackAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isAttacking", false);
        if (Target && (Target != null))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Handles the death of the enemy, triggering animations and cleanup.
    /// </summary>
    private void Dead()
    {
        isDead = true;
        ennemi.destination = ennemi.transform.position;
        animator.SetBool("isDead", true);
        spawnerzone.nbEnnemies -= 1;
        Destroy(gameObject, 1);
    }

    /// <summary>
    /// Finds the nearest player within the detection range.
    /// </summary>
    void findPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = detectionRange;
        GameObject closestPlayer = null;

        foreach (GameObject player in players)
        {
            if (player == GameObject.Find("XR Origin (XR Rig)"))
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
    /// Detects when the enemy sees the player for the first time and activates the red marker.
    /// </summary>
    public void SeePlayer()
    {
        if (Target == null && _Target != null)
        {
            _Target = null;
        }

        if (Target != null && _Target == null)
        {
            if (!redMarker.activeSelf)
            {
                ShowRedMarker();
                _Target = Target;
                durationTimeRedMarker = Time.time;
            }
        }
        if (redMarker.activeSelf && (Time.time - durationTimeRedMarker > 1))
        {
            HideRedMarker();
        }
    }

    /// <summary>
    /// Shows the red marker to indicate the target's position.
    /// </summary>
    public void ShowRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the red marker when the enemy stops seeing the player.
    /// </summary>
    public void HideRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(false);
        }
    }
}
