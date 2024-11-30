using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerUI : Singleton<ComputerUI>
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOn() {
        this.gameObject.SetActive(true);
    }

    public void TurnOff() {
        TrainingRoomManager.Instance.Exit();
        this.gameObject.SetActive(false);
    }

}
