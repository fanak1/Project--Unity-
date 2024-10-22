using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    [SerializeField] private Abilities ability;

    public Event onEvent;

    [SerializeField] private Stats amountIncrease;

    [SerializeField] private KeyCode button;

    [SerializeField] private SkillUI skillIconPrefabs;

    private SkillUI skillIcon;

    [SerializeField] private float cooldown = 0f;

    [SerializeField] private AbilityStat stat;

    public string description;

    public Rarity rarity;


    public Abilities Create() {
        Abilities p;
        if(ability != null) {
            p = Instantiate(ability);

        } else {
            var go = new GameObject("Ability");
            p = go.AddComponent<Abilities>();
        }
        
        
        p.Init(amountIncrease, onEvent, skillIconPrefabs, button, cooldown, rarity, description, stat);
        return p;
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

[Serializable]
public struct AbilityStat {
    public int manaSpend;
    public int amount;
}
