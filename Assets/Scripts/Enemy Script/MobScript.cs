using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobScript : MonoBehaviour
{
    public GameObject player;

    private UnitBase unitBase;

    private ProjectileHolder projectileHolder;

    private AlbilitiesHolder albilitiesHolder;


    private float time = 0f;

    

    [SerializeField] private float moveInterval = 2f;

    [SerializeField] private float shootPercentage = 0.5f;

    [SerializeField] private float moveDuration = 2f;

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
            if (player != null)
                if (Vector3.Distance(player.transform.position, transform.position) < 100f)
                    RandomMove();
        }
        if (time > moveInterval + moveDuration)
        {
            float shoot = Random.Range(0, shootPercentage);
            if (shoot < shootPercentage) RandomShoot();
            state = 0;
            time = 0f;
            if (player != null)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 50f)
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

    internal abstract void Move(int index);

    internal void Shoot(int index) {
        if(projectileHolder != null) {
            if (index >= 0) projectileHolder.Shoot(index - 1, transform.position, player.transform.position);
        }
    }

    protected virtual void RandomShoot() {
        if(projectileHolder != null) projectileHolder.RandomShoot(transform.position, player.transform.position);
    }

    protected virtual void MoveToward() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);
    }
}
