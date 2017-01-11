using System.Collections;
using System.Collections.Generic;
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
    //private Color crazyColor = new Color(0f, 0f, 0f, -1f);
    [SerializeField]
    private Image[] images;
    //private bool go = false;


    // Use this for initialization
    void Start()
    {
        loadingTimer = 0;
        //image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    loadingTimer = 0;
        //}
            if (loadingTimer < loadingTime)
            {
                loadingTimer += Time.deltaTime;
                toPercent = loadingTimer / loadingTime * 100f;
                toLoad = toPercent / 100f;
                TextIndicator.text = Mathf.Round(toPercent) + "%";
                loadingBar.fillAmount = toLoad;
            }
            else if (!complete)
            {

                TextIndicator.text = "100%";
                TextLoading.text = "Done!";
                loadingTime = 0f;
                //Debug.Log(targetObject.name);

                //targetObject.mineSuccess = targetObject.mineSuccess + 1;

            //Debug.Log(targetObject.GetComponent<WorldObject>().mineSuccess);
         
            //loadingBar.fillAmount = 0f;
            for (int i = 0; i < images.Length; i++)
                {
                images[i].enabled = false;
                //Debug.Log(i);
                }
                complete = true;
                targetObject.gameObject.SetActive(false);
                //GetComponentInChildren<Image>().color += crazyColor;
                //go = false;
            }
   



    }

    public void Activate(float loadTime, WorldObject target)
    {
        targetObject = target;
        GetComponentInParent<Transform>().position = target.transform.position;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = true;
        }
        //images[1].color -= crazyColor;
        //images[2].color -= crazyColor;
        loadingTime = loadTime;
        loadingTimer = 0f;
        complete = false;
        //go = true;
    }

    public void Cancel()
    {
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
