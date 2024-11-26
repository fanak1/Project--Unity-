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

    //Cipher --------------------------------------------------------------------------------------------------------------------------------------------------

    public List<ScriptableCipher> cipherList { get; private set; } //List of cipher to load

    public ScriptableCipher GetRandomCipherWithDifficulty(Difficulty d) { //Get the random cipher with a difficulty
        List<ScriptableCipher> list = new List<ScriptableCipher>();
        foreach(ScriptableCipher cipher in cipherList) {
            if(cipher.difficulty == d) {
                list.Add(cipher);
            }
        }
        int random = Random.Range(0, list.Count);
        return list[random];
    }

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        EnemyUnitList = Resources.LoadAll<ScriptableEnemyUnit>("Enemy").ToList(); //Load all ScriptableEnemyUnit from "EnemyUnit" folder inside "Resources" folder to List
        _EnemyUnitDict = EnemyUnitList.ToDictionary(r => r.enemyCodeName, r => r); //Insert all enemy from list to dict to find with code name

        cipherList = Resources.LoadAll<ScriptableCipher>("Cipher").ToList();
    }

    public List<ScriptableAlbilities> GetAllAbilities() => allAbilities;


    
}
