using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : GaugeBar {
    public bool canRegen = true;
    public float regenCooldown = 2f;
    private Coroutine regenCoroutine;

    protected override void Start() {
        base.Start();
        regenCooldown = 2f;
    }

    protected override void Update() {
        base.Update();
    }

    private IEnumerator StartRegenCooldown() {
        canRegen = false;
        yield return new WaitForSeconds(regenCooldown);
        canRegen = true;
    }

    private void ResetRegenCooldown() {
        if(regenCoroutine != null) {
            StopCoroutine(regenCoroutine);
        }
        regenCoroutine = CoroutineManager.Instance.StartNewCoroutine(StartRegenCooldown());

    }

    public void RegenCooldown() {
        ResetRegenCooldown();

    }

    public void SetRegenCooldown(float regenCooldown) {
        this.regenCooldown = regenCooldown;
    }

}
