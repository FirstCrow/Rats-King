using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Players Projectile
//Thanks to https://youtu.be/-bkmPm_Besk for the code
public class Arrow : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;           //Projectile speed of arrow
    public float lifeTime = 3f;         //Time before bullet destroys 
    public GameObject arrowSFX;         //Arrow SFX prefab
    private float lifeTimeTimer = 0f;

    [Header("Knockback Varibles")]
    public float knockbackStrength;
    public float knockbackDelay;

    private Rigidbody2D rb;
    private Vector3 playerPos;

    void Start()
    {
        lifeTimeTimer = lifeTime;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.rotation * new Vector3(0, speed, 0);
        Instantiate(arrowSFX);

    }

    void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if(lifeTimeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collider entered");
            DamageableEntity HitObject = other.GetComponent<DamageableEntity>();
            if (HitObject != null)
            {
                HitObject.TakeDamageAndKnockback(damage, knockbackStrength, knockbackDelay, this.transform);
            }
            Destroy(gameObject);
        }

    }

}
