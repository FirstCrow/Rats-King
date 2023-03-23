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

    public virtual void TakeDamageAndKnockback(float damage, float knockbackStrength, float knockbackDelay, Transform other)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Die();
        }

        
        takeKnockback(knockbackStrength, knockbackDelay, other);
    }


    public virtual void Die() {
        int cheeseCount = Random.Range(0, maxCheese + 1);
        for (int i = 0; i < cheeseCount; i++) {
            Instantiate(cheeseDrops, transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0f), Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void takeKnockback(float knockbackStrength, float knockbackDelay, Transform source)
    {
       
            Rigidbody2D enemy = GetComponent<Rigidbody2D>();  // Gets enemy game object
            enemy.isKinematic = false;
            Vector2 difference = transform.position - source.position;
            difference = difference.normalized * knockbackStrength;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            enemy.isKinematic = true;
            StartCoroutine(KnockbackCo(enemy, knockbackDelay));
    }

    // Adds a delay before the enemy can start walking towards the player again after knockback
    //-------------------------------------------------------------------------------------------
    private IEnumerator KnockbackCo(Rigidbody2D enemy, float knockbackDelay)
    {
        yield return new WaitForSeconds(knockbackDelay);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
    }

}
