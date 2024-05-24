using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    [SerializeField] private AlbilitiesBase albility;

    [SerializeField] private Event onEvent;

    

    [SerializeField] private Stats amountIncrease;

    public string description;

    public Rarity rarity;

    public void AttachTo(UnitBase target) {
        switch (onEvent) {
            case Event.OnHitting:
                target.OnHitting += albility.Action;
                break;
            case Event.OnDealDamage:
                target.OnDealDamage += albility.Action;
                break;
            case Event.OnDamageTaken:
                target.OnTakeDamage += albility.Action;
                break;
            case Event.IncreaseStat:
                target.IncreaseBaseStats(amountIncrease); 
                break;
            default:
                break;
        } 
    }

    
}

[Serializable]
public enum Event {
    OnHitting,
    OnDealDamage,
    OnDamageTaken,
    IncreaseStat
}

public enum Rarity {
    Normal,
    Rare,
    Epic,
    Legendary
}
