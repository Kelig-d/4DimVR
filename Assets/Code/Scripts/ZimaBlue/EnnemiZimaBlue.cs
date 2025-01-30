using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiZimaBlue : MonoBehaviour
{
    public GameObject redMarker;
    private float durationTimeRedMarker;
    public GameObject sonEnnemi;
    public NavMeshAgent ennemi;
    private Animator animator;
    public LayerMask playerLayer;
    public GameObject Target = null;
    public GameObject _Target = null;
    public SpawnerZoneZimaBlue spawnerzone;
    
    public float attackRange;
    public float attackRepeatTime = 1;
    public float TheDammage;
    public float enemyHealth;
    public float detectionRange;
    public float checkInterval;
    
    private float Distance;
    private float attackTime;
    private float nextCheckTime;
    private bool isDead = false;
    
    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackTag = Animator.StringToHash("Attack");

    private const int Dissolve = 1;
    private const int Attack = 2;
    private const int Surprised = 3;
    
    private Dictionary<int, bool> EnemyStatus = new Dictionary<int, bool>
    {
        { Dissolve, false },
        { Attack, false },
        { Surprised, false },
    };

    private float dissolveValue = 1f;
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;
    public bool IsAttacking => EnemyStatus[Attack];
    public bool IsTakingDamages => EnemyStatus[Surprised];
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

    private void Update()
    {
        UpdateStatus();
        StartCoroutine(EnnemiBehavior());
    }

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

                    //Debug.LogWarning(Target);
                    if(Distance > attackRange)
                    {
                        ennemi.destination = Target.transform.position;
                        animator.SetBool("isMoving", true);
                        animator.SetBool("isWaiting", false);
                    }else if (Distance < attackRange)
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
            }else{
                ennemi.destination = ennemi.transform.position;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void attack()
    {
        ennemi.destination = transform.position;
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        //Debug.LogWarning("Degat");
        Target.transform.parent.GetComponent<Player>().TakeDamage(TheDammage);
        attackTime = Time.time + attackRepeatTime;
        StartCoroutine(ResetAttackAnimation(0.4f));
    }

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

    private IEnumerator ResetDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isTakingDamages", false);
        animator.SetBool("isMoving", true);
    }
    private IEnumerator ResetAttackAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isAttacking", false);
        if(Target && (Target != null))
        {
            animator.SetBool("isMoving", true); 
        }else{
            animator.SetBool("isMoving", false);
        }
    }

    private IEnumerator DelayOfDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject, 1);
    }
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
        //Debug.LogWarning(Target);
        Distance = minDistance;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Alternative si tu utilises des triggers au lieu de colliders normaux
        if (other.CompareTag("Arme"))
        {
            ApplyDammage(1);
        }
    }
    public void ShowRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(true); // Affiche le point rouge
        }
    }

    public void HideRedMarker()
    {
        if (redMarker != null)
        {
            redMarker.SetActive(false); // Cache le point rouge
        }
    }

    public void SeePlayer()
    {
        // reset de _Target
        if(Target == null && _Target != null)
        {
            _Target = null;
        }

        // Si l'ennemie voit le joueur pour le "première" fois
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