using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBoss : BossScript
{
    public float timer = 0f;

    public float moveTime = 2f;

    public ProjectileHitBox skill2;

    public ProjectileHitBox skill1;

    public ScriptableProjectiles normalAttack;

    public Projectiles normalAttackProjectile;

    public Vector3 playerPosition;

    public Transform skill2position;

    public Transform normalAttackPosition;

    public UnitBase source;

    private bool turnRed = false;

    private Dictionary<string, int> moves = new Dictionary<string, int>
    {
        { "Normal_Attack", 60 },
        { "Skill1", 25 },
        { "Skill2", 15 }
    };

    protected override void Start()
    {
        base.Start();

        source = GetComponent<UnitBase>();
        normalAttackProjectile = normalAttack.Create();
        
    }

    private Dictionary<string, int> GetBaseMoves()
    {
        if (turnRed)
        {
            moves = new Dictionary<string, int>
            {
                {  "Normal_Attack", 30 },
                { "Skill1", 50 },
                { "Skill2", 20 }
            };
        } else
        {
            moves = new Dictionary<string, int>
            {
                { "Normal_Attack", 60 },
                { "Skill1", 25 },
                { "Skill2", 15 }
            };
        }
        return moves;
    }

    public override void RandomMove()
    {
        if(source.nowHP <= source.stats.hp / 2 && !turnRed) {
            turnRed = true;
            moveTime = 1.5f;

        }
        if (PlayerUnit.instance != null) {
            playerPosition = PlayerUnit.instance.GetPosition();
        }
        timer += Time.deltaTime;
        if(timer > moveTime)
        {
            timer = 0f;

            int totalWeight = 0;
            foreach (var move in moves)
                totalWeight += move.Value;

            int randomValue = Random.Range(0, totalWeight);
            int cumulative = 0;
            foreach (var move in moves)
            {
                cumulative += move.Value;
                if (randomValue < cumulative)
                {
                    
                    StartAnimation(move.Key);
                    if(move.Key == "Normal_Attack")
                    {
                        moves["Normal_Attack"] = Mathf.Max(move.Value - 10, 0);
                    } 
                    else if(move.Key == "Skill1")
                    {
                        moves["Skill1"] = Mathf.Max(move.Value - 10, 0);
                    }
                    else
                    {
                        moves = GetBaseMoves();
                    }
                    return;
                }
            }
        }
    }

    public void Anim_Skill1()
    {
        CoroutineManager.Instance.StartNewCoroutine(CreateChaseGround());
    }

    public void Anim_Skill2()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.stoneBossLaser);
        Vector3 direction = playerPosition - skill2position.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);

        skill2position.rotation = rot;

        var obj = Instantiate(skill2, skill2position.position, Quaternion.identity);
        obj.Init(source);
        obj.transform.SetParent(skill2position);
    }

    public void Anim_NormalAttack()
    {
        TurnFace();
        if (PlayerUnit.instance != null) playerPosition = PlayerUnit.instance.GetPosition();
        normalAttackProjectile.Shoot(this.source, normalAttackPosition.position, playerPosition);
    }

    private IEnumerator CreateChaseGround()
    {
        bool hit = false;
        float count = 0;
        while(!hit && count < 20 && source != null)
        {
            if(PlayerUnit.instance != null)
            {
                Vector3 playerPos = PlayerUnit.instance.GetPosition();
                var sk1 = Instantiate(skill1, playerPos, Quaternion.identity);
                sk1.Init(source);
                sk1.OnHit = () => hit = true;
                yield return new WaitForSeconds(0.2f); // Adjust the interval as needed
            }
            count++;
        }
    }
}
