using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : PersistentSingleton<ResourceSystem>
{
    //Enemy---------------------------------------------------------------------------------------------------------------------------------------------------
    public List<ScriptableEnemyUnit> EnemyUnitList {  get; private set; } //List to load the Resources into
    private Dictionary<EnemyCodeName, ScriptableEnemyUnit> _EnemyUnitDict; //Dictionary for find EnemyUnit with code name
    public List<ScriptableAlbilities> allAbilities;

    public List<ScriptableStage> allStages;
    private Dictionary<StageType, List<ScriptableStage>> allStagesDict = new();


    public ScriptableEnemyUnit GetEnemyWithCodeName(EnemyCodeName enemyCodeName) => _EnemyUnitDict[enemyCodeName]; //Get 1 ScriptableEnemyUnit with code name

    //-------------------------------------------------------------------------------------------------------------------------------------------------Enemy



    //Level----------------------------------------------------------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------------------------------------------------Level
    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        EnemyUnitList = Resources.LoadAll<ScriptableEnemyUnit>("Enemy").ToList(); //Load all ScriptableEnemyUnit from "EnemyUnit" folder inside "Resources" folder to List
        _EnemyUnitDict = EnemyUnitList.ToDictionary(r => r.enemyCodeName, r => r); //Insert all enemy from list to dict to find with code name

        allAbilities = Resources.LoadAll<ScriptableAlbilities>("Abilities").ToList();

        allStages = Resources.LoadAll<ScriptableStage>("Stages").ToList();

        foreach (StageType stage in Enum.GetValues(typeof(StageType)))
        {
            allStagesDict[stage] = new List<ScriptableStage>();
        }

        allStages.ForEach(stage =>
        {
            allStagesDict[stage.stageType].Add(stage);    
        });
    }

    public List<ScriptableAlbilities> GetAllAbilities() => allAbilities;

    public List<ScriptableStage> GetStages(StageType type) => allStagesDict[type];

    public Level GetLevel(string levelName) => Resources.Load<Level>($"Environment/Level/{levelName}");

    public T GetComponent<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    
}
