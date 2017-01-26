using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    //public GameObject area;
    public GameObject spawnObj;
    //public int MinX = 0;
    //public int MaxX = 10;
    //public int MinY = 0;
    //public int MaxY = 10;

    [SerializeField]
    float waitPeriod;
    [SerializeField]
    float distanceFromCenter;
    public int objectAmmount;
    public int dayNumber;
    public bool night;
    public bool stop = false;

    void Update ()
    {

        if (night == true && stop == false)
        {
            StartCoroutine(SpawnLoop());
            stop = true;
        }

    }

    IEnumerator SpawnLoop()
    {
        //float areaX = transform.position.x;
        //float areaY = transform.position.y;

        Vector3 center = transform.position;

        for (int i = 0; i < objectAmmount * dayNumber; i++)
        {
            yield return new WaitForSecondsRealtime(waitPeriod);
            Debug.Log("Monster # " + i + " Spawned");
            Vector3 pos = RandomCircle(center, distanceFromCenter);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

            //float maxposX = Random.Range(areaX + MaxX, areaX + MinX);
            //float minposX = Random.Range(areaX - MaxX, areaX - MinX);
            //float maxposY = Random.Range(areaY + MaxY, areaY + MinY);
            //float minposY = Random.Range(areaY - MaxY, areaY - MinY);

            //float x = Random.Range(maxposX, minposX);
            //float y = Random.Range(maxposY, minposY);
            Instantiate(spawnObj, pos, Quaternion.identity);
           
            

        }
        Debug.Log("Spawning Done");
        //stop = true;
        yield return null;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
