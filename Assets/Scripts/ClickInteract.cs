using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteract : MonoBehaviour
{
    GameObject player;


    private Camera cam;
    [SerializeField]
    Vector3 distanceModifier;
    [HideInInspector]
    public Vector3 standLocation;
    [SerializeField]
    bool Interactable = false;
    //[SerializeField]
    //bool Destructable = false;
    // Use this for initialization


    void Awake()
    {
        if (name != "Nothing")
        {
            standLocation = (new Vector3(-GetComponent<SpriteRenderer>().bounds.extents.x, -GetComponent<SpriteRenderer>().bounds.extents.y, 0) + transform.position + distanceModifier);
        }
        player = GameObject.FindGameObjectWithTag("Player");
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

                Debug.Log("Bob");
                player.GetComponent<PlayerController>().WalkTo(standLocation /*, radius, Destructable, gameObject */);
            
        }
        else /* if (tag == "Ground") */
        {
           // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            player.GetComponent<PlayerController>().WalkTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(standLocation, 0.1f);
    }
}
