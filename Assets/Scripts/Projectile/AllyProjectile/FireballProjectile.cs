using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Spread
{
    public override float Damage() {
        return source.stats.atk * this._projectileAttribute.scale;
    }
}

