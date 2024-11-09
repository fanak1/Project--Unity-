using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingCircle : MonoBehaviour
{
    private Animator animator;

    public static Queue<ClosingCircle> closingCirclePool = new Queue<ClosingCircle>();

    public static GameObject prefab;

    private void Start() {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        Invoke("Disable", 1f);
    }

    private void OnEnable() {
        Play();
    }

    private void Play() {
        if(animator != null) animator.SetTrigger("Play");
        Invoke("Disable", 1f);
    }

    private void Disable() {
        transform.gameObject.SetActive(false);
        closingCirclePool.Enqueue(this);
    }

    public static ClosingCircle Create(Vector2 position) {
        ClosingCircle closingCircle;
        if(closingCirclePool.Count <= 0) {
            if (prefab == null) {
                prefab = Resources.Load<GameObject>("FunnyPrefabs/ClosingCircle");
                Debug.Log(prefab);
            }
            var go = Instantiate(prefab, position, Quaternion.identity);
            closingCircle = go.GetComponent<ClosingCircle>();
        } else {
            closingCircle = closingCirclePool.Dequeue();
            closingCircle.gameObject.transform.position = position;

            closingCircle.gameObject.SetActive(true);
        }
        closingCircle.transform.position += new Vector3(0, 0, 5);
        return closingCircle;
    }
}
