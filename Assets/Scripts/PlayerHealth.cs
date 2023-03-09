using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Controls all compoments relating to player health
//Using https://www.youtube.com/watch?v=BLfNP4Sc_iA as reference
public class PlayerHealth : MonoBehaviour
{
    [Header ("Health")]                     //Tracks GUI regarding Player health
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider;             //Main slider to keep track of health

    public Slider delaySlider;              //A secondary slider to keep track of total damage taken (within a short time)
    public float delayDuration = 2f;
    private float delayTimer = 0f;          
    private float timer = 0f;

    public TextMeshProUGUI healthText;

    [Header("Special")]                     //Tracks GUI regarding Player Special Meter (Blood meter)
    public int maxBlood = 100;
    private int currentBlood;
    public Slider bloodSlider;


    void Start()
    {
        currentHealth = maxHealth;
        currentBlood = maxBlood;
        SetMaxHealth(maxHealth);

        bloodSlider.maxValue = maxBlood;
        SetBlood(maxBlood);
    }

    void Update()
    {
        //Debug: Testing damage function and slider
        if (Input.GetKeyDown(KeyCode.Alpha1))           //1 = Test damage
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))           //2 = Full heal
        {
            SetHealth(maxHealth);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))           //3 = Full recover blood
        {
            SetBlood(maxBlood);
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
        healthSlider.value = health;
        healthText.text = currentHealth + "/" + maxHealth;

        //Delay is instant when healing
        if(health > delaySlider.value)
        {
            delaySlider.value = health;
        }
    }

    //Resets health to a new max health
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        healthText.text = currentHealth + "/" + maxHealth;

        delaySlider.maxValue = health;
        delaySlider.value = health;
    }

    public void SetBlood(int blood)
    {
        currentBlood = blood;
        bloodSlider.value = blood;
    }

    //Decrements health by a certain amount
    public void TakeDamage(int damage)
    {
        currentHealth -= Mathf.Clamp(damage, 0, maxHealth);
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player ded (Health <= 0)");
        }

        delayTimer = delayDuration;
    }

    //Decrements blood by a certain amount
    public void UseBlood(int blood)
    {
        currentBlood -= Mathf.Clamp(blood, 0, maxBlood);
        SetBlood(currentBlood);

    }

    //Get current amount of blood{
    public int GetBlood()
    {
        return currentBlood;
    }
}
