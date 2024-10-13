using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private HealthBar healthBar;

    protected override void Start() {
        healthBar = GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<HealthBar>();
        base.Start();
        
    }

    internal override void Destroy() {
        StageManager.numberEnemyLeft--;
        base.Destroy();
    }

    internal override void ReduceHealth(float dmgTaken) {
        base.ReduceHealth(dmgTaken);
        healthBar.Decrease(dmgTaken);
    }

    internal override void IncreaseHP(int hp) {
        base.IncreaseHP(hp);
        healthBar.SetMaxValue(maxHP);
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        healthBar.SetMaxValue(maxHP);
    }
}
