
using System.Collections.Generic;
using UnityEngine;

public class AlbilitiesHolder : MonoBehaviour
{
    public List<ScriptableAlbilities> list;

    public List<Abilities> passive;

    public List<Abilities> active;

    public Abilities increaseStats;

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

    public void UpgradeAbility(ScriptableAlbilities a) {
        if (a.onEvent != Event.IncreaseStat) {
            foreach (ScriptableAlbilities ability in list) {
                if (ability.skillType == a.skillType && ability.skillType != "None") {
                    DeleteAbility(ability);
                    break;
                }
            }
        }
            
        AddAbility(a);
    }

    public Stats ShowIncreaseStats() {
        if (increaseStats != null)  return increaseStats.ShowIncreaseStats();
        else {
            return new Stats();
        }
    }

    public void ClaimAbility(ScriptableAlbilities a) {
        UpgradeAbility(a);
    }

    public void AddAbility(ScriptableAlbilities a) {
        Abilities ability = null;
        
        if(a.onEvent == Event.IncreaseStat) {
            if(increaseStats != null) {
                increaseStats.StackIncreaseStats(a.amountIncrease);
            } else {
                ability = a.Create();
                increaseStats = ability;
            }
        }

        else if(a.onEvent != Event.OnButtonClick) {
            ability = a.Create();
            list.Add(a);
            passive.Add(ability);
        } 

        else {
            ability = a.Create();
            list.Add(a);
            active.Add(ability);
        }
        if(ability != null) Attach(ability);
    }

    
    public void DeleteAbility(ScriptableAlbilities a) {
        Abilities ability = a.ability;
        if (a.onEvent == Event.IncreaseStat) {
            list.Remove(a);
            increaseStats.StackIncreaseStats(a.amountIncrease, -1);
        }
        if (a.onEvent != Event.OnButtonClick) {
            list.Remove(a);
            foreach(Abilities ab in passive) {
                if(ab.GetType() == ability.GetType()) {
                    passive.Remove(ab);
                    Destroy(ab.gameObject);
                    Detach(ability);
                    break;
                }
            }
            
        } else {
            list.Remove(a);
            foreach (Abilities ab in active) {
                if (ab.GetType() == ability.GetType()) {
                    active.Remove(ab);
                    Destroy(ab.gameObject);
                    Detach(ability);
                    break;
                }
            }
            
        }
        
    }

    private void Attach(Abilities a) {
        a.AttachTo(source);
        if(a.onEvent == Event.OnButtonClick) {
            
            abilityKey.Add(a.button, a);
        } 
    }

    private void Detach(Abilities a) {
        a.Detach(source);
        if (a.onEvent == Event.OnButtonClick) {

            abilityKey.Remove(a.button);
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

    public bool Usable(Abilities a) => a.Usable();
}
