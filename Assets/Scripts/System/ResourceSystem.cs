using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSystem : PersistentSingleton<ResourceSystem>
{
    //Enemy---------------------------------------------------------------------------------------------------------------------------------------------------
    public List<ScriptableEnemyUnit> EnemyUnitList {  get; private set; } //List to load the Resources into
    private Dictionary<EnemyCodeName, ScriptableEnemyUnit> _EnemyUnitDict; //Dictionary for find EnemyUnit with code name
    public List<ScriptableAlbilities> allAbilities;
    public Dictionary<Rarity, List<ScriptableAlbilities>> allAbilitiesByRarity = new();
    public Dictionary<string, ScriptableAlbilities> allAbilitiesDict;

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


        //Ability start
        allAbilities = Resources.LoadAll<ScriptableAlbilities>("Abilities").ToList();
        allAbilitiesDict = allAbilities.ToDictionary(a => a.name, a => a);

        foreach(Rarity r in Enum.GetValues(typeof(Rarity)))
        {
            allAbilitiesByRarity[r] = new List<ScriptableAlbilities>();
        }

        allAbilities.ForEach(a =>
        {
            allAbilitiesByRarity[a.rarity].Add(a);
        });

        //Ability end

        //AllStage start
        allStages = Resources.LoadAll<ScriptableStage>("Stages").ToList();

        foreach (StageType stage in Enum.GetValues(typeof(StageType)))
        {
            allStagesDict[stage] = new List<ScriptableStage>();
        }

        allStages.ForEach(stage =>
        {
            allStagesDict[stage.stageType].Add(stage);    
        });
        //AllStage end
    }

// -- Ability Section

    public List<ScriptableAlbilities> GetAllAbilities() => allAbilities;

    public ScriptableAlbilities GetAlbilities(string name) => allAbilitiesDict[name];

    public List<ScriptableAlbilities> GetListAlbilities(List<string> list)
    {
        List<ScriptableAlbilities> l = new();
        list.ForEach(s => l.Add(GetAlbilities(s)));
        return l;
    }

    public List<ScriptableAlbilities> GetAllAbilitiesForCharacter(CharacterCode characterCode)
    {
        var list = new List<ScriptableAlbilities>(allAbilities);
        list.RemoveAll(a => { return a.characterCode != CharacterCode.None && a.characterCode != characterCode; });
        return list;
    }

    public List<ScriptableAlbilities> GetAllAbilitiesByRarity(Rarity rarity)
    {
        return allAbilitiesByRarity[rarity];
    }

    public ScriptableAlbilities GetHigherRarityAbilitiy(ScriptableAlbilities a)
    {
        if (a.onEvent == Event.IncreaseStat) return a;
        Rarity r;
        if (a.rarity == Rarity.Normal)
            r = Rarity.Rare;
        else if (a.rarity == Rarity.Rare)
            r = Rarity.Epic;
        else if (a.rarity == Rarity.Epic)
            r = Rarity.Legendary;
        else
            r = Rarity.Legendary;

        ScriptableAlbilities temp = a;
        foreach (var ab in allAbilitiesByRarity[r])
        {
            if(ab.skillType == a.skillType)
            {
                temp = ab;
                break;
            }
        }
        return temp;
    }

    public List<ScriptableAlbilities> GetClaimableAbilitiesForPlayer()
    {
        var pa = PlayerUnit.instance.ShowAbilities();
        var list = new List<ScriptableAlbilities>();
        foreach(var a1 in allAbilities)
        {
            bool addFlag = true;
            foreach(var a2 in pa)
            {
                if(a1.skillType == a2.skillType)
                {
                    if (a1.rarity <= a2.rarity)
                    {
                        addFlag = false;
                    } 
                    break;
                }
                
            }
            if(addFlag) list.Add(a1);
        }
        return list;
    }

// End Ability Section --

    public List<ScriptableStage> GetStages(StageType type) => allStagesDict[type];

    public ScriptableStage GetStageByName(string name)
    {
        return Resources.Load<ScriptableStage>($"Stages/{name}");
    }


    public Level GetLevel(string levelName) => Resources.Load<Level>($"Environment/Level/{levelName}");

    public T GetComponent<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    
}
