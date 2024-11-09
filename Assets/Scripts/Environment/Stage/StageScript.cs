using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageScript : MonoBehaviour
{

    [SerializeField] private GameObject[] doors;

    [SerializeField] private StageTrigger trigger;

    [SerializeField] private List<SpawnPoint> spawnPoint;

    public ScriptableStage stageContent;

    internal void Start() {
        for(int i=0; i<doors.Length; i++) {
            doors[i].SetActive(false);
        }
    }

    public void StageStart() {
        CloseDoor();
        
        //trigger.gameObject.SetActive(false);
        StageManager.Instance.StartStage(this);
    }
    
    public void StageBegin() {
        //Initiate animation of stage
        StageManager.Instance.Ready();
    }

    public void StageFinish() {
        OpenDoor();
        trigger.Clear();
        //trigger.gameObject.SetActive(true);
    }

    private void CloseDoor() {
        for (int i = 0; i < doors.Length; i++) {
            doors[i].SetActive(true);
        }
    }

    private void OpenDoor() {
        for (int i = 0; i < doors.Length; i++) {
            doors[i].SetActive(false);
        }
    }

    public List<SpawnPoint> GetSpawnPoints() => this.spawnPoint;
}
