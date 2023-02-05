using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputController", menuName = "InputController/PlayerInputController")]
public class PlayerInputController : InputController
{
    public override bool RetrieveSlide()
    {
        return Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1);
    }

    public override bool RetrieveAttack()
    {
        return Input.GetButton("Fire1");
    }

    public override Vector2 RetrieveXYInputs()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
