using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobScript : MonoBehaviour
{
    public GameObject player;

    private UnitBase unitBase;

    private ProjectileHolder projectileHolder;

    private AlbilitiesHolder albilitiesHolder;

    private Rigidbody2D rb;


    private float time = 0f;

    

    [SerializeField] protected float moveInterval = 2f;

    [SerializeField] private float shootPercentage = 0.5f;

    [SerializeField] protected float moveDuration = 2f;

    private bool pause = false;

    internal int state = 0; //0 - stand still; 1 - moving

    internal float spd;

    internal bool tooFar = false;


    private void Awake() {
        unitBase = GetComponent<UnitBase>();

        projectileHolder = GetComponent<ProjectileHolder>();

        albilitiesHolder = GetComponent<AlbilitiesHolder>();

    }

    protected virtual void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        time = Random.Range(0f, moveInterval/2f);

        spd = unitBase.stats.spd;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (GameManager.Instance.pause)
            return;

        UpdateFunction();
    }

    protected virtual void UpdateFunction()
    {
        if (time < 50f) time += Time.deltaTime;

        if (time > moveInterval)
        {
            if (PlayerUnit.instance != null)
                if (Vector3.Distance(PlayerUnit.instance.GetPosition(), transform.position) < 100f)
                    RandomMove();
        }
        if (time > moveInterval + moveDuration)
        {
            float shoot = Random.Range(0, shootPercentage);
            if (shoot < shootPercentage) RandomShoot();
            state = 0;
            time = 0f;
            if (PlayerUnit.instance != null)
            {
                if (Vector3.Distance(PlayerUnit.instance.GetPosition(), transform.position) > 50f)
                {
                    tooFar = true;
                }
                else
                {
                    tooFar = false;
                }
            }
        }
    }


    internal abstract void RandomMove();

    internal void TurnFace()
    {
        if (PlayerUnit.instance == null) return;

        if (PlayerUnit.instance.GetPosition().x - this.transform.position.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    internal abstract void Move(int index);

    internal void Shoot(int index) {
        if(projectileHolder != null) {
            if (index >= 0) projectileHolder.Shoot(index - 1, transform.position, PlayerUnit.instance.GetPosition());
        }
    }

    protected virtual void RandomShoot() {
        if(projectileHolder != null) projectileHolder.RandomShoot(transform.position, PlayerUnit.instance.GetPosition());
    }

    protected virtual void MoveToward() {
        transform.position = Vector3.MoveTowards(transform.position, PlayerUnit.instance.GetPosition(), spd * Time.deltaTime);
    }
}
