using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashVFX : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    public float lifeTime = 0.5f;
    float lifeTimeTimer = 0f;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        lifeTimeTimer = lifeTime;
    }

    void Update()
    {
        lifeTimeTimer -= Time.deltaTime;

        float t = lifeTimeTimer / lifeTime;
        t = t * t * t;
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, t);    //Color fades out depending on lifeTIme

        if(lifeTimeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
