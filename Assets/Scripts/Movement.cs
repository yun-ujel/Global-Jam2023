using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputController inputC;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CursorPosition cursorPos;

    [Header("Speed Values")]
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float acceleration = 35f;
    float speedDivider;

    [Header("Rolling")]
    [SerializeField, Range(0f, 100f)] private float rollSpeed = 4f;
    [SerializeField, Range(0f, 10f)] float rollCooldown;
    float rollCooldownCounter;

    [SerializeField, Range(0f, 5f)] float rollDuration;
    [HideInInspector] public float rollCounter; // Time Spent rolling

    Vector2 direction;
    Vector2 rollDirection;

    Vector2 desiredVelocity;
    Vector2 velocity;

    private float maxSpeedChange;

    void Update()
    {
        direction = new Vector2(inputC.RetrieveXInput(), inputC.RetrieveYInput());
        if (direction.sqrMagnitude > 1f)
        {
            direction = direction.normalized;
        }

        if (inputC.RetrieveRoll() && rollCooldownCounter <= 0f)
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

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        if (rollCounter > 0f)
        {
            velocity = rollDirection * rollSpeed;

            rollCounter -= Time.deltaTime;
        }
        else
        {
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity = new Vector2
            (
                Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange),
                Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange)
            );
        }

        rb.velocity = velocity;
    }

    void Roll()
    {
        if (direction == Vector2.zero)
        {
            rollDirection = (cursorPos.position - new Vector2(transform.position.x, transform.position.y)).normalized;
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
            Debug.Log("ur dead lmao");
        }
    }
}
