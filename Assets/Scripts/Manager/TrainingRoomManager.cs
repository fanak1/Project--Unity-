using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainingRoomManager : Singleton<TrainingRoomManager>
{

    private List<ScriptableAlbilities> allAbilities;

    public Action OnExit;


    //Guard Condition for change of stats, ability
    public bool changeStats = false;
    public bool changeAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        allAbilities = ResourceSystem.Instance.GetAllAbilities();
    }


    public void AddAbility(ScriptableAlbilities ability) {
        if (PlayerUnit.instance != null) {
            PlayerUnit.instance.AddAbility(ability);
            changeStats = true;
            changeAbility = true;
        }
    }

    public List<ScriptableAlbilities> GetPlayerAbility() {
        if(PlayerUnit.instance != null) {
            return PlayerUnit.instance.ShowAbilities();
        } 
        return null;
    }


    private void Init() {

    }

    public void Interact() {
        ComputerUI.Instance.TurnOn();
    }

    public void Exit() {
        OnExit?.Invoke();
    }

    public Stats ShowBaseStats() {
        return PlayerUnit.instance.ShowBaseStats();
    }

    public Stats ShowIncreaseStats() {
        return PlayerUnit.instance.ShowIncreaseStats();
    }
}
