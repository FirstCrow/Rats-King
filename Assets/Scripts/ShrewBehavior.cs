using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrewBehavior : DamageableEntity
{
    public float speed;                //Enemy move speed
    public float shootSpeed;       //Time between shots
    public float shootRadius;         //Range at which enemy starts shooting
    public float idealRange;            //Distance Shrew wants to keep player at
    public GameObject player;

    public GameObject Knife;

    //[Header("Enemy Footsteps")]

    //private bool footstepsPlaying;
    //private AudioSource footstep;
    private float shootTimer = 0;

    private Animator anim;

    void Start()
    {
        //footstep = GetComponent<AudioSource>();
        //footstep.loop = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (Vector2.Distance(transform.position, player.transform.position) < shootRadius)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > shootSpeed) {
                anim.SetTrigger("attack");
                Debug.Log("Shooting");
                //Shrew should start shooting towards player
                Vector3 dir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //Quaternion rotation = Quaternion.Euler(0, 0, angle);
                //transform.rotation = rotation;

                GameObject FiredProjectileObject = Instantiate(Knife, transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(1.5f, 0, 0),
                                                Quaternion.Euler(0, 0, angle));
                shootTimer = 0;
            }
        }

        if (Vector2.Distance(transform.position, player.transform.position) < idealRange) {
            Debug.Log("Retreating");
            anim.SetBool("retreating", true);

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            //shrew should start backing away
            //probably can use an inverted form of movetowards
        }
        else
        {
            anim.SetBool("retreating", false);
        }

    }

    /*public void playFootsteps()
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
    }*/
}
