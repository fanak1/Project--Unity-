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

    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        cipherManager = cipherManagerGameObject.GetComponent<CipherManager>();
        rewardManager = rewardManagerGameObject.GetComponent<RewardManager>();

        playerAlbilitiesHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AlbilitiesHolder>();

        InitiateManager();
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

    }

}
