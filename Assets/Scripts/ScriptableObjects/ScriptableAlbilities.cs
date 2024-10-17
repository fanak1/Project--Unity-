using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    [SerializeField] private AlbilitiesBase albilityEffect;

    public Event onEvent;

    [SerializeField] private Stats amountIncrease;

    [SerializeField] private KeyCode button;

    [SerializeField] private SkillUI skillIconPrefabs;

    private SkillUI skillIcon;

    [SerializeField] private float cooldown = 0f;

    public string description;

    public Rarity rarity;


    public Abilities Create() {
        var go = new GameObject("Ability");
        var p = go.AddComponent<Abilities>();
        p.Init(amountIncrease, onEvent, skillIconPrefabs, button, cooldown, rarity, description);
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
