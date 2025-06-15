using UnityEngine;

public class Transition : PersistentSingleton<Transition>
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void StartLoading()
    {
        animator.SetTrigger("Start");
        SoundManager.Instance.PlayBackground(SoundManager.Instance.loading);
    }

    public void EndLoading()
    {
        animator.SetTrigger("End");
    }
}
