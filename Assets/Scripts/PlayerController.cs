using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardMovementSpeed, upMovementSpeed, downMovementSpeed;

    [SerializeField] private Animator characterAnimator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground, groundFinish;

    [SerializeField] private Transform stackerTransform;

    void Update()
    {
        ForwardMovement();
        VerticalMovement();
        CheckGround();
    }

    private void ForwardMovement()
    {
        transform.position += transform.forward * Time.deltaTime * forwardMovementSpeed;
    }

    private void VerticalMovement()
    {
        if (Climber.Instance.isClimbing == false)
        {
            if (Input.GetMouseButton(0))
            {
                if (stackerTransform.childCount >= 1)
                {
                    transform.position += transform.up * Time.deltaTime * upMovementSpeed;
                }
                else if (transform.position.y > 0)
                {
                    transform.position -= transform.up * Time.deltaTime * downMovementSpeed;

                    characterAnimator.SetBool("isFalling", true);
                }
            }
            else if (transform.position.y > 0)
            {
                transform.position -= transform.up * Time.deltaTime * downMovementSpeed;

                characterAnimator.SetBool("isFalling", true);
            }
        }      
    }

    private void CheckGround()
    {
        if (IsCharacterGrounded() == true)
        {
            characterAnimator.SetBool("isFalling", false);
            characterAnimator.SetBool("isWalking", true);
        }
        else
        {
            characterAnimator.SetBool("isWalking", false);
        }

        if (IsCharacterGroundedFinish() == true)
        {
            forwardMovementSpeed = 0;

            characterAnimator.SetBool("isGroundedFinish", true);

            GameManager.Instance.OnGameCompleted();
        }
    }

    private bool IsCharacterGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }

    private bool IsCharacterGroundedFinish()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundFinish);
    }
}
