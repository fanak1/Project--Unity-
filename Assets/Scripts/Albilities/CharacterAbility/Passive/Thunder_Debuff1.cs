using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Debuff1 : Effect {
    protected override void ActionOnDie() {
        Explode();
    }

    protected override void ActionOnStart() {
        
    }

    protected override void ActionOnUpdate() {
        
    }

    private void Explode() {
        if (gameObject == null) return;
        // Find all colliders within the explosion radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5f);
        
        Vector3 p1 = transform.position;

        foreach (Collider2D hitCollider in hitColliders) {
            // Check if the object has the Enemy component
            EnemyBase otherEnemy = hitCollider.gameObject.GetComponent<EnemyBase>();
            if (otherEnemy != null && otherEnemy != this.target) // Exclude self
            {
                // Apply explosion effect to the other enemy (for example, reduce health)
                Vector3 p2 = otherEnemy.transform.position;
                var effect = LightningEffect.Create();
                effect.OnEffectHit += EffectHit;
                effect.StartRender(this.target, otherEnemy);
            }
        }
    }

    public void EffectHit(UnitBase enemy) {
        if(enemy != null)
        source.DealDamage(enemy, this.value);
    }

}
