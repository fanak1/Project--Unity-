using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class RenderSpriteBetweenTwoPoint : MonoBehaviour {
    public GameObject start;
    public GameObject end;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    public float PIXEL_X = 16;

    private float scale;

    public Action<UnitBase> OnEffectHit;

    private Vector2 startPos;

    private Vector2 endPos;


    protected virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scale = 16/PIXEL_X;
    }

    protected virtual void Start() {

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (start != null) startPos = start.transform.position;
        if(end != null) endPos = end.transform.position;
        Draw(startPos, endPos);
    }

    public virtual void StartRender(UnitBase origin, UnitBase target) {
        Debug.Log(" " + origin + " " + target);
        startPos = origin.transform.position;
        endPos = target.transform.position;
        start = origin.gameObject; 
        end = target.gameObject;
        
        animator.SetTrigger("Play");
        StartCoroutine(PlayAnimationAfterStart(target));

    }

    protected virtual IEnumerator PlayAnimationAfterStart(UnitBase target) {

        // Retrieve the animation duration
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
        // Wait for the animator to start playing
    
        
         float duration = clipInfo[0].clip.length;
         Debug.Log($"Animation duration: {duration} seconds");

        // Continue with your logic
        if (target != null) {

            yield return PlayAnimation(duration, target);
        } else {
            animator.SetTrigger("End");
            OnEffectHit?.Invoke(target);
        }
    }

    protected virtual void Draw(Vector2 start, Vector2 end) {
        Vector2 direction = (end - start);

        float angle = Mathf.Atan2(direction.y,  direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = (end + start) / 2;


        transform.localScale = new Vector3(scale * direction.magnitude, transform.localScale.y, transform.localScale.z);

    }

    protected virtual IEnumerator PlayAnimation(float duration, UnitBase target) {
        yield return new WaitForSeconds(duration);
        Debug.Log("End");

        animator.SetTrigger("End");
        OnEffectHit?.Invoke(target);
        
    }

}
