using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void StartButton() {
        PlayerSelection.Instance.TurnOn();
    }

    public void TrainingButton()
    {
        GameManager.Instance.StartTrainingLevel();
    }
}
