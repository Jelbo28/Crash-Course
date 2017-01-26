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
    [SerializeField]
    bool on = false;

    private Light sun;
    private float dayTimer;
    private float eveningTimer;
    private float nightTimer;
    //private bool toggle = true;
    private int dayPeriod = 0;
    private float origDayLength;
    private bool colorChange = false;
    private Color newColor;
    private Color currColor;
    private float t = 0f;
    public MonsterWave spawner;

    void Start()
    {
        dayPeriod = 0;
        newColor = lightColor;
        //Configures the ammount of time (in seconds) each part of the day gets.
        dayTimer = dayLength * dayTimeRatio;
        eveningTimer = dayLength * eveningTimeRatio;
        nightTimer = dayLength * nightTimeRatio;
        origDayLength = dayLength;
        //Configures light component for later use.
        sun = GetComponent<Light>();
        spawner.GetComponent<MonsterWave>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dayPeriod);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            newColor = twilightColor;
            currColor = sun.color;
            colorChange = true;
        }
        if (on)
        {
            Day();
        }
        if (colorChange == true)
        {
            ColorChange();
        }
    }

    void Day()
    {
        dayLength -= Time.deltaTime;
        if (dayPeriod == 0)
        {
            
            newColor= lightColor;
            currColor = sun.color;
            colorChange = true;
            dayPeriod++;
        }
        if (dayLength <= eveningTimer + nightTimer)
        {
            if (dayPeriod == 1)
            {
                
                Debug.Log("bob");
                newColor = twilightColor;
                currColor = sun.color;
                colorChange = true;
                dayPeriod++;
            }
            if (dayLength <= nightTimer)
            {
                
                spawner.night = true;
                Debug.Log(spawner.night);
                if (dayPeriod == 2)
                {
                    spawner.stop = false;
                    newColor = darkColor;
                    currColor = sun.color;
                    colorChange = true;
                    dayPeriod++;
                }
                if (dayLength <= 3)
                {
                   
                    if (dayPeriod == 3)
                    {
                        spawner.night = false;
                        Debug.Log(spawner.night);
                        newColor= lightColor;
                        currColor = sun.color;
                        colorChange = true;
                        dayPeriod++;
                    }
                    if (dayLength <= 0)
                    {
                        dayLength = origDayLength;
                        dayPeriod = 0;
                    }
                }
            }
        }
        //sun.intensity = Mathf.Pow(((2 * dayTimer / dayLength) - 1), sunPower) * -1 + .8f;
    }

    void ColorChange()
    {
        //Debug.Log("going...");
            sun.color = Color.Lerp(currColor, newColor, t);
            if (t < 1)
            { // while t is below the end limit...
              // increment it at the desired rate every update:
                t += Time.deltaTime / transitionLength;
           // Debug.Log("T = " + t);
            }
            else
            {
            t = 0;
                colorChange = false; // turns off color changing for now.
            }
    }
}
