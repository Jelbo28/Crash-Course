using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    float speed; // Designated movement speed for the player
    [SerializeField]
    RadialProgressMeter progressMeter; // The progress meter

    public Vector2 bottom; // Bottom/middle of the player's sprite

    private float moveHorizontal; // vertical movement
    private float moveVertical; // horizontal movement
    private float mineSpeed = 0f; // What speed the object will be mined at, this gets altered and is the load time

    private bool moving = false; // if clickwalking or manual movement is happening.
    private bool currMining = false; // if the player is mining something
    private bool clickWalking = false;
    private bool harvest = false;

    private Vector3 currTarget; // Where the player wants to be
    private WorldObject interactable; // Whatever object the player currently interacts with
    private string currentHeldItem; // The player's currently equipped item (will be changedwhen I have an inventory)
    private Light faceLight; // Flashlight attached to the player, child
    #endregion

    void Awake()
    {
        bottom = new Vector2(transform.position.x, transform.position.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y); // calculates the bottom middle of the player's sprite
        currentHeldItem = GetComponent<PlayerInventory>().CurrentlyEquippedTool; // Gets equipped item from inventory
        faceLight = GetComponentInChildren<Light>();
    }

    void Update()
    {
        Debug.Log(moving);
        Interact();
        AdjustLayer();
        Movement();
        Flashlight();

    }

    void Flashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            faceLight.enabled = !faceLight.enabled;
        }
    }

    void Movement()
    {
        // Defines the manual movement of the player, i.e. wasd or arrow keys
        moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (moveHorizontal != 0 || moveVertical != 0) //Checks for any manual movement
        {
            moving = true; // The player is moving
            clickWalking = false; // while moving with the keyboard, you can't click and walk
            transform.Translate(moveHorizontal, moveVertical, 0f); // Moves player manually
            currTarget = transform.position; // Sets target area to whereever the player is.
        }
        else if (clickWalking == true) // Checks if the player should be going to click location
        {
            moving = true; // The player is moving

            // Moving to a target position at a certain speed, the same speed for manual movement.
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currTarget, step);

            if (transform.position == currTarget) // Waits for the player to get to their target location.
            {
                clickWalking = false; // turns off walking once there
                if (harvest) // checks to see if it should be mining the object.
                {
                    currMining = true; // tells script that it has begun mining something.
                    Debug.Log("HA");
                    if (interactable.primaryTool == currentHeldItem) // Checks to see if the players item is the right tool type to harvest the object, if not the spped will be slower.
                    {
                        mineSpeed = interactable.primarySpeed;
                    }
                    else
                    {
                        mineSpeed = interactable.defaultSpeed;
                    }
                    //mineSpeed *= currentHeldItem.level; This would multiply it based on your tool's level (wood, iron, diamond)
                    progressMeter.Activate(mineSpeed, interactable); // Sets the loading meter up by calling its Activate function.
                    interactable.Mine(mineSpeed); // Tells the object that it's getting harvested, triggering another function
                    //interactable.party.SetActive(true);
                    harvest = false; // Now that the object and loading bar know what they're doing so we turn this off.
                }
            }
        }
        else
        {
            moving = false;
        }

        if (moving && currMining) // Checks if the player was in the middle of mining something and they're leaving it.
        {
            // These are all of the functions in separate scripts running that needed to be reversed or cancelled.
            progressMeter.Cancel();
            interactable.Cancel();
            harvest = false; // It's impossible to move and mine something, you need to right click it!
            currMining = false; // Turns bool off after canceling everything.
        }

    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(1) && GM.instance.currHighlight != null) //Player right clicks a world object that is highlighted by the mouse.
        {
            interactable = GM.instance.currHighlight.GetComponent<WorldObject>(); //Gets the world object script for interaction
            switch (currentHeldItem /* .tag  */) // Checks what the player is equipped with
            {
                case "Hands":
                    if (interactable.harvestable) //Checks if this item will work with the object
                    {
                        WalkTo(interactable.location); // Makes the player walk to the mining location.
                        harvest = true; // Sets mining to true
                    }
                    break;
                case "Axe":
                    if (interactable.choppable) //Checks if this item will work with the object
                    {
                        WalkTo(interactable.location); // Makes the player walk to the mining location.
                        harvest = true; // Sets mining to true
                    }
                    break;

                default:
                    Debug.Log("Item equipped not compatable with world object!");
                    break;
            }
        }
        else
        {
            Debug.Log("Nothing highlighted");
        }
    }

    void AdjustLayer()
    {
        int layerNum = Mathf.RoundToInt((bottom.y + transform.position.y) * 100) * -1; // Layers player based on their y-position
        GetComponent<SpriteRenderer>().sortingOrder = layerNum; // Changing the layer
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Biome") // Detects what biome you're in with triggers
        {
            //Debug.Log(other.name);
        }
    }

    public void WalkTo(Vector2 location) // Simple click walkto function
    {
        currTarget = location;
        clickWalking = true;
    }
}
