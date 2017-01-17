using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    private float dayTimer;
    [SerializeField]
    Color moonColor;
    [SerializeField]
    Color sunColor;
    [SerializeField]
    int dayLength;
    [SerializeField]
    int sunPower = 2;

    private Light sun;

    void Start()
    {
        dayTimer = dayLength / 2;
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dayTimer);
        DayNight();
    }


    void DayNight()
    {
        dayTimer -= Time.deltaTime;
        if (dayTimer <= 12)
        {

            sun.color = Color.Lerp(sun.color, moonColor, Time.deltaTime);


            if (dayTimer < 0)
            {
                sun.color = sunColor;
                dayTimer = dayLength;
            }
        }
        sun.intensity = Mathf.Pow(((2 * dayTimer / dayLength) - 1), sunPower) * -1 + .8f;
    }
}
