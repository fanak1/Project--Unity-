using TMPro;
using UnityEngine;

public class Statistic : StaticInstance<Statistic>
{
    [SerializeField] TextMeshProUGUI score;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }
    public void Open(bool win)
    {
        this.gameObject.SetActive(true);
        if (win)
        {
            animator.CrossFade("StatisticWin", 0f);
        }
        else
        {
            animator.CrossFade("StatisticLose", 0f);
        }
        
        score.SetText(GameManager.Instance.currentStatistics.score.ToString());
        PlayerUnit.instance.mouseBlock = true;
    }

    public void ExitToMainMenu()
    {
        GameManager.Instance.TryToResume();
        GameManager.Instance.Menu();
    }
}
