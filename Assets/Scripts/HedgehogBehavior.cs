using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class HedgehogBehavior : DamageableEntity
{
    public float speed;                //Enemy move speed
    public float detectRadius;         //Enemy detection radius
    public float attackRange;
    private float attackTimer;
    private bool attacking;
    public GameObject player;
    


    [Header("Enemy Footsteps")]

    private bool footstepsPlaying;
    private AudioSource footstep;


    void Start()
    {
        footstep = GetComponent<AudioSource>();
        footstep.loop = false;
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
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange) {
            //get attack animation length
            attackTimer = 1.5f;
            Debug.Log("Hedgehog Attacking");
            //play attack animation
            attacking = true;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < detectRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            playFootsteps();
        }
        else
            stopFootsteps();
    }


    //Active only when on screen (Applies to scene view too!)
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
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