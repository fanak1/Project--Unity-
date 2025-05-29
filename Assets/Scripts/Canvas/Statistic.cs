using TMPro;
using UnityEngine;

public class Statistic : StaticInstance<Statistic>
{
    [SerializeField] TextMeshProUGUI score;
    

    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void Open()
    {
        GameManager.Instance.TryToPause();
        this.gameObject.SetActive(true);
        score.SetText(GameManager.Instance.currentStatistics.score.ToString());
    }

    public void ExitToMainMenu()
    {
        GameManager.Instance.TryToResume();
        GameManager.Instance.Menu();
    }
}
