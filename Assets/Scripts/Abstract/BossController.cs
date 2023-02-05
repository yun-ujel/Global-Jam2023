using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossController", menuName = "InputController/BossController")]
public class BossController : InputController
{
    public override bool RetrieveSlide()
    {
        return Input.GetButton("Jump") || Input.GetMouseButton(1);
    }

    public override bool RetrieveAttack()
    {
        return false;
    }

    public override Vector2 RetrieveXYInputs()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
