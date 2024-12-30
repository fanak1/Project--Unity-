using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour {
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

    public string skillType;
    public Color32 skillTypeColor;


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


    public void Detach(UnitBase target) {
        switch (onEvent) {
            case Event.OnHitting:
                target.OnHitting -= Action;
                break;
            case Event.OnDealDamage:
                target.OnDealDamage -= Action;
                break;
            case Event.OnDamageTaken:
                target.OnTakeDamage -= Action;
                break;
            case Event.IncreaseStat:
                target.DecreaseStats(amountIncrease);
                break;
            case Event.OnButtonClick:
                //target.OnAbilityKeyPressed += PerformAbility;
                Destroy(skillIcon.gameObject);
                break;
            default:
                break;
        }
        CleanUpOnDetach();
    }

    virtual protected void CleanUpOnDetach() {

    }


    virtual public void PerformAbility() {
        if (skillIcon.usable) {
            ActionPressed(button);
        }
    }

    public void StackIncreaseStats(Stats increase, int ratio = 1) {

        if (ratio > 0) source.IncreaseStats(increase);
        else source.DecreaseStats(increase);
        amountIncrease.hp += ratio * increase.hp;
        amountIncrease.mp += ratio * increase.mp;
        amountIncrease.def += ratio * increase.def;
        amountIncrease.spd += ratio * increase.spd;
        amountIncrease.atk += ratio * increase.atk;

        description = $"Atk: +{amountIncrease.atk}, " +
                        $"Hp: +{amountIncrease.hp}" +
                        $"Def: +{amountIncrease.def}" +
                        $"Spd: +{amountIncrease.spd}" +
                        $"Mp: +{amountIncrease.mp}";
    }

    public Stats ShowIncreaseStats() {
        return this.amountIncrease;
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
            source.ReduceMP(stat.manaSpend);
            if(skillIcon != null) skillIcon.UseSkill();
        }
    }

    virtual public bool EnoughMana(float mana) {
        if (mana < stat.manaSpend) return false;
        return true;
    }

    virtual public bool Usable() => skillIcon.usable;

    virtual public void Init(Stats amountIncrease, Event onEvent, SkillUI skillIconPrefabs, KeyCode button, float cooldown, Rarity rarity, string description, AbilityStat stat, string skillType, Color32 skillTypeColor) {
        this.amountIncrease = amountIncrease;
        this.onEvent = onEvent;
        this.skillIconPrefabs = skillIconPrefabs;
        this.button = button;
        this.cooldown = cooldown;
        this.rarity = rarity;
        this.description = description;
        this.stat = stat;
        this.skillType = skillType;
        this.skillTypeColor = skillTypeColor;
    }

    virtual public void Init(UnitBase b) {
        this.gameObject.transform.parent = b.gameObject.transform;
        this.source = b;
    }
}
