using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveXInput();

    public abstract float RetrieveYInput();

    public abstract bool RetrieveSlide();

    public abstract bool RetrieveAttack();
}
