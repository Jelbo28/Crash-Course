using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //Text score;
    [SerializeField]
    float speed;
    [SerializeField]
    KeyCode[] attack;
    [SerializeField]
    RadialProgressMeter progressMeter;
    //[SerializeField]
    //Transform[] spawn;
    //[SerializeField]
    //GameObject projectilePrefab;

    //private Animator anim;
    private float moveHorizontal;
    private float moveVertical;
    //private string bob;
    //private Transform projectile;
    //private GameObject ProjectileGO;
    //private int points;
    // private Rigidbody2D thisRigidbody;
    private bool clickWalking = false;
    // private bool walking = false;
    private Vector3 currTarget;
    private Vector3 pastTarget;
    private Vector3 meterTarget;
    private float roughness = 0.2f;
    public Vector3 bottom;
    private bool harvest = false;
    private GameObject mine;
    private WorldObject interactable;
    //private float bound;
    //public int damageInflcited = 0;
    private string currentHeldItem;

    // Use this for initialization
    //   void Start ()
    //   {
    //       anim = GetComponent<Animator>();
    //}
    void Awake()
    {
        bottom = new Vector3(transform.position.x, transform.position.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y);
        //thisRigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        currentHeldItem = GetComponent<PlayerInventory>().CurrentlyEquippedTool;
        Interact();
        AdjustLayer();
        //GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Movement();

        //Debug.Log(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical"));
        //if (transform.position != pastTarget)
        //{
        //    interactable.party.SetActive(false);
        //    progressMeter.Cancel();
        //    harvest = false;
        //}

            //Animate();
        
    }

    void Movement()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical, 0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) > 0f) // Checks for manual movement.
        {
            progressMeter.Cancel();


        }
            if (clickWalking)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) > 0f) // Checks for manual movement.
            {
                //Debug.Log("Bob");
                clickWalking = false;

            }

            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, currTarget, step);
            //thisRigidbody.velocity = close;
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    thisRigidbody.velocity = Vector3.zero;
            //}

            if (transform.position == currTarget)
            {
                // thisRigidbody.velocity = Vector3.zero;
                clickWalking = false;
                if (harvest)
                {
                    pastTarget = currTarget;
                    //Debug.Log("HA");
                    float mineSpeed = 0f;
                    if (interactable.primaryTool == currentHeldItem)
                    {
                        mineSpeed = interactable.primarySpeed;
                    }
                    else
                    {
                        mineSpeed = interactable.defaultSpeed;
                    }
                    //mineSpeed *= currentHeldItem.level; This would multiply it based on your tool's level (wood, iron, diamond)
                    progressMeter.Activate(mineSpeed, interactable.gameObject);
                    interactable.party.SetActive(true);
                    harvest = false;
                }
                //Debug.Log("Whoop");
            }
        }

    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(1) && GM.instance.currHighlight.name != "Nothing")
        {
            Debug.Log("Wha?");
            Debug.Log(GM.instance.currHighlight.name);
            interactable = GM.instance.currHighlight.GetComponent<WorldObject>();
            switch (currentHeldItem)
            {
                case "Hands":
                    if (interactable.harvestable)
                    {
                        //meterTarget = interactable.transform.position;
                        currTarget = interactable.location;
                        clickWalking = true;
                        harvest = true;
                    }
                    break;
                case "Axe":
                    if (interactable.choppable)
                    {
                        //meterTarget = interactable.transform.position;
                        currTarget = interactable.location;
                        clickWalking = true;
                        harvest = true;
                    }
                    break;

                default:
                    break;
            }
        }

        //else
        //{
        //    Debug.Log("2");
        //}

    }

    void AdjustLayer()
    {
        int layerNum = Mathf.RoundToInt((bottom.y + transform.position.y) * 100) * -1;
        GetComponent<SpriteRenderer>().sortingOrder = layerNum;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Biome")
        {
            Debug.Log(other.name);
        }
    }

    public void WalkTo(Vector2 location)
    {

        currTarget = location;
        //if (transform.position.x - currTarget.x < 0)
        //{
        //   currTarget.x *= -1;
        //}
        clickWalking = true;
    }
}
