using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public UnitBase source;

    public Stats amountIncrease;

    public KeyCode button;

    public SkillUI skillIconPrefabs;

    public float cooldown;

    public Event onEvent;

    public string description;

    public Rarity rarity;

    private SkillUI skillIcon;


    public void AttachTo(UnitBase target) {
        Init(target);
        switch (onEvent) {
            case Event.OnHitting:
                target.OnHitting += Action;
                break;
            case Event.OnDealDamage:
                target.OnDealDamage += Action;
                break;
            case Event.OnDamageTaken:
                target.OnTakeDamage += Action;
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
            ActionPressed(button);
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

    virtual public void Action(UnitBase b, float amount) {

    }

    virtual public void ActionPressed(KeyCode key) {
        DebugMessege.Instance.Messege(key + " is Pressed");
    }

    virtual public void Init(Stats amountIncrease, Event onEvent, SkillUI skillIconPrefabs, KeyCode button, float cooldown, Rarity rarity, string description) {
        this.amountIncrease = amountIncrease;
        this.onEvent = onEvent;
        this.skillIconPrefabs = skillIconPrefabs;
        this.button = button;
        this.cooldown = cooldown;
        this.rarity = rarity;
        this.description = description;
    }

    virtual public void Init(UnitBase b) {
        this.gameObject.transform.parent = b.gameObject.transform;
        this.source = b;
    }
}
