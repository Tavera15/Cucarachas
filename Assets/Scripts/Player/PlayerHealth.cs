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
    
    private Color initialBlinkColor;                                // Save original "screen" color
    float lastHitTime = 0f;                                         // Timer of last time when player was damaged 

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
        
        // Increase lastHitTime timer to disable immunity
        lastHitTime += Time.deltaTime;

        if (lastHitTime >= immunityDuration)
            isImmune = false;
    }

    // Will be called when an enemy laser hits the player
    public void ReceiveDamage(float amount)
    {
        if (isImmune) { return; }

        lastHitTime = 0f;                       // Reset lastHitTime timer
        screenBlink.color = blinkColor;         // Change the screen color to "blink"
        health -= amount;                       // Decrease health
        isImmune = true;                        // Player will be immune to attacks for a duration
    }
}
