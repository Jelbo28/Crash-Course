using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteract : MonoBehaviour
{
    [SerializeField]
    GameObject player;


    private Camera cam;
    [SerializeField]
    public GameObject standLocation;
    [SerializeField]
    bool Interactable = false;
    //[SerializeField]
    //bool Destructable = false;
    // Use this for initialization


    void Awake()
    {
        // We can also avoid calculating where the bottom of the sprite is by just setting it as the pivot point.
       //standLocation =  new Vector3(transform.position.x, transform.position.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y, transform.position.z);
       //standLocation = new
    }

    void Start()
    {
        Physics.queriesHitTriggers = true;
    }

    void Update()
    {
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

    void OnMouseOver()
    {

        GM.instance.ItemDisplay(gameObject);

    }

    void OnMouseDown()
    {
        if (Interactable)
        {
            player.GetComponent<PlayerController>().WalkTo(standLocation.transform.position /*, radius, Destructable, gameObject */);
        }
        else /* if (tag == "Ground") */
        {
           // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            player.GetComponent<PlayerController>().WalkTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
