using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thanks to https://youtu.be/-bkmPm_Besk for the code
public class Arrow : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;           //Projectile speed of arrow
    public float lifeTime = 3f;         //Time before bullet destroys 
    public GameObject arrowSFX;         //Arrow SFX prefab
    private float lifeTimeTimer = 0f;

    private Rigidbody2D rb;
    private Vector3 playerPos;

    void Start()
    {
        lifeTimeTimer = lifeTime;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        rb = GetComponent<Rigidbody2D>();
        Vector3 rotation = playerPos - transform.position;
        Vector3 direction = transform.position - playerPos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

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
                HitObject.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
