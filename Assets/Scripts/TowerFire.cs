using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFire : MonoBehaviour
{
    //public
    //private
    bool inSight;
    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
    }

	void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            inSight = true;
        }
        else
        {
            inSight = false;
        }

        Anim.SetBool("inSight", inSight);
    }

    void OnTriggerExit()
    {
        inSight = false;
        Anim.SetBool("inSight", inSight);
        
    }
}
