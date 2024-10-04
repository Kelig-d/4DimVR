using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    private bool canWalk = false;
    private int isWalkingHash;

    private ActionBasedContinuousMoveProvider subject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        subject = FindObjectOfType<ActionBasedContinuousMoveProvider>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool joystickMovement = subject.leftHandMoveAction.action.ReadValue<Vector2>().magnitude > 0.1f;
        if (!isWalking && joystickMovement)
            animator.SetBool(isWalkingHash, true );
        if (isWalking && !joystickMovement)
            animator.SetBool(isWalkingHash, false);
    }
}
