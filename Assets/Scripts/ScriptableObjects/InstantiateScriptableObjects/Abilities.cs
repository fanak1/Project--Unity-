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

    public AbilityStat stat;

    internal SkillUI skillIcon;


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
                //target.OnAbilityKeyPressed += PerformAbility;
                skillIcon = InitSkillIcon();
                break;
            default:
                break;
        }
    }

    

    virtual public void PerformAbility() {
        if (skillIcon.usable) {
            ActionPressed(button);
            
        }
    }

    virtual public SkillUI InitSkillIcon() {
        if (skillIconPrefabs != null) {

            var sIcon = Instantiate(skillIconPrefabs);
            sIcon.transform.SetParent(SkillContainerDD.Instance.transform);
            sIcon.Init(this.cooldown, button.ToString());
            return sIcon;
        }
        return null;
    }

    virtual public void Action(UnitBase target, float amount) {

    }

    virtual public void Action() {

    }

    virtual public void ActionPressed(KeyCode key) {
        if (skillIcon.usable) {
            DebugMessege.Instance.Messege(key + " is Pressed");
            Action();
            if(skillIcon != null) skillIcon.UseSkill();
        }
            
    }

    virtual public bool EnoughMana(float mana) {
        if (mana < stat.manaSpend) return false;
        return true;
    }

    virtual public bool Usable() => skillIcon.usable;

    virtual public void Init(Stats amountIncrease, Event onEvent, SkillUI skillIconPrefabs, KeyCode button, float cooldown, Rarity rarity, string description, AbilityStat stat) {
        this.amountIncrease = amountIncrease;
        this.onEvent = onEvent;
        this.skillIconPrefabs = skillIconPrefabs;
        this.button = button;
        this.cooldown = cooldown;
        this.rarity = rarity;
        this.description = description;
        this.stat = stat;
    }

    virtual public void Init(UnitBase b) {
        this.gameObject.transform.parent = b.gameObject.transform;
        this.source = b;
    }
}
