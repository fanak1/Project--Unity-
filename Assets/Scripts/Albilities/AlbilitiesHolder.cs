using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    public List<ScriptableAlbilities> listSkill;

    public List<Abilities> passive;

    public List<Abilities> active;

    public Dictionary<KeyCode, Abilities> abilityKey;

    private UnitBase source;

    private void Awake() {
        source = GetComponent<UnitBase>();
        abilityKey = new Dictionary<KeyCode, Abilities>();

        foreach(ScriptableAlbilities a in list) {
            var ability = a.Create();
            if(a.onEvent != Event.OnButtonClick) {
                passive.Add(ability);
            } else {
                active.Add(ability);
            }
            Attach(ability);
        }
    }

    public void AddAbility(ScriptableAlbilities a) {
        //Debug.Log(source);
        var ability = a.Create();
        if(a.onEvent != Event.OnButtonClick) {
            list.Add(a);
            passive.Add(ability);
        } else {
            listSkill.Add(a);
            active.Add(ability);
        }
        Attach(ability);
    }

    private void Attach(Abilities a) {
        a.AttachTo(source);
        if(a.onEvent == Event.OnButtonClick) {
            Debug.Log(abilityKey);
            abilityKey.Add(a.button, a);
            
        }
            
    }

    public void PerformAbility(Abilities a) {
        if(a.onEvent == Event.OnButtonClick) {
            a.PerformAbility();
        }
    }

    public void PerformAbility(KeyCode key) {
        PerformAbility(abilityKey[key]);
    }

    public void LoopCheckAbilityPress() {
        foreach(Abilities a in active) {
            if (Input.GetKeyDown(a.button)) {
                PerformAbility(a.button);
            }
                
        }
    }

    public bool EnoughMana(Abilities a, float mana) => a.EnoughMana(mana);

    public float MPCost(Abilities a) {
        return a.stat.manaSpend;
    }

    public bool Usable(Abilities a) => a.Usable();
}
