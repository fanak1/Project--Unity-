using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrainingRoomManager : Singleton<TrainingRoomManager>
{

    private List<ScriptableAlbilities> allAbilities;

    public Action OnExit;

    private StageScript trainingRoomScript;

    public EnemyCodeName EnemyCodeName { private set; get; }

    public int NumberOfEnemy { private set; get; }

    private List<EnemyCodeName> listEnemyCodeName;
    private int indexOfEnemy;


    //Guard Condition for change of stats, ability
    public bool changeStats = false;
    public bool changeAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        

        trainingRoomScript = GameObject.FindGameObjectWithTag("TrainingRoom").GetComponent<StageScript>();

        Init();

        Debug.Log(trainingRoomScript.gameObject.name);
    }


    public void AddAbility(ScriptableAlbilities ability) {
        if (PlayerUnit.instance != null) {
            Debug.Log("Added");

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
        EnemyCodeName = enemy;
        NumberOfEnemy = count;
        if(trainingRoomScript != null && count >= 0) {
            trainingRoomScript.ChangeStageContent(enemy, count);
        }
    }

    public void ChangeQuantityOfMonster(int amount) {
        if (NumberOfEnemy + amount < 0) return;
        ChangeTrainingRoom(EnemyCodeName, NumberOfEnemy + amount);
    }

    public void ChangeEnemy(EnemyCodeName enemy) {
        indexOfEnemy = listEnemyCodeName.IndexOf(enemy);
        ChangeTrainingRoom(enemy, NumberOfEnemy);
    }

    public void ChangeEnemyByIndex(int index) {
        indexOfEnemy = index % listEnemyCodeName.Count;
        ChangeEnemy(listEnemyCodeName[indexOfEnemy]);
    }

    public void ChangeEnemyAsCyclic(int amount) {
        indexOfEnemy = (indexOfEnemy + amount) % listEnemyCodeName.Count;
        ChangeEnemyByIndex(indexOfEnemy);
    }


    private void Init() {
        allAbilities = ResourceSystem.Instance.GetAllAbilities();

        listEnemyCodeName = new List<EnemyCodeName>((EnemyCodeName[])Enum.GetValues(typeof(EnemyCodeName)));
        indexOfEnemy = 0;
        EnemyCodeName = listEnemyCodeName[indexOfEnemy];
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

    public void InitializeNewRound() {
        trainingRoomScript.StageStart();
    }
}
