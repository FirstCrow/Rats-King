using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFootstepSFX : MonoBehaviour
{

    [Header("Enemy Footsteps")]

    private bool footstepsPlaying;
    private GameObject footstep;
    private Rigidbody2D rb;
    private Transform parent;
    private Object thisObject;



    public EnemyFootstepSFX(GameObject foot, Transform parentObject)
    {
        footstep = foot;
        parent = parentObject;
    }

    // Start is called before the first frame update
    void Start()
    {                                                                                                                                                                                                                                                                                                                                                                                                
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
            thisObject = Instantiate(footstep, parent);
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

    public Object getSFX()
    {
        return thisObject;
    }

}
