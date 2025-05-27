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
    }

    public void EndLoading()
    {
        animator.SetTrigger("End");
    }
}
