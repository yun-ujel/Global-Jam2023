using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    bool isRolling;

    private void Update()
    {
        CalculateState();

        if (isRolling)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void CalculateState()
    {
        isRolling = movement.rollCounter > 0f;
    }
}
