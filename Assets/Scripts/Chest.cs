using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int amount = 10;
    [SerializeField] bool open = false;
    [SerializeField] bool nearPlayer = false;
    SpriteRenderer mainSprite;
    public GameObject cheese;
    public Sprite openChestSprite;
    public AudioSource openChestAudio;
    public AudioSource backgroundMusic;
    GameObject player;

    void Start()
    {
        mainSprite = GetComponentInParent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(nearPlayer && !open && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        open = true;
        mainSprite.sprite = openChestSprite;
        
        for(int i = 0; i < amount; i++)
        {
            Instantiate(cheese, transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-4f, 0f)), Quaternion.identity);
        }

        player.GetComponent<PlayerController>().EnableChefHat();
        backgroundMusic.Stop();
        backgroundMusic.loop = false;
        openChestAudio.Play();
        StartCoroutine(chestAudio());
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nearPlayer = false;
        }
    }

    IEnumerator chestAudio()
    {
        yield return new WaitForSeconds(16f);
        openChestAudio.Stop();
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }
}
