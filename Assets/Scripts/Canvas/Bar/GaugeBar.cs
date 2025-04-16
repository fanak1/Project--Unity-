using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] internal Slider increaseDelaySlider;
    [SerializeField] internal Slider decreaseDelaySlider;
    [SerializeField] internal Slider valueSlider;


    /* when the valueBar decrease, decreaseBar will delay a short time b4 change its bar to valueBar
     * when the valueBar have to increase in interval of time, increaseBar will increase to target value instantly.
     */

    private float decreaseSpeed = 200f;
    private float increaseSpeed = 20f;
    private float delay = 1f;
    private float scale = 1f;

    private Coroutine decreaseDelayCoroutine;
    private bool delayCoroutineEnd;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        SetMaxValue(300);
    }

    // Update is called once per frame
    protected virtual void Update()
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
        valueSlider.value -= value * scale;
        increaseDelaySlider.value -= value * scale;
    }

    public void IncreaseWithInterval(float value, float speed = 20f) {
        increaseDelaySlider.value += value * scale;
        increaseSpeed = speed * scale;
    }

    public void Increase(float value) {
        valueSlider.value += value * scale;
    }

    public void SetValue(float value, bool resetBar = false) {
        if(resetBar) {
            increaseDelaySlider.value = value * scale;
            decreaseDelaySlider.value = value * scale;
        }
        valueSlider.value = value * scale;
    }

    public void SetMaxValue(float value, bool resetBar = false) {
        rectTransform.sizeDelta = new Vector2(value * scale, 30);
        increaseDelaySlider.maxValue = value * scale;
        decreaseDelaySlider.maxValue = value * scale;
        valueSlider.maxValue = value * scale;

        if(resetBar) SetValue(value); 
    }

    public void Init(float maxValue, float scale=1f) {
        this.scale = scale;
        SetMaxValue(maxValue, true);
    }

    public bool DelayEnd() => this.delayCoroutineEnd;


}
