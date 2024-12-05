using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Ghost kill test
using UnityEngine.XR;

namespace Sample
{
    /// <summary>
    /// Handles the behavior and interaction of a ghost character in the game.
    /// This includes animation control, movement, health management, and response to player actions like attack and damage.
    /// </summary>
    public class GhostScript : MonoBehaviour
    {
        /// <summary>
        /// Reference to the <see cref="Animator"/> for controlling the ghost's animations.
        /// </summary>
        private Animator Anim;

        /// <summary>
        /// Reference to the <see cref="CharacterController"/> for managing the ghost's movement.
        /// </summary>
        private CharacterController Ctrl;

        /// <summary>
        /// The direction the ghost is moving in.
        /// </summary>
        private Vector3 MoveDirection = Vector3.zero;

        // Cache hash values for animation states
        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
        private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
        private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
        private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
        private static readonly int AttackTag = Animator.StringToHash("Attack");

        /// <summary>
        /// References to the <see cref="SkinnedMeshRenderer"/> components for dissolving the ghost.
        /// </summary>
        [SerializeField] private SkinnedMeshRenderer[] MeshR;

        /// <summary>
        /// Value controlling the dissolve effect of the ghost.
        /// </summary>
        private float Dissolve_value = 1;

        /// <summary>
        /// Flag to indicate whether the ghost is currently dissolving.
        /// </summary>
        private bool DissolveFlg = false;

        private const int maxHP = 3;

        /// <summary>
        /// Current health points of the ghost.
        /// </summary>
        private int HP = maxHP;

        /// <summary>
        /// Reference to the UI text element displaying the ghost's health.
        /// </summary>
        private Text HP_text;

        // Ghost kill test
        private InputDevice rightController;

        /// <summary>
        /// Movement speed of the ghost.
        /// </summary>
        [SerializeField] private float Speed = 4;

        /// <summary>
        /// Initializes references and setups the ghost's state.
        /// </summary>
        void Start()
        {
            Anim = this.GetComponent<Animator>();
            Ctrl = this.GetComponent<CharacterController>();
            HP_text = GameObject.Find("Canvas/HP").GetComponent<Text>();
            HP_text.text = "HP " + HP.ToString();

            // Ghost kill test: Get the right hand controller (Meta Quest 2)
            var rightHandDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

            if (rightHandDevices.Count > 0)
            {
                rightController = rightHandDevices[0];
            }
            else
            {
                Debug.LogError("No right hand controller detected.");
            }
        }

        /// <summary>
        /// Updates the ghost's state every frame.
        /// </summary>
        void Update()
        {
            // Check if the right controller is valid
            if (rightController.isValid)
            {
                // Check if the A button is pressed
                if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
                {
                    Debug.Log("A button pressed on Meta Quest 2!");
                    // Execute the code when the A button is pressed
                }
            }
            else
            {
                Debug.LogWarning("Right hand controller not valid. Reconnecting...");
                Start();
            }

            STATUS();
            GRAVITY();
            Respawn();

            // Check for current player status
            if (!PlayerStatus.ContainsValue(true))
            {
                MOVE();
                PlayerAttack();
                Damage();
            }
            else if (PlayerStatus.ContainsValue(true))
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
                    // No action during surprised state
                }
            }

            // Handle dissolve effect when health reaches 0
            if (HP <= 0 && !DissolveFlg)
            {
                Anim.CrossFade(DissolveState, 0.1f, 0, 0);
                DissolveFlg = true;
            }
            // Reset dissolve effect on respawn
            else if (HP == maxHP && DissolveFlg)
            {
                DissolveFlg = false;
            }
        }

        //---------------------------------------------------------------------
        // Character Status Handling
        //---------------------------------------------------------------------

        private const int Dissolve = 1;
        private const int Attack = 2;
        private const int Surprised = 3;

        private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
        {
            {Dissolve, false},
            {Attack, false},
            {Surprised, false},
        };

        /// <summary>
        /// Updates the current status of the ghost based on animation states.
        /// </summary>
        private void STATUS()
        {
            // During dissolve
            if (DissolveFlg && HP <= 0)
            {
                PlayerStatus[Dissolve] = true;
            }
            else if (!DissolveFlg)
            {
                PlayerStatus[Dissolve] = false;
            }

            // During attacking
            if (Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
            {
                PlayerStatus[Attack] = true;
            }
            else if (Anim.GetCurrentAnimatorStateInfo(0).tagHash != AttackTag)
            {
                PlayerStatus[Attack] = false;
            }

            // During damage
            if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
            {
                PlayerStatus[Surprised] = true;
            }
            else if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash != SurprisedState)
            {
                PlayerStatus[Surprised] = false;
            }
        }

        /// <summary>
        /// Handles the dissolve effect by fading out the ghost's material.
        /// </summary>
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

        /// <summary>
        /// Triggers the ghost's attack animation.
        /// </summary>
        private void PlayerAttack()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Anim.CrossFade(AttackState, 0.1f, 0, 0);
            }
        }

        //---------------------------------------------------------------------
        // Gravity Handling
        //---------------------------------------------------------------------

        /// <summary>
        /// Applies gravity to the ghost to simulate falling.
        /// </summary>
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

        //---------------------------------------------------------------------
        // Ground Check
        //---------------------------------------------------------------------

        /// <summary>
        /// Checks whether the ghost is currently grounded.
        /// </summary>
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

        //---------------------------------------------------------------------
        // Movement Handling
        //---------------------------------------------------------------------

        /// <summary>
        /// Controls the movement of the ghost based on keyboard input.
        /// </summary>
        private void MOVE()
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
            {
                if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
                }
                else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
                }
                else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
                {
                    MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, 270, 0));
                }
            }
            KEY_DOWN();
            KEY_UP();
        }

        /// <summary>
        /// Sets the movement velocity and rotation for the ghost.
        /// </summary>
        private void MOVE_Velocity(Vector3 velocity, Vector3 rot)
        {
            MoveDirection = new Vector3(velocity.x, MoveDirection.y, velocity.z);
            if (Ctrl.enabled)
            {
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
            MoveDirection.x = 0;
            MoveDirection.z = 0;
            this.transform.rotation = Quaternion.Euler(rot);
        }

        //---------------------------------------------------------------------
        // Handle Key Presses
        //---------------------------------------------------------------------

        /// <summary>
        /// Handles key press events for movement input.
        /// </summary>
        private void KEY_DOWN()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
        }

        /// <summary>
        /// Handles key release events for movement input.
        /// </summary>
        private void KEY_UP()
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    Anim.CrossFade(IdleState, 0.1f, 0, 0);
                }
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    Anim.CrossFade(IdleState, 0.1f, 0, 0);
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
                {
                    Anim.CrossFade(IdleState, 0.1f, 0, 0);
                }
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
                {
                    Anim.CrossFade(IdleState, 0.1f, 0, 0);
                }
            }
        }

        //---------------------------------------------------------------------
        // Damage Handling
        //---------------------------------------------------------------------

        /// <summary>
        /// Handles damage taken by the ghost.
        /// </summary>
        private void Damage()
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
                HP--;
                HP_text.text = "HP " + HP.ToString();
            }
        }

        //---------------------------------------------------------------------
        // Respawn Handling
        //---------------------------------------------------------------------

        /// <summary>
        /// Respawns the ghost to its initial position with full health.
        /// </summary>
        private void Respawn()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HP = maxHP;
                Ctrl.enabled = false;
                this.transform.position = Vector3.zero;
                this.transform.rotation = Quaternion.Euler(Vector3.zero);
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
