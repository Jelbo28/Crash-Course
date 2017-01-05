using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField]
    float radius = 1f;
    [SerializeField]
    public bool harvestable = true; // requires player.heldItem to be hands
    [SerializeField]
    public bool choppable = true; // requires player.heldItem to be an axe tool
    [SerializeField]
    public bool minable = false; // requires player.heldItem to be picaxe tool

    public Vector3 location;
    [SerializeField]
    public GameObject party;
    public string primaryTool = "Axe";
    public int primarySpeed = 1;
    public int defaultSpeed = 3;

    void Awake()
    {
        location = GetComponent<ClickInteract>().standLocation.transform.position;
        Debug.Log(location);
        SetSpriteLayer();
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
