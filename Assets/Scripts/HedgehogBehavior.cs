using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HedgehogBehavior : DamageableEntity
{
    public float speed;                //Enemy move speed
    public float detectRadius;         //Enemy detection radius
    public float attackRange;
    private float attackTimer;
    private bool attacking;
    public GameObject player;
    private int direction;
    private float scale;
    public ParticleSystem dust;
    private Vector3 randompoint;
    private int randomsteps;
    private int steps = 0;

    [Header("Enemy Footsteps")]

    private bool footstepsPlaying;
    public AudioSource footstep;
    private Animator anim;
    public AudioSource attackSFX;


    void Start()
    {
        randompoint = transform.position + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), 0f);
        randomsteps = Random.Range(10, 500);
        footstep.loop = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        direction = 1;
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) {
            return;
        }
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
            attackTimer = 1.5f;
            Debug.Log("Hedgehog Attacking");
            //play attack animation
            attacking = true;
            attackSFX.Play();
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < detectRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            direction = (transform.position.x > player.transform.position.x) ? 1 : -1;  //Enemy faces towards player when moving
            playFootsteps();
        }
        else
        {
            stopFootsteps();
            RandomMotion();
        }

        transform.localScale = new Vector2(scale * direction, scale);   //Determines which direction enemy faces

        //Animations
        anim.SetBool("attacking", attacking);
        anim.SetBool("moving", !attacking && Vector2.Distance(transform.position, player.transform.position) < detectRadius);
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

    public void PlayDust()
    {
        dust.Play();
    }

    public void RandomMotion() {
        transform.position = Vector2.MoveTowards(transform.position, randompoint, speed * Time.deltaTime);
        steps++;
        if (steps > randomsteps) {
            steps = 0;
            randompoint = transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0f);
            randomsteps = Random.Range(10, 500);
        }

    }
}