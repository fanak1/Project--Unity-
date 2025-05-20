using System;
using UnityEngine;

public class GamePlayStatesManager : GameStatesManager
{
    public override GameStates StateName { get; protected set; } = GameStates.GAMEPLAY;

    private StageManager stageManager;
    private RewardManager rewardManager;
    private UnitBase playerAlbilitiesHolder;

    public GamePlayState state;
    private GamePlayState beforePause;




    public override void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        rewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();

        if(stageManager == null || rewardManager == null)
        {
            throw new NullReferenceException();
        }

        InitiateManager();

        state = GamePlayState.Play;
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

    public void Pause()
    {
        if (state != GamePlayState.Pause)
        {
            beforePause = state;
            state = GamePlayState.Pause;
            Time.timeScale = 0;
            gameManager.pause = true;
        }
    }

    public void UnPause()
    {
        if (state == GamePlayState.Pause)
        {
            state = beforePause;
            Time.timeScale = 1;
            gameManager.pause = false;
        }
    }


    public void BeginRewardUI()
    {
        if(state == GamePlayState.Play)
        {
            state = GamePlayState.Reward;
            rewardManager.InitReward();
        }
            
    }

    public void GainReward(ScriptableAlbilities a)
    {
        if (state != GamePlayState.Pause)
        {
            playerAlbilitiesHolder = PlayerUnit.instance;
            playerAlbilitiesHolder.AddAbility(a);
            stageManager.RewardStateEnd();
            state = GamePlayState.Play;
        }
        
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

public enum GamePlayState
{
    Play, Pause, Reward
}
