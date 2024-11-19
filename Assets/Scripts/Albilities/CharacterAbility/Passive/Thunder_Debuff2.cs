using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Debuff2 : Effect {

    GameObject blueEmission;

    public static List<UnitBase> underEffect = new List<UnitBase>();

    protected override void Start() {
        blueEmission = Resources.Load<GameObject>("FunnyPrefabs/BlueEmission");
        var go = Instantiate(blueEmission);
        go.transform.SetParent(transform);
        go.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        go.transform.localPosition = Vector3.zero;
        base.Start();

    }
    protected override void ActionOnDie() {
        if(this.target != null) underEffect.Remove(this.target);
    }

    protected override void ActionOnStart() {
        if(this.target != null) underEffect.Add(this.target);
    }

    protected override void ActionOnUpdate() {

    }

    protected override void EndEffect() {
        base.EndEffect();
        if(target != null) underEffect.Remove(target);
    }
}
