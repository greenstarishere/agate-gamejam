using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timers : MonoBehaviour
{
    public Slider timerSlider;
    private float sliderTimer;
    public bool stopTimer = false;
    public UnityEvent timerOut;
    public float defaultTime;

    // Start is called before the first frame update
    void Start()
    {
        timerSlider.maxValue = defaultTime;
        timerSlider.value = defaultTime;
        sliderTimer = defaultTime;
    }
    
    public void StopTimer()
    {
        stopTimer = true;
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
        stopTimer = false;
    }

    IEnumerator StartTheTimerTicker()
    {
        while (stopTimer == false)
        {
            sliderTimer -= Time.deltaTime / 2;
            yield return new WaitForSeconds(0.001f);

            if(sliderTimer <= 0)
            {
                stopTimer = true;
                timerOut.Invoke();
            }

            if(stopTimer == false)
            {
                timerSlider.value = sliderTimer;
            }
        }
    }

    public void Reset()
    {
        timerSlider.maxValue = defaultTime;
        timerSlider.value = defaultTime;
        sliderTimer = defaultTime;
    }
}
