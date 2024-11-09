using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Passive1 : Abilities
{
    public override void Action(UnitBase target, float amount) {
        var debuff = new GameObject("thunder debuff");
        var d = debuff.AddComponent<Thunder_Debuff2>();
        var value = this.stat.amount;
        d.Init(cooldown, target, source, value);
    }
}
