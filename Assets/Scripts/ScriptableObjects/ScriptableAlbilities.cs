using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAlbilities : ScriptableObject {

    public enum Action {
        Heal,
        DealDamage,
        Move
    }
    public enum OnEvent {
        OnButtonDown,
        OnHit,
        OnShoot,
        OnDead
    }

    [SerializeField]
    private OnEvent onEvent;

    public Action action { private set; get; }

}
