using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyFootstepSFX : MonoBehaviour
{
    private bool footstepsPlaying;
    private AudioSource footstep;
    private AudioSource thisFootstep;
    private Rigidbody2D rb;
    private Transform parent;
    private Object thisObject;



    public EnemyFootstepSFX(AudioSource foot)
    {
        footstep = foot;
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Transform>();
        footstep.loop = false;
        footstepsPlaying = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void playFootsteps()
    {
        if (footstepsPlaying == false)
        {
            footstepsPlaying = true;
            thisObject = Instantiate(footstep);
            Debug.Log("Footsteps Playing");
        }
    }

    public void stopFootsteps()
    {
        if (footstepsPlaying == true)
        {
            footstepsPlaying = false;
            Destroy(thisObject);
            Debug.Log("Footsteps not Playing");
        }
    }

}
