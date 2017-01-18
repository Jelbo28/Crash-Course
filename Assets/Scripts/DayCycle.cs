using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField]
    Color darkColor;
    [SerializeField]
    Color twilightColor;
    [SerializeField]
    Color lightColor;
    [SerializeField]
    float transitionLength;
    [SerializeField]
    float dayLength;
    //[SerializeField]
    //int sunPower = 2;

    [SerializeField]
    float dayTimeRatio = 1/2;
    [SerializeField]
    float eveningTimeRatio = 1/4;
    [SerializeField]
    float nightTimeRatio = 1/4;

    private Light sun;
    private float dayTimer;
    private float eveningTimer;
    private float nightTimer;
    private bool toggle = true;
    private float origDayLength;


    void Start()
    {
        //Configures the ammount of time (in seconds) each part of the day gets.
        dayTimer = dayLength * dayTimeRatio;
        eveningTimer = dayLength * eveningTimeRatio;
        nightTimer = dayLength * nightTimeRatio;
        origDayLength = dayLength;
        //Configures light component for later use.
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        dayLength -= Time.deltaTime;

        //dayTimer -= Time.deltaTime;
        if (toggle)
        {
            StartCoroutine(ColorChange(lightColor, transitionLength));
            toggle = !toggle;
        }
        if (dayLength <= eveningTimer + nightTimer)
        {
            //eveningTimer -= Time.deltaTime;
            if (!toggle)
            {
                StartCoroutine(ColorChange(twilightColor, transitionLength));
                toggle = !toggle;
            }
            if (dayLength <= nightTimer)
            {
                //nightTimer -= Time.deltaTime;
                if (toggle)
                {
                    StartCoroutine(ColorChange(darkColor, transitionLength));
                    toggle = !toggle;
                }
                if (dayLength <= 3)
                {
                    if (!toggle)
                    {
                        StartCoroutine(ColorChange(lightColor, transitionLength));
                        toggle = !toggle;
                    }
                    if (dayLength <= 0)
                    {
                        dayLength = origDayLength;

                    }
                    //sun.color = sunColor;
                    //dayTimer = dayLength * dayTimeRatio;
                    //eveningTimer = dayLength * eveningTimeRatio;
                    //nightTimer = dayLength * nightTimeRatio;
                }
            }
        }
        //sun.intensity = Mathf.Pow(((2 * dayTimer / dayLength) - 1), sunPower) * -1 + .8f;
    }

    IEnumerator ColorChange(Color color, float time)
    {
        float step = time * 1 / 60f;
        while (time >= 0) //  Make this not instantanious.
        {
            time -= step;
            //yield return new WaitForSeconds(1 / 60f);
            sun.color = Color.Lerp(sun.color, color, step);
        }
        yield return null;
    }
}
