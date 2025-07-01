using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : ProjectileBase
{
    private float offset;
    

    public override void Start() { //Init the speed, deviation, and rotation of projecile
        Initialize();
        base.Start();
    }

    public override void OnEnable() {
        Initialize();
        base.OnEnable();
    }

    public override float Damage() { //The scale of this bullet damage
        return source.stats.atk * _projectileAttribute.scale; //Base Atk * scale
    }

    public override void Trajectory() { //How the bullet travel
        spd += Time.deltaTime * accel;

        transform.Translate(Vector3.right * Time.deltaTime * spd);
    }

    private void Initialize() {
        accel = _projectileAttribute.accel;
        spd = _projectileAttribute.speed;
        offset = _projectileAttribute.deviation;
        Vector3 ray = destination - position;
        float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg;
        float trueOffset = offset * (60 / _projectileAttribute.numberOfBullet) * Random.Range(-bulletIndex - offset, bulletIndex + offset);
        transform.rotation = Quaternion.AngleAxis(angle + trueOffset, Vector3.forward);
    }
}
