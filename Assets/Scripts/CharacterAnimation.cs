using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particleSys;
    private ParticleSystem.EmissionModule ps;
    bool isRolling;
    bool isWalking;
    bool isAttacking;


    private void Awake()
    {
        ps = particleSys.emission;
    }

    private void Update()
    {
        CalculateState();

        if (isRolling && !isAttacking)
        {
            animator.Play("roll");
            ps.rateOverTime = 20f;
        }
        else if (isWalking && !isAttacking)
        {
            animator.speed = 1f;
            animator.Play("walk");
            ps.rateOverTime = 5f;
        }
        else
        {
            animator.Play("walk");
            animator.speed = 0f;
            ps.rateOverTime = 0f;
        }

        if (movement.direction.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (movement.direction.x > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
    }

    void CalculateState()
    {
        isRolling = movement.rollCounter > 0f;

        isWalking = movement.velocity.sqrMagnitude > 0f;

        isAttacking = movement.isAttacking;
    }
}
