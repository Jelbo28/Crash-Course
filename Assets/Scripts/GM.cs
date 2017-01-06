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
        dayTimer = dayLength / 2;
        sun = GameObject.Find("Sunlight").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        DayNight();
    }

    public void ItemDisplay(GameObject thing /*, int itemType */)
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

    void DayNight()
    {
        dayTimer -= Time.deltaTime;
        if (dayTimer <= 0)
        {
            dayTimer = dayLength;
        }
        sun.intensity = Mathf.Pow(((2 * dayTimer / dayLength) - 1), sunPower) * -1 + 1;
    }
}
