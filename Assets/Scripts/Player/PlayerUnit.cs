using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private HealthBar healthBar;
    private ManaBar manaBar;

    protected override void Start() {
        healthBar = GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<HealthBar>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBarUI").GetComponent<ManaBar>();
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

    internal override void ReduceMP(float mpTaken) {
        base.ReduceMP(mpTaken);
        manaBar.Decrease(mpTaken);
    }



    internal override void IncreaseMaxHP(int hp) {
        base.IncreaseMaxHP(hp);
        healthBar.SetMaxValue(maxHP);
    }

    internal override void IncreaseMaxMP(int mp) {
        base.IncreaseMaxMP(mp);
        manaBar.SetMaxValue(maxMP);
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        healthBar.Init(maxHP, 1.5f);
    }

    internal override void InitializeMP() {
        base.InitializeMP();
        manaBar.Init(maxMP, 1.5f);
    }

    internal override void RegenMP(float regenSpeed) {
        if (manaBar.canRegen) {
            base.RegenMP(regenSpeed);
            manaBar.SetValue(nowMP);
        }
            
    }

}
