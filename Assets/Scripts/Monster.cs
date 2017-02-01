using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float health;
    Animator anim;
    [SerializeField]
    List<Transform> targets;


    Attackable[] objs;
    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
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
	

    void LateUpdate()
    {

        GetComponent<SpriteRenderer>().sortingOrder = (int)Camera.main.WorldToScreenPoint(GetComponent<SpriteRenderer>().bounds.min).y * -1;
    }

    public void Damage(float ammount)
    {
        //Mathf.Lerp(health, health - ammount, Time.deltaTime);
        health -= ammount;
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //Debug.Log("GOOO");
            Damage(10f);
        }
    }

}
