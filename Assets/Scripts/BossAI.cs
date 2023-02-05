using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private Transform bossTransform;
    [SerializeField] private Transform playerTransform;

    Vector2 bossPosMax;
    Vector2 bossPosMin;

    bool isSlide;

    Vector2 moveDir;

    void CalculatePosition()
    {
        bossPosMax = new Vector2
        (
            bossTransform.position.x + (bossTransform.localScale.x *  0.5f),
            bossTransform.position.y + (bossTransform.localScale.y * 0.5f)
        );

        bossPosMin = new Vector2
        (
            bossTransform.position.x - (bossTransform.localScale.x * 0.5f),
            bossTransform.position.y - (bossTransform.localScale.y * 0.5f)
        );
    }

    public bool RetrieveSlide()
    {
        Vector2 bT2 = bossTransform.position;
        Vector2 pT2 = playerTransform.position;
        if (pT2.x > bossPosMin.x && pT2.x < bossPosMax.x)
        {
            if (pT2.y < bT2.y)
            {
                moveDir = new Vector2(0, -1f);
            }
            else if (pT2.y > bT2.y)
            {
                moveDir = new Vector2(0, 1f);
            }
            return true;
        }
        else if (pT2.y > bossPosMin.y && pT2.y < bossPosMax.y)
        {
            if (pT2.x < bT2.x)
            {
                moveDir = new Vector2(-1f, 0);
            }
            else if (pT2.x > bT2.x)
            {
                moveDir = new Vector2(1f, 0);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        CalculatePosition();

        if (!RetrieveSlide())
        {
            MoveTowards();
        }
    }

    void MoveTowards()
    {
        Vector2 diff = Vector3.Normalize(playerTransform.position - bossTransform.position);

        moveDir = new Vector2(Mathf.Round(diff.x), Mathf.Round(diff.y));
    }

    public Vector2 RetrieveXYInputs()
    {
        return moveDir;
    }

}
