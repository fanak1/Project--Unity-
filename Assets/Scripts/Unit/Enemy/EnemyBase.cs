
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{

    private Animator animator;

    public EnemyType enemyType;

    public bool finishAnimationDie = false;
    
    protected override void Start() {
        faction = Faction.Enemy;
        //mmHealthBar = GetComponent<MMHealthBar>();
        base.Start();
        animator = GetComponent<Animator>();
    }

    internal override void TakeDamage(UnitBase source, float dmg, bool crit) {
        animator.SetTrigger("Hit");
        base.TakeDamage(source, dmg, crit);
    }

    internal override void Destroy() {
        if (enemyType == EnemyType.Boss)
        {
            CoroutineManager.Instance.StartNewCoroutine(BossDeadAnimation());
        } else
        {
            DecreaseEnemyCountOnDead();
            DieParticles.Create(this.transform.position);
            base.Destroy();
        }
            
    }

    void DecreaseEnemyCountOnDead()
    {
        StageManager.Instance.numberEnemyLeft--;
        StageManager.Instance.lastEnemyDiePos = this.transform.position;
    }

    IEnumerator BossDeadAnimation()
    {
        DecreaseEnemyCountOnDead();

        animator.CrossFade("Die", 0f);

        while(!finishAnimationDie)
        {
            yield return null;  
        }

        DieParticles.Create(this.transform.position);
        base.Destroy();
    }

    public void FinishAnimationDie()
    {
        finishAnimationDie = true;
    }

    internal override void ReduceHealth(float dmgTaken) {
        base.ReduceHealth(dmgTaken);
        
        if (enemyType == EnemyType.Boss)
        {
            HealthBarBoss.Instance.Decrease(dmgTaken);
        }
        //mmHealthBar.UpdateBar(nowHP, 0, maxHP, true);
    }

    internal override void IncreaseMaxHP(int hp) {
        base.IncreaseMaxHP(hp);
        //mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        if (enemyType == EnemyType.Boss)
        {
            HealthBarBoss.Instance.Init(stats.hp, gameObject.name);
        }
        //mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

}
