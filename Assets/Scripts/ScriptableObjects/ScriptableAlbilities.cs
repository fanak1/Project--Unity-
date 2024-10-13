using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    [SerializeField] private AlbilitiesBase albility;

    [SerializeField] private Event onEvent;

    [SerializeField] private Stats amountIncrease;

    [SerializeField] private KeyCode button; 

    public string description;

    public Rarity rarity;

    public void AttachTo(UnitBase target) {
        if (albility != null) albility.source = target;        
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
                target.IncreaseStats(amountIncrease); 
                break;
            case Event.OnButtonClick:
                target.OnAbilityKeyPressed += PerformAbility;
                break;
            default:
                break;
        }
    }

    public void PerformAbility() {
        if (Input.GetKeyDown(button)) {
            albility.ActionPressed(button);
        }
    }

    
}

[Serializable]
public enum Event {
    OnHitting,
    OnDealDamage,
    OnDamageTaken,
    IncreaseStat,
    OnButtonClick
}

public enum Rarity {
    Normal,
    Rare,
    Epic,
    Legendary
}
