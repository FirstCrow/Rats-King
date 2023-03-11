using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float damage;
    public Collider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        //Automatically pulls the collider
        hitbox = GetComponent<Collider2D>();
    }

    //Applies damage to any game object that triggers it if the object is a damageable entity
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider entered");
        DamageableEntity HitObject = other.GetComponent<DamageableEntity>();
        Debug.Log(other.gameObject.name);
        if (HitObject != null)
        {
            HitObject.TakeDamage(damage);
            //hitbox.enabled = false;           //uncomment this line to make it only hit one enemy at a time
        }
        
    }
}
