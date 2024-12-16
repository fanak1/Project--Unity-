using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerUI : Singleton<ComputerUI>
{
    // Start is called before the first frame update

    [SerializeField] private GameObject playerPanel;

    [SerializeField] private GameObject trainingRoomPanel;

    private bool panel = true; // 0 - trainingRoomPanel, 1 - playerPanel
    void Start()
    {
        this.gameObject.SetActive(false);
        SwitchPanel();
    }

    public void TurnOn() {
        this.gameObject.SetActive(true);
    }

    public void TurnOff() {
        TrainingRoomManager.Instance.Exit();
        this.gameObject.SetActive(false);
    }

    public void ConfirmButton() {
        TurnOff();
        TrainingRoomManager.Instance.InitializeNewRound();
    }

    public void SwitchPanel() {
        panel = !panel;
        playerPanel.SetActive(panel);
        trainingRoomPanel.SetActive(!panel);
    }

    public void SwitchPanel(bool panel) {
        this.panel = panel;
        playerPanel.SetActive(panel);
        trainingRoomPanel.SetActive(!panel);
    }

}
