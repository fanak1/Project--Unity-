using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Active1 : Abilities
{
    public override void Action() {
        List<UnitBase> enemies = new List<UnitBase>(Thunder_Debuff2.underEffect);
        Vector3 p1 = source.transform.position;

        foreach(var enemy in enemies) {
            if(enemy is EnemyBase && enemy != null)
            LightningEffect.Create(p1, enemy.transform.position);
            source.DealDamage(enemy, this.stat.amount);
            if (enemy == null) return;
            var debuff = new GameObject("thunder debuff");
            var d = debuff.AddComponent<Thunder_Debuff1>();
            var value = this.stat.amount/2;
            d.Init(cooldown, enemy, source, value);

        }
    }
}
