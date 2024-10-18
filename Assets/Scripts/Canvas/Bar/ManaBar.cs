using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : GaugeBar
{
    public bool canRegen = true;

    protected override void Update() {
        base.Update();
        canRegen = DelayEnd();
    }
}
