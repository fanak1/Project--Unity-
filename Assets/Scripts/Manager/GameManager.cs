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
    private UnitBase playerAlbilitiesHolder;

    public ExitDoor exitDoor;

    private Difficulty difficulty;

    public List<ScriptableStage> stageList; //Will have to generate stageList for GameManager (Can be random or scripted)

    public int stageIndex;


    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        cipherManager = cipherManagerGameObject.GetComponent<CipherManager>();
        rewardManager = rewardManagerGameObject.GetComponent<RewardManager>();

        InitiateManager();

        Debug.Log("New Stage: " + stageIndex);

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
        playerAlbilitiesHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<UnitBase>();
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
        Debug.Log("Stage: " + stageIndex);

        LoadNewStage(stageList[stageIndex]);

        stageManager.Ready();

        stageIndex++;
    }

    private void GenerateRewardForNewStage() {
        //Give Reward Manager a List of Ability
    }

    public void LoadNewStage(ScriptableStage stage) {
        //Give Stage Manager a Scriptable Stage

        stageManager.ChangeStage(stage);
    }

    public void StageClear() { // Let OnStageClear of stage manager += StageClear to call when stage is cleared
        //StartCoroutine(NewStage);
        var exit = Instantiate(exitDoor, transform.position, Quaternion.identity);
        exit.OnDoorEnter += NextStage;
    }

    public void LoadScene() {
        SceneManager.LoadScene(0);
    }

    public void NextStage() {
        if (stageIndex >= stageList.Count) {
            Debug.Log("Done All");
            return;
        } else {
            Debug.Log("Stage: " + stageIndex);
            NewStage();
            LoadScene();
        }
    }

    public void LoadStage(ScriptableStage stage) {
        LoadNewStage(stage);
        stageManager.Ready();
        LoadScene();
    }
}