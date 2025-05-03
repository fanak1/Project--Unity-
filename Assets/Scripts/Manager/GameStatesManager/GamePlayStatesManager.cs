using System;
using UnityEngine;

public class GamePlayStatesManager : GameStatesManager
{
    public override GameStates StateName { get; protected set; } = GameStates.GAMEPLAY;

    private StageManager stageManager;
    private RewardManager rewardManager;
    private UnitBase playerAlbilitiesHolder;


    public override void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        rewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();

        if(stageManager == null || rewardManager == null)
        {
            throw new NullReferenceException();
        }

        InitiateManager();
    }

    public override void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!inventoryOpen)
        //    {
        //        inventoryTest.SetActive(true);
        //        inventoryOpen = true;
        //    }
        //    else
        //    {
        //        inventoryTest.SetActive(false);
        //        inventoryOpen = false;
        //    }
        //}
    }


    public void BeginRewardUI()
    {
        rewardManager.InitReward();
    }

    public void GainReward(ScriptableAlbilities a)
    {
        playerAlbilitiesHolder = PlayerUnit.instance;
        playerAlbilitiesHolder.AddAbility(a);
        stageManager.RewardStateEnd();
    }


    public void GainAbility(ScriptableAlbilities a)
    {
        playerAlbilitiesHolder = PlayerUnit.instance;
        playerAlbilitiesHolder.AddAbility(a);
    }
    private void InitiateManager()
    {

        stageManager.OnRewardBegin += BeginRewardUI;

        stageManager.OnStageFinish += StageClear;

        rewardManager.gameStatesManager = this;

    }

    private void GenerateRewardForNewStage()
    {
        //Give Reward Manager a List of Ability
    }

    public void LoadNewStage(ScriptableStage stage)
    {
        //Give Stage Manager a Scriptable Stage

        stageManager.ChangeStage(stage);
    }

    public void StageClear()
    { // Let OnStageClear of stage manager += StageClear to call when stage is cleared
        //StartCoroutine(NewStage);
        //var exit = Instantiate(exitDoor, transform.position, Quaternion.identity);
        //exit.OnDoorEnter += NextStage;
    }

    public void LoadStage(ScriptableStage stage)
    {
        LoadNewStage(stage);
        stageManager.Ready();
    }
}
