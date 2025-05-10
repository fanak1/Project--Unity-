using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StageScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> doors;

    [SerializeField] private List<StageScript> scripts;


    [SerializeField] private StageTrigger trigger;

    [SerializeField] private List<SpawnPoint> spawnPoint;

    public ScriptableStage stageContent;

    internal void Start() {
        ResetDoors();
    }

    private void ResetDoors() {
        var door = Utils.FindChildInTransform(this.transform, "Door");

        doors.Clear();
        foreach (Transform d in door)
        {
            doors.Add(d.gameObject);
            d.gameObject.SetActive(true);
        }
    }

    public void FindChildName(string name)
    {

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
        foreach (var d in doors)
        {
            d.gameObject.SetActive(true);
        }
    }

    private void OpenDoor() {
        foreach (var d in doors)
        {
            d.gameObject.SetActive(false);
        }
    }

    public void ChangeStageContent(EnemyCodeName enemy, int count) {
        this.stageContent.ChangeEnemyForRound(enemy, count);
    }

    public List<SpawnPoint> GetSpawnPoints() => this.spawnPoint;
}
