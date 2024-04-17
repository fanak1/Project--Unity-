using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameObject stageManagerGameObject;
    [SerializeField] private GameObject cipherManagerGameObject;
    [SerializeField] private GameObject rewardManagerGameObject;

    private StageManager stageManager;
    private CipherManager cipherManager;
    private RewardManager rewardManager;
    private AlbilitiesHolder playerAlbilitiesHolder;

    private Difficulty difficulty;

    public List<ScriptableStage> stageList; //Will have to generate stageList for GameManager (Can be random or scripted)

    private int stageIndex;


    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        cipherManager = cipherManagerGameObject.GetComponent<CipherManager>();
        rewardManager = rewardManagerGameObject.GetComponent<RewardManager>();

        playerAlbilitiesHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AlbilitiesHolder>();

        InitiateManager();

        NewStage();
    }

    public void BeginCipherUI() {
        cipherManager.InitiateQuestion(difficulty);
    }

    public void CloseCipherUI() {
        stageManager.CipherStateEnd();
    }

    public void BeginRewardUI() {
        rewardManager.InitReward();
    }

    public void GainReward(ScriptableAlbilities a) {
        playerAlbilitiesHolder.AddAbility(a);
        stageManager.RewardStateEnd();
    }

    private void InitiateManager() {
        stageManager.OnCipherBegin += BeginCipherUI;
        cipherManager.OnCipherFinish += CloseCipherUI;

        stageManager.OnRewardBegin += BeginRewardUI;
        rewardManager.OnRewardFinish += GainReward;

        stageManager.OnStageFinish += StageClear;

    }

    private void NewStage() {

        //LoadNewStage();

        //GenerateRewardForNewStage();

        //Delay

        //if Done then

        //stageManager.Ready();

        if(stageIndex >= stageList.Count) {
            Debug.Log("Done All");
            return;
        }

        LoadNewStage(stageList[stageIndex]);

        stageManager.Ready();

        stageIndex++;
    }

    private void GenerateRewardForNewStage() {
        //Give Reward Manager a List of Ability
    }

    private void LoadNewStage(ScriptableStage stage) {
        //Give Stage Manager a Scriptable Stage

        stageManager.ChangeStage(stage);
    }

    public void StageClear() { // Let OnStageClear of stage manager += StageClear to call when stage is cleared
        //StartCoroutine(NewStage);
        NewStage();
    }
}
