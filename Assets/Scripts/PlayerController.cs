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

    public float speed = 10f;               //Player speed
    public PlayerState currentState;        //Determines which state the player is currently in

    private Vector2 direction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.moving;
    }

    void Update() 
    {
        //-----MOVING STATE-----
        //Player movement
        if(currentState == PlayerState.moving)
        {
            direction.x = Input.GetAxisRaw("Horizontal");   //Get Horizontal Inputs (A or D | Left or Right)
            direction.y = Input.GetAxisRaw("Vertical");     //Get Vertical Inputs (W or S | Up or Down)

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Attack Button Pressed!");
                currentState = PlayerState.attacking;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Ranged Button Pressed!");
                currentState = PlayerState.aiming;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
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
            currentState = PlayerState.moving;
        }

        //-----AIMING STATE-----
        //Used for player ranged attacks (eventually?)
        else if (currentState == PlayerState.aiming)
        {
            direction = Vector2.zero;                 //Stops movement before aiming

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Ranged Button Released!");
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


    }

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
}
