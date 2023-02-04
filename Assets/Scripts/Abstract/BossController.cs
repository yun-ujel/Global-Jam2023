using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossController", menuName = "InputController/BossController")]
public class BossController : InputController
{
    public override float RetrieveXInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override float RetrieveYInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public override bool RetrieveRoll()
    {
        return Input.GetButton("Jump") || Input.GetMouseButton(1);
    }
}
