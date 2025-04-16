using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameObject stageManagerGameObject;
    [SerializeField] private GameObject rewardManagerGameObject;

    private StageManager stageManager;
    private RewardManager rewardManager;
    private UnitBase playerAlbilitiesHolder;

    public GameObject inventoryTest;

    //public ExitDoor exitDoor;

    public int stageIndex;

    private bool inventoryOpen = false;

    public int concious;

    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        rewardManager = rewardManagerGameObject.GetComponent<RewardManager>();

        InitiateManager();


        //NewStage();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!inventoryOpen) {
                inventoryTest.SetActive(true);
                inventoryOpen = true;
            } else {
                inventoryTest.SetActive(false);
                inventoryOpen = false;
            }
        }
    }

    public void BeginRewardUI() {
        rewardManager.InitReward();
    }

    public void GainReward(ScriptableAlbilities a) {
        playerAlbilitiesHolder = PlayerUnit.instance;
        playerAlbilitiesHolder.AddAbility(a);
        stageManager.RewardStateEnd();
    }


    public void GainAbility(ScriptableAlbilities a) {
        playerAlbilitiesHolder = PlayerUnit.instance;
        playerAlbilitiesHolder.AddAbility(a);
    }
    private void InitiateManager() {

        stageManager.OnRewardBegin += BeginRewardUI;

        stageManager.OnStageFinish += StageClear;

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
        //var exit = Instantiate(exitDoor, transform.position, Quaternion.identity);
        //exit.OnDoorEnter += NextStage;
    }

    public void LoadScene() {
        inventoryOpen = false;
        inventoryTest.SetActive(false);
        SceneManager.LoadScene(0);
    }


    public void LoadStage(ScriptableStage stage) {
        LoadNewStage(stage);
        stageManager.Ready();
        LoadScene();
    }

    public void AddAbility(ScriptableAlbilities ability) {
        if (PlayerUnit.instance != null) {

            PlayerUnit.instance.AddAbility(ability);
        }
    }

    public void DeleteAbility(ScriptableAlbilities ability) {
        if (PlayerUnit.instance != null) {
            PlayerUnit.instance.DeleteAbility(ability);
        }
    }
}
