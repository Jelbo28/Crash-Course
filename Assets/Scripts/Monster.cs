using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float health;

    [SerializeField]
    List<Transform> targets;


    Attackable[] objs;
    // Use this for initialization
    void Awake ()
    {
        objs = FindObjectsOfType<Attackable>();
        int i = 0;
        foreach (Attackable item in objs)
        {
            targets.Add(item.transform);
            i++;
            Debug.Log(i);
        }
        //Debug.Log(GetClosestTarget(targets).name);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(float ammount)
    {
        //Mathf.Lerp(health, health - ammount, Time.deltaTime);
        health -= ammount;
    }

    Transform GetClosestTarget(List<Transform> targets)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in targets)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.name == "Laser")
    //    {
    //        Debug.Log("GOOO");
    //        Damage(10f);
    //    }
    //}
}
