using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseEntity : MonoBehaviour
{
    public PlayerController player;   //moves towards player
    public float maxRange;  //max range the cheese will start moving from the player
    public float speed;     //Manipulates the speed the cheese will travel
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        //Debug.Log(Vector2.Distance(player.transform.position, transform.position));
        if (Vector2.Distance(player.transform.position, transform.position) <= maxRange) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime / Vector2.Distance(player.transform.position, transform.position));  
            //Long command but in theory will mean that the closer the player gets the faster it will move
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Cheese collider entered");
        if (other.gameObject.tag == "Player")
        {
            player.cheese++;
            player.cheeseText.text = "" + player.cheese;
            Destroy(gameObject);
        }

    }
}
