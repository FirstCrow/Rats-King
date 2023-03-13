using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to detect what objects the slash is connecting with for sound effects

public class SwordSlashSFX : MonoBehaviour
{

    private Collider2D hitbox;

    [Header("Sound Effect Prefabs")]

    public GameObject WallSFX;
    public GameObject EnemyHitSFX;

    // Start is called before the first frame update
    //------------------------------------------------
    void Start()
    {
        hitbox = GetComponent<Collider2D>();            // Automatically pulls the collider
    }


    // Detects when the hurtbox enters an object with a collider
    //------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {

        // Uses the tag of the object collided with to determine what sound effect to play and then plays the correct SFX

        if (other.tag == "Wall")
        {
            Instantiate(WallSFX);
        }
        else if (other.tag == "Enemy")
        {
            Instantiate(EnemyHitSFX);
        }
    }
}
