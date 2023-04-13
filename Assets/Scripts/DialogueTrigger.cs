using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool Repeatable;
    public GameObject player;
    public GameObject DialogueObject;
    //public string message;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            DialogueObject.SetActive(true);
            if (Repeatable == false) {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }
}
