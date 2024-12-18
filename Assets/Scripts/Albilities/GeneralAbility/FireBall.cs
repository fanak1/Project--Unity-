using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBall : Abilities
{
    [SerializeField] private ScriptableProjectiles projectiles;

    private Projectiles projectile;

    private bool usable = true;

    private void Start() {
        projectile = projectiles.Create();
        projectile.SetScaleForProjectile(this.stat.amount);
        projectile.Init(this.source);
    }
    public override void Action(UnitBase target, float amount) {
        if(usable) {
            projectile.Shoot(this.source, source.gameObject.transform.position, target.gameObject.transform.position);
            StartCoroutine(StartCooldown());
        }
        
    }

    IEnumerator StartCooldown() {
        usable = false;
        yield return new WaitForSeconds(this.cooldown);
        usable = true;
    }
}
