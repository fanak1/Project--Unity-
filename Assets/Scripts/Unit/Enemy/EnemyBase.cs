using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    [SerializeField] private MMHealthBar mmHealthBar;

    protected override void Start() {
        faction = Faction.Enemy;
        mmHealthBar = GetComponent<MMHealthBar>();
        base.Start();
        
    }

    internal override void Destroy() {
        StageManager.numberEnemyLeft--;
        base.Destroy();
    }

    internal override void ReduceHealth(float dmgTaken) {
        base.ReduceHealth(dmgTaken);
        mmHealthBar.UpdateBar(nowHP, 0, maxHP, true);
    }

    internal override void IncreaseHP(int hp) {
        base.IncreaseHP(hp);
        mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

}
