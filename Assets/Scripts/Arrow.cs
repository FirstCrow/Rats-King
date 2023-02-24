using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thanks to https://youtu.be/-bkmPm_Besk for the code
public class Arrow : MonoBehaviour
{
    public float speed = 10f;           //Projectile speed of arrow
    public float lifeTime = 3f;         //Time before bullet destroys itself
    private float lifeTimeTimer = 0f;

    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector3 mousePos;

    void Start()
    {
        lifeTimeTimer = lifeTime;

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if(lifeTimeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
