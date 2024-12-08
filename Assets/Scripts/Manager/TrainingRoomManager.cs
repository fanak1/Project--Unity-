using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainingRoomManager : Singleton<TrainingRoomManager>
{

    private List<ScriptableAlbilities> allAbilities;

    public Action OnExit;

    private StageScript trainingRoomScript;

    public EnemyCodeName enemyCodeName { private set; get; }

    public int numberOfEnemy { private set; get; }


    //Guard Condition for change of stats, ability
    public bool changeStats = false;
    public bool changeAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        allAbilities = ResourceSystem.Instance.GetAllAbilities();

        trainingRoomScript = GameObject.FindGameObjectWithTag("TrainingRoom").GetComponent<StageScript>();

        Debug.Log(trainingRoomScript.gameObject.name);
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

    public void ChangeTrainingRoom(EnemyCodeName enemy, int count) {
        enemyCodeName = enemy;
        numberOfEnemy = count;
        if(trainingRoomScript != null) {
            trainingRoomScript.ChangeStageContent(enemy, count);
        }
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
