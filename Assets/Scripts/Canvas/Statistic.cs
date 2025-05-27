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
        this.gameObject.SetActive(true);
        score.SetText(GameManager.Instance.score.ToString());
    }

    public void ExitToMainMenu()
    {
        GameManager.Instance.Menu();
    }
}
