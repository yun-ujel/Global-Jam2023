using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputController", menuName = "InputController/PlayerInputController")]
public class PlayerInputController : InputController
{
    public override float RetrieveXInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override float RetrieveYInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public override bool RetrieveRollInput()
    {
        return Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1);
    }
}
