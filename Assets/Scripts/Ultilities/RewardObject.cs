using UnityEngine;

public class RewardObject : MonoBehaviour
{
    bool isInit = false;

    public Animator animator;

    private void Start()
    {
        animator.CrossFade("ChestAnimation", 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isInit)
        {
            int ogSpd = PlayerUnit.instance.stats.spd;
            PlayerUnit.instance.stats.spd = 0;
            RewardManager.Instance.InitReward(() => { PlayerUnit.instance.stats.spd = ogSpd; });
            isInit = true;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.rewardBegin);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
            Destroy(gameObject);
    }
}
