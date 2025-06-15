
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private HealthBar healthBar;
    private ManaBar manaBar;
    public bool mouseBlock = false;

    public static PlayerUnit instance;

    public GameObject pivot;

    protected override void Start() {
        if (instance == null) instance = this;
        
        healthBar = GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<HealthBar>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBarUI").GetComponent<ManaBar>();
        base.Start();
        transform.position -= new Vector3(0, 0, transform.position.z);
    }

    public Vector3 GetPosition() => pivot.transform.position;

    internal override void Destroy() {
        Statistic.Instance.Open(false);
        instance = null;
        base.Destroy();
    }

    internal override void ReduceHealth(float dmgTaken) {
        base.ReduceHealth(dmgTaken);
        healthBar.Decrease(dmgTaken);
    }

    internal override void ReduceMP(float mpTaken) {
        base.ReduceMP(mpTaken);
        manaBar.Decrease(mpTaken);
        manaBar.RegenCooldown();
    }



    internal override void IncreaseMaxHP(int hp) {
        base.IncreaseMaxHP(hp);
        healthBar.SetMaxValue(maxHP);
    }

    internal override void IncreaseMaxMP(int mp) {
        base.IncreaseMaxMP(mp);
        manaBar.SetMaxValue(maxMP);
    }

    internal override void InitializeHP() {
        base.InitializeHP();
        healthBar.Init(maxHP, 1.5f);
    }

    internal override void InitializeMP() {
        base.InitializeMP();
        manaBar.Init(maxMP, 1.5f);
    }

    internal override void RegenMP(float regenSpeed) {
        if (manaBar.canRegen) {
            base.RegenMP(regenSpeed);
            manaBar.SetValue(nowMP);
        }
            
    }

    public PlayerData Save()
    {
        PlayerData p = new();
        var listA = ShowAbilities();
        List<string> listS = new();
        listA.ForEach(a => listS.Add(a.name));
        p.allAlbilities = listS;

        p.code = characterCode;

        p.stats = stats;

        return p;
    }

}

public struct PlayerData
{
    public List<string> allAlbilities;
    public Stats stats;
    public CharacterCode code;
}
