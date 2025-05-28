using UnityEngine;

public class PlayerSelection : Singleton<PlayerSelection>
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(animator != null) animator.CrossFade("PlayerSelectionAnim", 0f);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        animator.CrossFade("PlayerSelectionAnimDown", 0f);
    }

}
