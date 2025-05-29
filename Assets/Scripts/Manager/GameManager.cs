using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{

    

    //public ExitDoor exitDoor;

    public int stageIndex;

    public int concious;


    public string currentScene;

    public GameStatesManager gameStates;

    private GameStates currentState;

    public bool pause = false;

    public int currentMoney;

    public Action MoneyChangeObserver;

    public int level = 0;

    public Statistics currentStatistics;

    public SaveGameData tempSave;


    private void Start() {
        
    }

    private void Update() {

        // Temporary check gameStates null, will remove when we init gamestate at the first frame of application
        if(gameStates != null) 
            gameStates.Update();

    }

    public bool TryToPause()
    {

        if(gameStates.GetType() == typeof(GamePlayStatesManager))
        {
            ((GamePlayStatesManager) gameStates).Pause();
            return true;
        }
        return false;
    }

    public bool TryToResume()
    {
        if (gameStates.GetType() == typeof(GamePlayStatesManager))
        {
            ((GamePlayStatesManager)gameStates).UnPause();
            return true;
        }
        return false;
    }

    // -- Game State

    public void BeginState(GameStatesManager gameState)
    {
        this.gameStates = gameState;
        this.currentState = gameState.StateName;
        this.gameStates.Start();
    }
    

    public void BeginState(GameStates gameState)
    {
        BeginState(Registry.States(gameState));
    }


    // Game State --



    // -- Multi role

    public void LoadScene() {
        
        SceneManager.LoadScene(0);
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

    public void IncreaseMoney(int money)
    {
        this.currentMoney += money;
        currentStatistics.money += money;
        MoneyChangeObserver?.Invoke();
    }

    public void DecreaseMoney(int money)
    {
        this.currentMoney -= money;
        MoneyChangeObserver?.Invoke();
    }

 // -- Scene Manager code
    public void LoadScene(string sceneName, Action onSceneLoaded) {
        CoroutineManager.Instance.StartNewCoroutine(LoadSceneThenExecute(sceneName, onSceneLoaded));
    }

    private IEnumerator LoadSceneThenExecute(string sceneName, Action onSceneLoaded) {
        float time = 0f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        Transition.Instance.StartLoading();

        while (asyncLoad.progress < 0.9f && time < 4f) {
            time += Time.deltaTime;
            yield return null;
        }

        if (time < 1f)
        {
            yield return new WaitForSeconds(1 - time);
        } 

        asyncLoad.allowSceneActivation = true;

        yield return null;
        // -- After scene loaded code
        onSceneLoaded?.Invoke();
        Transition.Instance.EndLoading();
        // -- End after scene loaded code
    }

    public LevelData LoadRandomLevel()
    {
        LevelData data = new LevelData();
        data.levelName = "Level_Sand";
        return data;
    }

    //Lambda for load scene: 1/Load Character data, 2/Load Level

    public void StartGame(ScriptablePlayerUnit player)
    {
        level = 1;
        LoadScene("GamePlayScene", () => {
            PlayerSpawn.Instance.scriptablePlayer = player;
            LevelManager.Instance.LoadStageInScene(LoadRandomLevel());
            BeginState(GameStates.GAMEPLAY);
        });
    }

    public void StartGame(bool persistent = false) {
        if(!persistent)
        {
            currentStatistics = new();
            LoadScene("GamePlayScene", () => {
                LevelManager.Instance.LoadStageInScene(LoadRandomLevel());
                BeginState(GameStates.GAMEPLAY);
            });
        } else
        {
            LoadScene("GamePlayScene", () => {
                PlayerSpawn.Instance.data = tempSave.player;
                LevelManager.Instance.LoadStageInScene(LoadRandomLevel());
                BeginState(GameStates.GAMEPLAY);
            });
        }
        
    }

    public void NextLevel()
    {
        CreateTempSave();
        if(level == 3)
        {
            StartLastLevel();
        } else
        {
            level++;
            StartGame(true);
        }
    }

    public void StartLastLevel()
    {
        LoadScene("GamePlayScene", () => {
            PlayerSpawn.Instance.data = tempSave.player;
            LevelData data = LoadRandomLevel();
            data.isLastLevel = true;
            LevelManager.Instance.LoadStageInScene(data);
            BeginState(GameStates.GAMEPLAY);
        });
    }

    public void StartTrainingLevel()
    {
        LoadScene("GamePlayScene", () =>
        {
            LevelData data = new LevelData();
            data.levelName = "Training_Level";
            data.isTraining = true;
            LevelManager.Instance.LoadStageInScene(data);
            BeginState(GameStates.GAMEPLAY);
        });
    }

    public void LoadGame(string path = "") {
        string filePath = Application.persistentDataPath + path + "/save.json";
        string saveGameData = System.IO.File.ReadAllText(filePath);

        SaveGameData data = JsonUtility.FromJson<SaveGameData>(saveGameData);

        LoadScene("GamePlayScene", () => {
            LevelManager.Instance.LoadStageInScene(data.level);
            BeginState(GameStates.GAMEPLAY);
        });
    }


    public void Menu() {

        // -- Trigger Point cut
        SaveGame();
        // End Trigger Pointcut --

        LoadScene("MenuScene", () => {
            
        });
    }

    public void CreateTempSave()
    {
        SaveGameData saveGame = new();
        if (LevelManager.Instance)
        {
            saveGame.level = LevelManager.Instance.data;
        }
        if (PlayerUnit.instance)
        {
            saveGame.player = PlayerUnit.instance.Save();
        }
        saveGame.stats = currentStatistics;

        saveGame.runLevel = level;

        tempSave = saveGame;
    }

    public void SaveGame(string path = "") {
        SaveGameData saveGame = new();
        if (LevelManager.Instance) {
            saveGame.level = LevelManager.Instance.data;
        }
        if (PlayerUnit.instance)
        {
            saveGame.player = PlayerUnit.instance.Save();
        }
        saveGame.stats = currentStatistics;

        saveGame.runLevel = level;

        string saveGameData = JsonUtility.ToJson(saveGame);
        string filePath = Application.persistentDataPath + path + "/save.json";
        System.IO.File.WriteAllText(filePath, saveGameData);

    }

    public void CalculateScore(float timeFinish, float stageScore, int numberOfEnemyKill)
    {
        float score = (stageScore * 10) + (numberOfEnemyKill * 5) + (1000f / Mathf.Max(Mathf.Min(timeFinish, 360f), 10f));
        currentStatistics.score += (int)score;
    }


    // Scene Manger code end --

    public void QuitGame()
    {
        Application.Quit();
    }
}


    // Multi role --

public struct SaveGameData {
    public LevelData level;
    public PlayerData player;
    public Statistics stats;
    public int runLevel;
}

public struct Statistics
{
    public int score;
    public int money;
}
