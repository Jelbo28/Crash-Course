using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindpolyPoint : MonoBehaviour {

    //Public
    //public GameObject area;
    public GameObject spawnObj;
    //public int MinX = 0;
    //public int MaxX = 10;
    //public int MinY = 0;
    //public int MaxY = 10;
    public int objectAmmount;
    //Private

    void Start()
    {
        //float areaX = area.transform.position.x;
        //float areaY = area.transform.position.y;




    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Bob");
            for (int i = 0; i < objectAmmount; i++)
            {
                //float maxposX = Random.Range(areaX + MaxX, areaX + MinX);
                //float minposX = Random.Range(areaX - MaxX, areaX - MinX);
                //float maxposY = Random.Range(areaY + MaxY, areaY + MinY);
                //float minposY = Random.Range(areaY - MaxY, areaY - MinY);

                //float x = Random.Range(maxposX, minposX);
                //float y = Random.Range(maxposY, minposY);
                Instantiate(spawnObj, PointInArea(), Quaternion.identity);

            }
        }
    }

    public Vector2 PointInArea()
    {
        Bounds bounds = GetComponent<PolygonCollider2D>().bounds;
        Vector2 center = bounds.center;

        float x = 0;
        float y = 0;
        int attempt = 0;
        do
        {
            x = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
        } while (!GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(x, y)) && attempt <= 100);
        Debug.Log("Attempts: " + attempt + ", pos = (" + x + ", " + y + ")");

        return new Vector2(x, y);
    }
}
