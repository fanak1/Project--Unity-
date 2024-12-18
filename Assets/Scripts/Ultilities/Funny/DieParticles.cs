using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieParticles : MonoBehaviour
{
    private static Queue<DieParticles> objectPooling = new Queue<DieParticles>();

    private static GameObject instance;

    private Animator animator;

    private bool isStart = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Countdown());
        isStart = false;
    }

    private void OnEnable() {
        if (isStart) return;
        animator.enabled = true;
        StartCoroutine(Countdown());
    }

    void PlayAnimation() {
        animator.SetTrigger("Play");

    }

    IEnumerator Countdown() {
        PlayAnimation();
        yield return new WaitForSeconds(1);
        ReturnToPool();
    }

    void ReturnToPool() {
        animator.enabled = false;
        gameObject.SetActive(false);
        objectPooling.Enqueue(this);
    }

    public static DieParticles Create(Vector3 position) {
        DieParticles dieParticles = null;
        if(objectPooling.Count <= 0) {
            if(instance == null) {
                instance = Resources.Load<GameObject>("FunnyPrefabs/DieParticles");
            }
            if(instance != null) {
                var p = Instantiate(instance, position, Quaternion.identity);
                dieParticles = p.GetComponent<DieParticles>();
            }
                    
        } else {
            dieParticles = objectPooling.Dequeue();
            dieParticles.gameObject.SetActive(true);
            dieParticles.gameObject.transform.position = position;
        }
        return dieParticles;
    }
}
