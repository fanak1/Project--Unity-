using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Slider increaseDelaySlider;
    [SerializeField] private Slider decreaseDelaySlider;
    [SerializeField] private Slider valueSlider;


    /* when the valueBar decrease, decreaseBar will delay a short time b4 change its bar to valueBar
     * when the valueBar have to increase in interval of time, increaseBar will increase to target value instantly.
     */

    private float decreaseSpeed = 50f;
    private float increaseSpeed = 20f;
    private float delay = 1f;

    private Coroutine decreaseDelayCoroutine;
    private bool delayCoroutineEnd;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        SetMaxValue(300);
    }

    // Update is called once per frame
    void Update()
    {
        if(valueSlider.value < increaseDelaySlider.value) {
            valueSlider.value += Time.deltaTime * increaseSpeed;
        } else if(valueSlider.value > increaseDelaySlider.value){
            increaseDelaySlider.value = valueSlider.value;
        }
        if(decreaseDelaySlider.value > valueSlider.value) {
            if(delayCoroutineEnd) decreaseDelaySlider.value -= Time.deltaTime * decreaseSpeed;
        } else if(decreaseDelaySlider.value < valueSlider.value)
        {
            decreaseDelaySlider.value = valueSlider.value;
        }

    }

    IEnumerator Delay(float delay) {
        delayCoroutineEnd = false;
        yield return new WaitForSeconds(delay);
        delayCoroutineEnd = true;
    }

    internal Coroutine ResetCoroutine(Coroutine coroutine) {
        if(coroutine != null) StopCoroutine(coroutine);

        return StartCoroutine(Delay(delay));
    }

    public void Decrease(float value) {
        decreaseDelayCoroutine = ResetCoroutine(decreaseDelayCoroutine);
        valueSlider.value -= value;
        increaseDelaySlider.value -= value;
    }

    public void IncreaseWithInterval(float value, float speed = 20f) {
        increaseDelaySlider.value += value;
        increaseSpeed = speed;
    }

    public void Increase(float value) {
        valueSlider.value += value;
    }

    public void SetValue(float value) {
        increaseDelaySlider.value = value;
        decreaseDelaySlider.value = value;
        valueSlider.value = value;
    }

    public void SetMaxValue(float value, bool resetBar = true) {
        rectTransform.sizeDelta = new Vector2(value, 30);
        increaseDelaySlider.maxValue = value;
        decreaseDelaySlider.maxValue = value;
        valueSlider.maxValue = value;

        if(resetBar) SetValue(value); 
    }

    
}
