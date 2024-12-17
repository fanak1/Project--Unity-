using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieParticles : MonoBehaviour
{
    private static Queue<DieParticles> objectPooling = new();

    private static DieParticles instance;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Countdown());
    }

    private void OnEnable() {
        StartCoroutine(Countdown());
    }

    void PlayAnimation() {
        animator.Play("New Animation");
    }

    IEnumerator Countdown() {
        PlayAnimation();
        yield return new WaitForSeconds(1);
        ReturnToPool();
    }

    void ReturnToPool() {
        gameObject.SetActive(false);
        objectPooling.Enqueue(this);
    }

    public static DieParticles Create(Vector3 position) {
        DieParticles dieParticles;
        if(objectPooling.Count <= 0) {
            if(instance == null) {
                instance = Resources.Load<GameObject>("FunnyPrefabs/DieParticles").GetComponent<DieParticles>();
            }
            dieParticles = Instantiate(instance, position, Quaternion.identity);
        } else {
            dieParticles = objectPooling.Dequeue();
            dieParticles.gameObject.SetActive(true);
            dieParticles.gameObject.transform.position = position;
        }
        return dieParticles;
    }
}
