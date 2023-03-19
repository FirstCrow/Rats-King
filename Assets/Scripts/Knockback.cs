using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{

    public int knockbackAmount;
    private Rigidbody2D rb;
    public float delay;
    public UnityEvent OnBegin, OnDone;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        delay = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeKnockback()
    {

    }
}
