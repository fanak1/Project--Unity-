using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class HistoryUI : StaticInstance<HistoryUI>
{
    public Transform container;

    public HistoryCard prefabs;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        this.gameObject.SetActive(false); 
    }

    public void Init(List<History> list)
    {
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        if(list.Count > 0)
        {
            List<History> sortedHistory = list
            .OrderBy(h => Math.Abs((DateTime.Now - DateTime.Parse(h.currentTime)).TotalSeconds))
            .ToList();

            foreach (var h in sortedHistory)
            {
                var history = Instantiate(prefabs);
                history.Init(h);
                history.gameObject.transform.SetParent(container);
            }
        } else
        {

        }

       
    }

    public void OnEnable()
    {
        if(animator != null)
            animator.CrossFade("HistoryInit", 0f);
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
        Init(GameManager.Instance.LoadHistoryJson());
    }

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void StartTurnOff()
    {
        animator.CrossFade("HistoryClose", 0f);
    }
}
