using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract bool RetrieveSlide();

    public abstract bool RetrieveAttack();

    public abstract Vector2 RetrieveXYInputs();
}
