using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Controls all compoments relating to player health
//Using https://www.youtube.com/watch?v=BLfNP4Sc_iA as reference
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider slider;                   //Main slider to keep track of health

    public Slider delaySlider;              //A secondary slider to keep track of total damage taken (within a short time)
    public float delayDuration = 2f;
    private float delayTimer = 0f;          
    private float timer = 0f;

    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //Debug: Testing damage function and slider
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetHealth(maxHealth);
        }

        //Delay slider
        if(delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            float t = timer / delayDuration;
            t = Mathf.Sin((t * Mathf.PI) / 2);                                      //https://easings.net/#easeOutSine
            delaySlider.value = Mathf.Lerp(delaySlider.value, currentHealth, t);    //Lerp allows for smooth accelerated movements
        }
    }

    //Sets health to a certain value
    public void SetHealth(int health)
    {
        currentHealth = health;
        slider.value = health;
        healthText.text = currentHealth + "/" + maxHealth;

        //Delay is instant when healing
        if(health > delaySlider.value)
        {
            delaySlider.value = health;
        }
    }

    //Resets health back to max health
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = currentHealth + "/" + maxHealth;

        delaySlider.maxValue = health;
        delaySlider.value = health;
    }

    //Decrements health by a certain amount
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player ded (Health <= 0)");
        }

        delayTimer = delayDuration;
    }
}
