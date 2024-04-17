using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUps : MonoBehaviour
{
    private TextMeshPro tmp;

    public int dmg;

    private float time;

    [SerializeField] private float gravity;

    [SerializeField] private float preVelocity;

    [SerializeField] private float direct;

    private void Awake() {
        tmp = gameObject.GetComponent<TextMeshPro>();
        SetUp(dmg);
        Destroy(gameObject, 1f);
    }

    private void Update() {
        time += Time.deltaTime;
        FallSimulated();
    }

    private void FallSimulated() {
        float accel = gravity * time;
        float velocity = preVelocity + accel;
        transform.Translate((Vector3.right * (1f/2 - direct) + Vector3.up * velocity) * Time.deltaTime);
    }

    public void SetUp(int damage) {
        tmp.SetText(damage.ToString());
        direct = Random.Range(0, 2);
    }
}
