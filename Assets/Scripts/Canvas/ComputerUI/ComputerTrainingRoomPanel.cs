using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComputerTrainingRoomPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monsterCodeName;

    [SerializeField] private TextMeshProUGUI quantity;

    private void Start() {
        
    }

    private void Update() {
        monsterCodeName.SetText(TrainingRoomManager.Instance.EnemyCodeName.ToString());
        quantity.SetText(TrainingRoomManager.Instance.NumberOfEnemy.ToString());
    }

    public void IncreaseQuanity() {
        TrainingRoomManager.Instance.ChangeQuantityOfMonster(1);
    }

    public void DecreaseQuanity() {
        TrainingRoomManager.Instance.ChangeQuantityOfMonster(-1);
    }

    public void ChangeToRightMonster() {
        TrainingRoomManager.Instance.ChangeEnemyAsCyclic(1);
    }

    public void ChangeToLeftMonster() {
        TrainingRoomManager.Instance.ChangeEnemyAsCyclic(-1);
    }
}
