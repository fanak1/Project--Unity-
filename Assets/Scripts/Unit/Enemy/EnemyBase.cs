using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    protected override void Start() {
        faction = Faction.Enemy;
        base.Start();
    }

}
