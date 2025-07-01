using UnityEngine;

public class IncreaseProjectilesStats : Abilities
{
    public ProjectileAttribute attribute;

    public override void Action()
    {
        var projectiles = source.GetComponentsInChildren<Projectiles>();
        foreach (var projectile in projectiles)
        {
            if (projectile != null)
            {
                projectile.projectileAttribute += attribute * stat.amount;
            }
        }
    }
}
