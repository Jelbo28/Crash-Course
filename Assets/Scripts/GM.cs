using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [SerializeField]
    int dayLength;
    [SerializeField]
    int sunPower = 2;
    [SerializeField]
    Text itemDisplay;

    public static GM instance = null;
    private float dayTimer;
    [SerializeField]
     Color moonColor;
    [SerializeField]
     Color sunColor;
    public GameObject currHighlight;
    private Light sun;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        dayTimer = dayLength/2;
        sun = GameObject.Find("Sunlight").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dayTimer);
        DayNight();
    }

    public void ItemDisplay(GameObject thing /*, int itemType */)
    {
        //Debug.Log(thing.name);
        if (thing.tag == "Interactable")
        {
            if (thing.name != "Nothing")
            {
                itemDisplay.text = thing.name;
            }
            else
            {
                itemDisplay.text = " ";
            }
            currHighlight = thing;
        }
        else
        {
            //Debug.Log("poop");
        }



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
