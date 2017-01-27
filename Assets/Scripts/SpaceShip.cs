using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject laser;

    private bool toggle = false;
    private bool laserToggle = false;
    private bool nearShip = false;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        laser = GameObject.FindGameObjectWithTag("Laser");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(toggle);
        if (Input.GetKeyDown(KeyCode.E) && nearShip && !toggle)
        {
            Debug.Log("Bobby");
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            toggle = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && toggle)
        {
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            toggle = false;
        }
        Laser();
    }

    void Laser()
    {
        if (toggle == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !laserToggle)
            {
                laser.GetComponent<ArmRotation>().enabled = true;
                laser.GetComponentInChildren<TestManager>().enabled = true;
                laserToggle = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && laserToggle)
            {
                laser.GetComponent<ArmRotation>().enabled = false;
                laser.GetComponentInChildren<TestManager>().enabled = false;
                laserToggle = false;
            }
        }
        else
        {
                laser.GetComponent<ArmRotation>().enabled = false;
                laser.GetComponentInChildren<TestManager>().enabled = false;
                laserToggle = false;
        }
    }

    void LateUpdate()
    {

        GetComponent<SpriteRenderer>().sortingOrder = (int)Camera.main.WorldToScreenPoint(GetComponent<SpriteRenderer>().bounds.min).y * -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            nearShip = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            nearShip = false;

        }
    }
}
