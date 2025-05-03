using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Particles<T> : MonoBehaviour where T : MonoBehaviour
{
    private Animator animator;

    public static Queue<T> objectPooling = new Queue<T>();

    public static T instance;

    private bool isStart = true;
    void Start() {
        animator = GetComponent<Animator>();
        CoroutineManager.Instance.StartNewCoroutine(Countdown());
        isStart = false;
    }

    private void OnEnable() {
        if (isStart) return;
        animator.enabled = true;
        CoroutineManager.Instance.StartNewCoroutine(Countdown());
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
        objectPooling.Enqueue(this as T);
    }


    public static T Create(Vector3 position) {
        T particles = null;
        if (objectPooling.Count <= 0) {
            if (instance == null) {
                T[] temp = Resources.LoadAll<T>("FunnyPrefabs");
                instance = temp[0];
                Debug.Log(instance);
            }
            if (instance != null) {
                var p = Instantiate(instance, position, Quaternion.identity);
                particles = p;
            }

        } else {
            particles = objectPooling.Dequeue();
            particles.gameObject.SetActive(true);
            particles.gameObject.transform.position = position;
        }
        return particles;
    }
}
