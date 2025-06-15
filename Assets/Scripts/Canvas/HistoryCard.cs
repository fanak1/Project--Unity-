using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryCard : MonoBehaviour
{
    public bool expand = false;

    public GameObject expandObj;

    public Image characterIcon;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dateTime;
    public TextMeshProUGUI level;
    public TextMeshProUGUI score;
    public TextMeshProUGUI boss;
    public TextMeshProUGUI enemyKill;
    public TextMeshProUGUI moneyEarn;
    public TextMeshProUGUI timeFinish;

    private void Start()
    {
        expandObj.SetActive(false);
        expand = false;
    }

    public void Init(History history)
    {
        var character = Registry.Character(history.character);
        if(character != null)
            characterIcon.sprite = character.Icon;

        characterName.SetText(history.character.ToString());
        dateTime.SetText(history.currentTime.ToString("dd/MM/yyyy"));
        level.SetText(history.highestLevel.ToString());
        score.SetText(history.stats.score.ToString());

        string bosses = "";
        if (history.bossName.Count > 0)
        {
            for (int i = 0; i < history.bossName.Count - 1; i++)
            {
                bosses += history.bossName[i] + ",";
            }
            bosses += history.bossName[history.bossName.Count - 1];
            boss.SetText(bosses);
        } else
        {
            boss.SetText("None");
        }

        enemyKill.SetText(history.enemyKill.ToString());
        moneyEarn.SetText(history.stats.money.ToString());
        timeFinish.SetText(FormatTime(history.time));
    }
    private string FormatTime(float timeInSeconds)
    {
        int totalSeconds = Mathf.FloorToInt(timeInSeconds);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        if (hours > 0)
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        else
            return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Click()
    {
        if(!expand)
        {
            expandObj.SetActive(true);
            expand = true;
        } else
        {
            expandObj.SetActive(false);
            expand = false;
        }
    }
}
