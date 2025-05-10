using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using TMPro;

public class StageManager : PersistentSingleton<StageManager> {

    public StageState state;

    [SerializeField] private ScriptableStage stage;

    [SerializeField] private int roundIndex; //Index of round

    [SerializeField] private int stateIndex; //Index of state

   // [SerializeField] private SpawnPoint[] spawnList; //GameObject we create in Scene

    [SerializeField] private SpawnPoint spawn;

    [SerializeField] private List<SpawnPoint> spawnList;

    public static int numberEnemyLeft; //number enemy we have to clear each round

    private List<StageState> stateList;

    private List<ScriptableRounds> roundList;

    private bool stateInit = false; //For prevent init loop

    private bool loop = true;

    [SerializeField] private StageScript stageContent;

    //Event ------------------------------------------------------------------------------------------------------------------------------------

    public event Action<int> OnRoundFinish; //On round "number" finish. ex: OnRoundFinish(1) is called when round 1 is finish

    public event Action OnRewardBegin;

    public event Action OnRewardFinish; //On reward of round "number" finish

    public event Action OnStageFinish; //On Clear Stage


    //------------------------------------------------------------------------------------------------------------------------------------------

    private void FinishAndSwitchState() { //Swtich stage
        //Time.timeScale = 0;
        stateInit = false;
        stateIndex++;
        if(stateIndex < stateList.Count) this.state = stateList[stateIndex];
        else
        {
            FinishState();
        }

    }


    public void DecreaseEnemyOnDead() { //Decrease count for each enemy die
        
        numberEnemyLeft -= 1;
    }

    //private IEnumerator SpawnEnemy(List<ScriptableEnemyUnit> enemy, SpawnPoint spawn) { //Spawn enemy at the spawn

    //    StartCoroutine(spawn.BeginSpawn());

    //    yield return new WaitForSeconds(1f);
        
    //    var obj = spawn.SpawnWithDelay(enemy);
    //    foreach(var e in obj) { 
    //        e.OnDead += DecreaseEnemyOnDead;
    //    }
    //}

    private void SpawnEnemy(List<ScriptableEnemyUnit> enemy, Vector3 spawnRange, Vector3 offset) {
        spawn.SetSpawnRange(spawnRange);
        var spawnTemp = Instantiate(spawn, transform.position + offset, Quaternion.identity);
        spawnTemp.SetEnemy(enemy);
    }

    private void SpawnEnemy(List<SpawnPoint> spawn, List<ScriptableEnemyUnit> enemy) {
        List<ScriptableEnemyUnit> temp = new List<ScriptableEnemyUnit>(enemy);
        
        for (int i=0; i<spawn.Count-1; i++) {
            int random = UnityEngine.Random.Range(0, temp.Count+1);
            spawn[i].SetEnemy(ListConfiguration<ScriptableEnemyUnit>.TakeRandomFromList(temp, random));
        }
        spawn[spawn.Count - 1].SetEnemy(ListConfiguration<ScriptableEnemyUnit>.TakeRandomFromList(temp, temp.Count));

        for(int i=0; i<spawn.Count; i++) {
            spawn[i].Spawn();
        }
    }


    private void ReadyState() {
        if (!stateInit) {
            stateInit = true; //Only call this code when done animation of Stage Informing
            FinishAndSwitchState();
        } else {
            //stateInit = false;
            
        }
    }

    private void RoundState() { //Round state - 0

        if (!stateInit) {

            List<ScriptableEnemyUnit> enemyList = new List<ScriptableEnemyUnit>(roundList[roundIndex].LoadEnemyForRound()); //List of enemy in this round

            stateInit = true; //Prevent loop init

            //Load enemy of this round

            numberEnemyLeft = enemyList.Count; //number enemy we have to clear to win

            SpawnEnemy(spawnList, enemyList);

            /*
            int firstSpawn = UnityEngine.Random.Range(0, (numberEnemyLeft + 1) / 2); //number of enemy we spawn in first spawnpoint
            int secondSpawn = numberEnemyLeft - firstSpawn; //number of enemy we spawn in second spawnpoint

            List<ScriptableEnemyUnit> enemyList1 = new List<ScriptableEnemyUnit>();
            List<ScriptableEnemyUnit> enemyList2 = new List<ScriptableEnemyUnit>();

            for (int i = 0; i < firstSpawn; i++) { //Spawn enemy in first spawnpoint
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                enemyList1.Add(enemyList[enemyIndex]);
                
            }

            for(int i=0; i< secondSpawn; i++) { //Spawn enemy in second spawn point
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                enemyList2.Add(enemyList[enemyIndex]);

            }
            //StartCoroutine(SpawnEnemy(enemyList1, spawnList[spawnFirst]));
            //StartCoroutine(SpawnEnemy(enemyList2, spawnList[spawnSecond]));

            SpawnEnemy(enemyList1, new Vector3(3, 3, 0), new Vector3(1, 1, 0));
            SpawnEnemy(enemyList2, new Vector3(3, 3, 0), new Vector3(-1, 1, 0));

            */
            
        }
        
        if(numberEnemyLeft <= 0) { //When we cleared this round

            OnRoundFinish?.Invoke(roundIndex);
            FinishAndSwitchState();
            roundIndex++;
        }
    }


    private void RewardState() {
        if (!stateInit) {
            OnRewardBegin?.Invoke();
            stateInit = true;
        }
    }

    public void RewardStateEnd() {
        OnRewardFinish?.Invoke();
        FinishAndSwitchState();
    }

    private void FinishState() {
        if (!stateInit) {
            
            stateInit = true;
            stageContent.StageFinish();
            OnStageFinish?.Invoke();
        }
        
    }

    private void ResetStageParameter() {
        roundIndex = 0;
        stateIndex = -1;
        stateInit = false;

        stateList = stage.GetStateList();

        roundList = stage.GetRoundList();

        state = StageState.Unready;
    }

    public void ChangeStage(ScriptableStage stage) {
        this.stage = stage;
        this.Start();
    }

    public void Ready() {
        state = StageState.Ready;
        stateInit = false;
    }


    internal virtual void Start() {
        ResetStageParameter();
    }



    public void StartStage(StageScript s) {
        stageContent = s;
        stage = stageContent.stageContent;
        spawnList = stageContent.GetSpawnPoints();
        ResetStageParameter();
        stageContent.StageBegin();
    }

    internal virtual void Update() {
        if (!loop) return;
        switch (state) {
            case StageState.Ready:
                ReadyState();
                break;
            case StageState.Round:
                RoundState();
                break;
            case StageState.Reward:
                RewardState();
                break;
            case StageState.Finish:
                FinishState();
                break;
            default:
                break;
        }
    }
}

public enum SpawnDirection {
    Left = 0,
    Right = 2,
    Up = 1,
    Down = 3
}

