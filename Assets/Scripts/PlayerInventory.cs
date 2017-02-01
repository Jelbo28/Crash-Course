using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public string CurrentlyEquippedTool;
	

    void Awake()
    {
        CurrentlyEquippedTool = "Hands";
    }
}
