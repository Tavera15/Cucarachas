using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public bool isImmune = false;
    public Image screenBlink;                                       // Reference to image on screen to blink when damaged
    public Color blinkColor = new Color(255, 0, 0, 100f);           // Color to flash when player is hurt
    public float immunityDuration = 1.5f;
    public Image healthBar;
    public float lastHitImmunityTimer = 1.5f;
    
    private Color initialBlinkColor;                                // Save original "screen" color
    private float immunityTimer = 0f;                               // Timer of last time when player was damaged 


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        screenBlink.enabled = true;
        initialBlinkColor = screenBlink.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Screen will reset to original color if it changed
        screenBlink.color = Color.Lerp(screenBlink.color, initialBlinkColor, 5 * Time.deltaTime);

        // Decrease lastHitTime timer to disable immunity
        if (immunityTimer <= 0)
            isImmune = false;

        immunityTimer -= Time.deltaTime;

        // Adjust healthbar fill and color according to the amount of health
        healthBar.fillAmount = health / maxHealth;
        healthBar.color = (health <= 30 ? Color.red : Color.green);
    }

    // Will be called when an enemy laser hits the player
    public void ReceiveDamage(float amount)
    {
        if (isImmune) { return; }

        isImmune = true;                                    // Player will be immune to attacks for a duration
        health -= amount;                                   // Decrease health
        screenBlink.color = blinkColor;                     // Change the screen color to "blink"
        immunityTimer = lastHitImmunityTimer;               // Reset lastHitTime timer
    }

    public void IncreaseHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, 100);      // Health can only be between 0 and 100
        screenBlink.color = new Color(0, 255, 0, .4f);      // Screen will blink green when healed
    }

    /// <summary>
    /// Player will be immune to enemy attacks for a specified duration.
    /// </summary>
    /// <param name="duration">Specify the duration the player will be immuned for.</param>
    public void ActivateImmunity(float duration)
    {
        isImmune = true;
        immunityTimer = duration;
        screenBlink.color = new Color(0, 0, 255, .4f);
    }
}
