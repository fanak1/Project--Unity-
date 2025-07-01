using System.Collections;
using UnityEngine;

public class CreateProjectileAbility : Abilities
{
    [SerializeField] private ScriptableProjectiles projectiles;

    [SerializeField] private Projectiles projectile;

    private bool usable = true;

    public float delay = 0.2f;

    public int numberOfProjectiles = 1;

    public override void Init(UnitBase unitBase)
    {
        base.Init(unitBase);
        if (projectiles == null) return;
        projectile = projectiles.Create();
        projectile.SetScaleForProjectile(this.stat.amount);
        projectile.Init(this.source);
    }

    public override void Action(UnitBase target, float amount)
    {
        if (projectile == null)
        {
            return;
        }
        if (usable)
        {
            projectile.Shoot(this.source, source.gameObject.transform.position, target.gameObject.transform.position);
            CoroutineManager.Instance.StartNewCoroutine(StartCooldown());
        }
    }

    public override void Action(Projectiles projectiles, Vector3 position, Vector3 destination)
    {
        StartCoroutine(ShootNumberOfProjectilesWithDelay(projectiles, position, destination));
    }

    IEnumerator ShootNumberOfProjectilesWithDelay(Projectiles projectiles, Vector3 position, Vector3 destination)
    {
        int i = 0;
        while (i < numberOfProjectiles)
        {
            yield return new WaitForSeconds(delay);
            projectiles.Shoot(this.source, position, destination, false);
            i++;
        }
    }
    IEnumerator StartCooldown()
    {
        usable = false;
        yield return new WaitForSeconds(this.cooldown);
        usable = true;
    }

    protected override void CleanUpOnDetach()
    {
        Destroy(projectile.gameObject);
    }
}
