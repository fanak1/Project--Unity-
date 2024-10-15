using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities")]
public class ScriptableAlbilities : ScriptableObject {

    [SerializeField] private AlbilitiesBase albility;

    public Event onEvent;

    [SerializeField] private Stats amountIncrease;

    [SerializeField] private KeyCode button;

    [SerializeField] private SkillUI skillIconPrefabs;

    private SkillUI skillIcon;

    [SerializeField] private float cooldown = 0f;

    public Action perform;

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
                skillIcon = InitSkillIcon();
                break;
            default:
                break;
        }
    }

    virtual public void PerformAbility() {
        if (Input.GetKeyDown(button) && skillIcon.usable) {
            albility.ActionPressed(button);
            skillIcon.UseSkill();
            Debug.Log(skillIcon);
        }
    }

    virtual public SkillUI InitSkillIcon() {
        var sIcon = Instantiate(skillIconPrefabs);
        sIcon.transform.SetParent(SkillContainerDD.Instance.transform);
        sIcon.Init(cooldown, button.ToString());
        return sIcon;
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
