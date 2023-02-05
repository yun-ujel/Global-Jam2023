using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputController inputController;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private CursorPosition cursorPosition;
    [SerializeField] private Attack attack;

    [Header("Speed Values")]
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float acceleration = 35f;

    [Header("Rolling")]
    [SerializeField, Range(0f, 100f)] private float rollSpeed = 4f;
    [SerializeField, Range(0f, 10f)] private float rollCooldown;
    private float rollCooldownCounter;

    [SerializeField, Range(0f, 5f)] private float rollDuration;
    [HideInInspector] public float rollCounter; // Time Spent rolling

    public Vector2 direction;
    Vector2 rollDirection;

    Vector2 desiredVelocity;
    public Vector2 velocity;

    public bool isAttacking;

    void Update()
    {
        direction = inputController.RetrieveXYInputs();

        isAttacking = inputController.RetrieveAttack();

        if (!isAttacking)
        {
            if (direction.sqrMagnitude > 1f)
            {
                direction = direction.normalized;
            }

            if (inputController.RetrieveSlide() && rollCooldownCounter <= 0f)
            {
                Roll();

                rollCooldownCounter = rollCooldown + rollDuration;
            }
            else
            {
                rollCooldownCounter -= Time.deltaTime;

                desiredVelocity = new Vector2(direction.x, direction.y) * maxSpeed;
            }
        }
        else
        {
            Debug.Log("Attacking");
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            attack.ResetDamage();

            velocity = body.velocity;

            if (rollCounter > 0f)
            {
                velocity = rollDirection * rollSpeed;

                rollCounter -= Time.deltaTime;
            }
            else
            {
                float maxSpeedChange = acceleration * Time.deltaTime;
                velocity = new Vector2
                (
                    Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange),
                    Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange)
                );
            }

            body.velocity = velocity;
        }
        else
        {
            body.velocity = Vector2.zero;
            direction = Vector2.zero;
            rollCounter = 0f;

            attack.DealDamage();
        }
    }

    void Roll()
    {
        if (direction == Vector2.zero)
        {
            rollDirection = (new Vector2(cursorPosition.worldPosition.x, cursorPosition.worldPosition.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
        else
        {
            rollDirection = direction;
        }
        rollCounter = rollDuration;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
