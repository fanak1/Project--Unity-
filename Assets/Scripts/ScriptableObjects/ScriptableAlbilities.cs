using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    public Abilities ability;

    public Event onEvent;

    public Stats amountIncrease;

    [SerializeField] private KeyCode button;

    [SerializeField] private SkillUI skillIconPrefabs;

    private SkillUI skillIcon;

    [SerializeField] private float cooldown = 0f;

    [SerializeField] private AbilityStat stat;

    public string description;

    public Rarity rarity;

    public string skillType = "None";
    public Color32 skillTypeColor = Color.black;

    public Sprite icon;

    public int cost = 0;

    public CharacterCode characterCode;

    public bool isGenProjectile = false;


    public Abilities Create() {
        Abilities p;
        if(ability != null) {
            p = Instantiate(ability);

        } else {
            var go = new GameObject("Ability");
            p = go.AddComponent<Abilities>();
        }
        
        
        p.Init(amountIncrease, onEvent, skillIconPrefabs, button, cooldown, rarity, description, stat, skillType, skillTypeColor);
        return p;
    }
}

[Serializable]
public enum Event {
    OnHitting,
    OnDealDamage,
    OnDamageTaken,
    IncreaseStat,
    OnButtonClick,
    OnClaimed,
    OnProjectileShoot
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
    public float amount;
}

public enum CharacterCode {
    None,
    Thunder,
}
