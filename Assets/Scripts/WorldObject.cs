using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField]
    GameObject particles;
    [SerializeField]
    float radius = 1f;
    [SerializeField]
    public bool harvestable = true; // requires player.heldItem to be hands
    [SerializeField]
    public bool choppable = true; // requires player.heldItem to be an axe tool
    [SerializeField]
    public bool minable = false; // requires player.heldItem to be picaxe tool

    public Vector3 location;
    public string primaryTool = "Axe";
    //  public int mineSuccess;
    // public int mineSpeed;
    // public bool mineStart;
    public int primarySpeed = 1;
    public int defaultSpeed = 3;

    void Awake()
    {
        location = GetComponent<ClickInteract>().standLocation;
        //Debug.Log(location);
        //SetSpriteLayer();
    }


    void Update()
    {

    }

    void LateUpdate()
    {

        GetComponent<SpriteRenderer>().sortingOrder = (int)Camera.main.WorldToScreenPoint(GetComponent<SpriteRenderer>().bounds.min).y * -1;
    }


    public void Mine(float mineSpeed)
    {
        particles = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
        particles.transform.SetParent(gameObject.transform);
        //particles.GetComponent<ParticleSystem>().
       //Destroy(gameObject, mineSpeed);
    }
    public void Cancel()
    {
        //Debug.Log("bob");
        CancelInvoke();
        particles.SetActive(false);
    }

    void SetSpriteLayer()
    {
        if (GetComponent<SpriteRenderer>())
        {
            int layerNum = Mathf.RoundToInt((location.y) * 100) * -1;
            GetComponent<SpriteRenderer>().sortingOrder = layerNum;
            //Debug.Log(layerNum + ", Back");
        }
    }

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(location, Vector3.forward, radius);
    }


}
