using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ProjectileBase : MonoBehaviour
{
    public UnitBase source; //Source of projectile

    public Vector3 position; //position to begin firing projectile
    public Vector3 destination; //destination of projectile aim

    public int bulletIndex;

    public ProjectileAttribute _projectileAttribute;

    private bool collided = false; //Check if target Collide with another, if collided, then dont collide with another

    private bool playCollidedAnimation = false;

    public Action<ProjectileBase> OnDisable;

    protected Animator animator;

    internal float spd;
    internal float accel;

    private bool isStart = true;

    public ParticlesType particles;

    public virtual void Start() {
        collided = false;
        animator = GetComponent<Animator>();
        playCollidedAnimation = false;
        animator = GetComponent<Animator>();
        if (animator != null) animator.SetTrigger("Init");
        isStart = false;
    }

    public virtual void OnEnable() {
        if (isStart) return;
        transform.position = position;
        collided = false;
        playCollidedAnimation = false;
        if (animator != null) animator.SetTrigger("Init");
    }

    public virtual void FixedUpdate() {
        Trajectory();
        DestroyOnOverBounds();
    }

    public void SetProjectileAttribute(ProjectileAttribute projectileAttribute) => _projectileAttribute = projectileAttribute; //Set Projectile Attribute when Instantiate
    public abstract void Trajectory(); //Detemine how projectile flying from source to destination and index of bullet

    public abstract float Damage(); //Detemine the scale of damage depend on source

    public void SetSourceAndDestination(UnitBase source, Vector3 position, Vector3 destination) { //Set Source and Destination when Instantiate
        this.source = source; 
        this.position = position;
        this.destination = destination;
    }

    private bool CheckOpponent(GameObject collider) { //Check if we hit opponent instead of wall...
        if(!source.CompareTag(collider.tag)) {
            
            if (!collider.CompareTag("Player") && !collider.CompareTag("Enemy")) return false;
            return true;
        }
        return false;
    }

    public virtual void OnTriggerEnter2D(Collider2D c) {
        
        if (source == null) return;
        if (c.gameObject.CompareTag(source.tag)) return;
        if(CheckOpponent(c.gameObject) && !collided) {
            if (IfCollide(c.gameObject)) return;
            Hit();
            UnitBase cUnit = c.gameObject.GetComponent<UnitBase>();
            if(cUnit != null) {
                cUnit.damagePosition = transform.position;

                source.Hitting(cUnit, Damage());
                
            }
        } else {

        }
    }

    public virtual void DestroyOnOverBounds() {
        if (Vector2.Distance(this.position, this.transform.position) > 200f) Collided();
    }

    private void OnBecameInvisible() {
        Collided();
    }

    protected void Disable() {
        OnDisable?.Invoke(this);
        gameObject.SetActive(false);
    }

    virtual protected void Hit() {
        if ( particles != ParticlesType.None)
        {
            Registry.CreateParticle(particles, transform.position);
        }
        Collided();
    }

    virtual protected bool IfCollide(GameObject a) {
        if (!collided) {
            collided = true;
            return false;
        }
        return true;
    }

    private void Collided() {
        if(!gameObject.activeInHierarchy) return;
        accel = 0;
        spd = 0;
        if (animator != null) {
            if(!playCollidedAnimation) {
                playCollidedAnimation = true;
                CoroutineManager.Instance.StartNewCoroutine(PlayCollidedAnimation());
            }
            
        } else Disable();
    }


    private IEnumerator PlayCollidedAnimation() {
        animator.SetTrigger("Collide");

        AnimatorClipInfo[] clipInfo;
        while (true) {
            clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length <= 0) {
                Debug.LogWarning("No animation clip is currently playing.");
                yield return null;
            } else {
                break;
            }
        }
        float duration = clipInfo[0].clip.length;

        yield return CollidedAnimation(duration);
    }

    private IEnumerator CollidedAnimation(float duration) {
        yield return new WaitForSeconds(duration);

        Disable();
    }

}
