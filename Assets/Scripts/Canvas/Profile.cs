using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Profile : Singleton<Profile>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Image characterIcon;

    public TextMeshProUGUI money;
    void Start()
    {
        StartCoroutine(TryToGetPlayerIcon());

        money.SetText(GameManager.Instance.money.ToString());
        GameManager.Instance.MoneyChangeObserver += () => { money.SetText(GameManager.Instance.money.ToString()); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TryToGetPlayerIcon()
    {
        float time = 0f;
        while (PlayerUnit.instance == null && time < 4f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (PlayerUnit.instance != null) {
            characterIcon.sprite = PlayerUnit.instance.icon;
        }
    }
}
