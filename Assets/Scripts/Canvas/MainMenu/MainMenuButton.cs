using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void StartButton() {
        GameManager.Instance.StartGame();
    }
}
