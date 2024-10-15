using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPressed : AlbilitiesBase
{
    public override void Action(UnitBase unit, float amount) {
        
    }

    public override void ActionPressed(KeyCode key) {
        DebugMessege.Instance.Messege(key + " is Pressed");
    }
}
