using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : ProjectileBase
{
    private float offset;
    private float spd;
    private float accel;

    public override void Start() { //Init the speed, deviation, and rotation of projecile
        accel = _projectileAttribute.accel;
        spd = _projectileAttribute.speed;
        offset = _projectileAttribute.deviation;
        Vector3 ray = destination - position;
        float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg;
        float trueOffset = offset * (int)((bulletIndex + 1) / 2) * (100f / _projectileAttribute.numberOfBullet) * Mathf.Pow(-1, bulletIndex);
        transform.rotation = Quaternion.AngleAxis(angle + trueOffset, Vector3.forward);
        Debug.Log(transform.rotation);
        base.Start();
    }

    public override float Damage() { //The scale of this bullet damage
        return Mathf.Sqrt(source.stats.atk);
    }

    public override void Trajectory() { //How the bullet travel
        spd += Time.deltaTime * accel;
        transform.Translate(Vector3.right * Time.deltaTime * spd);
    }
}
