using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;           //Projectile speed of arrow
    public float lifeTime = 3f;         //Time before bullet destroys 
    //public GameObject arrowSFX;         //Arrow SFX prefab
    private float lifeTimeTimer = 0f;

    [Header("Knockback Varibles")]
    public float knockbackStrength;
    public float knockbackDelay;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.rotation * new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeTimer += Time.deltaTime;
        if (lifeTimeTimer > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider entered");
        DamageableEntity HitObject = other.GetComponent<DamageableEntity>();
        if (HitObject != null)
        {
            HitObject.TakeDamage(damage);
            //HitObject.TakeDamageAndKnockback(damage, knockbackStrength, knockbackDelay, this.transform);
        }
        Destroy(gameObject);
    }
}