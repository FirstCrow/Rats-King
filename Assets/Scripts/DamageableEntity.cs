using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can be inherited by the player, enemies, or any object that will take damage and then be destroyed
public class DamageableEntity : MonoBehaviour
{
    public GameObject cheeseDrops;
    public int maxCheese;
    public float HP;

    public virtual void TakeDamage(float damage) {
        HP -= damage;

        if (HP <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        int cheeseCount = Random.Range(0, maxCheese + 1);
        for (int i = 0; i < cheeseCount; i++) {
            Instantiate(cheeseDrops, transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0f), Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
}
