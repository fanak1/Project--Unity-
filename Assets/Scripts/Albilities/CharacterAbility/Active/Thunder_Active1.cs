using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Thunder_Active1 : Abilities
{
    public override void Action() {
        List<UnitBase> enemies = new List<UnitBase>(Thunder_Debuff2.underEffect);

        foreach(var enemy in enemies) {
            if(enemy is EnemyBase && enemy != null) {
                var effect = LightningEffect.Create();
                
                 effect.OnEffectHit += EffectHit;
                 effect.StartRender(source, enemy);
            }
            

        }
    }

    public void EffectHit(UnitBase enemy) {

        if (enemy == null) return;
        source.DealDamage(enemy, this.stat.amount);
        var debuff = new GameObject("thunder debuff");
        var d = debuff.AddComponent<Thunder_Debuff1>();
        var value = this.stat.amount / 2;
        d.Init(cooldown, enemy, source, value);
    }
}