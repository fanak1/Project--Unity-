using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

public class GameManager : PersistentSingleton<GameManager>
{

    

    //public ExitDoor exitDoor;

    public int stageIndex;

    public int concious;


    public string currentScene;

    public GameStatesManager gameStates;

    private GameStates currentState;



    private void Start() {
        //Will remove when have more state
        BeginState(GameStates.GAMEPLAY);

    }

    private void Update() {

        gameStates.Update();

    }

    // -- Game State

    public void BeginState(GameStatesManager gameState)
    {
        this.gameStates = gameState;
        this.currentState = gameState.StateName;
        gameState.Start();
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


    // Multi role --

public struct SaveGameData {
    public LevelData level;
}
