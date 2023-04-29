using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("General Varibles")]

    public float damage;
    //public int bloodGainAmount = 20;
    //private Collider2D hitbox;
    //private PlayerController player;


    [Header("Sound Effect Prefabs")]

    public GameObject WallSFX;
    public GameObject EnemyHitSFX;
    public GameObject WoodBreakingSFX;
    public GameObject BreakableWallSFX;

    [Header("Knockback Varibles")]
    public float knockbackStrength = 8;
    public float knockbackDelay = 0.15f;

    public GameObject MeleeVFX;

    // Start is called before the first frame update
    void Start()
    {
        //Automatically pulls the collider
        //hitbox = GetComponent<Collider2D>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); ;

    }

    // Detects when the hurtbox enters an object with a collider
    //------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Applies damage to any game object that triggers it if the object is a damageable entity
        // ------------------------------------------------------------------------------------------------------
        Debug.Log("Collider entered");
        DamageableEntity HitObject = other.GetComponent<DamageableEntity>();
        Debug.Log(other.gameObject.name);
        if (HitObject != null)
        {
            Debug.Log("Enemy has made contact with something");
            if (HitObject.CompareTag("Player"))
            {
                Debug.Log("Enemy has made contact with player");
                HitObject.TakeDamage(damage);
                
            }

            //else
              //  HitObject.TakeDamage(damage);
            //hitbox.enabled = false;           //uncomment this line to make it only hit one enemy at a time
        }


        // Uses the tag of the object collided with to determine what sound effect to play and then plays the correct SFX
        // ----------------------------------------------------------------------------------------------------------------
        if (other.tag == "Wall")
        {
            //Vector3 contact = (other.transform.position - transform.position);
            //player.TriggerRecoil(contact.normalized);
            //Instantiate(WallSFX);
        }
        else if (other.tag == "Enemy")
        {
            //Instantiate(EnemyHitSFX);

            //Instantiate(MeleeVFX, this.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Wood")
        {
            //Instantiate(WoodBreakingSFX);
        }
        else if (other.tag == "Breakable Wall")
        {
            //Instantiate(BreakableWallSFX);
        }

    }
}
