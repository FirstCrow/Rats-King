using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
