using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Debuff1 : Effect {
    private LineRenderer lineRenderer;
    protected override void ActionOnDie() {
        Explode();
    }

    protected override void ActionOnStart() {
        
    }

    protected override void ActionOnUpdate() {
        
    }

    private void Explode() {
        // Find all colliders within the explosion radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        Debug.Log("Debuff Location:" + transform.position);

        foreach (Collider2D hitCollider in hitColliders) {
            // Check if the object has the Enemy component
            EnemyBase otherEnemy = hitCollider.gameObject.GetComponent<EnemyBase>();
            if (otherEnemy != null && otherEnemy != this) // Exclude self
            {
                Debug.Log("" + otherEnemy.gameObject);
                // Apply explosion effect to the other enemy (for example, reduce health)
                source.DealDamage(otherEnemy, this.value);
            }
        }

        // You can add explosion visual effects, sounds, etc., here
        Debug.Log("Enemy exploded and affected other enemies in the radius!");
    }

}
