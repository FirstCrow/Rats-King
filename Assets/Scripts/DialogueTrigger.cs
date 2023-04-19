using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool Repeatable;
    public GameObject player;
    public GameObject DialogueObject;

    public bool showPortrait;
    public GameObject dialogueBox;
    public GameObject portrait;
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
            //Pause Game
            Time.timeScale = 0;

            DialogueObject.SetActive(true);
            portrait.SetActive(true);
            if (Repeatable == false) {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
            if (!showPortrait)
            {
                portrait.SetActive(false);
                dialogueBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-80, dialogueBox.GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }
}
