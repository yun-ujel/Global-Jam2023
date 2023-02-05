using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("References")]
    //f[SerializeField] private InputController inputController;
    [SerializeField] private BossAI inputController;

    [SerializeField] private Rigidbody rb;
    public GameObject hitBox;

    [Header("Sliding")]
    [SerializeField, Range(0f, 100f)] private float slideAcceleration;
    [SerializeField, Range(0f, 100f)] private float maxSlideSpeed;
    [SerializeField, Range(0f, 100f)] private float maxSlideDistance;
    private bool isSliding;

    Vector2 moveDir;
    Vector2 rotateDir;

    [Header("Flipping")]
    [SerializeField] private float flipRotationSpeed = 120f;
    private Vector3 rotatePivot;
    private float rotationProgress;
    private bool isFlipping;

    [Header("Difficulty")]
    public float actionCooldown;
    private float actionCooldownCounter;
    [SerializeField] private float slideStartup;

    void Update()
    {
        if (!isSliding && !isFlipping)
        {
            GetInputs();
        }
        else if (isSliding)
        {
            SetHitbox();
        }
    }

    private void FixedUpdate()
    {
        if (actionCooldownCounter < 0f)
        {
            if (isSliding)
            {
                DoSlide(moveDir * maxSlideSpeed);
            }
            else if (isFlipping)
            {
                hitBox.SetActive(false);
                DoFlip(moveDir);
            }
        }
        else
        {
            actionCooldownCounter -= Time.deltaTime;
        }
        
    }

    void GetInputs()
    {

        Vector2 direction = inputController.RetrieveXYInputs();
        if (inputController.RetrieveSlide())
        {
            if (direction.x != 0)
            {
                LockPosition();
                moveDir = new Vector2(direction.x, 0);
                isSliding = true;
            }
            else if (direction.y != 0)
            {
                LockPosition();
                moveDir = new Vector2(0, direction.y);
                isSliding = true;
            }
        }
        else
        {
            if (direction.x != 0 && direction.y != 0)
            {
                int flipCoin = Random.Range(0, 1);

                if (flipCoin == 1)
                {
                    FlipHorizontal(direction);
                }
                else
                {
                    FlipVertical(direction);
                }
            }
            else  if (direction.x != 0)
            {
                FlipHorizontal(direction);
            }
            else if (direction.y != 0)
            {
                FlipVertical(direction);
            }
        }
    }

    void FlipHorizontal(Vector2 direction)
    {
        LockPosition();

        moveDir = new Vector2(direction.x, 0);
        rotateDir = new Vector2(0, -direction.x);

        rotatePivot = new Vector3(transform.position.x + (moveDir.x * (transform.localScale.x * 0.5f)), transform.position.y, transform.position.z + (transform.localScale.z * 0.5f));

        isFlipping = true;
    }

    void FlipVertical(Vector2 direction)
    {
        LockPosition();

        moveDir = new Vector2(0, direction.y);
        rotateDir = new Vector2(direction.y, 0);

        rotatePivot = new Vector3(transform.position.x, transform.position.y + (moveDir.y * (transform.localScale.y * 0.5f)), transform.position.z + (transform.localScale.z * 0.5f));

        isFlipping = true;
    }
 
    void LockPosition()
    {
        Vector2 gridPosition = CalculateGridPosition();
        transform.position = new Vector3(gridPosition.x * transform.localScale.x, gridPosition.y * transform.localScale.y, -(transform.localScale.x * 0.5f));

        Vector3 rotationAsQuarters = new Vector3(Mathf.Round(transform.localEulerAngles.x / 90f), Mathf.Round(transform.localEulerAngles.y / 90f), Mathf.Round(transform.localEulerAngles.z / 90f));

        transform.rotation = Quaternion.Euler(rotationAsQuarters.x * 90f, rotationAsQuarters.y * 90f, rotationAsQuarters.z * 90f);
    }

    void DoSlide(Vector2 desiredVelocity)
    {
        Vector2 slideVelocity = rb.velocity;

        float maxSpeedChange = slideAcceleration * Time.deltaTime;
        slideVelocity = new Vector2
        (
            Mathf.MoveTowards(slideVelocity.x, desiredVelocity.x, maxSpeedChange),
            Mathf.MoveTowards(slideVelocity.y, desiredVelocity.y, maxSpeedChange)
        );

        rb.velocity = new Vector3(slideVelocity.x, slideVelocity.y, 0);
    }

    void FinishSlide()
    {
        actionCooldownCounter = actionCooldown * 2f;

        rb.velocity = Vector3.zero;
        isSliding = false;
    }

    void DoFlip(Vector2 direction)
    {
        transform.RotateAround(rotatePivot, rotateDir, flipRotationSpeed);

        rotationProgress += flipRotationSpeed;

        if (rotationProgress >= 90f)
        {
            FinishFlip();
        }
    }

    void FinishFlip()
    {
        actionCooldownCounter = actionCooldown;

        rotationProgress = 0f;
        isFlipping = false;

        SetHitbox();
    }

    Vector2 CalculateGridPosition()
    {
        return new Vector2
        (
            Mathf.Round(transform.position.x / transform.localScale.x),
            Mathf.Round(transform.position.y / transform.localScale.y)
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Wall");
        FinishSlide();
    }

    void SetHitbox()
    {
        hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
        hitBox.SetActive(true);
    }
}
