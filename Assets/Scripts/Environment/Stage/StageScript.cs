using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StageScript : MonoBehaviour
{

    [SerializeField] private List<StageDoor> doors;

    [SerializeField] private List<StageScript> scripts;


    [SerializeField] private StageTrigger trigger;

    [SerializeField] private List<SpawnPoint> spawnPoint;

    public ScriptableStage stageContent;

    private bool isStarted = false;

    internal void Start() {
        
    }

    internal void Init()
    {
        ResetDoors();
    }

    private void ResetDoors() {
        
        foreach(var d in doors)
        {
            d.Init();
        }
        CloseDoor(() => { });
    }

    public void FindChildName(string name)
    {

    }

    public void StageStart() {
        CloseDoor(() =>
        {
            if(!isStarted)
            {
                StageManager.Instance.StartStage(this);
                isStarted = true;
            }
            
        });
        
        //trigger.gameObject.SetActive(false);
        
    }
    
    public void StageBegin() {
        //Initiate animation of stage
        StageManager.Instance.Ready();
    }

    public void StageFinish(Action onDoorOpen) {
        OpenDoor(onDoorOpen);
        trigger.Clear();
        //trigger.gameObject.SetActive(true);
    }

    private void CloseDoor(Action o) {
        foreach (var d in doors)
        {
            d.gameObject.SetActive(true);
            d.CloseDoor( o);
        }
    }

    private void OpenDoor(Action onDoorOpen) {
        foreach (var d in doors)
        {
            if (!d.gameObject.activeSelf) d.gameObject.SetActive(true);
            d.OpenDoor(() => { onDoorOpen(); d.setActiveInNextFrame = false; });
        }
    }

    public void ChangeStageContent(EnemyCodeName enemy, int count) {
        this.stageContent.ChangeEnemyForRound(enemy, count);
    }

    public List<SpawnPoint> GetSpawnPoints() => this.spawnPoint;
}
