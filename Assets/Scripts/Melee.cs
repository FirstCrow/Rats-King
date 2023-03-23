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

    [Header("Knockback Varibles")]
    public float knockbackStrength = 8;
    public float knockbackDelay = 0.15f;

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

        // Uses tag of the object to apply knockback to enemies (Can be later updated to have different knockback values for each enemy)
        //---------------------------------------------------------------------------------------------------------------------
        if (other.tag == "Enemy")
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();  // Gets enemy game object
            enemy.isKinematic = false;
            Vector2 difference = other.transform.position - transform.position;
            difference = difference.normalized * knockbackStrength;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            enemy.isKinematic = true;
            StartCoroutine(KnockbackCo(enemy));
        }

    }

    // Adds a delay before the enemy can start walking towards the player again after knockback
    //-------------------------------------------------------------------------------------------
    private IEnumerator KnockbackCo(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(knockbackDelay);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
    }
}
