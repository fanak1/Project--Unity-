using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUps : MonoBehaviour
{
    private TextMeshPro tmp;

    public int dmg;

    private void Awake() {
        tmp = gameObject.GetComponent<TextMeshPro>();
        SetUp(dmg);
        Destroy(gameObject, 1f);
    }

    public void SetUp(int damage) {
        tmp.SetText(damage.ToString());
    }
}
