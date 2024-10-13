using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlbilitiesBase : MonoBehaviour
{
    public UnitBase source;
    public abstract void Action(UnitBase unit, float amount);

    public abstract void ActionPressed(KeyCode key);

}
