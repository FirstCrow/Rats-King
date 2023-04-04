using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : DamageableEntity
{
    public float speed;                //Enemy move speed

    public float attackRange;            //Distance mouse can attack at
    private float attackTimer;
    private bool attacking;
    public GameObject player;


    [Header("Enemy Footsteps")]

    private bool footstepsPlaying;
    private AudioSource footstep;

    void Start()
    {
        //footstep = GetComponent<AudioSource>();
        //footstep.loop = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                Debug.Log("Hedgehog finished Attacking");
                attacking = false;
            }
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            //get attack animation length
            attackTimer = .5f;
            Debug.Log("Hedgehog Attacking");
            //play attack animation
            attacking = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //playFootsteps();
        }
        
    }



    public void playFootsteps()
    {

        if (footstepsPlaying == false)
        {
            footstepsPlaying = true;
            footstep.loop = true;
            footstep.Play();
            Debug.Log("Footsteps Playing");
        }
    }


    public void stopFootsteps()
    {
        if (footstepsPlaying == true)
        {
            footstepsPlaying = false;
            footstep.loop = true;
            footstep.Stop();
            Debug.Log("Footsteps not Playing");
        }
    }
}
