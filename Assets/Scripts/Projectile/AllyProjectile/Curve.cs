using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : ProjectileBase
{
    private Vector3 midPoint;

    private Vector3 breakPoint;

    private float velocity;

    private void Start() {
        float offset = _projectileAttribute.numberOfBullet / 2;
        midPoint = (source.transform.position + destination)/2;
        breakPoint = midPoint + Vector3.up * (bulletIndex - offset) * _projectileAttribute.deviation;
      
    }
    private void Update() {
        
        Trajectory();
    }
    public override void Trajectory() {
        velocity = (_projectileAttribute.speed + _projectileAttribute.accel * Time.deltaTime)/100;
        
    }

    public override float Damage() {
        return 0;
    }

    
}
