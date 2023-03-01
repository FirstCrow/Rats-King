using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        moving,
        dashing,
        attacking,
        aiming,
        talking,
        dead
    }
    public PlayerState currentState;        //Determines which state the player is currently in

    [Header("Main Variables")]
    public float speed = 10f;               //Player speed
    private Vector2 direction;              //Player direction (based on movement inputs [WASD])
    public GameObject rotationPoint;        

    [Header("Ranged Variables")]
    
    public GameObject arrow;                //Same here. could be replaced with anything else

    [Header("Melee Variables")]
    private float attackTimer = 0f;         //Duration of melee attack(s)

    Rigidbody2D rb;
    Animator anim;
    private AnimationClip[] clips;          //Stores all animation clips (used to get animation lengths)
    Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentState = PlayerState.moving;
        rotationPoint.SetActive(false);
    }

    void Update() 
    {
        //-----MOVING STATE-----
        //Player movement
        if(currentState == PlayerState.moving)
        {
            direction.x = Input.GetAxisRaw("Horizontal");   //Get Horizontal Inputs (A or D | Left or Right)
            direction.y = Input.GetAxisRaw("Vertical");     //Get Vertical Inputs (W or S | Up or Down)

            //Melee Attack (Left Mouse)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Attack Button Pressed!");
                direction = Vector2.zero;                   //Stops movement before attacking
                rotationPoint.SetActive(true);
                EnableRotationPoint();                      //Enables the rotation point for one frame only

                //Actual attack is done through animations instead of code
                //Ideally, the player will be pushed forwards slightly here
                attackTimer = GetAnimationClipLength("Player_Melee_1");
                currentState = PlayerState.attacking;
            }

            //Ranged Attack (Right Mouse)
            if (Input.GetKeyDown(KeyCode.Mouse1))           //Right Mouse (Hold) = Aim Ranged Attack
            {
                Debug.Log("Ranged Button Pressed!");
                direction = Vector2.zero;                   //Stops movement before aiming
                rotationPoint.SetActive(true);
                currentState = PlayerState.aiming;
            }

            //Dash (LShift)
            if (Input.GetKeyDown(KeyCode.LeftShift))        //LShift = Dash/Roll
            {
                Debug.Log("Dash Button Pressed!");
                currentState = PlayerState.dashing;
            }
        }

        //-----DASHING STATE-----
        //Used whenever the player dashes
        else if (currentState == PlayerState.dashing)
        {
            currentState = PlayerState.moving;
        }

        //-----ATTACKING STATE-----
        //Used for player melee attacks
        else if (currentState == PlayerState.attacking)
        {
            attackTimer -= Time.deltaTime;

            if(attackTimer <= 0)
            {
                rotationPoint.SetActive(false);
                currentState = PlayerState.moving;
            }
        }

        //-----AIMING STATE-----
        //Used for player ranged attacks (eventually?)
        else if (currentState == PlayerState.aiming)
        {
            EnableRotationPoint();                          //Allows rotation during the aim state

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Ranged Button Released!");       //Right Mouse (Release) = Shoot Ranged Attack
                Instantiate(arrow, transform.position, Quaternion.identity);

                rotationPoint.SetActive(false);
                currentState = PlayerState.moving;
            }
        }

        //-----TALKING STATE-----
        //Used for cutscenes
        else if (currentState == PlayerState.talking)
        {
            currentState = PlayerState.moving;
        }

        //-----DEAD STATE-----
        //Game over trigger
        else if (currentState == PlayerState.dead)
        {

        }


        //Animation
        /* STATES:
         * 0 = moving
         * 1 = dashing
         * 2 = attacking
         * 3 = aiming
         * 4 = talking
         * 5 = dead
        */
        anim.SetInteger("curState", (int)currentState);
        anim.SetBool("isMoving", Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.y) > 0.1f);

    }

    //Fixed Update runs at a fixed framerate (say 60FPS). Typically used for physics-based calculations (like rigidbody movement)
    void FixedUpdate()
    {
        //Main movement
        if(Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0)        //Limits diagonal movement speed
        {
            rb.velocity = direction * speed / Mathf.Sqrt(2);
        }
        else
        {
            rb.velocity = direction * speed;                                //Moves the player
        }

    }

    //Enables rotational movement between the player and the mouse (Points from the player towards the mouse)
    //Used mainly to determine attack direction
    //Used in tangent with rotationPoint.SetActive(true/false)
    void EnableRotationPoint()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);         //Get mouse position (using world coordinates)
        Vector3 rotation = mousePos - transform.position;                   //Get direction vector from player to mouse
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;   //Get angle of direction vector (in degrees)
        rotationPoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);              //Rotate position towards mouse using angle
    }

    //Gets the Animation Clip length of a given name (invalid names return error)
    float GetAnimationClipLength(string name)
    {
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == name)
            {
                return clip.length;
            }
        }

        Debug.LogError("Animation clip name not found!: " + name);
        return 0f;
    }
}