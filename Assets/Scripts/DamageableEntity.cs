using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can be inherited by the player, enemies, or any object that will take damage and then be destroyed
public class DamageableEntity : MonoBehaviour
{

    public float HP;

    public void TakeDamage(float damage) {
        HP -= damage;

        if (HP <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
    
}
