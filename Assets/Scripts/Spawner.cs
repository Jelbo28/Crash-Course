using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Public
    //public GameObject area;
    [SerializeField]
    Collider2D spawnColl;
    public GameObject[] spawnObject;
    //public int MinX = 0;
    //public int MaxX = 10;
    //public int MinY = 0;
    //public int MaxY = 10;
    public int objectAmmount;
    //Private


    void Start()
    {

        Debug.Log("Spawn");
        int objectType = 0;
        for (int i = 0; i < objectAmmount; i++)
        {
            objectType = Mathf.RoundToInt(Random.Range(0, spawnObject.Length)); // Randomly assigns the object being spawned.
                                                              // We could make this more seasoned by giving more or less chance to certain objects, more grass, less mushrooms, etc.
                                                              // I also suggest making separate polygon areas for groups of the same resource like a bunch of pine trees.
                                                              // We could do this by making children of this object, each with a polygon collider, then assign the apropriate portion according to the total ratio.                                                 
            Instantiate(spawnObject[objectType], PointInArea(), Quaternion.identity); // Generates the object with our specified instruction.
        }

    }

    public Vector2 PointInArea()
    {
        Bounds bounds = spawnColl.bounds;
        Vector2 center = bounds.center;

        float x = 0;
        float y = 0;
        int attempt = 0;
        do
        {
            x = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            attempt++;
        } while (!GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(x, y)) && attempt <= 100);


        return new Vector2(x, y);
    }
}

