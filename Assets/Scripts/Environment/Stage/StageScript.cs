using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageScript : MonoBehaviour
{

    [SerializeField] private List<StageDoor> doors;


    [SerializeField] private StageTrigger trigger;

    [SerializeField] private List<SpawnPoint> spawnPoint;

    public ScriptableStage stageContent;

    [SerializeField]
    public StageType[] stageTypes;

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

    public virtual void StageStart() {
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
        if (stageContent.stageType == StageType.Boss)
        {
            ExitDoor exit = ResourceSystem.Instance.GetComponent<ExitDoor>("ExitDoor");
            if(exit != null)
            {
                Instantiate(exit, gameObject.transform.position, Quaternion.identity);
            }
        }
        trigger.Clear();
        //trigger.gameObject.SetActive(true);
    }

    protected void CloseDoor(Action o) {
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
