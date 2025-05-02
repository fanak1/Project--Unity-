using System;
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

    

    //public ExitDoor exitDoor;

    public int stageIndex;

    public int concious;

    public string currentScene;


    private void Start() {
        stageManager = stageManagerGameObject.GetComponent<StageManager>();
        rewardManager = rewardManagerGameObject.GetComponent<RewardManager>();

        InitiateManager();


        //NewStage();
    }

    private void Update() {
        
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



 // -- Scene Manager code
    public void LoadScene(string sceneName, Action onSceneLoaded) {
        CoroutineManager.Instance.StartNewCoroutine(LoadSceneThenExecute(sceneName, onSceneLoaded));
    }

    private IEnumerator LoadSceneThenExecute(string sceneName, Action onSceneLoaded) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            // -- Can do animation code there

            // Animation code end --
            yield return null;
        }

        // -- After scene loaded code
        onSceneLoaded?.Invoke();
        // -- End after scene loaded code
    }

    public void StartGame() {
        LoadScene("GamePlayScene", () => {
            LevelManager.Instance.LoadStageInScene(new LevelData());
        });
    }

    public void LoadGame(string path = "") {
        string filePath = Application.persistentDataPath + path + "/save.json";
        string saveGameData = System.IO.File.ReadAllText(filePath);

        SaveGameData data = JsonUtility.FromJson<SaveGameData>(saveGameData);

        LoadScene("GamePlayScene", () => {
            LevelManager.Instance.LoadStageInScene(data.level);
        });
    }


    public void Menu() {

        // -- Trigger Point cut
        SaveGame();
        // End Trigger Pointcut --

        LoadScene("MenuScene", () => {
            
        });
    }

    public void SaveGame(string path = "") {
        SaveGameData saveGame = new();
        if (LevelManager.Instance) {
            saveGame.level = LevelManager.Instance.data;
        }

        string saveGameData = JsonUtility.ToJson(saveGame);
        string filePath = Application.persistentDataPath + path + "/save.json";
        System.IO.File.WriteAllText(filePath, saveGameData);

    }


    // Scene Manger code end --
}

public struct SaveGameData {
    public LevelData level;
}
