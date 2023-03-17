using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class FootstepFX : MonoBehaviour
{

    private bool footstepsPlaying;
    public AudioSource footstep;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        footstep.loop = false;
        footstepsPlaying = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!footstepsPlaying && rb.velocity.magnitude != 0)
        {
            footstep.loop = true;
            footstep.Play();
            footstepsPlaying = true;
        }

        else if (footstepsPlaying && rb.velocity.magnitude == 0)
        {
            footstep.loop = false;
            footstep.Stop();
            footstepsPlaying = false;
        }

        
    }
}
