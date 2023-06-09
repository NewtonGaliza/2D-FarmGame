using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimerController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    [SerializeField] private Color dayLightColor = Color.white;
    [SerializeField] private Color nightLightColor;
    [SerializeField] private AnimationCurve nightTimeCurve;
    private float time;
    [SerializeField] private Text text;
    [SerializeField] private float timeScale = 60f;
    [SerializeField] Light2D globalLight;
    private int days;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }


    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = Hours.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if(time > secondsInDay)
        {
            NextDay();
        }
    }


    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
