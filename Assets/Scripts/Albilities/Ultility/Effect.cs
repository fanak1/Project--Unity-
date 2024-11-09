using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{

    public UnitBase source, target;

    private float countdown;

    public float value;

    private SkillUI icon;

    protected abstract void ActionOnUpdate();

    protected abstract void ActionOnStart();

    protected abstract void ActionOnDie();

    protected virtual void OnResetEffect() {

    }

    protected virtual void Start() {
        
    }

    protected virtual void Update() {
        ActionOnUpdate();
    }


    protected virtual void EndEffect() {
        //underEffect.Remove(target);
    }

    protected virtual void Die() {
        EndEffect();
        if(this != null) Destroy(this.gameObject);
    }

    protected virtual void EffectPersistent(Effect d) {
        d.ResetEffect();
        Destroy(this.gameObject);
    }

    protected virtual void ResetEffect() {
        OnResetEffect();
        StopCoroutine(EffectCountdown());
        StartCoroutine(EffectCountdown());
    }

    public bool AttachTo(UnitBase target, UnitBase source) {
        var p = target.gameObject.GetComponentsInChildren<Effect>();
        if (p != null) {
            foreach (Effect d in p) {
                if (d.GetType() == this.GetType()) {
                    EffectPersistent(d);
                    return false;
                }
            }
        }

        this.source = source;
        this.target = target;

        gameObject.transform.parent = target.gameObject.transform;
        //underEffect.Add(this.target);
        this.target.OnDead += ActionOnDie;
        this.target.OnDead += Die;

        this.gameObject.transform.localPosition = new Vector3(0, 0, 0);

        ActionOnStart();
        return true;
    }

    public void Init(float countdown, UnitBase target, UnitBase source, float value = 0f) {
        if (target != null && source != null) {
            this.countdown = countdown;
            this.value = value;

            if (AttachTo(target, source)) ResetEffect();
        }
    }
        

    private IEnumerator EffectCountdown() {
        yield return new WaitForSeconds(countdown);
        Die();
    }


}
