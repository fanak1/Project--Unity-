using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;
using System.Runtime.CompilerServices;


public class LevelManager : StaticInstance<LevelManager>
{
    public Dictionary<StageScript, bool> stageFinished = new Dictionary<StageScript, bool>();

    public Dictionary<string, StageScript> stageLists = new Dictionary<string, StageScript>();

    public StageScript currentStage;

    public bool isLastLevel;

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

        GenerateStageContent(stages, data.stageContents);

        if (data.stageFinished == null) {
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


        if (data.currentStage != null)
            currentStage = stageLists[data.currentStage];
        else {
            currentStage = stages[0];
        }

        isLastLevel = data.isLastLevel;

        LoadStage(currentStage.gameObject.name);

    }

    public LevelData SaveLevelData() {
        LevelData data = new();
        foreach (var stage in stageFinished) {
            data.stageFinished[stage.Key.gameObject.name] = stage.Value;
        }
        foreach (var stage in stageLists)
        {
            data.stageContents[stage.Key] = stage.Value.stageContent.name;
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
        if (spawn != null) spawn.Spawn();
        else
        {

        }
        return stage;
    }

    public void Start() {

    }


    private StageScript[] GenerateStageContent(StageScript[] stages, Dictionary<string, string> stageContents = null)
    {
        if (stageContents != null && stageContents.Count >= 0)
        {
            foreach (var stage in stages)
            {
                GenerateContentForStage(stage, stageContents[stage.name]);
            }
        }
        else
            foreach (StageScript stage in stages)
            {
                if (data.isTraining)
                {
                    return stages;
                }
                if (stage.stageTypes.Length > 0)
                {
                    GenerateRandomContentForStage(stage, stage.stageTypes);
                }
                else
                {
                    GenerateRandomContentForStage(stage, new StageType[1] { StageType.Interacting });
                }
            }
        return stages;
    }

    private StageScript GenerateRandomContentForStage(StageScript stage, StageType[] stageType)
    {
        int radSize = stageType.Length;
        int rad = UnityEngine.Random.Range(0, radSize);
        List<ScriptableStage> stages = ResourceSystem.Instance.GetStages(stageType[rad]);
        radSize = stages.Count;
        if (radSize <= 0)
        {
            Debug.LogError($"Stupid when get StageType: {stageType[rad]} of Stages: {stages}");
        }
        rad = UnityEngine.Random.Range(0, radSize);
        stage.stageContent = stages[rad];
        return stage;
    }

    private StageScript GenerateContentForStage(StageScript stage, string stageName)
    {
        var stageContent = ResourceSystem.Instance.GetStageByName(stageName);
        if (stageContent != null)
        {
            stage.stageContent = stageContent;
        }
        return stage;
    }



}

public struct LevelData {
    public Dictionary<string, bool> stageFinished;

    public Dictionary<string, string> stageContents;

    public string currentStage;

    public string levelName;

    public bool isTraining;

    public bool isLastLevel;
}

