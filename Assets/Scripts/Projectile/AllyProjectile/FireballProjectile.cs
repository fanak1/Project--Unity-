using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Pierce
{

    protected override void Hit() {
        if (!gameObject.activeInHierarchy) return;
        base.Hit();
        FireballHitParticles.Create(this.transform.position);
    }
}

