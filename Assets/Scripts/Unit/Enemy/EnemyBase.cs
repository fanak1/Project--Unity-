
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    
    protected override void Start() {
        faction = Faction.Enemy;
        //mmHealthBar = GetComponent<MMHealthBar>();
        base.Start();
        
    }

    internal override void Destroy() {
        StageManager.numberEnemyLeft--;
        base.Destroy();
    }

    internal override void ReduceHealth(float dmgTaken) {
        base.ReduceHealth(dmgTaken);
        //mmHealthBar.UpdateBar(nowHP, 0, maxHP, true);
    }

    internal override void IncreaseMaxHP(int hp) {
        base.IncreaseMaxHP(hp);
        //mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        //mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

}
