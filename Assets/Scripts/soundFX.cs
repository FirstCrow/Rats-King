using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundFX : MonoBehaviour
{
    public float lifetime = 3f;
    public float lifeTimeTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {

        lifeTimeTimer = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if (lifeTimeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
