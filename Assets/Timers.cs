using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timers : MonoBehaviour
{
    public Slider timerSlider;
    public float sliderTimer;
    public bool stopTimer = false;
    public UnityEvent timerOut;

    // Start is called before the first frame update
    void Start()
    {
        timerSlider.maxValue = sliderTimer;
        timerSlider.value = sliderTimer;
    }
    
    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker()
    {
        while (stopTimer == false)
        {
            sliderTimer -= Time.deltaTime;
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
}
