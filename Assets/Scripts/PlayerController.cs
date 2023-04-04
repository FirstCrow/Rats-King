using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : DamageableEntity
{
    //Porting Player health information into here
    [Header("Health")]                     //Tracks GUI regarding Player health
    private float maxHealth;
    public Slider healthSlider;             //Main slider to keep track of health
    public TextMeshProUGUI healthText;
    /*  Extra stuff that can be readded later if desired
    public Slider delaySlider;              //A secondary slider to keep track of total damage taken (within a short time)
    public float delayDuration = 2f;
    private float delayTimer = 0f;
    private float timer = 0f;
    */




    [Header("Special")]                     //Tracks GUI regarding Player Special Meter (Blood meter)
    private int maxBlood;
    public int currentBlood = 100;
    public Slider bloodSlider;

    [Header("Cheese")]
    public int cheese = 0;
    public TextMeshProUGUI cheeseText;

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
    //private PlayerHealth playerHealth;

    [Header("Ranged Variables")]
    
    public GameObject arrow;                //Arrow is temporary, could be replaced with anything else
    public int arrowCost = 20;

    [Header("Melee Variables")]
    private float attackTimer = 0f;         //Duration of melee attack(s)

    public bool AllowDash = true;   //Used to only allow one dash at a time and to limit the use of dashes
    public float DashTime = .1f;


    [Header("Sound Effect Prefabs")]
    public AudioSource footstep;
    private bool footstepsPlaying;
    public GameObject slashingSFX;  //Variable to reference slashing sound effect

    [Header("VFX Effects")]
    public ParticleSystem dust;

    Rigidbody2D rb;
    Collider2D hitbox;
    Animator anim;
    AnimationClip[] clips;          //Stores all animation clips (used to get animation lengths)
    Camera mainCam;
    Vector3 mousePos;

    private bool healing = false;
    private float healingTime = 0;
    void Start()
    {
        maxHealth = HP;
        healthSlider.maxValue = HP;
        healthSlider.value = HP;
        healthText.text = HP + "/" + maxHealth;

        maxBlood = currentBlood;
        bloodSlider.maxValue = currentBlood;
        bloodSlider.value = currentBlood;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentState = PlayerState.moving;
        rotationPoint.SetActive(false);
        //playerHealth = GetComponent<PlayerHealth>();
        hitbox = GetComponent<Collider2D>();
        footstep.loop = false;
        footstepsPlaying = false;
    }

    void Update() 
    {
        //-----MOVING STATE-----
        //Player movement
        if(currentState == PlayerState.moving)
        {

            if (Input.GetKeyDown(KeyCode.R) && healingTime == 0f && HP < maxHealth && currentBlood >= 10)   //Starting to heal
            {
                healingTime = 0.01f;
                healing = true;
                //animate
            }
            if (Input.GetKeyUp(KeyCode.R) && healing)   //Stop healing
            {
                healingTime = 0;
                healing = false;
                //animate
            }
            if (healingTime > 0)    //In process of Healing
            {
                healingTime += Time.deltaTime;
                if (healingTime >= 2.0f && healing) //2 is the current time it takes to heal
                {
                    if (HP < maxHealth && currentBlood >= 10) {
                        TakeDamage(-1);
                        UseBlood(10);
                    }
                    healing = false;

                    if (Input.GetKey(KeyCode.R))
                    {
                        //animate
                        healingTime = 0.01f;
                        healing = true; ;
                    }
                }
                
            }

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

                Instantiate(slashingSFX);
            }

            //Ranged Attack (Right Mouse)
            if (Input.GetKeyDown(KeyCode.Mouse1) && currentBlood >= arrowCost)           //Right Mouse (Hold) = Aim Ranged Attack
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
            if (AllowDash) {
                StartCoroutine(Dash());
            }
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
                
                                                            // The distance you want to spawn the object away from the source
                float distance = 1.0f;

                // Lowers blood meter when using arrow, prevents user from attacking after blood is out.
                UseBlood(arrowCost);

                // Determine the direction based on the Quaternion identity
                Vector3 direction = Quaternion.identity * Vector3.right;
                Debug.Log("Player location " + transform.position);
                Debug.Log("Spawn Position " + (transform.position + direction * distance));
                // Instantiate the object at a position that is distance units away from the source
                Instantiate(arrow, transform.position + (rotationPoint.transform.rotation * Vector3.right), Quaternion.identity);
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
        anim.SetBool("isMoving", rb.velocity.magnitude != 0);


        // Controls footstep SFX and VFX
        //-------------------------

        if (!footstepsPlaying && rb.velocity.magnitude != 0)
        {
            footstep.loop = true;
            footstep.Play();
            footstepsPlaying = true;
            dust.Play();
        }

        else if (footstepsPlaying && rb.velocity.magnitude == 0)
        {
            footstep.loop = false;
            footstep.Stop();
            footstepsPlaying = false;
            dust.Stop();
        }

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

    IEnumerator Dash() {
        AllowDash = false;      //Stops dash from being called repeatedly
        hitbox.enabled = false;
        float baseSpeed = speed;    //Saves the original speed 
        
        
        speed *= 9;     //Because of fixed update I can't just set a temp speed here, I have to change the speed thats being called every update
        //Speed and Dashtime are used to determine the distance the player will travel in the dash, I thought .1 seconds at a speed of 9 looked nice
        yield return new WaitForSeconds(DashTime);
        
        hitbox.enabled = true;
        speed = 0;

        yield return new WaitForSeconds(.5f);   //Creates a delay after the dash where the player can be hit and they cant move

        //Returns control and normal speed back to the player
        speed = baseSpeed;
        currentState = PlayerState.moving;

        //Another timer could be implemented here to stop the player from spamming dashes
        //I think the half second delay already accomplished that so I wont add it 
        AllowDash = true;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = HP;
        healthText.text = HP + "/" + maxHealth;
       
    }

    public void UseBlood(int blood)
    {
        currentBlood -= Mathf.Clamp(blood, 0, maxBlood);
        bloodSlider.value = currentBlood;
    }

    public void AddBlood(int blood)
    {
        currentBlood += blood;
        if (currentBlood > maxBlood)
            currentBlood = maxBlood;
        bloodSlider.value = currentBlood;
    }

}
