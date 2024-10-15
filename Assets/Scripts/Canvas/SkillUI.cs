using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI buttonText;
    [SerializeField] private Slider cooldownSlider;
    private float cooldown = 0f;
    public bool usable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float cooldown, string text = "") {
        buttonText.SetText(text);
        this.cooldown = cooldown;
        cooldownSlider.maxValue = cooldown;
        cooldownSlider.minValue = 0f;
        cooldownSlider.value = cooldownSlider.minValue;
    }

    private IEnumerator StartCooldown() {
        usable = false;
        float time = 0f;
        cooldownSlider.value = cooldownSlider.maxValue;
        while(time < cooldown) {
            time += Time.deltaTime;
            cooldownSlider.value -= Time.deltaTime;
            yield return null;
        }
        cooldownSlider.value = 0;
        usable = true;
    }

    public void UseSkill() {
        StartCoroutine("StartCooldown");
    }
}
