using UnityEngine;
using UnityEngine.UI;

public class RadialProgressMeter : MonoBehaviour
{
    [SerializeField]
    Text TextIndicator;
    [SerializeField]
    Text TextLoading;
    [SerializeField]
    Image loadingBar;
    [SerializeField]
    float loadingTime;

    private WorldObject targetObject;
    private float loadingTimer;
    private float toPercent;
    private float toLoad;
    private bool complete;
    private bool asleep = true;
    [SerializeField]
    Image[] images;

    void Start()
    {
        loadingTimer = 0;
    }

    void Update()
    {
        if (!asleep)
        {
            if (loadingTimer < loadingTime)
            {
                loadingTimer += Time.deltaTime;
                toPercent = loadingTimer / loadingTime * 100f;
                toLoad = toPercent / 100f;
                TextIndicator.text = Mathf.Round(toPercent) + "%";
                loadingBar.fillAmount = toLoad;
            }
            else if (loadingTimer >= loadingTime && complete == false)
            {

                TextIndicator.text = "100%";
                TextLoading.text = "Done!";
                loadingTime = 0f;

                //loadingBar.fillAmount = 0f;
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].enabled = false;
                }
                if (!asleep)
                {
                    Debug.Log(targetObject.name);
                    targetObject.gameObject.SetActive(false);
                }

                complete = true;
            }
        }
    }

    public void Activate(float loadTime, WorldObject target)
    {
        if (asleep)
        {
            asleep = false;
        }
        targetObject = target;
        GetComponentInParent<Transform>().position = target.transform.position;
        for (int i = 0; i < images.Length; i++)
        {
            //Debug.Log(i);
            images[i].enabled = true;
        }
        loadingTime = loadTime;
        loadingTimer = 0f;
        complete = false;
        //Debug.Log("Ready");
    }

    public void Cancel()
    {
        Debug.Log("Whyy");
        asleep = true;
        TextIndicator.text = "100%";
        TextLoading.text = "Done!";
        loadingTime = 0f;
        // Debug.Log("DOne");
        //loadingBar.fillAmount = 0f;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = false;
            //Debug.Log(i);
        }
        complete = true;
    }
}
