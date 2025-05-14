using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;


public class LevelManager : StaticInstance<LevelManager>
{
    public Dictionary<StageScript, bool> stageFinished = new Dictionary<StageScript, bool>();

    public Dictionary<string, StageScript> stageLists = new Dictionary<string, StageScript>();

    public StageScript currentStage;

    public Level level;


    public LevelData data;

    public void LoadLevelToScene(string levelName) { 
        var levelCheck = ResourceSystem.Instance.GetLevel(levelName);
        if (levelCheck != null)
        {
            level = Instantiate(levelCheck);
            level.gameObject.name = levelCheck.name;
        } else 
            return;
        
        var env = GameObject.Find("Environment/Grid");
        if (env != null)
        {
            level.transform.SetParent(env.transform);
        }
        var stages = level.gameObject.transform.GetComponentsInChildren<StageScript>();
        foreach (var s in stages)
        {
            s.Init();
        }
        

        
    }
    
    public void LoadStageInScene(LevelData data) {

        this.data = data;

        LoadLevelToScene(data.levelName);

        var en = GameObject.Find("Environment/Grid");
        var level = en.GetComponentInChildren<Level>();
        var levelObj = level.gameObject;
        var stages = levelObj.GetComponentsInChildren<StageScript>();
        
        stageLists.Clear();
        stageFinished.Clear();

        if(data.stageFinished == null) {
            foreach (var stage in stages) {
                stageLists.Add(stage.gameObject.name, stage);
                stageFinished[stage] = false;
            }
        } else {
            foreach (var stage in stages) {
                stageLists.Add(stage.gameObject.name, stage);
                if (data.stageFinished == null || !data.stageFinished.ContainsKey(stage.gameObject.name)) {
                    stageFinished[stage] = false;
                } else {
                    stageFinished[stage] = true;
                    stage.StageFinish(() => { });
                }
            }
        }
        

        if(data.currentStage != null)
            currentStage = stageLists[data.currentStage];
        else {
            currentStage = stages[0];
        }

        LoadStage(currentStage.gameObject.name);

    }

    public LevelData SaveLevelData() {
        LevelData data = new();
        foreach (var stage in stageFinished) {
            data.stageFinished[stage.Key.gameObject.name] = stage.Value;
        }
        data.currentStage = currentStage.gameObject.name;
        data.levelName = level.gameObject.name;

        return data;
    }

    public void StageFinish(StageScript stage) {
        stageFinished[stage] = true;
    }

    public void StageStepInto(StageScript stage) {
        currentStage = stage;
    }

    public StageScript LoadStage(string name) {
        StageScript stage = stageLists[name];
        var playerSpawn = GameObject.Find("Environment/PlayerSpawn");
        playerSpawn.transform.position = stage.gameObject.transform.position;
        var spawn = playerSpawn.GetComponent<PlayerSpawn>();
        if(spawn != null) spawn.Spawn();
        else
        {
            
        }
        return stage;
    }

    public void Start() {

    }



}

public struct LevelData {
    public Dictionary<string, bool> stageFinished;

    public string currentStage;

    public string levelName;
}

