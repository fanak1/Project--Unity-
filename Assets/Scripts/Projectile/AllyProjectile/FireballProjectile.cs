using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Pierce
{
    public override float Damage() {
        return source.stats.atk * this._projectileAttribute.scale;
    }

    protected override void Hit() {
        if (!gameObject.activeInHierarchy) return;
        base.Hit();
        FireballHitParticles.Create(this.transform.position);
    }
}

