using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample {
    public class GhostScript : MonoBehaviour
    {
        private Animator Anim;
        private CharacterController Ctrl;
        private Vector3 MoveDirection = Vector3.zero;

        // Références aux paramètres d'animation
        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
        private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
        private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
        private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
        private static readonly int AttackTag = Animator.StringToHash("Attack");

        [SerializeField] private SkinnedMeshRenderer[] MeshR;
        private float Dissolve_value = 1;
        private bool DissolveFlg = false;
        private const int maxHP = 3;
        private int HP = maxHP;

        // Vitesse de déplacement
        [SerializeField] private float Speed = 4;

        // Référence au joueur
        [SerializeField] private Transform player; // Assurez-vous de lier cette variable dans l'éditeur Unity

        void Start()
        {
            Anim = this.GetComponent<Animator>();
            Ctrl = this.GetComponent<CharacterController>();
        }

        void Update()
        {
            STATUS();
            GRAVITY();
            Respawn();

            // Vérifie si le joueur n'est pas en train de subir des effets
            if (!PlayerStatus.ContainsValue(true))
            {
                MOVE(); // Appel de la méthode de mouvement
                PlayerAttack();
                Damage();
            }
            else
            {
                HandlePlayerStatus();
            }

            // Dissoudre le fantôme si la santé est à zéro
            if (HP <= 0 && !DissolveFlg)
            {
                Anim.CrossFade(DissolveState, 0.1f, 0, 0);
                DissolveFlg = true;
            }
            else if (HP == maxHP && DissolveFlg)
            {
                DissolveFlg = false;
            }
        }

        private void HandlePlayerStatus()
        {
            int status_name = 0;
            foreach (var i in PlayerStatus)
            {
                if (i.Value == true)
                {
                    status_name = i.Key;
                    break;
                }
            }
            if (status_name == Dissolve)
            {
                PlayerDissolve();
            }
            else if (status_name == Attack)
            {
                PlayerAttack();
            }
            else if (status_name == Surprised)
            {
                // rien
            }
        }

        // Variables de statut du joueur
        private const int Dissolve = 1;
        private const int Attack = 2;
        private const int Surprised = 3;
        private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
        {
            {Dissolve, false },
            {Attack, false },
            {Surprised, false },
        };

        private void STATUS()
        {
            // Pendant la dissolution
            if (DissolveFlg && HP <= 0)
            {
                PlayerStatus[Dissolve] = true;
            }
            else if (!DissolveFlg)
            {
                PlayerStatus[Dissolve] = false;
            }

            // Pendant l'attaque
            if (Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
            {
                PlayerStatus[Attack] = true;
            }
            else
            {
                PlayerStatus[Attack] = false;
            }

            // Pendant le choc
            if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
            {
                PlayerStatus[Surprised] = true;
            }
            else
            {
                PlayerStatus[Surprised] = false;
            }
        }

        private void PlayerDissolve()
        {
            Dissolve_value -= Time.deltaTime;
            for (int i = 0; i < MeshR.Length; i++)
            {
                MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
            }
            if (Dissolve_value <= 0)
            {
                Ctrl.enabled = false;
            }
        }

        private void PlayerAttack()
        {
            // Condition d'attaque à implémenter ici
            if (Input.GetKeyDown(KeyCode.A))
            {
                Anim.CrossFade(AttackState, 0.1f, 0, 0);
            }
        }

        private void GRAVITY()
        {
            if (Ctrl.enabled)
            {
                if (CheckGrounded())
                {
                    if (MoveDirection.y < -0.1f)
                    {
                        MoveDirection.y = -0.1f;
                    }
                }
                MoveDirection.y -= 0.1f;
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
        }

        private bool CheckGrounded()
        {
            if (Ctrl.isGrounded && Ctrl.enabled)
            {
                return true;
            }
            Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
            float range = 0.2f;
            return Physics.Raycast(ray, range);
        }

        private void MOVE()
        {
            if (player != null) // Vérifie si le joueur est défini
            {
                // Calculer la direction vers le joueur
                Vector3 direction = (player.position - transform.position).normalized;
                MoveDirection = direction * Speed; // Déplacer le fantôme vers le joueur

                // Mise à jour de l'animation de mouvement
                if (direction.magnitude > 1.0f) // Si le fantôme se déplace
                {
                    Anim.SetBool("isMoving", true);
                }
                else
                {
                    Anim.SetBool("isMoving", false);
                }

                // Appliquer le mouvement
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
        }

        private void Damage()
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
                HP--;
            }
        }

        private void Respawn()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HP = maxHP;
                Ctrl.enabled = false;
                this.transform.position = Vector3.zero; // position de réapparition
                this.transform.rotation = Quaternion.Euler(Vector3.zero); // direction de réapparition
                Ctrl.enabled = true;

                Dissolve_value = 1;
                for (int i = 0; i < MeshR.Length; i++)
                {
                    MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
                }
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }
}