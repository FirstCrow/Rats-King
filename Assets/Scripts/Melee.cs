using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float damage;
    private Collider2D hitbox;


    [Header("Sound Effect Prefabs")]

    public GameObject WallSFX;
    public GameObject EnemyHitSFX;
    public GameObject WoodBreakingSFX;

    public GameObject MeleeVFX;

    // Start is called before the first frame update
    void Start()
    {
        //Automatically pulls the collider
        hitbox = GetComponent<Collider2D>();
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
            HitObject.TakeDamage(damage);
            //hitbox.enabled = false;           //uncomment this line to make it only hit one enemy at a time

            //HitObject.takeKnockback();
        }

        // Uses the tag of the object collided with to determine what sound effect to play and then plays the correct SFX
        // ----------------------------------------------------------------------------------------------------------------
        if (other.tag == "Wall")
        {
            Instantiate(WallSFX);
        }
        else if (other.tag == "Enemy")
        {
            Instantiate(EnemyHitSFX);

            Instantiate(MeleeVFX, this.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Wood")
        {
            Instantiate(WoodBreakingSFX);
        }

    }
}
