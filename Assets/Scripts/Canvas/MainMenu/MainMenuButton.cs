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

    public void QuitGameButton()
    {
        GameManager.Instance.QuitGame();
    }

    public void HistoryButton()
    {
        HistoryUI.Instance.TurnOn();
    }
}
