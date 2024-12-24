using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : ProjectileBase
{
    private Queue<GameObject> hitQueue = new Queue<GameObject>();
    public override float Damage() {
        return _projectileAttribute.scale * source.stats.atk;
    }

    public override void Trajectory() {
        transform.Translate(Vector3.right * Time.deltaTime * spd);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        base.Start();
    }

    private void OnEnable() {
        Initialize();
        base.OnEnable();
    }

    void Initialize() {
        hitQueue.Clear();
        accel = _projectileAttribute.accel;
        spd = _projectileAttribute.speed;
        Vector3 ray = destination - position;
        float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    virtual protected bool HitDelayOnSameTarget(GameObject target) {
        if (hitQueue.Contains(target)) return true;
        hitQueue.Enqueue(target);
        StartCoroutine(PopQueue());
        return false;
    }


    IEnumerator PopQueue() {
        yield return new WaitForSeconds(0.1f);

        if (hitQueue.Count > 0) hitQueue.Dequeue();
    }

    protected override void Hit() {
    }

    protected override bool IfCollide(GameObject a) {
        return HitDelayOnSameTarget(a);
    }


}
