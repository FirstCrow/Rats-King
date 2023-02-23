using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{

    public Transform Player;

    public float speed = 5f;
    public float MaxDis;
    public float MinDis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if(Vector2.Distance(transform.position, Player.position) >= MinDis)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if(Vector2.Distance(transform.position, Player.position) >= MaxDis)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}