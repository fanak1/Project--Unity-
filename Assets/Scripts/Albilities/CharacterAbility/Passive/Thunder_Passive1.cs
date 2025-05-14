using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Passive1 : Abilities
{
    public override void Action(UnitBase target, float amount) {
        switch (rarity)
        {
            case Rarity.Normal:
                Action_Normal(target, amount); break;
            case Rarity.Rare:
                Action_Rare(target, amount); break;
            case Rarity.Epic:
                Action_Epic(target, amount);
                break;
            case Rarity.Legendary:
                Action_Legendary(target, amount); break;
            default:
                break;

        }
    }

    private void ApplyThunderDebuff(UnitBase target, float amount)
    {
        var debuff = new GameObject("thunder debuff");
        var d = debuff.AddComponent<Thunder_Debuff2>();
        var value = this.stat.amount;
        d.Init(cooldown, target, source, value);
    }

    private void Action_Normal(UnitBase target, float amount)
    {
        float rad = Random.value * 100;
        if(rad <= 5)
        {
            ApplyThunderDebuff(target, amount);
        }
    }

    private void Action_Rare(UnitBase target, float amount)
    {
        float rad = Random.value * 100;
        if (rad <= 10)
        {
            ApplyThunderDebuff(target, amount);
        }
    }

    private void Action_Epic(UnitBase target, float amount)
    {
        float rad = Random.value * 100;
        if (rad <= 15)
        {
            ApplyThunderDebuff(target, amount);
        }
    }

    private void Action_Legendary(UnitBase target, float amount)
    {
        float rad = Random.value * 100;
        if (rad <= 100)
        {
            ApplyThunderDebuff(target, amount);
        }
    }

}
