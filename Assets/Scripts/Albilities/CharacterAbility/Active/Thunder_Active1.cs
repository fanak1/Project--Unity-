using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Thunder_Active1 : Abilities
{
    List<UnitBase> enemies;
    public override void ActionPressed(KeyCode key) {

        Thunder_Debuff2.underEffect.RemoveAll(item => item == null);
        enemies = new List<UnitBase>(Thunder_Debuff2.underEffect);
        if(enemies.Count > 0) base.ActionPressed(key);
    }
    public override void Action() {
       
        foreach(var enemy in enemies) {
            if(enemy is EnemyBase && enemy != null) {
                var effect = LightningEffect.Create();
                
                 effect.OnEffectHit += EffectHit;
                 effect.StartRender(source, enemy);
            }
        }
    }

    public override bool IsUsable()
    {
        Thunder_Debuff2.underEffect.RemoveAll(item => item == null);
        enemies = new List<UnitBase>(Thunder_Debuff2.underEffect);
        return enemies.Count > 0;
    }

    public void EffectHit(UnitBase enemy) {

        if (enemy == null) return;
        source.DealDamage(enemy, this.stat.amount * source.stats.atk);
        var debuff = new GameObject("thunder debuff");
        var d = debuff.AddComponent<Thunder_Debuff1>();
        var value = this.stat.amount * source.stats.atk / 2;
        d.Init(cooldown, enemy, source, value);
    }
}
