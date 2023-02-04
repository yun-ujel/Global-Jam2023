using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputController inputC;
    [SerializeField] private Rigidbody rb;
    public GameObject hitBox;

    [Header("Sliding")]
    [SerializeField, Range(0f, 100f)] private float slideAcceleration;
    [SerializeField, Range(0f, 100f)] private float maxSlideSpeed;
    [SerializeField, Range(0f, 100f)] private float maxSlideDistance;
    private Vector3 lastSlidePosition;
    private Vector2 slideDir;
    private bool isSliding;

    [Header("Flipping")]
    [SerializeField] private float flipRotationSpeed = 120f;
    private Vector3 rotatePivot;
    private float rotationProgress;
    private bool isFlipping;

    private MoveDir moveDir;
    private enum MoveDir
    {
        up,
        down,
        left,
        right
    }

    void Update()
    {
        if (!isFlipping && !isSliding)
        {
            hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
            GetInputs();
        }
        else if (rotationProgress < 90f && isFlipping)
        {
            hitBox.SetActive(false);
            DoFlip();
        }
        else if (isFlipping)
        {
            FinishFlip();
            hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
            hitBox.SetActive(true);
        }
        else if (isSliding)
        {
            DoSlide();
        }

    }



    void GetInputs()
    {
        if (!inputC.RetrieveSlide())
        {
            if (inputC.RetrieveXInput() == 1f)
            {
                FlipRight();
                isFlipping = true;
                moveDir = MoveDir.right;
            }
            else if (inputC.RetrieveXInput() == -1f)
            {
                FlipLeft();
                isFlipping = true;
                moveDir = MoveDir.left;
            }
            else if (inputC.RetrieveYInput() == 1f)
            {
                FlipUp();
                isFlipping = true;
                moveDir = MoveDir.up;
            }
            else if (inputC.RetrieveYInput() == -1f)
            {
                FlipDown();
                isFlipping = true;
                moveDir = MoveDir.down;
            }
        }
        else
        {
            slideDir = new Vector2(inputC.RetrieveXInput(), inputC.RetrieveYInput());

            if (slideDir != Vector2.zero)
            {
                Slide();
            }
        }
    }

    void Slide()
    {
        Debug.Log("Slide");
        lastSlidePosition = transform.position;
        isSliding = true;
    }

    void DoSlide()
    {
        if (Vector3.Distance(lastSlidePosition, transform.position) < maxSlideDistance)
        {
            Vector2 desiredSlideSpeed = slideDir * maxSlideSpeed;
            float maxSpeedChange = slideAcceleration * Time.deltaTime;

            rb.velocity = new Vector3
            (
            Mathf.MoveTowards(rb.velocity.x, desiredSlideSpeed.x, maxSpeedChange),
            Mathf.MoveTowards(rb.velocity.y, desiredSlideSpeed.y, maxSpeedChange),
            0
            );
            
            hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
        }
        else
        {
            rb.velocity = Vector3.zero;
            isSliding = false;
            hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
        }
    }

    void FlipRight()
    {
        Vector3 position = transform.position;
        rotatePivot = new Vector3(position.x + (transform.localScale.x * 0.5f), position.y, position.z + (transform.localScale.x * 0.5f));
    }
    void FlipLeft()
    {
        Vector3 position = transform.position;
        rotatePivot = new Vector3(position.x - (transform.localScale.x * 0.5f), position.y, position.z + (transform.localScale.x * 0.5f));
    }
    void FlipUp()
    {
        Vector3 position = transform.position;
        rotatePivot = new Vector3(position.x, position.y + (transform.localScale.x * 0.5f), position.z + (transform.localScale.x * 0.5f));
    }
    void FlipDown()
    {
        Vector3 position = transform.position;
        rotatePivot = new Vector3(position.x, position.y - (transform.localScale.x * 0.5f), position.z + (transform.localScale.x * 0.5f));
    }

    void DoFlip()
    {
        float rotationSpeed = Time.deltaTime * flipRotationSpeed;

        if (moveDir == MoveDir.right)
        {
            transform.RotateAround(rotatePivot, Vector3.down, rotationSpeed);
        }
        else if (moveDir == MoveDir.left)
        {
            transform.RotateAround(rotatePivot, Vector3.up, rotationSpeed);
        }
        else if (moveDir == MoveDir.up)
        {
            transform.RotateAround(rotatePivot, Vector3.right, rotationSpeed);
        }
        else if (moveDir == MoveDir.down)
        {
            transform.RotateAround(rotatePivot, Vector3.left, rotationSpeed);
        }

        rotationProgress += rotationSpeed;
    }

    void FinishFlip()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

        rotationProgress = 0f;
        isFlipping = false;
    }
}
