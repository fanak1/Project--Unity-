using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : StaticInstance<ResourceSystem>
{
    //Enemy---------------------------------------------------------------------------------------------------------------------------------------------------
    public List<ScriptableEnemyUnit> EnemyUnitList {  get; private set; } //List to load the Resources into
    private Dictionary<EnemyCodeName, ScriptableEnemyUnit> _EnemyUnitDict; //Dictionary for find EnemyUnit with code name
    public List<ScriptableAlbilities> allAbilities;

    public ScriptableEnemyUnit GetEnemyWithCodeName(EnemyCodeName enemyCodeName) => _EnemyUnitDict[enemyCodeName]; //Get 1 ScriptableEnemyUnit with code name

    //---------------------------------------------------------------------------------------------------------------------------------------------------------


    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        EnemyUnitList = Resources.LoadAll<ScriptableEnemyUnit>("Enemy").ToList(); //Load all ScriptableEnemyUnit from "EnemyUnit" folder inside "Resources" folder to List
        _EnemyUnitDict = EnemyUnitList.ToDictionary(r => r.enemyCodeName, r => r); //Insert all enemy from list to dict to find with code name

        allAbilities = Resources.LoadAll<ScriptableAlbilities>("Abilities").ToList();
    }

    public List<ScriptableAlbilities> GetAllAbilities() => allAbilities;


    
}
