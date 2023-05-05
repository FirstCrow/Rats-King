using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public bool Repeatable;
    public bool interactable;
    [SerializeField] bool nearPlayer = false;
    public GameObject player;
    public GameObject DialogueObject;

    public bool showPortrait;
    public GameObject dialogueBox;
    public GameObject portrait;

    [Header("Dialogue Variables")]
    public string message;
    public TextMeshProUGUI textBox;
    public Sprite portraitSprite;
    public Image portraitImage;
    public GameObject interactText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable && nearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        //Pause Game
        Time.timeScale = 0;

        DialogueObject.SetActive(true);
        portrait.SetActive(true);
        textBox.text = message;
        portraitImage.sprite = portraitSprite;
        if (Repeatable == false)
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
        if (!showPortrait)
        {
            portrait.SetActive(false);
            dialogueBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-80, dialogueBox.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player)
        {
            if (!interactable)
            {
                StartDialogue();
            }
            else
            {
                nearPlayer = true;
                interactText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == player)
        {
            nearPlayer = false;
            interactText.SetActive(false);
        }
    }
}
