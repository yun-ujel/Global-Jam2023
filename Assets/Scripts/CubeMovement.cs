using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private InputController inputC;

    public GameObject hitBox;
    Vector3 rotatePivot;
    bool isMoving;

    public float rotationProgress;

    private MoveDir moveDir;
    private enum MoveDir
    {
        up,
        down,
        left,
        right
    }

    public float maxRotationSpeed;

    void Update()
    {
        if (isMoving)
        {
            hitBox.SetActive(false);
        }

        if (!isMoving)
        {
            GetInputs();
        }
        else if (rotationProgress < 90f)
        {
            DoFlip();
        }
        else
        {
            FinishFlip();
        }
    }

    void DoFlip()
    {
        float rotationSpeed = Time.deltaTime * maxRotationSpeed;

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

    void GetInputs()
    {
        if (inputC.RetrieveXInput() == 1f)
        {
            FlipRight();
            isMoving = true;
            moveDir = MoveDir.right;
        }
        else if (inputC.RetrieveXInput() == -1f)
        {
            FlipLeft();
            isMoving = true;
            moveDir = MoveDir.left;
        }
        else if (inputC.RetrieveYInput() == 1f)
        {
            FlipUp();
            isMoving = true;
            moveDir = MoveDir.up;
        }
        else if (inputC.RetrieveYInput() == -1f)
        {
            FlipDown();
            isMoving = true;
            moveDir = MoveDir.down;
        }
    }

    void FinishFlip()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

        hitBox.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.x * 0.5f));
        hitBox.SetActive(true);

        rotationProgress = 0f;
        isMoving = false;
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
}
