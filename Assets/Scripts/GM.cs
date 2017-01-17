using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    [SerializeField]
    Text itemDisplay;

    public static GM instance = null;
    public GameObject currHighlight;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
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
}
